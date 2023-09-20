using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace racman
{
    public partial class RC2Unlocks : Form
    {
        public rac2 game;

        private List<GCItem> items = new List<GCItem>()
        {
            new GCItem("Decoy Glove", 0x1481A91),
            new GCItem("Heli-Pack", 0x1481A82),
            new GCItem("Thruster-Pack", 0x1481A83),
            new GCItem("Levitator", 0x1481A88),
            new GCItem("Swingshot", 0x1481A8d),
            new GCItem("Gravity Boots", 0x1481A93),
            new GCItem("Grindboots", 0x1481A94),
            new GCItem("Dynamo", 0x1481AA4),
            new GCItem("Electrolyzer", 0x1481AA6),
            new GCItem("Infiltrator", 0x1481AB3),
            new GCItem("Charge Boots", 0x1481AB6),
        };
        private List<uint> allItemAddresses = new List<uint>()
        {
            0x1481A82, 0x1481A83, 0x1481A84, 0x1481A85, 0x1481A88, 0x1481A89, 0x1481A8c, 0x1481A8d,
            0x1481A8e, 0x1481A90, 0x1481A91, 0x1481A92, 0x1481A93, 0x1481A94, 0x1481A95, 0x1481A96,
            0x1481A97, 0x1481A98, 0x1481A99, 0x1481A9A, 0x1481A9B, 0x1481A9C, 0x1481A9D, 0x1481A9E, 
            0x1481A9F, 0x1481AA0, 0x1481AA4, 0x1481AA5, 0x1481AA6, 0x1481AA7, 0x1481AA9, 0x1481AAB, 
            0x1481AAC, 0x1481AAD, 0x1481AAE, 0x1481AB0, 0x1481AB1, 0x1481AB2, 0x1481AB3, 0x1481AB5,
            0x1481AB6, 0x1481AB7
        };

        public RC2Unlocks(rac2 game)
        {
            InitializeComponent();
            this.game = game;
        }

        private void RC2Unlocks_Load(object sender, EventArgs e)
        {
            foreach (var it in items)
            {
                checklistItems.Items.Add(it.name, it.IsUnlocked(game));
            }
        }

        private void checklistItems_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool enable = true;
            if (e.NewValue == CheckState.Checked) enable = true;
            if (e.NewValue == CheckState.Unchecked) enable = false;
            // WHY DOES THIS EXIST
            if (e.NewValue == CheckState.Indeterminate) return;

            var gameItem = items[e.Index];
            gameItem.LockOrUnlock(game, enable);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checklistItems.Items.Count; i++) checklistItems.SetItemChecked(i, true);
            foreach (var it in items) it.LockOrUnlock(game, true);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checklistItems.Items.Count; i++) checklistItems.SetItemChecked(i, false);
            foreach (var ita in allItemAddresses) new GCItem("???", ita).LockOrUnlock(game, false);
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checklistItems.Items.Count; i++) checklistItems.SetItemChecked(i, true);
            foreach (var ita in allItemAddresses) new GCItem("???", ita).LockOrUnlock(game, true);
        }
    }

    // https://docs.google.com/spreadsheets/d/1rlQEmaPpJDOTZ4yImP3xkW5FbOTNGv8EwhIVreArLQs/
    public class GCItem
    {
        public string name;
        public uint unlock;

        public GCItem(string name, uint unlock)
        {
            this.name = name;
            this.unlock = unlock;
        }

        public void LockOrUnlock(rac2 game, bool enable) =>
              game.api.WriteMemory(game.pid, unlock, new byte[] { Convert.ToByte(enable) });

        public bool IsUnlocked(rac2 game)
        {
            var res = game.api.ReadMemory(game.pid, unlock, 1);
            return res[0] == 1;
        }
    }
}
