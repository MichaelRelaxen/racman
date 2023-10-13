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
        public rac2jp game;
        public Form InputDisplay;

        public RAC2JPForm(rac2jp game)
        {
            this.game = game;
            InitializeComponent();
            // game.SetupInputDisplayMemorySubs();
        }

        private void buttonSlots_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            api.WriteMemory(pid, rac2jp.addr.slotsManip, 60);
            api.Notify("Slots set up for skill point!");
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
    }
}
