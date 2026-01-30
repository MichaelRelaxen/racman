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
    public partial class WeaponForm : Form
    {
        public tod game;
        public WeaponForm(tod game)
        {
            this.game = game;
            InitializeComponent();
        }

        private void UnlockRelockWeapon(object sender, EventArgs e)
        {
            game.UnlockWeapon(comboBox1.SelectedItem.ToString());
        }

        private void UnlockRelockUpgrades(object sender, EventArgs e)
        {
            game.WeaponUpgrades(comboBox1.SelectedItem.ToString());
        }

        private void ApplyWeaponLevelClick(object sender, EventArgs e)
        {
            game.WeaponLevel(comboBox1.SelectedItem.ToString(), Convert.ToInt32(comboBox2.SelectedItem));
        }
    }
}
