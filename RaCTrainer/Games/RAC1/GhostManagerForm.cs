using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace racman
{
    // Front end for the ghost mod (mods/NPEA00385/ghost): its files on the PS3 over FTP, and which
    // ghost it plays through the mod's API block. State is read once per refresh, never polled:
    // sustained Ratchetron polling hangs the PS3.
    
    // To be so fr the forms are done exclusively by AI, I cannot be fucked with UI any day of the week, so thank Claude
    public partial class GhostManagerForm : Form
    {
        const uint ApiAddr = 0x717290;
        const uint ApiMagic = 0x4748533A;
        const uint CurrentPlanetAddr = 0x969C70;
        const uint FileMagic = 0x52474831;
        const int FileHeaderSize = 12;
        const int FrameSize = 108;
        const int PlayNameSize = 32;
        const string AttemptName = "ghost_tmp.rgh";
        const string PreviousName = "ghost_prev.rgh";
        const string CombosConfig = "ghostCombos";
        const string SpeedConfig = "ghostSpeed";
        const string NoSplitConfig = "ghostNoSplitPlanets";
        const string NoSplitLoadsConfig = "ghostNoSplitLoadPlanets";

        enum Cmd : byte { Save = 1, Restart = 2, RunArm = 4, RunStop = 5, SavePrevious = 6, NewAttempt = 7 }
        enum Mode : byte { Practice = 0, Race = 1, Off = 2, File = 3 }

        static readonly string[] FilePlanetNames = {
            "Veldin", "Novalis", "Aridia", "Kerwan", "Eudora", "Rilgar", "Blarg", "Umbris", "Batalia", "Gaspar",
            "Orxon", "Pokitaru", "Hoven", "Gemlik", "Oltanis", "Quartu", "Kalebo", "Fleet", "Veldin2"
        };
        static readonly Regex RunName = new Regex(@"^run_([0-9A-F]{8})_(\d{3})(?:_(\w+))?\.rgh$", RegexOptions.IgnoreCase);
        static readonly Regex PracticeName = new Regex(@"^ghost_(\d{2})\.rgh$", RegexOptions.IgnoreCase);

        class GhostFile
        {
            public string Name;
            public long Size;
            public int Planet = -1;
            public uint RunId;
            public int Segment = -1;
            public double Seconds => Math.Max(0, Size - FileHeaderSize) / FrameSize / 60.0;
        }

        class ApiState
        {
            public bool Running;
            public Mode Mode;
            public byte RunState;
            public uint RaceId, RunId, RunSeg, RaceSeg;
            public string PlayName;
        }

        readonly rac1 game;
        List<GhostFile> files = new List<GhostFile>();
        ApiState state = new ApiState();

        string RemoteDir => $"ftp://{AttachPS3Form.ip}:21/dev_hdd0/game/{AttachPS3Form.game}/USRDIR/";
        string LibraryDir => Path.Combine(Environment.CurrentDirectory, "ghosts", AttachPS3Form.game);

        public GhostManagerForm(rac1 game)
        {
            this.game = game;
            InitializeComponent();
            combosCheckBox.Checked = func.GetConfigData("config.txt", CombosConfig) != "off";
            combosCheckBox.CheckedChanged += combosCheckBox_CheckedChanged;
            speedCheckBox.Checked = func.GetConfigData("config.txt", SpeedConfig) == "on";
            speedCheckBox.CheckedChanged += speedCheckBox_CheckedChanged;
        }

        static uint ReadMask(string key)
        {
            uint.TryParse(func.GetConfigData("config.txt", key), System.Globalization.NumberStyles.HexNumber, null, out uint mask);
            return mask;
        }

        static void WriteMask(string key, uint mask)
        {
            func.ChangeFileLines("config.txt", mask.ToString("X8"), key);
        }

        private async void GhostManagerForm_Load(object sender, EventArgs e)
        {
            await RefreshAll();
        }

        string PlanetName(int planet)
        {
            if (planet < 0) return "?";
            return planet < game.planetsList.Length ? game.planetsList[planet] : $"Planet {planet}";
        }

        static string FormatTime(double seconds)
        {
            return TimeSpan.FromSeconds(seconds).ToString(seconds >= 3600 ? @"h\:mm\:ss\.ff" : @"m\:ss\.ff");
        }

        static string RunDate(uint runId)
        {
            return DateTimeOffset.FromUnixTimeSeconds(runId).LocalDateTime.ToString("d MMM HH:mm");
        }

        static uint BE32(byte[] b, int at)
        {
            return (uint)(b[at] << 24 | b[at + 1] << 16 | b[at + 2] << 8 | b[at + 3]);
        }

        static byte[] BE32(uint v)
        {
            return new[] { (byte)(v >> 24), (byte)(v >> 16), (byte)(v >> 8), (byte)v };
        }

        static GhostFile Parse(string name, long size)
        {
            var f = new GhostFile { Name = name, Size = size };
            Match m;
            if ((m = RunName.Match(name)).Success)
            {
                f.RunId = Convert.ToUInt32(m.Groups[1].Value, 16);
                f.Segment = int.Parse(m.Groups[2].Value);
                if (m.Groups[3].Success)
                {
                    string planet = m.Groups[3].Value;
                    f.Planet = Array.FindIndex(FilePlanetNames, n => n.Equals(planet, StringComparison.OrdinalIgnoreCase));
                    if (f.Planet < 0 && int.TryParse(planet, out int number)) f.Planet = number;
                }
            }
            else if ((m = PracticeName.Match(name)).Success)
            {
                f.Planet = int.Parse(m.Groups[1].Value);
            }
            return f;
        }

        #region FTP

        FtpWebRequest Ftp(string name, string method)
        {
            var request = (FtpWebRequest)WebRequest.Create(RemoteDir + name);
            request.Method = method;
            request.Timeout = 15000;
            request.UseBinary = true;
            request.KeepAlive = false;
            return request;
        }

        List<GhostFile> ListRemote()
        {
            var result = new List<GhostFile>();
            using (var response = Ftp("", WebRequestMethods.Ftp.ListDirectoryDetails).GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(28591)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 9 || parts[0].StartsWith("d")) continue;
                    if (!parts[8].EndsWith(".rgh", StringComparison.OrdinalIgnoreCase)) continue;
                    result.Add(Parse(parts[8], long.Parse(parts[4])));
                }
            }
            return result;
        }

        byte[] DownloadRemote(string name)
        {
            using (var client = new WebClient())
                return client.DownloadData(RemoteDir + name);
        }

        void UploadRemote(string name, string localPath)
        {
            using (var client = new WebClient())
                client.UploadFile(RemoteDir + name, WebRequestMethods.Ftp.UploadFile, localPath);
        }

        void DeleteRemote(string name)
        {
            Ftp(name, WebRequestMethods.Ftp.DeleteFile).GetResponse().Dispose();
        }

        #endregion

        #region Mod API

        void WriteApi(uint offset, byte[] bytes)
        {
            func.api.WriteMemory(AttachPS3Form.pid, ApiAddr + offset, bytes);
        }

        void SendCmd(Cmd cmd)
        {
            WriteApi(0x04, new[] { (byte)cmd });
        }

        void SetMode(Mode mode)
        {
            WriteApi(0x05, new[] { (byte)mode });
        }

        void PushSettings()
        {
            WriteApi(0x07, new[] { (byte)((combosCheckBox.Checked ? 0 : 1) | (speedCheckBox.Checked ? 2 : 0)) });
            WriteApi(0x40, BE32(ReadMask(NoSplitConfig)));
            WriteApi(0x44, BE32(ReadMask(NoSplitLoadsConfig)));
        }

        ApiState ReadApi()
        {
            byte[] b = func.api.ReadMemory(AttachPS3Form.pid, ApiAddr, 0x20 + PlayNameSize);
            var s = new ApiState { Running = BE32(b, 0) == ApiMagic };
            if (!s.Running) return s;
            s.Mode = (Mode)b[5];
            s.RunState = b[6];
            s.RaceId = BE32(b, 0x08);
            s.RunId = BE32(b, 0x0C);
            s.RunSeg = BE32(b, 0x10);
            s.RaceSeg = BE32(b, 0x14);
            int end = Array.IndexOf(b, (byte)0, 0x20, PlayNameSize);
            s.PlayName = Encoding.ASCII.GetString(b, 0x20, (end < 0 ? 0x20 + PlayNameSize : end) - 0x20);
            PushSettings();
            return s;
        }

        #endregion

        async Task RefreshAll()
        {
            await Run("Reading ghost files...", () =>
            {
                state = ReadApi();
                files = ListRemote();
            });
            ShowFiles();
            ShowState();
        }

        void RefreshState()
        {
            try
            {
                state = ReadApi();
            }
            catch (Exception ex)
            {
                SetStatus($"Couldn't read the mod's state: {ex.Message}");
            }
            ShowState();
        }

        async Task<bool> Run(string busyText, Action work)
        {
            SetBusy(true, busyText);
            try
            {
                await Task.Run(work);
                SetStatus("");
                return true;
            }
            catch (Exception ex)
            {
                SetStatus(ex.Message);
                return false;
            }
            finally
            {
                SetBusy(false, null);
            }
        }

        void SetBusy(bool busy, string text)
        {
            filesPanel.Enabled = ghostPanel.Enabled = gamePanel.Enabled = settingsPanel.Enabled = !busy;
            if (text != null) SetStatus(text);
            UseWaitCursor = busy;
        }

        void SetStatus(string text)
        {
            statusLabel.Text = text;
        }

        void ShowState()
        {
            ghostPanel.Enabled = gamePanel.Enabled = settingsPanel.Enabled = state.Running;
            if (!state.Running)
            {
                stateLabel.Text = "Ghost mod v0.5 isn't running. Apply it in the Mod Loader, then Refresh.";
                return;
            }

            string playing;
            switch (state.Mode)
            {
                case Mode.Practice: playing = "practice ghost of each planet"; break;
                case Mode.Race: playing = state.RaceId != 0 ? $"run {state.RaceId:X8}, next segment {state.RaceSeg}" : "your last run"; break;
                case Mode.File: playing = state.PlayName; break;
                default: playing = "nothing (ghost off)"; break;
            }
            string run;
            switch (state.RunState)
            {
                case 1: run = $"armed (run {state.RunId:X8}, starts on the next load)"; break;
                case 2: run = $"recording run {state.RunId:X8}, {state.RunSeg} segments so far"; break;
                default: run = "not recording a run"; break;
            }
            stateLabel.Text = $"Playing: {playing}\r\nRun: {run}";
        }

        void ShowFiles()
        {
            fileList.BeginUpdate();
            fileList.Items.Clear();
            fileList.Groups.Clear();

            var practice = fileList.Groups.Add("practice", "Practice ghosts");
            foreach (var f in files.Where(f => f.RunId == 0).OrderBy(f => f.Name == AttemptName || f.Name == PreviousName).ThenBy(f => f.Planet).ThenBy(f => f.Name))
                AddItem(f, practice, f.Name == AttemptName ? "Current attempt" : f.Name == PreviousName ? "Previous attempt" : PlanetName(f.Planet));

            foreach (var run in files.Where(f => f.RunId != 0).GroupBy(f => f.RunId).OrderByDescending(g => g.Key))
            {
                var segments = run.OrderBy(f => f.Segment).ToList();
                var group = fileList.Groups.Add(run.Key.ToString("X8"),
                    $"Run {run.Key:X8}  ({RunDate(run.Key)}, {segments.Count} segments, {FormatTime(segments.Sum(f => f.Seconds))})");
                foreach (var f in segments)
                    AddItem(f, group, $"{f.Segment + 1}. {PlanetName(f.Planet)}");
            }
            fileList.EndUpdate();
        }

        void AddItem(GhostFile f, ListViewGroup group, string label)
        {
            var item = new ListViewItem(new[] { label, FormatTime(f.Seconds), f.Name }, group) { Tag = f };
            if (state.Running && state.Mode == Mode.File && f.Name == state.PlayName) item.Font = new System.Drawing.Font(fileList.Font, System.Drawing.FontStyle.Bold);
            fileList.Items.Add(item);
        }

        List<GhostFile> SelectedFiles()
        {
            return fileList.SelectedItems.Cast<ListViewItem>().Select(i => (GhostFile)i.Tag).ToList();
        }

        GhostFile SingleSelected()
        {
            var selected = SelectedFiles();
            if (selected.Count == 1) return selected[0];
            MessageBox.Show("Select one ghost file first.", "Ghosts", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }

        bool InUse(GhostFile f)
        {
            return f.Name == AttemptName || f.Name == PreviousName || (state.Running && state.RunState == 2 && f.RunId == state.RunId);
        }

        private async void refreshButton_Click(object sender, EventArgs e)
        {
            await RefreshAll();
        }

        private async void downloadButton_Click(object sender, EventArgs e)
        {
            var selected = SelectedFiles();
            if (selected.Count == 0) selected = files;
            Directory.CreateDirectory(LibraryDir);
            if (await Run($"Downloading {selected.Count} files...", () =>
            {
                foreach (var f in selected)
                    File.WriteAllBytes(Path.Combine(LibraryDir, f.Name), DownloadRemote(f.Name));
            }))
            {
                SetStatus($"Downloaded {selected.Count} files to {LibraryDir}");
            }
        }

        private async void uploadButton_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(LibraryDir);
            using (var dialog = new OpenFileDialog { InitialDirectory = LibraryDir, Filter = "Ghost files (*.rgh)|*.rgh", Multiselect = true })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                var uploads = new List<string>();
                foreach (string path in dialog.FileNames)
                {
                    string name = Path.GetFileName(path);
                    string problem = CheckUpload(path, name);
                    if (problem != null)
                    {
                        MessageBox.Show($"{name}: {problem}", "Can't upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }
                    if (files.Any(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) &&
                        MessageBox.Show($"{name} is already on the PS3. Replace it?", "Upload", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        continue;
                    uploads.Add(path);
                }
                if (uploads.Count == 0) return;

                await Run($"Uploading {uploads.Count} files...", () =>
                {
                    foreach (string path in uploads)
                        UploadRemote(Path.GetFileName(path), path);
                });
                await RefreshAll();
            }
        }

        string CheckUpload(string path, string name)
        {
            if (name.Equals(AttemptName, StringComparison.OrdinalIgnoreCase)) return "the mod records into this file; rename it first.";
            if (Encoding.ASCII.GetByteCount(name) >= PlayNameSize || name.Any(c => c > 127 || c == ' '))
                return $"names must be under {PlayNameSize} characters, ASCII, no spaces.";
            byte[] header = new byte[FileHeaderSize];
            using (var stream = File.OpenRead(path))
                if (stream.Read(header, 0, header.Length) != header.Length) return "not a ghost file.";
            if (BE32(header, 0) != FileMagic || BE32(header, 8) != FrameSize) return "not a ghost file, or from another version of the mod.";
            return null;
        }

        private async void deleteButton_Click(object sender, EventArgs e)
        {
            var selected = SelectedFiles();
            if (selected.Count == 0) return;
            var inUse = selected.Where(InUse).ToList();
            var deletable = selected.Except(inUse).ToList();
            string skipped = inUse.Count == 0 ? ""
                : $"\n\nSkipping {string.Join(", ", inUse.Select(f => f.Name))}: the mod is recording into {(inUse.Count == 1 ? "it" : "them")}.";
            if (deletable.Count == 0)
            {
                MessageBox.Show($"Nothing to delete.{skipped}", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show($"Delete {deletable.Count} files from the PS3? Download them first if you want to keep them.{skipped}", "Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            await Run("Deleting...", () =>
            {
                foreach (var f in deletable)
                    DeleteRemote(f.Name);
            });
            await RefreshAll();
        }

        private void libraryButton_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(LibraryDir);
            Process.Start("explorer.exe", LibraryDir);
        }

        private async void playFileButton_Click(object sender, EventArgs e)
        {
            var f = SingleSelected();
            if (f == null) return;
            if (InUse(f))
            {
                MessageBox.Show("That file is still being recorded. Save the attempt (L3+R3) and play the saved ghost instead.", "Play", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Encoding.ASCII.GetByteCount(f.Name) >= PlayNameSize)
            {
                MessageBox.Show($"The mod can only play files with names under {PlayNameSize} characters.", "Play", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int planet = f.Planet;
            uint currentPlanet = 0;
            bool restart = restartCheckBox.Checked;
            if (!await Run("Setting the ghost...", () =>
            {
                if (planet < 0) planet = (int)BE32(DownloadRemote(f.Name), 4);
                var name = new byte[PlayNameSize];
                Encoding.ASCII.GetBytes(f.Name).CopyTo(name, 0);
                WriteApi(0x20, name);
                SetMode(Mode.File);
                currentPlanet = func.api.ReadMemory(AttachPS3Form.pid, CurrentPlanetAddr);
                if (restart && planet == currentPlanet) SendCmd(Cmd.Restart);
            }))
                return;

            RefreshState();
            ShowFiles();
            SetStatus(planet == currentPlanet
                ? (restart ? "Restarting the level against this ghost (once unpaused)." : "The ghost plays from the next load of this level.")
                : $"This ghost plays when you reach {PlanetName(planet)}.");
        }

        private async void raceRunButton_Click(object sender, EventArgs e)
        {
            var f = SingleSelected();
            if (f == null) return;
            if (f.RunId == 0)
            {
                MessageBox.Show("Select a segment of a run.", "Race", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!await Run("Setting the ghost...", () =>
            {
                WriteApi(0x08, BE32(f.RunId));
                WriteApi(0x14, BE32(0));
                SetMode(Mode.Race);
            }))
                return;

            RefreshState();
            SetStatus($"Racing run {f.RunId:X8}: each load plays its next segment on that planet. Arm a run (R1+L3+R3) to record yours.");
        }

        private async void SetModeAndRefresh(Mode mode, string done)
        {
            if (await Run("Setting the ghost...", () => SetMode(mode)))
            {
                RefreshState();
                ShowFiles();
                SetStatus(done);
            }
        }

        private void practiceModeButton_Click(object sender, EventArgs e)
        {
            SetModeAndRefresh(Mode.Practice, "Each planet plays its saved practice ghost from the next load.");
        }

        private void ghostOffButton_Click(object sender, EventArgs e)
        {
            SetModeAndRefresh(Mode.Off, "No ghost from the next load. Recording carries on.");
        }

        private async void SendCommand(Cmd cmd, string done)
        {
            if (await Run("Sending...", () => SendCmd(cmd)))
                SetStatus(done);
        }

        private void armRunButton_Click(object sender, EventArgs e)
        {
            SendCommand(Cmd.RunArm, "Arming a run: it starts on the next load (once unpaused).");
        }

        private void stopRunButton_Click(object sender, EventArgs e)
        {
            SendCommand(Cmd.RunStop, "Stopping the run (once unpaused).");
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            SendCommand(Cmd.Restart, "Restarting the level (once unpaused).");
        }

        private void savePracticeButton_Click(object sender, EventArgs e)
        {
            SendCommand(Cmd.Save, "Saving this attempt as the planet's practice ghost and restarting (once unpaused).");
        }

        private void savePreviousButton_Click(object sender, EventArgs e)
        {
            SendCommand(Cmd.SavePrevious, "Saving the attempt that ended in your last death or reload as that planet's practice ghost.");
        }

        private void newAttemptButton_Click(object sender, EventArgs e)
        {
            SendCommand(Cmd.NewAttempt, "Next death or reload starts a new attempt");
        }

        private void combosCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            func.ChangeFileLines("config.txt", combosCheckBox.Checked ? "on" : "off", CombosConfig);
            RefreshState();
        }

        private void speedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            func.ChangeFileLines("config.txt", speedCheckBox.Checked ? "on" : "off", SpeedConfig);
            RefreshState();
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Every planet plays its practice ghost while the Ghost patch is loaded.\n\n" +
                "L3 + R3: save attempt as practice ghost, restart\n" +
                "L3 + R3 within 3s of a death/reload: save the lost attempt\n" +
                "L1 + L3 + R3: restart without saving\n" +
                "R1 + L3 + R3: start run (on next load) / stop run\n\n" +
                "Refresh to see new files. Download/Upload to share ghosts.",
                "Ghost help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void noSplitButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new Form { Text = "Keep recording through deaths and reloads", FormBorderStyle = FormBorderStyle.FixedDialog, MinimizeBox = false, MaximizeBox = false, StartPosition = FormStartPosition.CenterParent, ClientSize = new System.Drawing.Size(440, 460) })
            {
                var info = new Label { Dock = DockStyle.Top, Height = 30, Padding = new Padding(6), Text = "Ticked planets keep one ghost instead of splitting." };
                var deaths = new CheckedListBox { Dock = DockStyle.Fill, CheckOnClick = true, IntegralHeight = false };
                var loads = new CheckedListBox { Dock = DockStyle.Fill, CheckOnClick = true, IntegralHeight = false };
                var grid = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2 };
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                grid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                grid.Controls.Add(new Label { Text = "Deaths", AutoSize = true, Padding = new Padding(0, 4, 0, 2) }, 0, 0);
                grid.Controls.Add(new Label { Text = "Same-planet reloads", AutoSize = true, Padding = new Padding(0, 4, 0, 2) }, 1, 0);
                grid.Controls.Add(deaths, 0, 1);
                grid.Controls.Add(loads, 1, 1);
                var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 36, FlowDirection = FlowDirection.RightToLeft };
                var ok = new Button { Text = "OK", DialogResult = DialogResult.OK };
                var all = new Button { Text = "All", AutoSize = true };
                var none = new Button { Text = "None", AutoSize = true };
                all.Click += (o, a) => { foreach (var list in new[] { deaths, loads }) for (int i = 0; i < list.Items.Count; i++) list.SetItemChecked(i, true); };
                none.Click += (o, a) => { foreach (var list in new[] { deaths, loads }) for (int i = 0; i < list.Items.Count; i++) list.SetItemChecked(i, false); };
                buttons.Controls.AddRange(new Control[] { ok, none, all });
                uint deathMask = ReadMask(NoSplitConfig), loadMask = ReadMask(NoSplitLoadsConfig);
                for (int i = 0; i < FilePlanetNames.Length; i++)
                {
                    deaths.Items.Add(PlanetName(i), (deathMask >> i & 1) != 0);
                    loads.Items.Add(PlanetName(i), (loadMask >> i & 1) != 0);
                }
                dialog.Controls.Add(grid);
                dialog.Controls.Add(info);
                dialog.Controls.Add(buttons);
                dialog.AcceptButton = ok;
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                deathMask = loadMask = 0;
                foreach (int i in deaths.CheckedIndices) deathMask |= 1u << i;
                foreach (int i in loads.CheckedIndices) loadMask |= 1u << i;
                WriteMask(NoSplitConfig, deathMask);
                WriteMask(NoSplitLoadsConfig, loadMask);
            }
            RefreshState();
        }
    }
}
