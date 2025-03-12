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

        private List<GCItem> weapons = new List<GCItem>()
        {
            //WEAPONS
            new GCItem("Lancer", 0x1481A9E),
            new GCItem("Gravity-Bomb", 0x1481AAA),
            new GCItem("Chopper", 0x1481A96),
            new GCItem("Seeker-Gun", 0x1481A98),
            new GCItem("Pulse-Rifle", 0x1481A97),
            new GCItem("Miniturret-Glove", 0x1481AA9),
            new GCItem("Blitz-Gun", 0x1481A9A),
            new GCItem("Shield-Charger", 0x1481AAD),
            new GCItem("Synthenoid", 0x1481A9F),
            new GCItem("Lava-Gun", 0x1481A9D),
            new GCItem("Bouncer", 0x1481AA5),
            new GCItem("Minirocket-Tube", 0x1481A9B),
            new GCItem("Plasma-Coil", 0x1481A9C),
            new GCItem("Hoverbomb-Gun", 0x1481A99),
            new GCItem("Spiderbot-Glove", 0x1481AA0),
            new GCItem("Sheepinator", 0x1481A90),
            new GCItem("Tesla-Claw", 0x1481A92),
            new GCItem("Bomb-Glove", 0x1481A8c),
            new GCItem("Wolloper", 0x1481AB5),
            new GCItem("Visi-bomb-Gun", 0x1481A8e),
            new GCItem("Decoy Glove", 0x1481A91),
            new GCItem("Zodiac", 0x1481AAB),
            new GCItem("RYNO-II", 0x1481AAC),
            new GCItem("Clank-Zapper", 0x1481A89),
        };

        private List<GCItem> gadgets = new List<GCItem>()
        {
            //GADGETS
            new GCItem("Swingshot", 0x1481A8d),
            new GCItem("Dynamo", 0x1481AA4),
            new GCItem("Therminator", 0x1481AA7),
            new GCItem("Tractor-Beam", 0x1481AAE),
            new GCItem("Hypnomatic", 0x1481AB7),
            new GCItem("Heli-Pack", 0x1481A82),
            new GCItem("Thruster-Pack", 0x1481A83),
            new GCItem("Gravity Boots", 0x1481A93),
            new GCItem("Grindboots", 0x1481A94),
            new GCItem("Charge Boots", 0x1481AB6),

        };

        private List<GCItem> items = new List<GCItem>()
        {
            
            //ITEMS
            new GCItem("Biker-Helmet", 0x1481AB0),
            new GCItem("Glider", 0x1481A95),
            new GCItem("Quark-Statuette(Magnetizer_pt1)", 0x1481AB1),
            new GCItem("Armor-Magnetizer", 0x1481A87),
            new GCItem("Box-Breaker", 0x1481AB2),
            new GCItem("Mapper", 0x1481A85),
            new GCItem("Electrolyzer", 0x1481AA6),
            new GCItem("Infiltrator", 0x1481AB3),
            new GCItem("HydroPack", 0x1481A84),
            new GCItem("Levitator", 0x1481A88)
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
            foreach (var it in weapons)
            {
                checkListWeapons.Items.Add(it.name, it.IsUnlocked(game));
            }

            foreach (var it in gadgets)
            {
                checkListGadgets.Items.Add(it.name, it.IsUnlocked(game));
            }

            foreach (var it in items)
            {
                checkListItems.Items.Add(it.name, it.IsUnlocked(game));
            }

        }

        private void checkListWeapons_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool enable = true;
            if (e.NewValue == CheckState.Checked) enable = true;
            if (e.NewValue == CheckState.Unchecked) enable = false;
            // WHY DOES THIS EXIST
            if (e.NewValue == CheckState.Indeterminate) return;

            var gameWeapon = weapons[e.Index];
            gameWeapon.LockOrUnlock(game, enable);

        }

        private void checkListGadgets_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool enable = true;
            if (e.NewValue == CheckState.Checked) enable = true;
            if (e.NewValue == CheckState.Unchecked) enable = false;
            if (e.NewValue == CheckState.Indeterminate) return;

            var gameGadget = gadgets[e.Index];
            gameGadget.LockOrUnlock(game, enable);

        }
        private void checkListItems_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool enable = true;
            if (e.NewValue == CheckState.Checked) enable = true;
            if (e.NewValue == CheckState.Unchecked) enable = false;
            if (e.NewValue == CheckState.Indeterminate) return;

            var gameItem = items[e.Index];
            gameItem.LockOrUnlock(game, enable);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkListWeapons.Items.Count; i++) checkListWeapons.SetItemChecked(i, true);
            foreach (var it in weapons) it.LockOrUnlock(game, true);

            for (int i = 0; i < checkListGadgets.Items.Count; i++) checkListGadgets.SetItemChecked(i, true);
            foreach (var it in gadgets) it.LockOrUnlock(game, true);

            for (int i = 0; i < checkListItems.Items.Count; i++) checkListItems.SetItemChecked(i, true);
            foreach (var it in items) it.LockOrUnlock(game, true);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkListWeapons.Items.Count; i++) checkListWeapons.SetItemChecked(i, false);

            for (int i = 0; i < checkListGadgets.Items.Count; i++) checkListGadgets.SetItemChecked(i, false);

            for (int i = 0; i < checkListItems.Items.Count; i++) checkListItems.SetItemChecked(i, false);
            foreach (var ita in allItemAddresses) new GCItem("???", ita).LockOrUnlock(game, false);
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkListWeapons.Items.Count; i++) checkListWeapons.SetItemChecked(i, true);

            for (int i = 0; i < checkListGadgets.Items.Count; i++) checkListGadgets.SetItemChecked(i, true);

            for (int i = 0; i < checkListItems.Items.Count; i++) checkListItems.SetItemChecked(i, true);
            foreach (var ita in allItemAddresses) new GCItem("???", ita).LockOrUnlock(game, true);
        }



        private void checkListWeapons_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkListItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkListGadgets_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

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
