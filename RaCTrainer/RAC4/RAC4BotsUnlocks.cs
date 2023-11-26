using racman.offsets;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace racman
{
    public partial class RAC4BotsUnlocks : Form
    {
        private rac4 game;

        private List<ACITWeapon> unlocks = new List<ACITWeapon>();

        public RAC4BotsUnlocks(rac4 game)
        {
            this.game = game;
            //unlocks = game.GetWeapons();
            InitializeComponent();

            for (int i = 0; i < unlocks.Count; i++)
            {
                String weaponName = unlocks[i].name + $" [{unlocks[i].level}]";
                botsUnlocksCheckList.Items.Add(weaponName, unlocks[i].isUnlocked);
                botsUnlocksCheckList.SetItemChecked(i, unlocks[i].isUnlocked);

                unlocks[i].levelChanged += (weapon) =>
                {
                    int index = unlocks.IndexOf(weapon);
                    botsUnlocksCheckList.Items[index] = unlocks[index].name + $" [{unlocks[index].level}]";
                };
            }

            botsUnlocksCheckList.ItemCheck += (s, e) =>
            {
                //game.SetUnlockState(unlocks[e.Index], e.NewValue == CheckState.Checked);
            };
        }

        private void buttonUnlockAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < unlocks.Count; i++)
            {
                botsUnlocksCheckList.SetItemChecked(i, true);
                //game.setUnlockState(unlocks[i], true);
            }
        }

        private void buttonRemoveAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < unlocks.Count; i++)
            {
                botsUnlocksCheckList.SetItemChecked(i, false);
                //game.setUnlockState(unlocks[i], false);
            }
        }
    }
}
