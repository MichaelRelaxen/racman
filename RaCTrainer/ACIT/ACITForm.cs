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
            this.game = game;

            InitializeComponent();
            bolts_textBox.KeyDown += bolts_TextBox_KeyDown;

            if (this.game.IsAutosplitterSupported)
            {
                autosplitterHelper = new AutosplitterHelper();
                autosplitterHelper.StartAutosplitterForGame(game);
                AutosplitterCheckbox.Checked = true;
            }
            else
            {
                AutosplitterCheckbox.Hide();
            }

            if (this.game.HasInputDisplay)
            {
                this.game.SetupInputDisplayMemorySubs();
            }
            else
            {
                inputdisplay.Enabled = false;
                inputdisplay.Hide();
            }
            
            if (this.game.IsSelfKillSupported)
            {
                killyourself.Enabled = true;
            }
            else
            {
                killyourself.Enabled = false;
                killyourself.Hide();
            }

            if (this.game.canRemoveCutscenes)
            {
                disableCutscenesCheckBox.Enabled = true;
            }
            else
            {
                disableCutscenesCheckBox.Enabled = false;
                disableCutscenesCheckBox.Hide();
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
            }
            else
            {
                // Enable auotpslitter
                Console.WriteLine("Autosplitter starting!");
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
