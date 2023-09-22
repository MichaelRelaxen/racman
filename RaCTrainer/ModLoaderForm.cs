using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLua;

namespace racman
{
    public partial class ModLoaderForm : Form
    {
        public static Mod[] mods;
        string gameModFolder;

        bool reloading = false;

        public ModLoaderForm()
        {
            InitializeComponent();

            gameModFolder = $"{Directory.GetCurrentDirectory()}\\mods\\{AttachPS3Form.game}\\";

            if (mods == null)
            {
                mods = this.LoadMods().ToArray();
            }

            foreach (var mod in mods)
            {
                this.modsCheckedListBox.Items.Add(mod.name, mod.loaded);
            }
        }

        public List<Mod> LoadMods()
        {
            if (!Directory.Exists(gameModFolder))
            {
                return new List<Mod>();
            }

            var modFolders = Directory.EnumerateDirectories(gameModFolder);

            List<Mod> mods = new List<Mod>();

            foreach (var modFolder in modFolders)
            {
                Mod mod = GetMod(modFolder);

                if (mod != null && (!mod.variables.ContainsKey("visible") || mod.variables["visible"] != "false"))
                {
                    mods.Add(mod);
                }
            }

            return mods;
        }

        private void ReloadMods()
        {
            reloading = true;

            List<Mod> allMods = new List<Mod>(ModLoaderForm.mods);
            List<string> modsFolders = new List<string>();

            foreach(Mod mod in mods)
            {
                modsFolders.Add(mod.modFolder);

                // Don't reload mods that are currently loaded
                if (!mod.loaded)
                {
                    var index = allMods.FindIndex(x => x.modFolder == mod.modFolder);

                    // Remove mod from list if it's removed from file system
                    if (!Directory.Exists(mod.modFolder))
                    {
                        allMods.RemoveAt(index);
                        continue;
                    }

                    allMods.RemoveAt(index);
                    allMods.Insert(index, GetMod(mod.modFolder));
                }
            }

            IEnumerable<string> modFolders = null;
            try
            {
                modFolders = Directory.EnumerateDirectories(gameModFolder);
            } catch (DirectoryNotFoundException)
            {
                return;
            }

            // Load new mods
            foreach (var modFolder in modFolders)
            {
                // Ignore already loaded mods
                if (modsFolders.Contains(modFolder))
                {
                    continue;
                }

                Mod mod = GetMod(modFolder);

                if (mod != null && (!mod.variables.ContainsKey("visible") || mod.variables["visible"] != "false"))
                {
                    allMods.Add(mod);
                }
            }

            mods = allMods.ToArray();

            this.modsCheckedListBox.Items.Clear();
            foreach (var mod in mods)
            {
                this.modsCheckedListBox.Items.Add(mod.name, mod.loaded);
            }

            reloading = false;
        }

        private Mod GetMod(string modFolder)
        {
            if (!File.Exists($"{modFolder}\\patch.txt"))
            {
                return null;
            }

            var mod = new Mod();
            mod.modFolder = modFolder;

            var patchFileStream = File.OpenRead($"{modFolder}\\patch.txt");

            using (StreamReader reader = new StreamReader(patchFileStream))
            {
                mod.patchLines = reader.ReadToEnd().Split('\n').ToList();

                foreach (var patchLine in mod.patchLines)
                {
                    if (patchLine.Length > 2 && patchLine.Substring(0, 2) == "#-")
                    {
                        var patchLineComponents = patchLine.Split(new char[] { ':' }, 2);
                        if (patchLineComponents.Length < 2)
                        {
                            continue;
                        }

                        var key = patchLineComponents[0].Substring(2).Trim();
                        var value = patchLineComponents[1].Trim();

                        mod.variables[key] = value;
                    }
                }
            }

            if (mod.variables.ContainsKey("name"))
            {
                mod.name = mod.variables["name"];
            }
            else
            {
                mod.name = new DirectoryInfo(modFolder).Name;
            }

            if (mod.variables.ContainsKey("version"))
            {
                mod.version = mod.variables["version"];
            }

            return mod;
        }

        private void modsCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (reloading)
            {
                return;
            }

            if (e.NewValue == CheckState.Checked)
            {
                if (!ModLoaderForm.mods[e.Index].Load())
                {
                    e.NewValue = CheckState.Unchecked;
                }
            }
            else
            {
                ModLoaderForm.mods[e.Index].Unload();
            }
        }

        private void ModLoaderForm_Load(object sender, EventArgs e)
        {

        }

