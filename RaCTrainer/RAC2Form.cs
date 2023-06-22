using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private int expEconomySubId = -1;

        public RAC2Form(rac2 game)
        {
            this.game = game;

            InitializeComponent();

            positions_comboBox.Text = "1";
            bolts_textBox.KeyDown += bolts_textBox_KeyDown;
            game.SetupInputDisplayMemorySubs();

            AutosplitterCheckbox.Checked = true;
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
            game.api.Disconnect();
            Application.Exit();
        }
        private void InputDisplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            InputDisplay = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!(game.api is Ratchetron))
            {
                MessageBox.Show("You need to be using the new API to use input display");
                return;
            }

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
            if(CComboCheckBox.Checked) 
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

            api.WriteMemory(pid, 0x13965F4, 0); // Hoverbike menu
            api.WriteMemory(pid, 0x1329AAC, 0); // Bolt economy
            api.WriteMemory(pid, 0x1A5815B, 0); // Endako cutscene
            api.WriteMemory(pid, 0x1AAC767, 0); // Game pyramid bolt drop

            api.Notify("Game Pyramid, Bolts manip, Hoverbike menu, and Endako Boss Cutscene are now reset and ready for runs");
        }

        private void labelLap_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            api.WriteMemory(pid, rac2.addr.snivBoss1, new byte[] { 10 });
            api.WriteMemory(pid, rac2.addr.snivBoss2, new byte[] { 10 });
            api.Notify("Snivelak boss act tunining done for NG+!");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            var api = game.api;
            var pid = api.getCurrentPID();
            var pos = "432dbf2e4422675643bee66500000000b6300000b5d00000bf92c68a0000";
            var tele = "442d25784414fa7943c152353f800000b5d00000b5d000004042fe940000";


            var res = MessageBox.Show("This will transport you to planet Yeedil. Do you want to continue?", "Protopet tuning", MessageBoxButtons.YesNo);

            if (res == DialogResult.Yes)
            {
                game.planetToLoad = 20;
                game.LoadPlanet();

                var playerState = 1;
                do
                {
                    // This should be player state
                    var stateBytes = api.ReadMemory(pid, rac2.addr.playerCoords + 0x10, 1);
                    playerState = stateBytes[0];
                }
                while (playerState != 0);

                System.Threading.Thread.Sleep(2500);
                api.WriteMemory(pid, rac2.addr.playerCoords, 30, tele);

                var wait = MessageBox.Show("RaCMAN will now take you to the Protopet. Click OK when you have loaded the protopet.", "Protopet tuning", MessageBoxButtons.OK);
                if (wait == DialogResult.OK)
                {
                    for (var i = 0; i < 20; i++)
                    {
                        // lol
                        api.WriteMemory(pid, rac2.addr.playerCoords, 30, pos);
                        System.Threading.Thread.Sleep(1000);
                        game.KillYourself();
                        System.Threading.Thread.Sleep(1000);
                    }
                }

                api.Notify("Protopet boss act tunining done for NG+!");
            }
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

        private void buttonGorn_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            api.WriteMemory(pid, rac2.addr.gornManip, 1);
        }
    }
}
