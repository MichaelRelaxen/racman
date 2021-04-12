using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace racman
{

    public partial class UnlocksWindow : Form
    {

        public UnlocksWindow()
        {
            InitializeComponent();
            checkedListBox1.ItemCheck += CheckedListBox1_ItemCheck;
        }

        private void CheckedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (e.Index < 34)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    {
                        func.WriteMemory_SingleByte(RAC1Form.ip, RAC1Form.pid, rac1.UnlockTable + (uint)e.Index + 2, "01");
                    }
                }
                else{
                    func.WriteMemory_SingleByte(RAC1Form.ip, RAC1Form.pid, rac1.UnlockTable + (uint)e.Index + 2, "00");
                }
            }


            else{
                if (e.NewValue == CheckState.Checked)
                {
                    {
                        func.WriteMemory(RAC1Form.ip, RAC1Form.pid, rac1.UnlockTable + (uint)e.Index + 2, "FFFFFFFF");
                    }
                }

                else{
                    func.WriteMemory(RAC1Form.ip, RAC1Form.pid, rac1.UnlockTable + (uint)e.Index + 2, "00000000");
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}