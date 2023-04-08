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
    public partial class MemoryForm : Form
    {
        struct WatchedAddress
        {
            public uint address;
            public int subID;
            public uint size;
            public bool isFloat;
            public bool hexRepresented;
            public bool isFrozen;
            public int freezeSub;
        }

        List<WatchedAddress> watchedAddresses = new List<WatchedAddress>();

        public MemoryForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        void SetItemValueText(ListViewItem item, string value)
        {
            this.Invoke(new Action(() =>
            {
                item.SubItems[2].Text = value;
            }));
        }

        private void addMemoryWatchButton_Click(object sender, EventArgs e)
        {
            uint address = 0;

            try
            {
                address = Convert.ToUInt32(registerAddressTextBox.Text, 16);
            } catch (Exception ex)
            {
                MessageBox.Show("Address must be hexadecimal");
                return;
            }

            ListViewItem item = new ListViewItem("Unknown");
            item.SubItems.Add(address.ToString("X"));
            item.SubItems.Add("Waiting...");

            WatchedAddress watched = new WatchedAddress();

            watched.address = address;

            switch (registerAddressTypeCombo.Text) 
            {
                case "Int32": 
                    {
                        watched.size = 4;
                        break;        
                    }
                case "Int64": 
                    {
                        watched.size = 8;
                        break;
                    }
                case "Int16": 
                    {
                        watched.size = 2;
                        break;
                    }
                case "Byte": 
                    {
                        watched.size = 1;
                        break;
                    }
                case "Float": 
                    {
                        watched.size = 4;
                        watched.isFloat = true;
                        break;
                    }
                case "Pointer":
                    {
                        watched.size = 4;
                        watched.hexRepresented = true;
                        break;
                    }
                default:
                    {
                        watched.size = 4;
                        watched.hexRepresented = true;
                        break;
                    }
            }

            Ratchetron api = (Ratchetron)func.api;

            watched.subID = api.SubMemory(api.getCurrentPID(), address, watched.size, (byte[] bytes) =>
            {
                if (watched.isFloat)
                {
                    float value = BitConverter.ToSingle(bytes.Reverse().ToArray(), 0);
                    SetItemValueText(item, value.ToString());

                    return;
                }

                long val = 0;

                switch (watched.size)
                {
                    case 1:
                        {
                            val = bytes[0];
                            break;
                        }
                    case 2:
                        {
                            val = BitConverter.ToInt16(bytes.Reverse().ToArray(), 0);
                            break;
                        }
                    case 4:
                        {
                            val = BitConverter.ToInt32(bytes, 0); 
                            break;
                        }
                    case 8:
                        {
                            val = BitConverter.ToInt64(bytes.Reverse().ToArray(), 0);
                            break;
                        }
                    default:
                        {
                            val = bytes[0];
                            break;
                        }
                }

                if (!watched.hexRepresented)
                {
                    SetItemValueText(item, val.ToString());
                } else
                {
                    SetItemValueText(item, val.ToString("X"));
                }
            });

            item.Tag = watched;

            watchedMemoryAddressesListView.Items.Add(item);
        }

        private void MemoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (ListViewItem item in watchedMemoryAddressesListView.Items)
            {
                if (item.Tag != null)
                {
                    WatchedAddress watched = (WatchedAddress)item.Tag;
                    ((Ratchetron)func.api).ReleaseSubID(watched.subID);
                    if (watched.isFrozen)
                        ((Ratchetron)func.api).ReleaseSubID(watched.freezeSub);
                }
            }
        }

        private void watchedMemoryAddressesListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = watchedMemoryAddressesListView.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    ContextMenuStrip menuStrip = new ContextMenuStrip();
                    menuStrip.Items.Add("Edit value...");
                    menuStrip.Items[0].Click += MenuStripEditValue_Click;

                    menuStrip.Items.Add("Freeze/unfreeze value");
                    menuStrip.Items[1].Click += MenuStripEditValue_Freeze;

                    menuStrip.Items.Add("Delete");
                    menuStrip.Items[2].Click += MenuStripDelete_Click;

                    menuStrip.Show(Cursor.Position);

                }
            }
        }

        private void MenuStripEditValue_Freeze(object sender, EventArgs e)
        {
            var focusedItem = watchedMemoryAddressesListView.FocusedItem;
            if (focusedItem != null)
            {
                if (focusedItem.Tag != null)
                {
                    WatchedAddress watched = (WatchedAddress)focusedItem.Tag;
                    Ratchetron api = (Ratchetron)func.api;
                    try
                    {
                        if (watched.isFrozen)
                        {
                            watched.isFrozen = false;
                            api.ReleaseSubID(watched.freezeSub);
                            watched.freezeSub = 0;
                        } 
                        else
                        {
                            watched.isFrozen = true;
                            // Get current value to freeze
                            byte[] currVal = api.ReadMemory(api.getCurrentPID(), watched.address, watched.size);
                            watched.freezeSub = api.FreezeMemory(api.getCurrentPID(), watched.address, watched.size, Ratchetron.MemoryCondition.Any, currVal);
                        }
                          
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private void MenuStripEditValue_Click(object sender, EventArgs e)
        {
            var focusedItem = watchedMemoryAddressesListView.FocusedItem;
            if (focusedItem != null)
            {
                if (focusedItem.Tag != null)
                {
                    WatchedAddress watched = (WatchedAddress)focusedItem.Tag;

                    Ratchetron api = (Ratchetron)func.api;

                    SimpleInputDialogForm inputDialog = new SimpleInputDialogForm("Edit value", focusedItem.SubItems[2].Text);
                    if (inputDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            if (watched.isFloat)
                            {
                                float value = Convert.ToSingle(inputDialog.inputTextBox.Text);

                                api.WriteMemory(api.getCurrentPID(), watched.address, watched.size, BitConverter.GetBytes(value).Take((int)watched.size).Reverse().ToArray());
                            }
                            else
                            {
                                long value = Convert.ToInt64(inputDialog.inputTextBox.Text);

                                api.WriteMemory(api.getCurrentPID(), watched.address, watched.size, BitConverter.GetBytes(value).Take((int)watched.size).Reverse().ToArray());
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }

        private void MenuStripDelete_Click(object sender, EventArgs e)
        {
            var focusedItem = watchedMemoryAddressesListView.FocusedItem;
            if (focusedItem != null)
            {
                if (focusedItem.Tag != null)
                {
                    WatchedAddress watched = (WatchedAddress)focusedItem.Tag;

                    Ratchetron api = (Ratchetron)func.api;

                    api.ReleaseSubID(watched.subID);

                    watchedMemoryAddressesListView.Items.Remove(focusedItem);
                }
            }
        }
    }
}
