using racman.offsets;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace racman
{
    public partial class ACITUnlocks : Form
    {
        private acit game;

        private List<ACITWeapon> weapons = new List<ACITWeapon>();

        public ACITUnlocks(acit game)
        {
            this.game = game;
            weapons = game.GetWeapons();
            InitializeComponent();

            for (int i = 0; i < weapons.Count; i++)
            {
                weaponsCheckList.Items.Add(weapons[i].name, weapons[i].isUnlocked);
                weaponsCheckList.SetItemChecked(i, weapons[i].isUnlocked);
            }

            weaponsCheckList.ItemCheck += (s, e) =>
            {
                game.setUnlockState(weapons[e.Index], e.NewValue == CheckState.Checked);
            };

        }

        private void buttonUnlockAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                weaponsCheckList.SetItemChecked(i, true);
                game.setUnlockState(weapons[i], true);
            }
        }

        private void buttonRemoveAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                weaponsCheckList.SetItemChecked(i, false);
                game.setUnlockState(weapons[i], false);
            }
        }
    }
}
