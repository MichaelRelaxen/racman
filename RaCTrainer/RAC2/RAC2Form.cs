using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace racman
{
    public partial class RAC2Form : Form
    {
        static ModLoaderForm modLoaderForm;
        static ChargebootColorPicker cosmeticsForm;

        AutosplitterHelper autosplitter;
        public rac2 game;
        public Form InputDisplay;
        private bool debugEnabled;
        private int savefileHelperSubID;
        private int fastLoadSubID = -1;
        private int expEconomySubId = -1;
        // Used to reset fast loads after load is finished
        private int loadScreenTypeSubId = -1;
        private byte prevLoadScreen = 255;

        public RAC2Form(rac2 game)
        {
            this.game = game;

            CoordsTimer.Interval = (int)16.66667;
            CoordsTimer.Tick += new EventHandler(UpdateCoordsLabel);
            game.GetPlayerCoordinates();

            InitializeComponent();

            positions_comboBox.Text = "1";
            bolts_textBox.KeyDown += bolts_textBox_KeyDown;
            game.SetupInputDisplayMemorySubs();

            AutosplitterCheckbox.Checked = true;
        }

        public Timer CoordsTimer = new Timer();

        public void UpdateLapFlag(int flagValue)
        {
            labelLap.Visible = true;
            labelLap.Text = $"Lap value: {flagValue}";
            if (flagValue == 1)
                labelLap.ForeColor = Color.Green;
            else
                labelLap.ForeColor = Color.Red;
        }

        private void RAC2Form_Load(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            savefileHelperSubID = game.api.SubMemory(game.api.getCurrentPID(), 0x10cd71d, 1, value =>
            {
                // this lione
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

            loadScreenTypeSubId = game.api.SubMemory(game.api.getCurrentPID(), rac2.addr.loadingScreenCount, 1, IPS3API.MemoryCondition.Changed, value =>
            {
                // Work around a bug in Ratchetron
                var loadScreen = value[0];
                if (loadScreen == prevLoadScreen) return;
                prevLoadScreen = loadScreen;

                // Only run once, on final load screen
                if (loadScreen != 2) return;

                // Disable force-override from reload file by setting to previous setting
                game.enableDisableFastLoads(SetFastLoadCheckbox.Checked);

                if (!checkBoxAutoReset.Checked) return;
                var planet = api.ReadMemory(pid, rac2.addr.currentPlanet, 4);
                if (planet[3] != 0) return;
                // Everything below here is A1-exclusive code
                resetMenuStorage();
                this.Invoke(new Action(() => {
                    freezeAmmoCheckbox.Checked = false;
                    freezeAmmoCheckbox_CheckedChanged(sender, EventArgs.Empty);
                }));
            });

            this.Invoke(new Action(() => {
                planets_comboBox.SelectedIndex = (int)game.planetIndex;
            }));
        }

        private void RAC2Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Fix crash on exit (lol)
            try
            {
                if (fastLoadSubID != -1) game.api.ReleaseSubID(fastLoadSubID);
                if (expEconomySubId != -1) game.api.ReleaseSubID(expEconomySubId);
                if (loadScreenTypeSubId != -1) game.api.ReleaseSubID(loadScreenTypeSubId);
                game.api.ReleaseSubID(savefileHelperSubID);
                game.api.Disconnect();
                Application.Exit();
            }
            catch
            {

            }
        }
        private void InputDisplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            InputDisplay = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (InputDisplay == null)
            {
                InputDisplay = new InputDisplay();
                InputDisplay.FormClosed += InputDisplay_FormClosed;
                InputDisplay.Show();
            }
        }

        private void savepos_Click(object sender, EventArgs e)
        {
            game.SavePosition();
        }

        private void loadpos_Click(object sender, EventArgs e)
        {
            game.LoadPosition();
        }

        private void killyourself_Click(object sender, EventArgs e)
        {
            game.KillYourself();
        }

        private void bolts_textBox_KeyDown(object sender, KeyEventArgs e)
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

        private void loadPlanetButton_Click(object sender, EventArgs e)
        {
            game.enableDisableFastLoads(true);
            game.LoadPlanet(resetFlags: checkBoxResetFlags.Checked);
        }

        private void planets_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            game.planetToLoad = (uint)planets_comboBox.SelectedIndex;
        }

        private void positions_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            game.selectedPositionIndex = positions_comboBox.SelectedIndex;
        }

        private void CComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CComboCheckBox.Checked)
                game.InputsTimer.Enabled = true;
            else
                game.InputsTimer.Enabled = false;
        }

        private void ghostCheckbox_CheckedChanged_1(object sender, EventArgs e)
        {
            game.SetGhostRatchet(ghostCheckbox.Checked);
        }

        private void switchGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClosing -= RAC2Form_FormClosing;
            Program.AttachPS3Form.Show();
            Close();
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

        private void AutosplitterCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!AutosplitterCheckbox.Checked)
            {
                // Disable autosplitter.
                autosplitter.Stop();
                autosplitter = null;
            }
            else
            {
                // Enable auotpslitter
                Console.WriteLine("Autosplitter starting!");
                autosplitter = new AutosplitterHelper();
                autosplitter.StartAutosplitterForGame(this.game);
            }
        }

        private int healthFreezeSubID = -1;
        private void freezeHealthCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (freezeHealthCheckbox.Checked)
            {
                healthFreezeSubID = game.api.FreezeMemory(game.api.getCurrentPID(), 0x14816AC, 42069);
            }
            else
            {
                game.api.ReleaseSubID(healthFreezeSubID);
            }
        }

        // Doesn't actually freeze anything, just gives you 2 billion of every ammo (clickbait)
        private void freezeAmmoCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();
            uint ammoResetAddr = 0x0B30C7C;

            if (freezeAmmoCheckbox.Checked)
            {
                api.WriteMemory(pid, 0x148185C, 136, "7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF");
                api.WriteMemory(pid, ammoResetAddr, 0x60000000); // nop
            }
            else
            {
                api.WriteMemory(pid, ammoResetAddr, 0x7C64292E); // default instr
            }
        }

        private void memoryUtilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryForm memoryForm = new MemoryForm();
            memoryForm.Show();
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

        private void raritaniumTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    game.api.WriteMemory(game.api.getCurrentPID(), rac2.addr.currentRaritanium, uint.Parse(raritaniumTextBox.Text));
                }
                catch
                {
                    MessageBox.Show("Please enter a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxHealthXP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    game.api.WriteMemory(game.api.getCurrentPID(), rac2.addr.healthExp, unchecked((uint)int.Parse(textBoxHealthXP.Text)));
                }
                catch
                {
                    MessageBox.Show("Please enter a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    game.api.WriteMemory(game.api.getCurrentPID(), rac2.addr.challengeMode, new byte[] { byte.Parse(challengeTextBox.Text) });
                }
                catch
                {
                    MessageBox.Show("Please enter a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void resetFileManipButton_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            api.WriteMemory(pid, 0x1329AAC, 0); // Bolt economy
            api.WriteMemory(pid, 0x1A5815B, 0); // Endako cutscene
            api.WriteMemory(pid, 0x1AAC767, 0); // Game pyramid bolt drop
            api.WriteMemory(pid, 0x1A4D7E0, 0); // Race storage

            api.Notify("Game Pyramid, Bolts manip, Race Storage and Endako Boss Cutscene are now reset and ready for runs");
        }


  


        private void SetFastLoadCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            game.enableDisableFastLoads(SetFastLoadCheckbox.Checked);
        }

        private void labelLap_Click(object sender, EventArgs e)
        {

        }

        private void loadFileButton_Click(object sender, EventArgs e)
        {
            game.loadSetAsideFile();
        }

        private void setAsideFileButton_Click(object sender, EventArgs e)
        {
            game.api.WriteMemory(game.api.getCurrentPID(), 0x10cd71f, new byte[] { 1 });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();
            // Swingshot has weapon ID 0D
            api.WriteMemory(pid, rac2.addr.prevHeldWeapon, new byte[] { 0x0D });
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            if (checkBoxExp.Checked)
            {
                expEconomySubId = api.FreezeMemory(pid, rac2.addr.expEconomy, 1, Ratchetron.MemoryCondition.Changed, new byte[] { 100 });
            }
            else
            {
                if (expEconomySubId != -1) api.ReleaseSubID(expEconomySubId);
                api.WriteMemory(pid, rac2.addr.expEconomy, new byte[] { 0 });
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void resetMenuStorage()
        {
            var api = game.api;
            var pid = api.getCurrentPID();
            // Disable race storage
            api.WriteMemory(pid, rac2.addr.savedRaceIndex, 0);
            // Disable ship opening cutscenes
            api.WriteMemory(pid, rac2.addr.feltzinOpening, 1);
            api.WriteMemory(pid, rac2.addr.gornOpening, 1);
            // Fix ship mission menus
            api.WriteMemory(pid, rac2.addr.feltzinMissionComplete, 0);
            api.WriteMemory(pid, rac2.addr.hrugisMissionComplete, 0);
            api.WriteMemory(pid, rac2.addr.gornMissionComplete, 0);

        }

        private void buttonRaceStorage_Click(object sender, EventArgs e)
        {
            resetMenuStorage();
            game.api.Notify("Reset menu storage!");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RC2Unlocks unlocks = new RC2Unlocks(game);
            unlocks.Show();
        }

        private void SetupGeneralNGPlusMenus()
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            api.WriteMemory(pid, rac2.addr.snivBoss, new byte[] { 20 });
            // We should setup pad manip, since this happens whenever Snivelak is visited.
            api.WriteMemory(pid, rac2.addr.padManip, 1103626240); // 25 as a float
            api.WriteMemory(pid, rac2.addr.yeedilBoss, new byte[] { 66 });
            api.WriteMemory(pid, rac2.addr.sibBoss, new byte[] { 20 });
            api.WriteMemory(pid, rac2.addr.gornManip, 1);
            api.WriteMemory(pid, rac2.addr.gornOpening, 1);
            api.WriteMemory(pid, rac2.addr.imInShortcuts, 1);

            api.Notify("Manips done!");
        }

        private void buttonNGPlusMenu_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();
            api.WriteMemory(pid, rac2.addr.shortcutsIndex, 7); // Museum
            SetupGeneralNGPlusMenus();
          
        }

        private void buttonNoIMGMenu_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();
            api.WriteMemory(pid, rac2.addr.shortcutsIndex, 1); // Barlow
            SetupGeneralNGPlusMenus();
        }

        private void buttonSetupAllMissions_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();
            api.WriteMemory(pid, rac2.addr.shortcutsIndex, 7); // Museum
            api.WriteMemory(pid, 0x1A5815B, 1); // Endako cutscene
            SetupGeneralNGPlusMenus();
        }

        private void debugToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            debugFeaturesToolStripMenuItem.Checked = debugEnabled;
        }

        private void debugFeaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            debugEnabled = !debugEnabled;
            debugFeaturesToolStripMenuItem.Checked = debugEnabled;

            var api = game.api;
            var pid = api.getCurrentPID();
            api.WriteMemory(pid, rac2.addr.debugFeatures, new byte[] { (byte)(debugEnabled ? 1 : 0) });
        }

        private void debugFeaturesToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Enables built-in debugging features. Press shoulder buttons to skip electrolyzer puzzles and arena missions.", menuStrip1, 1500);
        }

        private void coordsComboBox_CheckedChanged(object sender, EventArgs e)
        {
            var check = ((CheckBox)sender).Checked;
            CoordsTimer.Enabled = check;
            coordsLabel.Visible = true;
            if (check) this.Height += 50;
        }

        private void coordsLabel_Click(object sender, EventArgs e)
        {

        }

        public void UpdateCoordsLabel(object sender, EventArgs e)
        {
            coordsLabel.Text = $"X: {game.coords[0]}\nY: {game.coords[1]}\nZ: {game.coords[2]}";
        }

        private void activateQEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            var inputForm = new SimpleInputDialogForm("Overwrite save write-offset with what?", "-1");
            inputForm.Width = 400; // Otherwise the text gets hidden
            inputForm.ShowDialog();

            if (inputForm.DialogResult != DialogResult.OK) return;
            var text = inputForm.inputTextBox.Text;

            short writeOffset;
            if (short.TryParse(text, out writeOffset))
            {
                byte[] offsetBytes = BitConverter.GetBytes(writeOffset);
                if (BitConverter.IsLittleEndian) Array.Reverse(offsetBytes);
                api.WriteMemory(pid, rac2.addr.selectedSaveSlot, offsetBytes);
                MessageBox.Show("Done!");

            }
            else
            {
                MessageBox.Show("Please enter a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonRespawn_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            var currPos = api.ReadMemory(pid, rac2.addr.playerCoords, 24);
            api.WriteMemory(pid, rac2.addr.respawnCoords, currPos);
        }

        private void levelFlagViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var viewer = new FlagViewer(game, rac2.addr.levelFlags + (game.planetToLoad * 0x10), 0x10);
            viewer.Text = $"{planets_comboBox.SelectedItem} level flags";
            viewer.Show();
        }

        private void buttonUnlockAllPlat_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();
            byte[] unlocked = Enumerable.Repeat((byte)0xFF, 0x70).ToArray();
            api.WriteMemory(pid, rac2.addr.platinumBoltArray, unlocked);
        }

        private void buttonResetPlatBolts_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();
            byte[] locked = Enumerable.Repeat((byte)0x00, 0x70).ToArray();
            api.WriteMemory(pid, rac2.addr.platinumBoltArray, locked);
        }

        private void checkBoxResetFlags_CheckedChanged(object sender, EventArgs e)
        {
            game.resetFlagsRequested = checkBoxResetFlags.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            api.WriteMemory(pid, rac2.addr.slotsHit, new byte[] { 40 }); 
            api.WriteMemory(pid, rac2.addr.pBolts, new byte[] { 74 });
            api.WriteMemory(pid, rac2.addr.pJackpot, new byte[] { 45 });
            api.Notify("Maktar slots done!");
        }

        private void buttonCosmetics_Click(object sender, EventArgs e)
        {
            cosmeticsForm = new ChargebootColorPicker(
                game.api, 
                rac2.addr.chargebootsPrimaryFrontColor, 
                rac2.addr.chargebootsPrimaryBackColor,
                rac2.addr.chargebootsTintFrontColor,
                rac2.addr.chargebootsTintBackColor
            );
            cosmeticsForm.Show();
        }
    }
}
