using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using System.Net.Http;
using System.Threading.Tasks;

namespace racman
{
    public partial class RAC4Form : Form
    {
        public rac4 game;
        private static ModLoaderForm modLoaderForm;
        private AutosplitterHelper autosplitterHelper;

        private int savefileHelperSubID = -1;
        private int tutorialSubId = -1;
        // Tutorial flag - are we loading a fresh file?
        public byte prevTutorial;

        public RAC4Form(rac4 game)
        {
            this.game = game;

            game.SetupInputDisplayMemorySubs();

            InitializeComponent();
            bolts_textBox.KeyDown += bolts_TextBox_KeyDown;
            AutosplitterCheckbox.Checked = true;
        }

        public Form InputDisplay;
        public static string ip = AttachPS3Form.ip;
        public static int pid = AttachPS3Form.pid;
        public static uint SaveInfo = 0x11B1BD8;
        public static uint FrameCounter = 0xB3C59C;

        private string Challenge;
        private string src;


        private void RAC4Form_Load(object sender, EventArgs e)
        {
            checkBoxSoftlocks.Checked = true;

            savefileHelperSubID = game.api.SubMemory(game.api.getCurrentPID(), rac4.addr.savefile_api_enabled, 1, value =>
            {
                if (value[0] == 1)
                {
                    this.Invoke(new Action(() =>
                    {
                        // Savefile helper mod is enabled.
                        loadFileButton.Enabled = true;
                        setAsideFileButton.Enabled = true;
                        game.api.ReleaseSubID(savefileHelperSubID);
                    }));
                }
            });
        }

        private async void wrsFromSrcSiteCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (src == null)
            {
                List<string> x = new List<string>();

                try
                {
                    for (int i = 0; i <= 80; i += 20)
                    {
                        string result = await func.client.DownloadStringTaskAsync($"https://www.speedrun.com/api/v1/games/k6qwo06g/records?top=1&offset={i}");
                        x.Add(result);
                    }
                    src = string.Join(",", x);
                }
                catch
                {
                    // fuck handling error exceptions
                }
            }
        }


        private string GetWorldRecord(string challenge)
        {
            try
            {
                string a = src.Substring(src.IndexOf($"{Challenge.Replace(' ', '_')}#Solo"));
                string b = a.Substring(a.IndexOf("realtime_t") + 12, 3).Trim(new char[] { ',', '"' }); // ye
                TimeSpan t = TimeSpan.FromSeconds(int.Parse(b));
                return string.Format("{0:D1}:{1:D2}", t.Minutes, t.Seconds);
            }
            catch { 
                return "N/A"; 
            }


        }

        private Timer timer = new Timer();

        public void Init_timer()
        {
            timer.Interval = (int)16.66667;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;
        }
        private void writetext_CheckedChanged(object sender, EventArgs e)
        {

            if (writetext.Checked)
            {
                Init_timer();
            }
            if (!writetext.Checked)
                timer.Enabled = false;
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            int frames = Convert.ToInt32(func.ReadMemory(ip, pid, FrameCounter, 4), 16);
            TimeSpan t = TimeSpan.FromMilliseconds(frames * 1000 / 60);

            writetext.Text = string.Format("{0:D2}:{1:D2}.{2:D3}", t.Minutes, t.Seconds, t.Milliseconds);

            if (frames < 5)
            {
                levelinfo.Text = Encoding.ASCII.GetString(func.FromHex(func.ReadMemory(ip, pid, SaveInfo, 64))).Split(new string[] { "Time" }, StringSplitOptions.None).First(); // lmfao
                Challenge = levelinfo.Text.Split(new string[] { "-" }, StringSplitOptions.None).First().TrimEnd();

                if (wrsFromSrcSiteCheck.Checked)
                    wrtext.Text = $"WR: {GetWorldRecord(Challenge)}";
                else wrtext.Text = "WR: N/A";

            }
        }

