using System;
using System.Linq;
using System.Windows.Forms;
namespace Ratchetron
{

    public partial class UnlocksWindow : Form
    {
        public UnlocksWindow()
        {
            InitializeComponent();
            itemsCheckList.ItemCheck += itemsCheckList_ItemCheck;
            gwCheckList.ItemCheck += gwCheckList_ItemCheck;


            // Unlocked Items
            string[] checkedItems = func.SplitByN(func.ReadMemory(RAC1Form.ip, RAC1Form.pid, rac1.unlock_array + 2, 34), 2).ToArray();
            for (int i = 0; i < checkedItems.Length; i++)
            {
                itemsCheckList.SetItemChecked(i, Convert.ToBoolean(int.Parse(checkedItems[i])));
            }

            // Gold Weapons
            string[] checkedGW = func.SplitByN(func.ReadMemory(RAC1Form.ip, RAC1Form.pid, rac1.gold_weapons_array + 2, 34), 2).ToArray();
            for (int i = 0; i < checkedGW.Length; i++)
            {
                gwCheckList.SetItemChecked(i, Convert.ToBoolean(int.Parse(checkedGW[i])));
            }
        }

        private void itemsCheckList_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (e.Index < 34)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    {
                        func.WriteMemory_SingleByte(RAC1Form.ip, RAC1Form.pid, rac1.unlock_array + (uint)e.Index + 2, "01");
                    }
                }
                else
                {
                    func.WriteMemory_SingleByte(RAC1Form.ip, RAC1Form.pid, rac1.unlock_array + (uint)e.Index + 2, "00");
                }
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) //check/uncheck all button, forgot to name it
        {
            if (checkBox1.Checked)
            {
                func.WriteMemory(RAC1Form.ip, RAC1Form.pid, rac1.unlock_array + 2, string.Concat(Enumerable.Repeat("01", 34)));
                for (int i = 0; i < itemsCheckList.Items.Count; i++)
                {
                    itemsCheckList.SetItemChecked(i, true);
                }
            }
            else
            {
                func.WriteMemory(RAC1Form.ip, RAC1Form.pid, rac1.unlock_array + 2, string.Concat(Enumerable.Repeat("00", 34)));
                for (int i = 0; i < itemsCheckList.Items.Count; i++)
                {
                    itemsCheckList.SetItemChecked(i, false);
                }
            }
        }

        private void UnlocksWindow_Load(object sender, EventArgs e)
        {

        }
        private void gwCheckList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index < 34)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    {
                        func.WriteMemory_SingleByte(RAC1Form.ip, RAC1Form.pid, rac1.gold_weapons_array + (uint)e.Index + 2, "01");
                    }
                }
                else
                {
                    func.WriteMemory_SingleByte(RAC1Form.ip, RAC1Form.pid, rac1.gold_weapons_array + (uint)e.Index + 2, "00");
                }
            }

        }
        private void gwCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (gwCheckBox.Checked)
            {
                func.WriteMemory(RAC1Form.ip, RAC1Form.pid, rac1.gold_weapons_array + 2, string.Concat(Enumerable.Repeat("01", 34)));
                for (int i = 0; i < gwCheckList.Items.Count; i++)
                {
                    gwCheckList.SetItemChecked(i, true);
                }
            }
            else
            {
                func.WriteMemory(RAC1Form.ip, RAC1Form.pid, rac1.gold_weapons_array + 2, string.Concat(Enumerable.Repeat("00", 34)));
                for (int i = 0; i < gwCheckList.Items.Count; i++)
                {
                    gwCheckList.SetItemChecked(i, false);
                }

            }


        }
    }
}