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
    public partial class GadgetForm : Form
    {

        public tod game;

        public GadgetForm(tod game)
        {
            this.game = game;
            InitializeComponent();
        }

        private void AddRemoveGadget(object sender, EventArgs e)
        {
            foreach (string s in checkedListBox1.Items) 
            {
                if (checkedListBox1.CheckedItems.Contains(s))
                    game.SetGadgetAndInventoryItems(s, 1);
            }
        }
    }
}
