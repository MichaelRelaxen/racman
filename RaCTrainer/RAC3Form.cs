using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace racman
{
    public partial class RAC3Form : Form
    {
        int ohkoMemSubID = -1;

        public rac3 game;
        private AutosplitterHelper autosplitterHelper;

        public static string[] saves;
        public Timer CoordsTimer = new Timer();
        public RAC3Form(rac3 game)
        {
            this.game = game;

            autosplitterHelper = new AutosplitterHelper();
            autosplitterHelper.StartAutosplitterForGame(game);

            InitializeComponent();
            positions_ComboBox.Text = "1";
            planets_comboBox.Text = "Veldin";
            textBox1.KeyDown += TextBox1_KeyDown;

            CoordsTimer.Interval = (int)16.66667;
            CoordsTimer.Tick += new EventHandler(UpdateCoordsLabel);
            game.GetPlayerCoordinates();

            if (func.api is Ratchetron)
            {
                Ratchetron api = (Ratchetron)func.api;

                game.SetupInputDisplayMemorySubs();
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

        private void OHKOCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!(func.api is Ratchetron))
            {
                MessageBox.Show("You need to use the new API to use this function");
                return;
            }

            Ratchetron api = (Ratchetron)func.api;

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
            game.planetToLoad = (uint)planets_comboBox.SelectedIndex + 1;
        }

        private void controllerCombosCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (controllerCombosCheckbox.Checked)
                game.InputsTimer.Enabled = true;
            else
                game.InputsTimer.Enabled = false;
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


            if (coordsComboBox.Checked)
                CoordsTimer.Enabled = true;
            if (!coordsComboBox.Checked)
                CoordsTimer.Enabled = false;
        }
        public void UpdateCoordsLabel(object sender, EventArgs e)
        {
            coordsLabel.Text = $"X: {game.coords[0]}\nY: {game.coords[1]}\nZ: {game.coords[2]}\n";
        }

        /* private void uploadFileButton_Click(object sender, EventArgs e)
        {
            byte[] patchBytes;

            patchBytes = File.ReadAllBytes($"{ Directory.GetCurrentDirectory()}\\saves\\{AttachPS3Form.game}\\{savefileHelperComboBox.Text}");

            int bytesWritten = 0;
            byte[] bytesToWrite = new byte[] { };
            while (bytesWritten != patchBytes.Length)
            {
                bytesToWrite = patchBytes.Skip(bytesWritten).Take(1024).ToArray();

                game.api.WriteMemory(AttachPS3Form.pid, 0x1100000 + (uint)bytesWritten, (uint)bytesToWrite.Length, bytesToWrite);

                bytesWritten += bytesToWrite.Length;

                Console.WriteLine("Bytes written:" + bytesWritten);
            }
        } // This didn't really work, FTP upload is gonna be the way but I cant be fucked rn 
        */
    }
}