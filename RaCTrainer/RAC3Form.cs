using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using Timer = System.Windows.Forms.Timer;

namespace racman
{
    public partial class RAC3Form : Form
    {
        private enum Planets
        {
            Veldin,
            Florana,
            StarshipPhoenix,
            Marcadia,
            Daxx,
            PhoenixRescue,
            AnnihilationNation,
            Aquatos,
            Tyhrranosis,
            ZeldrinStarport,
            ObaniGemini,
            BlackwaterCity,
            Holostar,
            Koros,
            Unknown,
            Metropolis,
            CrashSite,
            Aridia,
            QwarksHideout,
            LaunchSite,
            ObaniDraco,
            CommandCenter,
            HolostarClank,
            InsomniacMuseum,
            Unknown2,
            MetropolisRangers,
            AquatosClank,
            AquatosSewers,
            TyhrranosisRangers,
            VidComic6,
            VidComic1,
            VidComic4,
            VidComic2,
            VidComic3,
            VidComic5,
            VidComic1SpecialEdition,
        }
        int ohkoMemSubID = -1;

        public rac3 game;
        private AutosplitterHelper autosplitterHelper;
        private AutosplitterConfigForm autosplitterConfigForm;

        public static string[] saves;
        public Timer CoordsTimer = new Timer();
        public RAC3Form(rac3 game)
        {
            this.game = game;

            autosplitterHelper = new AutosplitterHelper();
            autosplitterHelper.StartAutosplitterForGame(game);

            autosplitterConfigForm = new AutosplitterConfigForm();

            InitializeComponent();
            positions_ComboBox.Text = "1";
            planets_comboBox.Text = "Veldin";
            textBox1.KeyDown += TextBox1_KeyDown;

            CoordsTimer.Interval = (int)16.66667;
            CoordsTimer.Tick += new EventHandler(UpdateCoordsLabel);
            game.GetPlayerCoordinates();

            game.SetupInputDisplayMemorySubs();

            var sr = func.GetConfigData("config.txt", "rc3SplitRoute");
            if (sr != "")
            {
                if (autosplitterConfigForm.TrySelectRoute(sr))
                {
                    autosplitterHelper.WriteConfig(autosplitterConfigForm.SelectedRoute.bytes.ToArray());
                }
                else
                {
                    // The split route specified in config isn't available (most likely it was deleted)
                    func.ChangeFileLines("config.txt", "", "rc3SplitRoute");
                }
            }
            else
            {
                if (autosplitterConfigForm.TrySelectRoute("ATB"))
                {
                    autosplitterHelper.WriteConfig(autosplitterConfigForm.SelectedRoute.bytes.ToArray());
                }
            }

            /*saves = Directory.GetFiles($@"{Directory.GetCurrentDirectory()}\\saves\{AttachPS3Form.game}");

            foreach(string i in saves)
            {
                savefileHelperComboBox.Items.Add(i.Substring(i.IndexOf(AttachPS3Form.game) + 10));
            }*/

        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    game.SetBoltCount(uint.Parse(textBox1.Text));
                }
                catch
                {
                    MessageBox.Show("Please enter a number", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public Form InputDisplay;
        public static string ip = AttachPS3Form.ip;
        public static int pid = AttachPS3Form.pid;

        public int saved_pos_index = 1;
        public string current_planet;

        private void loadPosButton_Click(object sender, EventArgs e)
        {
            game.LoadPosition();
        }
        private void savePosButton_Click(object sender, EventArgs e)
        {
            game.SavePosition();
        }

        private void killyourself_Click(object sender, EventArgs e)
        {
            game.KillYourself();
        }

        private void positions_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            game.selectedPositionIndex = positions_ComboBox.SelectedIndex;
        }


        private void button6_Click(object sender, EventArgs e)
        {
            game.SetupFile();
        }

        private void loadPlanetButton_Click(object sender, EventArgs e)
        {
            game.LoadPlanet();
            game.SetFastLoads(true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set current challenge mode
            challengeModeInput.Value = game.GetChallengeMode();

            // Tick currently enabled vic comics
            for (var i = 0; i < vidComicCheckedListBox.Items.Count; i++)
            {
                vidComicCheckedListBox.SetItemChecked(i, game.GetVidComic(i));
            }

            int savefileHelperSubID = game.api.SubMemory(pid, 0xD9FF00, 1, (value) =>
            {
                if (value[0] == 1)
                {
                    this.Invoke(new Action(() => {
                        setAsideButton.Enabled = true;
                        loadFileButton.Enabled = true;
                    }));
                }
            });
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Tunes klunk, positions vid comic ingame menu to 'Ende' (end) and\n removes help desk message in command center at the thyrra button.", button6);
        }

        private void inputdisplay_Click(object sender, EventArgs e)
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

        private void OHKOCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IPS3API api = func.api;

            var isChecked = ((CheckBox)sender).Checked;

            if (isChecked)
            {
                ohkoMemSubID = api.FreezeMemory(AttachPS3Form.pid, rac3.addr.playerHealth, Ratchetron.MemoryCondition.Above, 1);
            } else
            {
                api.ReleaseSubID(ohkoMemSubID);
            }
        }

        private void switchGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClosing -= Form1_FormClosing;
            Program.AttachPS3Form.Show();
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            func.api.Disconnect();
            Application.Exit();
        }

