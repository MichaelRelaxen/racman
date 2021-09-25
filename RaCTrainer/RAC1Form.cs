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

        private AutosplitterHelper autosplitterHelper;

        public rac1 game;

        public RAC1Form(rac1 game)
        {
            this.game = game;

            autosplitterHelper = new AutosplitterHelper();
            autosplitterHelper.StartAutosplitterForGame(game);

            InitializeComponent();
            positions_comboBox.Text = "1";
            bolts_textBox.KeyDown += bolts_TextBox_KeyDown;

            goodiesCheck.Checked = game.GoodiesMenuEnabled();

            game.SetupMemorySubs();
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
            game.ToggleFastLoad(FastLoadToggle.Checked);
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
    }
}