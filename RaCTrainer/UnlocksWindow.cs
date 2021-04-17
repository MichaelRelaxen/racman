using System;
using System.Linq;
using System.Windows.Forms;
namespace racman
{

    public partial class UnlocksWindow : Form
    {
        public UnlocksWindow()
        {
            InitializeComponent();
            itemsCheckList.ItemCheck += itemsCheckList_ItemCheck;

            string[] checkedItems = func.SplitByN(func.ReadMemory(RAC1Form.ip, RAC1Form.pid, rac1.UnlockTable + 2, 34), 2).ToArray();
            for (int i = 0; i < checkedItems.Length; i++)
            {
                itemsCheckList.SetItemChecked(i, Convert.ToBoolean(int.Parse(checkedItems[i])));
            }
        }

        private void itemsCheckList_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (e.Index < 34)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    {
                        func.WriteMemory_SingleByte(RAC1Form.ip, RAC1Form.pid, rac1.UnlockTable + (uint)e.Index + 2, "01");
                    }
                }
                else
                {
                    func.WriteMemory_SingleByte(RAC1Form.ip, RAC1Form.pid, rac1.UnlockTable + (uint)e.Index + 2, "00");
                }
            }


            else
            {
                if (e.NewValue == CheckState.Checked)
                {
                    {
                        func.WriteMemory(RAC1Form.ip, RAC1Form.pid, rac1.UnlockTable + (uint)e.Index + 2, "FFFFFFFF");
                    }
                }

                else
                {
                    func.WriteMemory(RAC1Form.ip, RAC1Form.pid, rac1.UnlockTable + (uint)e.Index + 2, "00000000");
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void itemsCheckList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UnlocksWindow_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //check/uncheck all button, forgot to name it
        {
            if (checkBox1.Checked)
            {
                func.WriteMemory(RAC1Form.ip, RAC1Form.pid, rac1.UnlockTable + 2, string.Concat(Enumerable.Repeat("01", 34)));
                for (int i = 0; i < itemsCheckList.Items.Count; i++)
                {
                    itemsCheckList.SetItemChecked(i, true);
                }
            }
            else
            {
                func.WriteMemory(RAC1Form.ip, RAC1Form.pid, rac1.UnlockTable + 2, string.Concat(Enumerable.Repeat("00", 34)));
                for (int i = 0; i < itemsCheckList.Items.Count; i++)
                {
                    itemsCheckList.SetItemChecked(i, false);
                }
            }
        }
    }
}