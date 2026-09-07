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
                String weaponName = weapons[i].name + $" [{weapons[i].level}]";
                weaponsCheckList.Items.Add(weaponName, weapons[i].IsUnlocked);
                weaponsCheckList.SetItemChecked(i, weapons[i].IsUnlocked);

                weapons[i].levelChanged += (weapon) =>
                {
                    int index = weapons.IndexOf(weapon);
                    weaponsCheckList.Items[index] = weapons[index].name + $" [{weapons[index].level}]";
                };
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

        private void btnLevelCurrentTo1_Click(object sender, EventArgs e)
        {
            uint selectetWeapon = (uint)weaponsCheckList.SelectedIndex;

            if (selectetWeapon > ACITWeaponFactory.weaponCount - 1)
            {
                return;
            }

            game.setWeaponLevel(weapons[(int)selectetWeapon], 1);
        }

        private void btnLevelCurrentTo5_Click(object sender, EventArgs e)
        {
            uint selectetWeapon = (uint)weaponsCheckList.SelectedIndex;

            if (selectetWeapon > ACITWeaponFactory.weaponCount - 1)
            {
                return;
            }

            game.setWeaponLevel(weapons[(int)selectetWeapon], 5);
        }

        private void btnLevelCurrentTo10_Click(object sender, EventArgs e)
        {
            uint selectetWeapon = (uint)weaponsCheckList.SelectedIndex;

            if (selectetWeapon > ACITWeaponFactory.weaponCount - 1)
            {
                return;
            }

            game.setWeaponLevel(weapons[(int)selectetWeapon], 10);
        }

        private void btnAllToLevel1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                game.setWeaponLevel(weapons[i], 1);
            }
        }

        private void btnAllToLevel5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                game.setWeaponLevel(weapons[i], 5);
            }
        }

        private void btnAllToLevel10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                game.setWeaponLevel(weapons[i], 10);
            }
        }
    }
}