        private void ghostCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            game.SetGhostRatchet(ghostCheckbox.Checked);
        }

        private void planets_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int planetId;
            String planetText;
            planetText = (String)planets_comboBox.SelectedItem;
            if (planetText != null)
            {
                if(planetText != "")
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
                //planetId = planets_comboBox.SelectedIndex + 1;
                planetId = 3;
            }

            
            game.planetToLoad = (uint)planetId;
        }

        private void controllerCombosCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            game.InputsTimer.Enabled = ((CheckBox)sender).Checked;
        }

        private void freezeAmmoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            game.ToggleInfiniteAmmo(freezeAmmoCheckBox.Checked);
        }

        private void unlockTitaniumBoltsButton_Click(object sender, EventArgs e)
        {
            game.GiveAllTitaniumBolts();
        }

        private void resetTitaniumBoltsButton_Click(object sender, EventArgs e)
        {
            game.ResetAllTitaniumBolts();
        }

        private void unlockSkillPointsButton_Click(object sender, EventArgs e)
        {
            game.GiveAllSkillpoints();
        }

        private void resetSkillPointsButton_Click(object sender, EventArgs e)
        {
            game.ResetAllSkillpoints();
        }

        private void challengeModeInput_ValueChanged(object sender, EventArgs e)
        {
            game.SetChallengeMode((byte)challengeModeInput.Value);
        }
        
        private void vidComicCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This stops the vid comic item from staying selected
            vidComicCheckedListBox.ClearSelected();
        }

        private void vidComicCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            game.SetVidComic(e.Index, e.NewValue == CheckState.Checked);
        }

        private void armorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            game.SetArmor(armorComboBox.SelectedIndex);
        }

        private void freezeHealthCheck_CheckedChanged(object sender, EventArgs e)
        {
            game.SetInfiniteHealth(freezeHealthCheck.Checked);
        }

        static ModLoaderForm modLoaderForm;

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
            if (controllerCombosCheckbox.Checked)
                game.InputsTimer.Enabled = true;
        }
        private void loadFileButton_Click(object sender, EventArgs e)
        {
            game.api.WriteMemory(pid, 0xD9FF01, new byte[] { 0x01 });
        }
        private void setAsideButton_Click(object sender, EventArgs e)
        {
            game.api.WriteMemory(pid, 0xD9FF02, new byte[] { 0x01 });
        }
        static bool qsbool = false;
        private void toggleQS_Click(object sender, EventArgs e)
        {
               qsbool = !qsbool;
            if (qsbool)
                game.api.WriteMemory(pid, rac3.addr.quickSelectPause, new byte[] { 1 });
            if (!qsbool)
                game.api.WriteMemory(pid, rac3.addr.quickSelectPause, new byte[] { 0 }); 
        }

        private void coordsComboBox_CheckedChanged(object sender, EventArgs e)
        {
            var check = ((CheckBox)sender).Checked;
            CoordsTimer.Enabled = check;
            if (check) this.Height += 50;
        }
        public void UpdateCoordsLabel(object sender, EventArgs e)
        {
            coordsLabel.Text = $"X: {game.coords[0]}\nY: {game.coords[1]}\nZ: {game.coords[2]}";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            game.KlunkTuneToggle(((CheckBox)sender).Checked);
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void shipColourComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            game.SetShipColour(shipColourComboBox.SelectedIndex);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    game.api.WriteMemory(game.api.getCurrentPID(), 0xDA64E0, UInt32.Parse(textBox2.Text));
                }
                catch
                {
                    MessageBox.Show("Unable to parse number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            } 
        }

        private void memoryUtilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryForm memoryForm = new MemoryForm();
            memoryForm.Show();
        }

        private void buttonUnlocks_Click(object sender, EventArgs e)
        {
            UYAUnlocks unlocks = new UYAUnlocks(game);
            unlocks.Show();
        }

        private void buttonSetup_Click(object sender, EventArgs e)
        {
            UYAUnlocks unlocks = new UYAUnlocks(game);
            unlocks.SetupNGPWeapons();
            unlocks.Dispose();

            game.SetBoltCount(1561120);
            game.api.WriteMemory(pid, rac3.addr.quickSelectPause, new byte[] { 0 });
            // No idea what the max value is here
            game.api.WriteMemory(game.pid, rac3.addr.healthXP, 50000000);
            game.api.WriteMemory(game.pid, rac3.addr.playerHealth, 200);
            // See IGT textbox at textbox2_KeyDown
            game.api.WriteMemory(game.api.getCurrentPID(), 0xDA64E0, 2228300);
            // Lets go with infernox
            game.SetArmor(4);

            // We don't want to set up challenge mode from a non-challenge mode file, for some reason.
            // I think it's fine but see: https://github.com/MichaelRelaxen/racman/pull/47
            var cm = game.GetChallengeMode();
            if (cm == 0)
            {
                var errorMessage = "RaCMAN has successfully set up your weapons and armor, but could not set your challenge mode.\n"
                    + "To ensure your runs are valid, you should enter challenge mode manually, then hit the setup button again.\n"
                    + "See: https://github.com/MichaelRelaxen/racman/pull/47#issuecomment-1529163954\n\n"
                    + "RaCMAN can take you to the final level to make this process easier for you. Do you want to go now?";
                var res = MessageBox.Show(errorMessage, "Challenge mode", MessageBoxButtons.YesNo);

                if (res == DialogResult.Yes)
                {
                    game.planetToLoad = (uint)((int)Enum.Parse(typeof(Planets), "LaunchSite") + 1);
                    game.LoadPlanet();
                }
            } 
            else
            {
                game.SetChallengeMode(13);
                game.api.Notify("Set up weapons, armor and health for NG+ categories! Setup bolts, IGT and challenge mode for QE!");
            }
        }

        private void coordsLabel_Click(object sender, EventArgs e)
        {

        }

        private void editRouteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autosplitterConfigForm.ShowDialog();

            if (autosplitterConfigForm.SelectedRoute != null)
            {
                autosplitterHelper.WriteConfig(autosplitterConfigForm.SelectedRoute.bytes.ToArray());
                func.ChangeFileLines("config.txt", autosplitterConfigForm.SelectedRoute.Name, "rc3SplitRoute");
            }
        }
    }
}