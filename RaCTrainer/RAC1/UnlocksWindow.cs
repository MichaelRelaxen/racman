using System;
using System.Linq;
using System.Windows.Forms;
namespace racman
{

    public partial class UnlocksWindow : Form
    {
        rac1 game;

        (string, int)[] unlocks;

        public UnlocksWindow(rac1 game)
        {
            this.game = game;

            InitializeComponent();
            itemsCheckList.ItemCheck += itemsCheckList_ItemCheck;
            gwCheckList.ItemCheck += gwCheckList_ItemCheck;

            itemsCheckList.Items.Clear();
            gwCheckList.Items.Clear();

            unlocks = game.GetUnlocks();

            // Unlocked Items
            foreach((string, int) unlock in unlocks)
            {
                itemsCheckList.Items.Add(unlock.Item1, game.HasUnlock(unlock));
                gwCheckList.Items.Add(unlock.Item1, game.HasUnlock(unlock, true));
            }
        }

        private void itemsCheckList_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (e.Index < 34)
            {
                game.SetUnlock(unlocks[e.Index], e.NewValue == CheckState.Checked);
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) //check/uncheck all button, forgot to name it
        {
            var i = 0;
            foreach(var unlock in unlocks)
            {
                itemsCheckList.SetItemChecked(i, checkBox1.Checked);
                game.SetUnlock(unlock, checkBox1.Checked);
                i++;
            }
        }

        private void UnlocksWindow_Load(object sender, EventArgs e)
        {

        }
        private void gwCheckList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index < 34)
            {
                game.SetUnlock(unlocks[e.Index], e.NewValue == CheckState.Checked, true);
            }

        }
        private void gwCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var i = 0;
            foreach (var unlock in unlocks)
            {
                gwCheckList.SetItemChecked(i, gwCheckBox.Checked);
                game.SetUnlock(unlock, gwCheckBox.Checked, true);
                i++;
            }
        }
    }
}