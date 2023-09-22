using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static racman.rac2;

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
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            watchedMemoryAddressesListView.DoubleBuffering(true);
            mobyInspectorListView.DoubleBuffering(true);
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
                // If you put "0x" in the middle of your hex address I want you to die
                var strippedText = registerAddressTextBox.Text.Replace(" ", "").Replace("0x", "").Trim();
                address = Convert.ToUInt32(strippedText, 16);
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

            IPS3API api = func.api;

            watched.subID = api.SubMemory(api.getCurrentPID(), address, watched.size, (byte[] bytes) =>
            {
                if (watched.isFloat)
                {
                    float value = BitConverter.ToSingle(bytes, 0);
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
                            val = BitConverter.ToInt16(bytes, 0);
                            break;
                        }
                    case 4:
                        {
                            val = BitConverter.ToInt32(bytes, 0); 
                            break;
                        }
                    case 8:
                        {
                            val = BitConverter.ToInt64(bytes, 0);
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
                    func.api.ReleaseSubID(watched.subID);
                    if (watched.isFrozen)
                        func.api.ReleaseSubID(watched.freezeSub);
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
                    IPS3API api = func.api;
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
                            watched.freezeSub = api.FreezeMemory(api.getCurrentPID(), watched.address, watched.size, IPS3API.MemoryCondition.Any, currVal);
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

                    IPS3API api = func.api;

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

                    IPS3API api = func.api;

                    api.ReleaseSubID(watched.subID);

                    watchedMemoryAddressesListView.Items.Remove(focusedItem);
                }
            }
        }

        public void PopulateMobysComboBoxRac2()
        {
            // Clear combo box
            selectedMobyComboBox.Items.Clear();

            var pid = func.api.getCurrentPID();
            uint instance = BitConverter.ToUInt32(func.api.ReadMemory(pid, rac2.addr.mobyInstances, 4).Reverse().ToArray(), 0);
            uint end = BitConverter.ToUInt32(func.api.ReadMemory(pid, rac2.addr.mobyInstancesEnd, 4).Reverse().ToArray(), 0);

            while (instance < end) {
                ushort oClass = BitConverter.ToUInt16(func.api.ReadMemory(pid, instance + 0xaa, 0x2).Reverse().ToArray(), 0);
                byte state = func.api.ReadMemory(pid, instance + 0x20, 0x1)[0];

                selectedMobyComboBox.Items.Add($"0x{instance.ToString("X")}: 0x{oClass.ToString("X")} ({oClass}) (state: {state})");

                instance += 0x100;
            }
        }

        public void PopulateMobyInspectorRac2(int index)
        {   
            var pid = func.api.getCurrentPID();
            uint instance = BitConverter.ToUInt32(func.api.ReadMemory(pid, rac2.addr.mobyInstances, 4).Reverse().ToArray(), 0);

            byte[] memory = func.api.ReadMemory(pid, instance + (0x100 * (uint)index), 0x100);
            rac2.Moby moby = rac2.Moby.ByteArrayToMoby(memory);

            var type = typeof(rac2.Moby);
            var fields = type.GetFields();

            // Don't clear, but ensure we have the same count of items
            while (mobyInspectorListView.Items.Count < fields.Length)
            {
                mobyInspectorListView.Items.Add(new ListViewItem(new string[3])); // Placeholder
            }

            // Go through each item and set the right data
            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var offset = Marshal.OffsetOf(type, field.Name).ToInt32();

                var item = mobyInspectorListView.Items[i];
                item.Text = $"0x{(instance + (0x100 * (uint)index) + offset).ToString("X")}";
                item.SubItems[1].Text = field.Name;

                if (field.FieldType == typeof(rac2.Vec4))
                {
                    rac2.Vec4 vec = (rac2.Vec4)field.GetValue(moby);
                    item.SubItems[2].Text = $"x: {vec.x}, y: {vec.y}, z: {vec.z}, w: {vec.w}";
                }
                else if(field.FieldType == typeof(GamePtr))
                {
                    item.SubItems[2].Text = $"0x{((GamePtr)field.GetValue(moby)).addr.ToString("X")}";
                }
                else
                {
                    item.SubItems[2].Text = field.GetValue(moby).ToString();
                }
            }
        }

        private void refreshMobysButton_Click(object sender, EventArgs e)
        {
            if (AttachPS3Form.game == "NPEA00386")
            {
                WebMAN wmm = new WebMAN(func.api.GetIP());
                wmm.PauseRSX();
                   
                try
                {
                    PopulateMobysComboBoxRac2();
                } catch (Exception ex)
                {
                    wmm.ContinueRSX();
                    throw ex;
                }
                wmm.ContinueRSX();
            } 
            else
            {
                MessageBox.Show("Game is not supported.");
            }
        }
        System.Timers.Timer timer = new System.Timers.Timer(1000); // Set up the timer for 1 second intervals (1000 milliseconds = 1 second)

        private void selectedMobyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AttachPS3Form.game == "NPEA00386")
            {
                timer.Elapsed += (s, evt) =>
                {
                    // Inline function (or lambda expression) to be executed every second
                    this.Invoke(new Action(() =>
                    {
                        PopulateMobyInspectorRac2(((ComboBox)sender).SelectedIndex);
                    }));
                };
                timer.Start(); // Start the timer

                PopulateMobyInspectorRac2(((ComboBox)sender).SelectedIndex);
            }
        }
    }
}
