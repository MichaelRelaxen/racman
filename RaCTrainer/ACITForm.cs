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

            autosplitterHelper = new AutosplitterHelper();
            autosplitterHelper.StartAutosplitterForGame(game);

            InitializeComponent();

            game.SetupInputDisplayMemorySubs();

            AutosplitterCheckbox.Checked = true;
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, EventArgs e)
        {

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

        private void ACITForm_Load(object sender, EventArgs e)
        {

        }

        private void ACITForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            Application.Exit();
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
    }
}
