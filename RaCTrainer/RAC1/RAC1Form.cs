using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace racman
{

    public partial class RAC1Form : Form
    {
        public Form UnlocksWindow;
        public Form HovenHealthForm;
        public Form InputDisplay;
        public static string ip = AttachPS3Form.ip;
        public static int pid = AttachPS3Form.pid;
        private static Timer ForceLoadTimer = new Timer();

        private Mod gbspiMod = null;

        private AutosplitterHelper autosplitterHelper;

        public rac1 game;

        public RAC1Form(rac1 game)
        {
            this.game = game;

            InitializeComponent();
            positions_comboBox.Text = "1";
            bolts_textBox.KeyDown += bolts_TextBox_KeyDown;

            goodiesCheck.Checked = game.GoodiesMenuEnabled();

            game.SetupInputDisplayMemorySubs();

            if (func.GetConfigData("config.txt", "platinum") == "purchased")
            {
                platinumLabel.Visible = true;
            }

            if (func.GetConfigData("config.txt", "gbspi_split_enabled") == "true")
            {
                gbspiSplitToolStripMenuItem.Checked = true;
            }

            if (func.GetConfigData("config.txt", "autosplitter_enabled") != "false")
            {
                autosplitterEnabledToolStripMenuItem.Checked = true;
            } else
            {
                gbspiSplitToolStripMenuItem.Enabled = false;
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

        private void savePosButton_Click(object sender, EventArgs e)
        {
            SavePosition();
        }
        private void loadPosButton_Click(object sender, EventArgs e)
        {
            LoadPosition();
        }
        private void killyourself_Click(object sender, EventArgs e)
        {
            KillYourself();
        }
        private void loadPlanetButton_Click_1(object sender, EventArgs e)
        {
            LoadPlanet();
        }
        private void SavePosition()
        {
            game.SavePosition();
        }
        private void LoadPosition()
        {
            game.LoadPosition();
        }
        private void KillYourself()
        {
            game.KillYourself();
        }
        private void LoadPlanet()
        {
            game.LoadPlanet(lflagresetCb.Checked, resetGB_checkbox.Checked);
        }

        private void ForceOkayLoad(object sender, EventArgs e)
        {
            game.api.WriteMemory(game.pid, 0x9645C4, new byte[] { 0x00, 0x00, 0x00, 0x1a, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x02 });
            ForceLoadTimer.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => {
                planets_comboBox.SelectedIndex = (int)game.planetIndex;
            }));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (gbspiMod != null)
            {
                gbspiMod.Unload();
                gbspiMod = null;
            }

            if (autosplitterHelper != null && autosplitterHelper.IsRunning)
            {
                autosplitterHelper.Stop();
            }


            FreezeAmmoCheckbox.Checked = false;
            infHealth.Checked = false;
            FastLoadToggle.Checked = false;

            game.api.Disconnect();

            Application.Exit();
        }

        private void infHealth_Checkbox_Changed(object sender, EventArgs e)
        {
            game.SetInfiniteHealth(infHealth.Checked);
        }

        private void unlocksWindowButton_Click(object sender, EventArgs e)
        {
            if (UnlocksWindow == null)
            {
                UnlocksWindow = new UnlocksWindow(game);
                UnlocksWindow.FormClosed += UnlocksWindow_FormClosed;
                UnlocksWindow.Show();
            }
        }
        private void UnlocksWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnlocksWindow = null;
        }
        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void hovenHPButton_click(object sender, EventArgs e)
        {
            if (HovenHealthForm == null)
            {
                HovenHealthForm = new HovenHealthForm();
                HovenHealthForm.FormClosed += HovenHealthForm_FormClosed;
                HovenHealthForm.Show();
            }
        }

        private void HovenHealthForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HovenHealthForm = null;
        }

        private void switchGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClosing -= Form1_FormClosing;
            Program.AttachPS3Form.Show();
            Close();
        }

        private void inputdisplay_click(object sender, EventArgs e)
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
        private void goodiesCheck_CheckedChanged(object sender, EventArgs e)
        {
            game.SetGoodies(goodiesCheck.Checked);
        }

        private void ghostCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            game.SetGhostRatchet(ghostCheckbox.Checked);
        }

        private void drekskip_Click(object sender, EventArgs e)
        {
            game.SetDrekSkip(true);
        }

        private void CComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CComboCheckBox.Checked)
                game.InputsTimer.Enabled = true;
            else
                game.InputsTimer.Enabled = false;
        }

        private void FastLoadToggle_CheckedChanged(object sender, EventArgs e)
        {
            game.SetFastLoads(FastLoadToggle.Checked);
        }

        private void FreezeAmmoCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            game.ToggleInfiniteAmmo(FreezeAmmoCheckbox.Checked);
        }

        private void positions_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            game.selectedPositionIndex = positions_comboBox.SelectedIndex;
        }

        private void planets_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            game.planetToLoad = (uint)planets_comboBox.SelectedIndex;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void resetGBsButton_Click(object sender, EventArgs e)
        {
            game.ResetAllGoldBolts();
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
            if (CComboCheckBox.Checked)
                game.InputsTimer.Enabled = true;
        }

        private void resetSPsButton_Click(object sender, EventArgs e)
        {
            game.SetShootSkillPoints(true);
        }

        private void setupSPsButton_Click(object sender, EventArgs e)
        {
            game.SetShootSkillPoints(false);
        }

        private void buyPremiumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (func.GetConfigData("config.txt", "platinum") == "purchased")
            {
                MessageBox.Show("You're already one of our premium customers.");

                return;
            }

            DialogResult result = MessageBox.Show("Buy Platinum RaCMAN for 500,000 bolts?", "Purchase Platinum", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                int bolts = game.Bolts();

                if (bolts < 500000)
                {
                    MessageBox.Show("You can't afford that. You're too poor.");
                } else
                {
                    game.SetBoltCount((uint)bolts - 500000);

                    MessageBox.Show("Platinum successfully purchased!", "Sucker");

                    func.ChangeFileLines("config.txt", "purchased", "platinum");

                    platinumLabel.Visible = true;
                }
            } else
            {
                MessageBox.Show("LMAO you're poor.");
            }
        }

        private void gbspiSplitToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (gbspiSplitToolStripMenuItem.Checked)
            {
                gbspiMod = new Mod($"{Directory.GetCurrentDirectory()}\\mods\\{AttachPS3Form.game}\\gb_sp_as_helper\\");
                gbspiMod.Load();

                func.ChangeFileLines("config.txt", "true", "gbspi_split_enabled");
            } else
            {
                if (gbspiMod != null)
                {
                    gbspiMod.Unload();
                    gbspiMod = null;
                }

                func.ChangeFileLines("config.txt", "false", "gbspi_split_enabled");
            }
        }

        private void autosplitterEnabledToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (autosplitterEnabledToolStripMenuItem.Checked)
            {
                autosplitterHelper = new AutosplitterHelper();
                autosplitterHelper.StartAutosplitterForGame(game);

                func.ChangeFileLines("config.txt", "true", "autosplitter_enabled");
                gbspiSplitToolStripMenuItem.Enabled = true;
            }
            else
            {
                if (autosplitterHelper != null)
                {
                    autosplitterHelper.Stop();
                    autosplitterHelper = null;
                }

                func.ChangeFileLines("config.txt", "false", "autosplitter_enabled");

                gbspiSplitToolStripMenuItem.Checked = false;
                gbspiSplitToolStripMenuItem.Enabled = false;
            }
        }

        private void memoryUtilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryForm memoryForm = new MemoryForm();
            memoryForm.Show();
        }

        private void debugToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            updateHeroToolStripMenuItem.Checked = false;
            updateMobysToolStripMenuItem.Checked = false;
            updateParticlesToolStripMenuItem.Checked = false;
            normalCameraToolStripMenuItem.Checked = false;
            freecamToolStripMenuItem.Checked = false;
            freecamCharacterToolStripMenuItem.Checked = false;


            foreach (rac1.DebugOption option in game.DebugOptions())
            {
                switch (option)
                {
                    case rac1.DebugOption.UpdateRatchet:
                        updateHeroToolStripMenuItem.Checked = true;
                        break;
                    case rac1.DebugOption.UpdateMobys:
                        updateMobysToolStripMenuItem.Checked = true;
                        break;
                    case rac1.DebugOption.UpdateParticles:
                        updateParticlesToolStripMenuItem.Checked = true;
                        break;
                    case rac1.DebugOption.NormalCamera:
                        normalCameraToolStripMenuItem.Checked = true;
                        break;
                    case rac1.DebugOption.Freecam:
                        freecamToolStripMenuItem.Checked = true;
                        break;
                    case rac1.DebugOption.FreecamCharacter:
                        freecamCharacterToolStripMenuItem.Checked = true;
                        break;
                }
            }
        }

        private void updateHeroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            item.Checked = !item.Checked;

            game.SetDebugOption(rac1.DebugOption.UpdateRatchet, item.Checked);
        }

        private void updateMobysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            item.Checked = !item.Checked;

            game.SetDebugOption(rac1.DebugOption.UpdateMobys, item.Checked);
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            item.Checked = !item.Checked;

            game.SetDebugOption(rac1.DebugOption.UpdateParticles, item.Checked);
        }

        private void normalCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            item.Checked = true;
            freecamCharacterToolStripMenuItem.Checked = false;
            freecamCharacterToolStripMenuItem.Checked = false;


            game.SetDebugOption(rac1.DebugOption.NormalCamera, item.Checked);
        }

        private void freecamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            item.Checked = true;
            normalCameraToolStripMenuItem.Checked = false;
            freecamCharacterToolStripMenuItem.Checked = false;


            game.SetDebugOption(rac1.DebugOption.Freecam, item.Checked);
        }

        private void freecamCharacterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            item.Checked = true;
            freecamCharacterToolStripMenuItem.Checked = false;
            normalCameraToolStripMenuItem.Checked = false;

            game.SetDebugOption(rac1.DebugOption.FreecamCharacter, item.Checked);
        }

        private void buttonUnlockGB_Click(object sender, EventArgs e)
        {
            game.UnlockAllGoldBolts();
        }

        private void resetAllMissionsStuffButton_Click(object sender, EventArgs e)
        {
            var zero = new byte[] { 0, 0, 0, 0 };

            game.api.WriteMemory(game.pid, 0xA0CD04, 4, zero);

            // these next 3 are floats
            game.api.WriteMemory(game.pid, 0xE5EFD0, 4, zero);
            game.api.WriteMemory(game.pid, 0xE5EFD4, 4, zero);
            game.api.WriteMemory(game.pid, 0xE5EFD8, 4, zero);

            game.api.Notify("Blarg bridge and rilgar race reset for any% all missions. Good luck!");
        }
    }
}