using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace racman
{
    public partial class RAC2Form : Form
    {
        AutosplitterHelper autosplitter;
        public rac2 game;
        public Form InputDisplay;
        private bool debugEnabled;
        private int savefileHelperSubID;
        private int expEconomySubId = -1;

        public RAC2Form(rac2 game)
        {
            this.game = game;

            InitializeComponent();

            positions_comboBox.Text = "1";
            bolts_textBox.KeyDown += bolts_textBox_KeyDown;
            game.SetupInputDisplayMemorySubs();

            AutosplitterCheckbox.Checked = true;

            savefileHelperSubID = game.api.SubMemory(game.api.getCurrentPID(), 0x10cd71d, 1, value =>
            {
                if (value[0] == 1)
                {
                    this.Invoke(new Action(() =>
                    {
                        // Savefile helper mod is enabled.
                        loadFileButton.Enabled = true;
                        setAsideFileButton.Enabled = true;
                    }));
                }
            });
        }

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
            this.Invoke(new Action(() => {
                planets_comboBox.SelectedIndex = (int)game.planetIndex;
            }));
        }

        private void RAC2Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            game.api.ReleaseSubID(savefileHelperSubID);
            game.api.Disconnect();
            Application.Exit();
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
            game.LoadPlanet();
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
            if (freezeAmmoCheckbox.Checked)
            {
                game.api.WriteMemory(game.api.getCurrentPID(), 0x148185C, 136, "7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF7FFFFFFF");
            }
        }

        private void memoryUtilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryForm memoryForm = new MemoryForm();
            memoryForm.Show();
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

        private void raritaniumTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    game.api.WriteMemory(game.api.getCurrentPID(), 0x1329A94, uint.Parse(raritaniumTextBox.Text));
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
                    game.api.WriteMemory(game.api.getCurrentPID(), 0x1329AA2, new byte[] { byte.Parse(challengeTextBox.Text) });
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

        
        private int fastLoadSubID = -1;
        private void SetFastLoadCheckbox_CheckedChanged(object sender, EventArgs e)
        {

            if (SetFastLoadCheckbox.Checked)
            {
            // Address related to some kind of ship animations timing, it messes up the entering, exiting ship animations and crash if its active in cutscenes
                fastLoadSubID = game.api.FreezeMemory(game.api.getCurrentPID(), 0x01471890, 0);
            }
            else
            {
                game.api.ReleaseSubID(fastLoadSubID);
            }
            // Ship load screen addresses that I dont know what to do with it 0x147A257 (1-4 amount of load screens and display wich one is currently) 
            // and 0x0147A258 (0-2 sequence of the load screens, 0 is the first and 2 is the last)
        }

        private void labelLap_Click(object sender, EventArgs e)
        {

        }

        private void loadFileButton_Click(object sender, EventArgs e)
        {
            game.api.WriteMemory(game.api.getCurrentPID(), 0x10cd71e, new byte[] { 1 });
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

        private void buttonRaceStorage_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();
            api.WriteMemory(pid, 0x1A4D7E0, 0); // Race storages
            api.Notify("Reset Barlow race storage.");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RC2Unlocks unlocks = new RC2Unlocks(game);
            unlocks.Show();
        }

        private void buttonNGPlusMenu_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            api.WriteMemory(pid, rac2.addr.snivBoss, new byte[] { 20 });
            // We should setup pad manip, since this happens whenever Snivelak is visited.
            api.WriteMemory(pid, rac2.addr.padManip, 1103626240); // 25 as a float
            api.WriteMemory(pid, 0x1A9DF90, new byte[] { 66 }); // Just need to set this address to 20 in float value but Idk how to do it, with 66 in byte value works likewise. 
            api.WriteMemory(pid, rac2.addr.gornManip, 1);
            api.WriteMemory(pid, rac2.addr.gornOpening, 1);
            api.WriteMemory(pid, rac2.addr.imInShortcuts, 1);
            api.WriteMemory(pid, rac2.addr.shortcutsIndex, 7);
            api.Notify("NG+ insomniac museum menus, gorn manips, protopet and sniv boss tuning are set up!");
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

    }
}
