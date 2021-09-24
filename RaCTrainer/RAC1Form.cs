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

        public rac1 game;

        public RAC1Form(rac1 game)
        {
            this.game = game;

            InitializeComponent();
            positions_comboBox.Text = "1";
            bolts_textBox.KeyDown += bolts_TextBox_KeyDown;

            goodiesCheck.Checked = Convert.ToBoolean(int.Parse(func.ReadMemory(ip, pid, rac1.goodies_menu, 1)));

            Ratchetron api = (Ratchetron)func.api;



            game.SetupMemorySubs();
        }

        private void bolts_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    game.SetBoltCount(bolts_textBox.Text);
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
            func.WriteMemory(ip, pid, 0x9645C4, "0000001A0000000400000002");
            ForceLoadTimer.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Autosplitter.InitializeAutosplitter();

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
            func.api.Disconnect();
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac1.player_health, "00000003");
        }
        private void infHealth_Checkbox_Changed(object sender, EventArgs e)
        {
            if (infHealth.Checked)
                func.WriteMemory(ip, pid, 0x7F558, "30640000");
            else
                func.WriteMemory(ip, pid, 0x7F558, "30649CE0");
        }

        private void unlocksWindowButton_Click(object sender, EventArgs e)
        {
            if (UnlocksWindow == null)
            {
                UnlocksWindow = new UnlocksWindow();
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
            if (!(func.api is Ratchetron))
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
            if (goodiesCheck.Checked)
            {
                func.WriteMemory(ip, pid, rac1.goodies_menu, "01");
            }
            else
            {
                func.WriteMemory(ip, pid, rac1.goodies_menu, "00");
            }
        }

        private void ghostCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (func.api is Ratchetron)
            {
                Ratchetron api = (Ratchetron)func.api;
                if (ghostCheckbox.Checked)
                {
                    api.FreezeMemory(pid, rac1.ghost_timer, 10);
                }
                else
                {
                    api.ReleaseSubID(api.MemSubIDForAddress(rac1.ghost_timer));
                }
            }
        }

        private void drekskip_Click(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac1.drek_skip, "01");
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
            if (FastLoadToggle.Checked)
                game.ToggleFastLoad(true);
            if (!FastLoadToggle.Checked)
                game.ToggleFastLoad(false);
        }

        private void FreezeAmmoCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (func.api is Ratchetron)
            {
                Ratchetron api = (Ratchetron)func.api;

                if(FreezeAmmoCheckbox.Checked)
                {
                    func.WriteMemory(ip, pid, 0xAA2DC, "60000000");
                }
                else
                {
                    func.WriteMemory(ip, pid, 0xAA2DC, "7D05392E");
                }

            }
        }

        private void positions_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            game.selectedPositionIndex = positions_comboBox.SelectedIndex;
        }

        private void planets_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            game.planetToLoad = (uint)planets_comboBox.SelectedIndex;
        }
    }
}