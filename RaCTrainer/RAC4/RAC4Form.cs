using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using System.Net.Http;
using System.Threading;

namespace racman
{
    public partial class RAC4Form : Form
    {
    private enum Planets
        {
            DreadZone,            
            Catacrom,
            INFLOOP,
            Sarathos,
            Kronos,
            Shaar,
            Valix,
            Orxon,
            INFLOOP2,
            Torval,
            Stygia,
            INFLOOP3,
            Maraxus,
            GhostStation,
            Interior
        };
        private enum Skins
        {
            Marauder,
            Avenger,
            Crusader,
            Vindicator,           
            Liberator,
            AlphaClank,
            Squidzor,
            LandShark,          
            TheMuscle,
            W3RM,
            Starshield,
            KingClaude,
            Vernon,                 
            KidNova,
            Venus,
            Jak,            
            Ninja,
            SaurusRatchet,
            GenomeRatchet,
            SantaRatchet,
            PipoSaruRatchet,
            Clankchet,
        };
        public rac4 game;
        private static ModLoaderForm modLoaderForm;
        private AutosplitterHelper autosplitterHelper;

        private int savefileHelperSubID = -1;
        private int tutorialSubId = -1;
        // Tutorial flag - are we loading a fresh file?
        public byte prevTutorial;

        WebMAN wmm = null;

        public RAC4Form(rac4 game)
        {
            this.game = game;

            game.SetupInputDisplayMemorySubs();

            InitializeComponent();
            planets_comboBox.Text = "DreadZone";
            skins_comboBox.Text = "Marauder";
            bolts_textBox.KeyDown += bolts_TextBox_KeyDown;
            dreadPoints_textBox.KeyDown += dreadPoints_TextBox_KeyDown;
            CM_textBox.KeyDown += CM_TextBox_KeyDown;
            AutosplitterCheckbox.Checked = true;

            if (func.api is Ratchetron r)
                wmm = new WebMAN(r.GetIP());
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

        private void planets_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int planetId;
            String planetText;
            planetText = (String)planets_comboBox.SelectedItem;
            if (planetText != null)
            {
                if (planetText != "")
                {
                    planetId = (int)Enum.Parse(typeof(Planets), planetText) + 1;
                }
                else
                {
                    planetId = 2;
                }
            }
            else
            {
                planetId = 3;
            }


            game.planetToLoad = (uint)planetId;
        }

        // I just copied the planets one, idk how it works
        private void skins_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int skinId;
            String skinText;
            skinText = (String)skins_comboBox.SelectedItem;
            if (skinText != null)
            {
                if (skinText != "")
                {
                    skinId = (int)Enum.Parse(typeof(Skins), skinText);
                }
                else
                {
                    skinId = 2;
                }
            }
            else
            {
                skinId = 3;
            }


            game.skinToLoad = (uint)skinId;
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

        private void dreadPoints_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    api.WriteMemory(pid, rac4.addr.dreadPoints, uint.Parse(dreadPoints_textBox.Text));
                }
                catch
                {
                    MessageBox.Show("Please enter a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void CM_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    uint CM = uint.Parse(CM_textBox.Text);
                    api.WriteMemory(pid, rac4.addr.CM, new byte[] { (byte)CM });
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
            game.DieRac4();
        }

        private void savePosButton_Click(object sender, EventArgs e)
        {
            SavePosition();
        }

        private void SavePosition()
        {
            game.SavePosition();
        }


        private void loadPosButton_Click(object sender, EventArgs e)
        {
            LoadPosition();
        }

        private void LoadPosition()
        {
            game.LoadPosition();
        }

        private void botsUnlocksWindowButton_Click(object sender, EventArgs e)
        {
            RAC4BotsUnlocks unlocks = new RAC4BotsUnlocks(game);
            unlocks.Show();
        }

        private void switchGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClosing -= RAC4Form_FormClosing;
            Program.AttachPS3Form.Show();
            Close();
        }

        private int healthFreezeSubID = -1;

        // I couldn't get this to work, he just dies - isak
        private void freezeHealthCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (freezeHealthCheckbox.Checked)
            {
                // It puts Ratchet to 0 and makes it invencible, idk why 200 tbh
                healthFreezeSubID = game.api.FreezeMemory(game.api.getCurrentPID(), rac4.addr.playerHealth, 200);
            }
            else
            {
                game.api.ReleaseSubID(healthFreezeSubID);
            }
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

        private void savepos_Click(object sender, EventArgs e) 
        {
            game.SavePosition();
        }

        private void loadpos_Click(object sender, EventArgs e)
        {
            game.LoadPositionRac4();
        }

        // Only works on Dread Station, on the other planets you have to go to the Skins menu and exit to apply the skin, need to figure out but kinda works
        private void skinsButton_Click_1(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            api.WriteMemory(pid, rac4.addr.skin, new byte[] { (byte)game.skinToLoad });
            api.WriteMemory(pid, 0x0110D975, 1);
            KillYourself();
        }

        private void loadPlanetButton_Click_1(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            api.WriteMemory(pid, rac4.addr.targetPlanet, game.planetToLoad);
            api.WriteMemory(pid, rac4.addr.loadPlanet2, 1);
        }

        private void unlockPlanetsButton_Click_1(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            api.WriteMemory(pid, rac4.addr.badges, new byte[] { 0x00, 0x02, 0x00, 0x04, 0x02, 0x02 });
            api.WriteMemory(pid, rac4.addr.range, new byte[] { 0x04 });
            api.WriteMemory(pid, rac4.addr.dreadPoints, 1000000);
            api.WriteMemory(pid, rac4.addr.targetPlanet, 1);
            api.WriteMemory(pid, rac4.addr.loadPlanet2, 1);
        }
    }
}
