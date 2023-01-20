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
    public partial class ACITForm : Form
    {
        private AutosplitterHelper autosplitterHelper;

        public acit game;
        public ACITForm(acit game)
        {
            this.game = game;

            autosplitterHelper = new AutosplitterHelper();
            autosplitterHelper.StartAutosplitterForGame(game);

            InitializeComponent();
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
    }
}
