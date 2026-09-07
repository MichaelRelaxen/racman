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
    public partial class RAC2JPForm : Form
    {
        AutosplitterHelper autosplitter;
        public rac2jp game;
        public Form InputDisplay;

        public RAC2JPForm(rac2jp game)
        {
            this.game = game;
            InitializeComponent();
            game.SetupInputDisplayMemorySubs();
        }

        private void RAC2JPForm_Load(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => {
                planets_comboBox.SelectedIndex = (int)game.planetIndex;
            }));
        }

        private void buttonSlots_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            // Defaults to 370, decrease by 40.
            api.WriteMemory(pid, rac2jp.addr.pBolts, 330);
            // Defaults to 5, increase by 40.
            api.WriteMemory(pid, rac2jp.addr.pJackpot, 45);
            // For safety, let the game know the manip is done already:
            api.WriteMemory(pid, rac2jp.addr.slotsHits, 40);
            api.Notify("Slots manipulated for skill point! Good luck.");
        }

        private void textBoxRari_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var api = game.api;
                    var pid = api.getCurrentPID();

                    api.WriteMemory(pid, rac2jp.addr.currentRaritanium, uint.Parse(textBoxRari.Text));
                }
                catch
                {
                    MessageBox.Show("Please enter a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxBolts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    game.SetBoltCount(uint.Parse(textBoxBolts.Text));
                }
                catch
                {
                    MessageBox.Show("Please enter a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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

        private void menusToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void switchGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.AttachPS3Form.Show();
            Close();
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

        private void inputDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InputDisplay == null)
            {
                InputDisplay = new InputDisplay();
                InputDisplay.FormClosed += (s, _) => InputDisplay = null;
                InputDisplay.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            inputDisplayToolStripMenuItem_Click(sender, e);
        }

        private void planets_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            game.planetToLoad = (uint)planets_comboBox.SelectedIndex;
        }

        private void loadPlanetButton_Click(object sender, EventArgs e)
        {
            game.LoadPlanet();
        }
    }
}
