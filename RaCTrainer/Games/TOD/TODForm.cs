using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace racman.TOD
{
    public partial class TODForm : Form
    {
        static ModLoaderForm modLoaderForm;
        public InputDisplay InputDisplay;
        public tod game;

        private ToDAutosplitterClient autosplitterClient;
        private volatile bool inputSubscriptionsActive;
        public Form ConfigureCombos;

        public TODForm(tod game)
        {
            this.game = game;
            InitializeComponent();

            if (this.game.HasInputDisplay)
            {
                this.game.SetupInputDisplayMemorySubs();
                inputSubscriptionsActive = true;
            }
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

        private void levelFlagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLevelFlags form = new FormLevelFlags(game, 0x10211398, tod.LevelFlags, tod.LevelFlagPlanetOrder);
            form.Show();
        }

        private void planets_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            api.WriteMemory(pid, tod.addr.savePlanetId, new byte[] { (byte)planets_comboBox.SelectedIndex });
        }

        private void TODForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (autosplitterClient != null)
            {
                autosplitterClient.Dispose();
                autosplitterClient = null;
            }

            game.api.Disconnect();
            Application.Exit();
        }

        private void buttonStartAutosplitter_Click(object sender, EventArgs e)
        {
            /*
             * The old autosplitter asked which ASS/GASS route was being used,
             * then disconnected its memory subscriptions around autoscrollers.
             * The SPRX autosplitter now survives game exits itself, and split
             * route filtering is handled by LiveSplit, so that flow is no
             * longer needed.
             *
             * var choiceForm = new CategoryChoiceForm();
             * choiceForm.ShowDialog();
             * ...
            */
            if (game.api is Ratchetron)
            {
                try
                {
                    func.PrepareSPRX(AttachPS3Form.ip, "tod-autosplitter.sprx", 5);

                    autosplitterClient =
                        new ToDAutosplitterClient(
                            AttachPS3Form.ip,
                            game,
                            inputSubscriptionsActive,
                            InputSubscriptionStateChanged,
                            AutosplitterClient_Disconnected);

                    labelAutosplitterStatus.Text = "Autosplitter enabled!";
                    labelAutosplitterStatus.ForeColor = Color.Green;
                    labelSplitterRoute.Visible = false;
                    buttonStartAutosplitter.Text = "Autosplitter enabled";
                    buttonStartAutosplitter.Enabled = false;
                }
                catch (Exception ex)
                {
                    if (autosplitterClient != null)
                    {
                        autosplitterClient.Dispose();
                        autosplitterClient = null;
                    }

                    MessageBox.Show(
                        "Failed to start autosplitter:\n" + ex.Message,
                        "Autosplitter Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(
                    "SPRX autosplitter is not supported on RPCS3.",
                    "Autosplitter Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AutosplitterClient_Disconnected()
        {
            try
            {
                BeginInvoke(new Action(() =>
                {
                    if (IsDisposed)
                        return;

                    labelAutosplitterStatus.Text = "Autosplitter disconnected.";
                    labelAutosplitterStatus.ForeColor = Color.Red;
                    buttonStartAutosplitter.Text = "Reconnect Autosplitter";
                    buttonStartAutosplitter.Enabled = true;

                    if (autosplitterClient != null)
                    {
                        autosplitterClient.Dispose();
                        autosplitterClient = null;
                    }

                    MessageBox.Show(
                        this,
                        "The connection to the PS3 autosplitter was lost.",
                        "Autosplitter Disconnected",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }));
            }
            catch (InvalidOperationException)
            {
                // The form was disposed before the notification could run.
            }
        }

        private void InputSubscriptionStateChanged(bool active)
        {
            inputSubscriptionsActive = active;
        }

        private void DieButtonClick(object sender, EventArgs e)
        {
            game.KillYourself();
        }

        private void ChallegeModeButtonClick(object sender, EventArgs e)
        {
            game.SetChallengeMode();
        }

        private void ResetAllGoldBoltsClick(object sender, EventArgs e)
        {
            game.ResetAllGoldBolts();
        }

        private void GodRatchetClick(object sender, EventArgs e)
        {
            game.SetGodRatchet();
        }

        private void ResetAllRYNOPlans(object sender, EventArgs e)
        {
            game.ResetRYNOPlans();
        }

        private void PlayerValuesFormClick(object sender, EventArgs e)
        {
            PlayerValues form = new PlayerValues(game);
            form.Show();
        }

        private void ArmorSkinsFormClick(object sender, EventArgs e)
        {
            ArmorSkinsForm form = new ArmorSkinsForm(game);
            form.Show();
        }

        private void WeaponsFormClick(object sender, EventArgs e)
        {
            WeaponForm form = new WeaponForm(game); 
            form.Show();
        }

        private void GadgetsFormClick(object sender, EventArgs e)
        {
            GadgetForm form = new GadgetForm(game);
            form.Show();
        }

        private void ResetGroovitronStorageClick(object sender, EventArgs e)
        {
            game.ResetGoldenGrovitronStorage();
        }

        private void SavePositionClick(object sender, EventArgs e)
        {
            int temp = RadioButtonChoice();
            if (temp == -1)
                return;
            game.SavePosition(temp);
        }
        private void LoadPositionClick(object sender, EventArgs e)
        {
            int temp = RadioButtonChoice();
            if (temp == -1)
                return;
            game.LoadPosition(temp);
        }

        private int RadioButtonChoice()
        {
            if (radioButton1.Checked)
            {
                return 0;
            }
            else if (radioButton2.Checked)
            {
                return 1;
            }
            else if (radioButton3.Checked)
            {
                return 2;
            }
            else
            {
                MessageBox.Show("Choose a button you beech");
                return -1;
            }
        }

        private void InputViewerClick(object sender, EventArgs e)
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
            if (checkBox1.Checked)
                game.InputsTimer.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                game.InputsTimer.Enabled = true;
            else
                game.InputsTimer.Enabled = false;
        }

        private void configureButtonCombosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (ConfigureCombos == null)
            {
                ConfigureCombos = new ConfigureCombos();
                ConfigureCombos.FormClosed += ConfigureCombos_FormClosed;
                ConfigureCombos.Show();
                game.InputsTimer.Enabled = false;
            }
        }
    }
}