        private void modsCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modsCheckedListBox.SelectedIndex < 0)
            {
                return;
            }

            Mod mod = ModLoaderForm.mods[modsCheckedListBox.SelectedIndex];

            modNameLabel.Text = mod.name;
            authorNameLabel.Text = "N/A";
            versionLabel.Text = "N/A";
            linkLabel.Text = "";
            descriptionTextBox.Text = "";
            
            if (mod.variables.ContainsKey("author"))
            {
                authorNameLabel.Text = mod.variables["author"];
            }

            if (mod.variables.ContainsKey("version"))
            {
                versionLabel.Text = mod.variables["version"];
            }

            if (mod.variables.ContainsKey("href") && (mod.variables["href"].StartsWith("https://") || mod.variables["href"].StartsWith("http://")))
            {
                linkLabel.Text = mod.variables["href"];
                linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler((s, ev) => System.Diagnostics.Process.Start(mod.variables["href"]));
            }

            if (mod.variables.ContainsKey("description"))
            {
                descriptionTextBox.Text = mod.variables["description"];
            }
        }

        private void addZipButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "ZIP file (*.zip)|*.zip";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ZipFile.ExtractToDirectory(openFileDialog.FileName, $"{Path.GetTempPath()}\\racman\\{AttachPS3Form.game}\\");
                        var directories = Directory.GetDirectories($"{Path.GetTempPath()}\\racman\\{AttachPS3Form.game}\\");

                        // Find first directory with a path.txt file
                        string directoryName = "";
                        Mod mod = null;
                        foreach (var d in directories)
                        {
                            mod = GetMod(d);
                            if (mod != null)
                            {
                                directoryName = Path.GetFileName(d);

                                break;
                            }
                        }

                        if (directoryName == "")
                        {
                            MessageBox.Show("Invalid or corrupt mod. Redownload the ZIP or ask the mod developer for help.");
                            return;
                        }

                        // If there is already an installed mod, just replace it if version is newer
                        Mod installedMod = GetMod($"{Directory.GetCurrentDirectory()}\\mods\\{AttachPS3Form.game}\\{directoryName}");

                        bool upgrade = false;
                        if (installedMod != null)
                        {
                            Version installedVersion = new Version(installedMod.version);
                            Version zipVersion = new Version(mod.version);

                            if (installedVersion != null && zipVersion != null)
                            {
                                var result = installedVersion.CompareTo(zipVersion);

                                DialogResult userChoice = DialogResult.No;
                                if (result > 0)
                                {
                                    userChoice = MessageBox.Show("Current installed version is newer than your ZIP. Downgrade?", "Downgrade", MessageBoxButtons.YesNo);
                                }
                                else if (result < 0)
                                {
                                    upgrade = true;
                                }
                                else
                                {
                                    userChoice = MessageBox.Show("Mod already installed. Replace?", "Replace", MessageBoxButtons.YesNo);
                                }

                                if (!upgrade && userChoice == DialogResult.No)
                                {
                                    return;
                                }
                            }
                        }

                        // Merge folders
                        DirectoryInfo source = new DirectoryInfo($"{Path.GetTempPath()}\\racman\\{AttachPS3Form.game}\\{directoryName}");
                        DirectoryInfo target = new DirectoryInfo($"{Directory.GetCurrentDirectory()}\\mods\\{AttachPS3Form.game}\\{directoryName}");

                        CopyAll(source, target);

                        if (upgrade)
                        {
                            MessageBox.Show($"{mod.name} upgraded to version {mod.version}!");
                        } else
                        {
                            MessageBox.Show($"{mod.name} version {mod.version} installed.");
                        }


                        //ZipFile.ExtractToDirectory(openFileDialog.FileName, $"{Directory.GetCurrentDirectory()}\\mods\\{AttachPS3Form.game}\\");
                    } catch (IOException exception)
                    {
                        // There's apparently no easy way to tell ZipFile.ExtractToDirectory to overwrite files smh
                        MessageBox.Show("Failed to extract ZIP. ");
                    } finally
                    {
                        Directory.Delete($"{System.IO.Path.GetTempPath()}\\racman\\", true);
                    }

                    this.ReloadMods();
                }
            }
        }

        // Thanks Stack Overflow: https://stackoverflow.com/questions/9053564/c-sharp-merge-one-directory-with-another
        private void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            if (source.FullName.ToLower() == target.FullName.ToLower())
            {
                return;
            }

            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        private void ModLoaderForm_Activated(object sender, EventArgs e)
        {
            this.ReloadMods();
        }

        private void openConsoleButton_Click(object sender, EventArgs e)
        {
            if (!AttachPS3Form.console.IsDisposed)
            {
                AttachPS3Form.console.Show();
            } else
            {
                RacManConsole console = new RacManConsole();
                console.Show();
            }
        }

        private void buttonScripting_Click(object sender, EventArgs e)
        {
            if (!AttachPS3Form.scripting.IsDisposed)
            {
                AttachPS3Form.scripting.Show();
            }
            else
            {
                RacmanScripting scripting = new RacmanScripting();
                scripting.Show();
            }
        }
    }

    public class Mod
    {
        public string name;
        public string version;

        public bool loaded = false;

        public string modFolder = "";

        public Dictionary<string, string> variables = new Dictionary<string, string>();
        public List<string> patchLines = new List<string>();
        public Dictionary<uint, byte[]> originalData = new Dictionary<uint, byte[]>();

        List<LuaAutomation> luaAutomations = new List<LuaAutomation>();

        public Mod()
        {

        }

        public Mod(string modFolder)
        {
            this.modFolder = modFolder;
        }

        private void LoadOriginalData()
        {
            foreach (string patch in patchLines)
            {
                if (patch.Length < 2 || patch[0] == '#')
                {
                    continue;
                }

                var patchComponents = patch.Split(':');
                var addressString = patchComponents[0].Trim();
                
                if(!addressString.StartsWith("0x"))
                {
                    continue;  // Probably Lua automation or something
                }

                uint address = UInt32.Parse(addressString.Substring(addressString.IndexOf("0x") + 2), System.Globalization.NumberStyles.HexNumber);
                var value = patchComponents[1].Trim();

                byte[] patchBytes;

                if (value.Contains("0x"))
                {
                    patchBytes = BitConverter.GetBytes(UInt32.Parse(value.Substring(value.IndexOf("0x") + 2), System.Globalization.NumberStyles.HexNumber)).Reverse().ToArray();
                }
                else
                {
                    patchBytes = File.ReadAllBytes($"{modFolder}\\{value}");
                }

                int bytesRead = 0;
                byte[] bytesToWrite = new byte[] { };
                List<byte> ogData = new List<byte>();
                while (bytesRead < patchBytes.Length)
                {
                    bytesToWrite = patchBytes.Skip(bytesRead).Take(1024).ToArray();

                    IPS3API api = func.api;
                    ogData.AddRange(api.ReadMemory(AttachPS3Form.pid, address + (uint)bytesRead, (uint)bytesToWrite.Length));

                    bytesRead += bytesToWrite.Length;
                }

                originalData[address] = ogData.ToArray();
            }
        }

        public bool Load()
        {
            Console.WriteLine($"Loading mod: {this.name}");

            if (this.originalData.Keys.Count <= 0)
            {
                this.LoadOriginalData();
            }

            IPS3API api = func.api;
            WebMAN wmm = new WebMAN(func.api.GetIP());

            wmm.PauseRSX();

            bool dirty = false;

            var patchFileStream = File.OpenRead($"{modFolder}\\patch.txt");

            using (StreamReader reader = new StreamReader(patchFileStream))
            {
                this.patchLines = reader.ReadToEnd().Split('\n').ToList();

                foreach (string patch in patchLines)
                {
                    if (patch.Length < 2 || patch[0] == '#')
                    {
                        continue;
                    }

                    var patchComponents = patch.Split(':');
                    var addressString = patchComponents[0].Trim();
                    var value = patchComponents[1].Trim();

                    if (addressString == "automation")
                    {
                        // Lua "automation" file

                        if (!this.LoadLuaAutomation($"{modFolder}\\{value}"))
                        {
                            // We need to unload, but we need to do it later because we don't know what patches might have been applied that need to be reverted.
                            dirty = true;
                        }

                        continue;
                    }

                    uint address = UInt32.Parse(addressString.Substring(addressString.IndexOf("0x") + 2), System.Globalization.NumberStyles.HexNumber);

                    byte[] patchBytes;

                    if (value.Contains("0x"))
                    {
                        patchBytes = BitConverter.GetBytes(UInt32.Parse(value.Substring(value.IndexOf("0x") + 2), System.Globalization.NumberStyles.HexNumber)).Reverse().ToArray();
                    }
                    else
                    {
                        patchBytes = File.ReadAllBytes($"{modFolder}\\{value}");
                    }

                    int bytesWritten = 0;
                    byte[] bytesToWrite = new byte[] { };
                    while (bytesWritten < patchBytes.Length)
                    {
                        bytesToWrite = patchBytes.Skip(bytesWritten).Take(1024).ToArray();

                        api.WriteMemory(AttachPS3Form.pid, address + (uint)bytesWritten, (uint)bytesToWrite.Length, bytesToWrite);

                        bytesWritten += bytesToWrite.Length;
                    }
                }
            }

            if (dirty)
            {
                // We failed something at some point, revert patches.
                this.Unload();
                wmm.ContinueRSX();
                return false;
            }

            this.loaded = true;
            wmm.ContinueRSX();
            return true;
        }

        private bool LoadLuaAutomation(string filename)
        {
            LuaAutomation automation = new LuaAutomation(filename, AttachPS3Form.game, this);
            this.luaAutomations.Add(automation);

            return !automation.failed;
        }

        public void Unload()
        {
            WebMAN wmm = new WebMAN(func.api.GetIP());
            wmm.PauseRSX();
            foreach (KeyValuePair<uint, byte[]> entry in this.originalData)
            {
                int bytesWritten = 0;
                byte[] bytesToWrite = new byte[] { };
                while (bytesWritten < entry.Value.Length)
                {
                    bytesToWrite = entry.Value.Skip(bytesWritten).Take(1024).ToArray();

                    IPS3API api = func.api;
                    api.WriteMemory(AttachPS3Form.pid, entry.Key + (uint)bytesWritten, (uint)bytesToWrite.Length, bytesToWrite);

                    bytesWritten += bytesToWrite.Length;
                }
            }
            wmm.ContinueRSX();

            // Stop and clear out Lua automations
            foreach(LuaAutomation automation in luaAutomations)
            {
                automation.Unload();
            }

            this.luaAutomations.Clear();

            this.loaded = false;
        }
    }

}