        private void RAC4Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            writetext.Checked = false;
            try 
            {
                if (tutorialSubId != -1)
                    game.api.ReleaseSubID(tutorialSubId);
                if (savefileHelperSubID != -1)
                    game.api.ReleaseSubID(savefileHelperSubID);

                game.api.Disconnect();
                Application.Exit();
            } 
            catch
            {
                // lol
            }
        }

        private void ghostcheck_CheckedChanged(object sender, EventArgs e)
        {
            game.SetGhostRatchet(ghostcheck.Checked);
        }

        private void inputdisplaybutton_Click(object sender, EventArgs e)
        {
            if (InputDisplay == null)
            {
                InputDisplay = new InputDisplay();
                InputDisplay.FormClosed += InputDisplay_FormClosed;
                InputDisplay.Show();
            }
        }

        private void InputDisplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            InputDisplay = null;
        }

        private void patchLoaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Application.OpenForms["ModLoaderForm"] as ModLoaderForm) != null)
            {
                modLoaderForm.Activate();
            }
            else
            {
                modLoaderForm = new ModLoaderForm();
                modLoaderForm.Show();
            }
        }

        private void memoryUtilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryForm memoryForm = new MemoryForm();
            memoryForm.Show();
        }

        private void AutosplitterCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!AutosplitterCheckbox.Checked)
            {
                // Disable autosplitter.
                autosplitterHelper.Stop();
                autosplitterHelper = null;
            }
            else
            {
                // Enable auotpslitter
                Console.WriteLine("Autosplitter starting!");
                autosplitterHelper = new AutosplitterHelper();
                autosplitterHelper.StartAutosplitterForGame(this.game);
            }
        }

        private void checkBoxSoftlocks_CheckedChanged(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            if (checkBoxSoftlocks.Checked)
            {
                api.SubMemory(pid, rac4.addr.tutorialFlags + 3, 1, (value) =>
                {
                    var tutorial = value[0];
                    if (tutorial == prevTutorial) return;
                    prevTutorial = tutorial;

                    if (tutorial == 0)
                        api.WriteMemory(pid, rac4.addr.qualifierSoftlock, new byte[] { 0 });
                });
            }
            else if (tutorialSubId != -1)
            {
                api.ReleaseSubID(tutorialSubId);
                tutorialSubId = -1;
            }
        }

        private void bolts_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    game.SetBoltCount(uint.Parse(bolts_textBox.Text));
                }
                catch
                {
                    MessageBox.Show("Please enter a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void killyourself_Click(object sender, EventArgs e)
        {
            KillYourself();
        }

        private void KillYourself()
        {
            game.KillYourself();
        }

        private void botsUnlocksWindowButton_Click(object sender, EventArgs e)
        {
            RAC4BotsUnlocks unlocks = new RAC4BotsUnlocks(game);
            unlocks.Show();
        }

        private void buttonActTune_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            api.WriteMemory(pid, rac4.addr.shellshockTuning, new byte[] { 20 });
            api.WriteMemory(pid, rac4.addr.reactorTuning, new byte[] { 20 });
            api.WriteMemory(pid, rac4.addr.evisceratorTuning, new byte[] { 20 });
            api.WriteMemory(pid, rac4.addr.aceTuning, new byte[] { 20 });
            api.Notify("Act tuning done!");
        }

        private void setAsideFileButton_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();
            api.WriteMemory(pid, rac4.addr.savefile_api_setaside, new byte[] { 1 });
        }

        private void loadFileButton_Click(object sender, EventArgs e)
        {
            game.loadSetAsideFile();
        }

        public Form ConfigureCombos;
        private void configureButtonCombosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfigureCombos == null)
            {
                ConfigureCombos = new ConfigureCombos();
                ConfigureCombos.FormClosed += ConfigureCombos_FormClosed;
                ConfigureCombos.Show();
                game.InputsTimer.Enabled = false;
            }
        }

        private void ConfigureCombos_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConfigureCombos = null;
            if (CComboCheckBox.Checked)
                game.InputsTimer.Enabled = true;
        }

        private void CComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CComboCheckBox.Checked)
                game.InputsTimer.Enabled = true;
            else
                game.InputsTimer.Enabled = false;
        }
    }
}