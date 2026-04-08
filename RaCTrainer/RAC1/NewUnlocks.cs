using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace racman.RAC1
{
    public partial class NewUnlocks : Form
    {
        public rac1 game;
        public IPS3API api;
        public int pid = AttachPS3Form.pid;
        public NewUnlocks(rac1 game)
        {
            this.game = game;
            InitializeComponent();
            AddItems();
        }
        public void AddItems()
        {
            weaponsListBox.ItemCheck -= new ItemCheckEventHandler(listBoxItemCheck);
            itemsListBox.ItemCheck -= new ItemCheckEventHandler(listBoxItemCheck);
            gadgetsItemsListBox.ItemCheck -= new ItemCheckEventHandler(listBoxItemCheck);
            foreach (var it in items)
            {
                if (it.Type == ItemType.Weapon)
                {
                    weaponsListBox.Items.Add(it.Name, it.IsUnlocked());
                }
                else if (it.Type == ItemType.Gadget)
                {
                    gadgetsItemsListBox.Items.Add(it.Name, it.IsUnlocked());
                }
                else if (it.Type == ItemType.Item)
                {
                    itemsListBox.Items.Add(it.Name, it.IsUnlocked());
                }
            }
            weaponsListBox.ItemCheck += new ItemCheckEventHandler(listBoxItemCheck);
            itemsListBox.ItemCheck += new ItemCheckEventHandler(listBoxItemCheck);
            gadgetsItemsListBox.ItemCheck += new ItemCheckEventHandler(listBoxItemCheck);
        }


        private List<RaC1Item> items = new List<RaC1Item>
            {
            // Weapons
            // name, idx, unlock, ammo, gold, max ammo, type
            new RaC1Item("Bomb Glove", 10, 0x96c14a, 0x96c0cc, 0x969cb2, 40, ItemType.Weapon),
            new RaC1Item("Pyrocitor", 16, 0x96c150, 0x96c0e4, 0x969cb8, 240, ItemType.Weapon),
            new RaC1Item("Blaster", 15, 0x96c14f, 0x96c0e0, 0x969cb7, 200, ItemType.Weapon),
            new RaC1Item("Glove Of Doom", 20, 0x96c154, 0x96c0f4, 0x969cbc, 10, ItemType.Weapon),
            new RaC1Item("Mine Glove", 17, 0x96c151, 0x96c0e8, 0x969cb9, 50, ItemType.Weapon),
            new RaC1Item("Taunter", 14, 0x96c14e, 0x96c0dc, 0x969cb6, 0, ItemType.Weapon),
            new RaC1Item("Suck Cannon", 9, 0x96c149, 0x96c0c8, 0x969cb1, 0, ItemType.Weapon),
            new RaC1Item("Devastator", 11, 0x96c14b, 0x96c0d0, 0x969cb3, 20, ItemType.Weapon),
            new RaC1Item("Walloper", 18, 0x96c152, 0x96c0ec, 0x969cba, 0, ItemType.Weapon),
            new RaC1Item("Visibomb", 13, 0x96c14d, 0x96c0d8, 0x969cb5, 20, ItemType.Weapon),
            new RaC1Item("Decoy Glove", 25, 0x96c159, 0x96c108, 0x969cc1, 20, ItemType.Weapon),
            new RaC1Item("Drone Device", 24, 0x96c158, 0x96c104, 0x969cc0, 10, ItemType.Weapon),
            new RaC1Item("Tesla Claw", 19, 0x96c153, 0x96c0f0, 0x969cbb, 240, ItemType.Weapon),
            new RaC1Item("Morph-O-Ray", 21, 0x96c155, 0x96c0f8, 0x969cbd, 0, ItemType.Weapon),
            new RaC1Item("RYNO", 23, 0x96c157, 0x96c100, 0x969cbf, 50, ItemType.Weapon),
            new RaC1Item("Wrench", 8, 0x96c148, 0x96c0c4, 0x969cb0, 0, ItemType.Weapon),
    
            // Gadgets
            new RaC1Item("Heli-Pack", 2, 0x96c142, 0x96c0ac, 0x969caa, 0, ItemType.Gadget),
            new RaC1Item("Thruster-Pack", 3, 0x96c143, 0x96c0b0, 0x969cab, 0, ItemType.Gadget),
            new RaC1Item("Hydro-Pack", 4, 0x96c144, 0x96c0b4, 0x969cac, 0, ItemType.Gadget),
            new RaC1Item("PDA", 32, 0x96c160, 0x96c124, 0x969cc8, 0, ItemType.Gadget),
            new RaC1Item("Swingshot", 12, 0x96c14c, 0x96c0d4, 0x969cb4, 0, ItemType.Gadget),
            new RaC1Item("O2 Mask", 6, 0x96c146, 0x96c0bc, 0x969cae, 0, ItemType.Gadget),
            new RaC1Item("Pilots Helmet", 7, 0x96c147, 0x96c0c0, 0x969caf, 0, ItemType.Gadget),
            new RaC1Item("Magneboots", 28, 0x96c15c, 0x96c114, 0x969cc4, 0, ItemType.Gadget),
            new RaC1Item("Grindboots", 29, 0x96c15d, 0x96c118, 0x969cc5, 0, ItemType.Gadget),
            new RaC1Item("Trespasser", 26, 0x96c15a, 0x96c10c, 0x969cc2, 0, ItemType.Gadget),
            new RaC1Item("Hydrodisplacer", 22, 0x96c156, 0x96c0fc, 0x969cbe, 0, ItemType.Gadget),
            new RaC1Item("Sonic Summoner", 5, 0x96c145, 0x96c0b8, 0x969cad, 0, ItemType.Gadget),
            new RaC1Item("Metal Detector", 27, 0x96c15b, 0x96c110, 0x969cc3, 0, ItemType.Gadget),
            new RaC1Item("Hologuise", 31, 0x96c15f, 0x96c120, 0x969cc7, 0, ItemType.Gadget),
    
            // Items
            new RaC1Item("Map-O-Matic", 33, 0x96c161, 0x96c128, 0x969cc9, 0, ItemType.Item),
            new RaC1Item("Bolt Grabber", 34, 0x96c162, 0x96c12c, 0x969cca, 0, ItemType.Item),
            new RaC1Item("Persuader", 35, 0x96c163, 0x96c130, 0x969ccb, 0, ItemType.Item),
            new RaC1Item("Hoverboard", 30, 0x96c15e, 0x96c11c, 0x969cc6, 0, ItemType.Item),
            new RaC1Item("Zoomerator", -1, 0x96bff0, 0, 0, 0, ItemType.Item),
            new RaC1Item("Raritanium", -1, 0x96bff1, 0, 0, 0, ItemType.Item),
            new RaC1Item("Codebot", -1, 0x96bff2, 0, 0, 0, ItemType.Item),
            new RaC1Item("Premium Nanotech", -1, 0x96bff4, 0, 0, 0, ItemType.Item),
            new RaC1Item("Ultra Nanotech", -1, 0x96bff5, 0, 0, 0, ItemType.Item),
        };
        private RaC1Item itemByName(string itemName) {
            return items.Find(obj => obj.Name == itemName);
        }

        private void listBoxItemCheck(object sender, ItemCheckEventArgs e)
        {
            // copied from uya unlocks ty robo
            bool enable = true;
            if (e.NewValue == CheckState.Checked) enable = true;
            if (e.NewValue == CheckState.Unchecked) enable = false;
            if (e.NewValue == CheckState.Indeterminate) return;
            CheckedListBox senderBox = (CheckedListBox)sender;
            string itemName = senderBox.Items[e.Index].ToString();

            var gameItem = itemByName(itemName);
            gameItem.Unlock(enable);
        }

        private void LockUnlockAllClick(object sender, EventArgs e)
        {
            bool state = true;

            if (sender.ToString().Contains("Remove"))
                state = false;
            if (sender.ToString().Contains("Unlock"))
                state = true;


            for (int i = 0; i < weaponsListBox.Items.Count; i++)
                weaponsListBox.SetItemChecked(i, state);
            for (int i = 0; i < gadgetsItemsListBox.Items.Count; i++)
                gadgetsItemsListBox.SetItemChecked(i, state);
            for (int i = 0; i < itemsListBox.Items.Count; i++)
                itemsListBox.SetItemChecked(i, state);

            foreach (var it in items) it.Unlock(state);
        }
        private void GoldAllClick(object sender, EventArgs e)
        {
            bool state = true;

            if (sender.ToString().Contains("No"))
                state = false;
            if (sender.ToString().Contains("All"))
                state = true;

            foreach (var it in items)
                if(it.Type == ItemType.Weapon) it.Gold(state);
        }
        private void AmmoAllClick(object sender, EventArgs e)
        {
            foreach (var it in items)
                if(it.Type == ItemType.Weapon) it.SetAmmo((uint)it.MaxAmmo);
        }

        private void weaponsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox senderBox = (CheckedListBox)sender;
            var gameItem = itemByName(senderBox.SelectedItem.ToString());

            // Disable event handler while updating checkbox
            goldStatusCheckBox.CheckedChanged -= goldStatusCheckBox_CheckedChanged;
            goldStatusCheckBox.Checked = gameItem.IsGold();
            goldStatusCheckBox.CheckedChanged += goldStatusCheckBox_CheckedChanged;

            ammoTextBox.Text = Convert.ToString(gameItem.GetAmmo());
        }

        private void goldStatusCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try {
                var gameItem = itemByName(weaponsListBox.SelectedItem.ToString());
                gameItem.Gold(goldStatusCheckBox.Checked);
            }
            catch { }
        }

        private void ammoTextBox_TextChanged(object sender, EventArgs e)
        {
            try { 
                var gameItem = itemByName(weaponsListBox.SelectedItem.ToString());
                gameItem.SetAmmo(Convert.ToUInt32(ammoTextBox.Text)); 
            }
            catch { }
        }
    }
    public class RaC1Item
    {
        public IPS3API api = func.api;
        public int pid = AttachPS3Form.pid;

        public string Name { get; set; }
        public int Index { get; set; }
        public uint UnlockOffset { get; set; }
        public uint AmmoOffset { get; set; }
        public uint GoldOffset { get; set; }
        public int MaxAmmo { get; set; }
        public ItemType Type { get; set; }

        public RaC1Item(string name, int index, uint unlockAddress, uint ammoAddress,
                        uint goldAddress, int maxAmmo, ItemType type)
        {
            Name = name;
            Index = index;
            UnlockOffset = unlockAddress;
            AmmoOffset = ammoAddress + 8 ; // accidentally messed up the offset oops oh well
            GoldOffset = goldAddress; 
            MaxAmmo = maxAmmo;
            Type = type;
        }

        public bool IsWeapon() => Type == ItemType.Weapon;
        public bool IsGadget() => Type == ItemType.Gadget;
        public bool IsItem() => Type == ItemType.Item;
        public bool HasAmmo() => MaxAmmo > 0;

        public void Unlock(bool state)
        {
            api.WriteMemory(pid, UnlockOffset, new byte[1] { Convert.ToByte(state) });

            if((Type == ItemType.Weapon || MaxAmmo != 0) && state) // give max ammo as well
                api.WriteMemory(pid, AmmoOffset, (uint)MaxAmmo);
        }
        public void Gold(bool state)
        {
            api.WriteMemory(pid, GoldOffset, new byte[1] { Convert.ToByte(state) });
        }
        public void SetAmmo(uint amount)
        {
            api.WriteMemory(pid, AmmoOffset, amount);
        }
        public bool IsUnlocked()
        {
            var res = api.ReadMemory(pid, UnlockOffset, 1);
            return res[0] == 1;
        }
        public bool IsGold()
        {
            var res = api.ReadMemory(pid, GoldOffset, 1);
            return res[0] == 1;
        }
        public uint GetAmmo()
        {
            return api.ReadMemory(pid, AmmoOffset);
        }
    }

    public enum ItemType
    {
        Weapon,
        Gadget,
        Item
    }
}
