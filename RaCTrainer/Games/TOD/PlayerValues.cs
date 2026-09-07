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
    public partial class PlayerValues : Form
    {

        public tod game;

        public PlayerValues(tod game)
        {
            this.game = game;
            InitializeComponent();
        }

        private void ChangePlayerValueClick(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out _))
            {
                MessageBox.Show("Input a number you beech!!!");
                return;
            }
            ChangePlayerValue(comboBox1.SelectedItem.ToString(), Convert.ToUInt32(textBox1.Text));
        }

        private void ChangePlayerValue(string option, uint value)
        {
            game.PlayerValues(option, value);
        }
    }
}
