using System;
using System.Windows.Forms;

namespace racman
{
    public partial class ACITForm : Form
    {
        private AutosplitterHelper autosplitterHelper;
        public acit game;
        public Form InputDisplay;

        public ACITForm(acit game)
        {
            InitializeComponent();


            this.game = game;

            Text = $"A Crack in Time ({game.api.getGameTitleID()})";

            bolts_textBox.KeyDown += bolts_TextBox_KeyDown;

            if (this.game.IsAutosplitterSupported)
            {
                // This raises an event that creates the autosplitterHelper object.
                // creating it manually is not necessary and also can cause a crash
                AutosplitterCheckbox.Checked = true;
            }
            else
            {
                AutosplitterCheckbox.Enabled = false;
            }

            if (this.game.HasInputDisplay)
            {
                this.game.SetupInputDisplayMemorySubs();
            }
            else
            {
                inputDisplayButton.Enabled = false;
            }
            
            if (this.game.IsSelfKillSupported)
            {
                killYourselfButton.Enabled = true;
            }
            else
            {
                killYourselfButton.Enabled = false;
            }

            if (this.game.canRemoveCutscenes)
            {
                disableCutscenesCheckBox.Enabled = true;
            }
            else
            {
                disableCutscenesCheckBox.Enabled = false;
            }
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, EventArgs e)
        {

        }

        static ModLoaderForm modLoaderForm;

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

        private void ACITForm_Load(object sender, EventArgs e)
        {

        }

        private void ACITForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (autosplitterHelper != null && autosplitterHelper.IsRunning) autosplitterHelper.Stop();
            game.api.Disconnect();
            Application.Exit();
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
                this.game.IsAutosplitterEnabled = false;
            }
            else
            {
                // Enable auotpslitter
                Console.WriteLine("Autosplitter starting!");
                this.game.IsAutosplitterEnabled = true;
                autosplitterHelper = new AutosplitterHelper();
                autosplitterHelper.StartAutosplitterForGame(this.game);
            }
        }

        private void disableCutscenesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            game.EnableCutscenes(!disableCutscenesCheckBox.Checked);
        }

        private void killyourself_Click(object sender, EventArgs e)
        {
            KillYourself();
        }

        private void KillYourself()
        {
            game.KillYourself();
        }

        private void unlocksWindowButton_Click(object sender, EventArgs e)
        {
            ACITUnlocks unlocks = new ACITUnlocks(game);
            unlocks.Show();
        }
    }
}
