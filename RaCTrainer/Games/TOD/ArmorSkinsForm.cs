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
    public partial class ArmorSkinsForm : Form
    {
        tod game;
        public ArmorSkinsForm(tod game)
        {
            InitializeComponent();
            this.game = game;
        }

        private void UnlockSkinsButton(object sender, EventArgs e)
        {
            foreach (string s in checkedListBox2.CheckedItems)
            {
                game.UnlockSkins(s);
            }
        }

        private byte RadioButtonChoice()
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
            else if (radioButton4.Checked)
            {
                return 3;
            }
            else if (radioButton5.Checked)
            {
                return 4;
            }
            else
            {
                MessageBox.Show("Choose a button you beech");
                return 5;
            }
        }

        private void SwitchArmor(object sender, EventArgs e)
        {
            byte temp = RadioButtonChoice();
            if (temp == 5)
            {
                return;
            }
            game.SetArmor(temp);
        }
    }
}
