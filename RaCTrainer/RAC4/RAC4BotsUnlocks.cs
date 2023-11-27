using racman.offsets.RAC4;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace racman
{
    public partial class RAC4BotsUnlocks : Form
    {
        private rac4 game;

        private List<BotsUnlocks> unlocks;

        public RAC4BotsUnlocks(rac4 game)
        {
            this.game = game;
            unlocks = game.GetBotsUnlocks();
            InitializeComponent();

            for (int i = 0; i < unlocks.Count; i++)
            {
                botsUnlocksCheckList.Items.Add(unlocks[i].name, unlocks[i].IsUnlocked);
                botsUnlocksCheckList.SetItemChecked(i, unlocks[i].IsUnlocked);
            }

            botsUnlocksCheckList.ItemCheck += (s, e) =>
            {
                game.SetUnlockState(unlocks[e.Index], e.NewValue == CheckState.Checked);
            };
        }

        private void buttonUnlockAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < unlocks.Count; i++)
            {
                botsUnlocksCheckList.SetItemChecked(i, true);
                game.SetUnlockState(unlocks[i], true);
            }
        }

        private void buttonRemoveAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < unlocks.Count; i++)
            {
                botsUnlocksCheckList.SetItemChecked(i, false);
                game.SetUnlockState(unlocks[i], false);
            }
        }
    }
}
