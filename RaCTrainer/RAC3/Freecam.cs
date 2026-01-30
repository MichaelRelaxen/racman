using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace racman.RAC3
{
    public partial class Freecam : Form
    {

        public rac3 game;
        private static IPS3API api = func.api;
        public static int controlSubID;
        public static int lookAtSubID;
        public Timer timer = new Timer();

        // Offsets from the mod.
        uint moveSpeed = 0x00d9f068;
        uint moveFriction = 0x00d9f09c;
        uint turnSpeed = 0x00d9f06c;
        uint turnFriction = 0x00d9f098;
        uint currentControl = 0x00d9f064;
        uint currentLookAt = 0x00d9f060;
        uint savePosOffset = 0x00d9f000;
        uint loadCameraPosition = 0x00d9f0bc;
        uint saveCameraPosition = 0x00d9f0c0;
        uint modEnabled = 0x00d9f0b8;

        public Freecam()
        {
            InitializeComponent();
            timer.Interval = (int)16;
            timer.Tick += new EventHandler(updatelabel);
            timer.Enabled = true;
            SetupUpdateLabels();
            try
            {
                mstracker.Value = SetupTracker(moveSpeed, 100.0f);
                mftracker.Value = SetupTracker(moveFriction, 20.0f);
                tstracker.Value = SetupTracker(turnSpeed, 100.0f);
                tftracker.Value = SetupTracker(turnFriction, 20.0f);
            }
            catch { }

            string[] glob = File.ReadAllLines("config.txt");
            foreach(string globra in glob)
            {
                if (globra.Contains("freecampos")) 
                {
                    int fahj = globra.IndexOf(" = ");
                    string slice = globra.Substring(10, fahj - 10);
                    listbox.Items.Add(slice);
                }
            }
        }

        public int SetupTracker(uint address, float multiplier)
        {
            byte[] temp = api.ReadMemory(AttachPS3Form.pid, address, 4);
            float temp2 = BitConverter.ToSingle(temp.Reverse().ToArray(), 0) * multiplier;
            return (int)temp2;
        }

        string controllingString = "Controlling";
        string lookAtString = "Looking at: ";
        public void SetupUpdateLabels()
        {
            
            var pid = api.getCurrentPID();
            controlSubID = api.SubMemory(pid, currentControl + 3, 1,IPS3API.MemoryCondition.Any, (value) =>
            {
                if (value[0] == 0)
                    controllingString = "Controlling: Ratchet";
                else
                    controllingString = "Controlling: Camera";
            });

            controlling.Text = controllingString;
            lookAtSubID = api.SubMemory(pid, currentLookAt + 3, 1, IPS3API.MemoryCondition.Any, (value) =>
            {
                if (value[0] == 0)
                    lookAtString = "Looking at: Nothin";
                else if (value[0] == 1)
                    lookAtString = "Looking at: Fixed Position";
                else
                    lookAtString = "Looking at: Ratchet";
            });
            lookingat.Text = lookAtString;
        }
        public void updatelabel(object sender, EventArgs e)
        {
            controlling.Text = controllingString;
            lookingat.Text = lookAtString;
        }

        private void loadbutton_Click(object sender, EventArgs e)
        {
            if (listbox.SelectedItem != null)
            {
                string temp;
                temp = func.GetConfigData("config.txt", "freecampos" + listbox.SelectedItem.ToString());
                api.WriteMemory(AttachPS3Form.pid, savePosOffset, 76, temp);
                System.Threading.Thread.Sleep(10);
                api.WriteMemory(AttachPS3Form.pid, loadCameraPosition, 1);
            }
            else
            {
                MessageBox.Show("No savepos selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            string cuh;
            if (string.Empty != savebox.Text)
            {

                if (!listbox.Items.Contains(savebox.Text))
                {
                    listbox.Items.Add(savebox.Text);
                    listbox.SelectedIndex = listbox.Items.Count - 1;
                }
                api.WriteMemory(AttachPS3Form.pid, saveCameraPosition, 1);
                System.Threading.Thread.Sleep(10);
                
                cuh = api.ReadMemoryStr(AttachPS3Form.pid, savePosOffset, 76);
                func.ChangeFileLines("config.txt", cuh, "freecampos" + savebox.Text);
            }
        }

        private void enablebutton_Click(object sender, EventArgs e)
        {
            bool enabled = Convert.ToBoolean(api.ReadMemory(AttachPS3Form.pid, modEnabled));
            enabled = !enabled;
            api.WriteMemory(AttachPS3Form.pid, modEnabled, Convert.ToUInt32(enabled));
            if (enabled)
                enablebutton.BackColor = Color.LightGreen;
            else
                enablebutton.BackColor = Color.Red;
        }

        // default values:
        //rotspeed = 0.03f;
        //movespeed = 0.05f;
		//airfriction = -0.1f;
		//turnfriction = -0.1f;
        private void mstracker_ValueChanged(object sender, EventArgs e)
        {
           float ba;
           ba =  mstracker.Value / 100.0f;
           mslabel.Text = $"Move speed: {ba}";
           api.WriteMemory(AttachPS3Form.pid, moveSpeed, BitConverter.GetBytes(ba).Reverse().ToArray());
        }

        private void mftracker_ValueChanged(object sender, EventArgs e)
        {
            float ba;
            ba = mftracker.Value / 20.0f;
            mflabel.Text = $"Move friction: {-ba * 100}%";
            api.WriteMemory(AttachPS3Form.pid, moveFriction, BitConverter.GetBytes(ba).Reverse().ToArray());
        }

        private void tftracker_ValueChanged(object sender, EventArgs e)
        {
            float ba;
            ba = tftracker.Value / 20.0f;
            tflabel.Text = $"Turn friction: {-ba * 100}%";
            api.WriteMemory(AttachPS3Form.pid, turnFriction, BitConverter.GetBytes(ba).Reverse().ToArray());
        }

        private void tstracker_ValueChanged(object sender, EventArgs e)
        {
            float ba;
            ba = tstracker.Value / 100.0f;
            tslabel.Text = $"Turn speed: {ba}";
            api.WriteMemory(AttachPS3Form.pid, turnSpeed, BitConverter.GetBytes(ba).Reverse().ToArray());
        }

        private void savebox_Click(object sender, EventArgs e)
        {
            savebox.Text = "";
        }

        private void listbox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                menu.Items.Add("Delete");
                menu.Items[0].Click += deletepos;
                menu.Show(Cursor.Position);
            }
        }

        public void deletepos(object sender, EventArgs e)
        {
            if (listbox.SelectedIndex != -1)
            {
                List<string> glob = File.ReadAllLines("config.txt").ToList();
                foreach (string globra in glob)
                {
                    if (globra.Contains(listbox.SelectedItem.ToString()))
                    {
                        glob.Remove(globra);
                        listbox.Items.RemoveAt(listbox.SelectedIndex);
                        break;
                    }
                }
                File.WriteAllLines("config.txt", glob);
            }
        }

    }
}
