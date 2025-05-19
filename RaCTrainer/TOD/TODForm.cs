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
        public TODForm(tod game)
        {
            this.game = game;
            InitializeComponent();

            if (game.api is Ratchetron r)
            {
                // r.setDisconnectCallback(() =>
                // {
                //     game.api.Disconnect();
                // });

                r.setReconnectCallback(() => 
                {
                    // autosplitter?.Stop();
                    // game.api.Disconnect();
                    // game.api.Connect();

                    // This is obviously hacky, but seems to get things to work
                    // var subId = r.SubMemory(r.getCurrentPID(), tod.addr.savePlanetId, 1, (_) => { });
                    //r.ReleaseSubID(subId);

                    System.Threading.Thread.Sleep(8000);
                    autosplitter.Reconnect();

                });
            }


            AutosplitterCheckbox.Checked = true;
        }

        AutosplitterHelper autosplitter;
        static ModLoaderForm modLoaderForm;
        public tod game;

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

        private void planets_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            api.WriteMemory(pid, tod.addr.savePlanetId, new byte[] { (byte)planets_comboBox.SelectedIndex });
        }

        private void AutosplitterCheckbox_CheckedChanged(object sender, EventArgs e)
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

        private void TODForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            autosplitter?.Stop();
            autosplitter = null;


            game.api.Disconnect();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            api.SubMemory(pid, tod.addr.boltCount, 1, (a) => 
            {
                Action write = delegate { label2.Text = a[0].ToString(); };
                label2.Invoke(write);
            });
        }
    }
}
