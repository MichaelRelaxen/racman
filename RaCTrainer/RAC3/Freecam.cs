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
        public static int controlindex;
        public static int lookingatindex;
        public Timer timer = new Timer();
        public Freecam()
        {
            InitializeComponent();
            timer.Interval = (int)16;
            timer.Tick += new EventHandler(updatelabel);
            timer.Enabled = true;
            setupcontrolling();
            try
            {
                mstracker.Value = setuptracker(0x00d9f068, 100.0f);
                mftracker.Value = setuptracker(0x00d9f09c, 20.0f);
                tstracker.Value = setuptracker(0x00d9f06c, 100.0f);
                tftracker.Value = setuptracker(0x00d9f098, 20.0f);
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

        public int setuptracker(uint address, float multiplier)
        {
            byte[] temp = api.ReadMemory(AttachPS3Form.pid, address, 4);
            float temp2 = BitConverter.ToSingle(temp.Reverse().ToArray(), 0) * multiplier;
            return (int)temp2;
        }

        string lol = "Controlling";
        string lol2 = "Looking at: ";
        public void setupcontrolling()
        {
            
            var pid = api.getCurrentPID();
            controlindex = api.SubMemory(pid, 0x00d9f064 + 3, 1,IPS3API.MemoryCondition.Any, (value) =>
            {
                if (value[0] == 0)
                    lol = "Controlling: Ratchet";
                else
                    lol = "Controlling: Camera";
            });

            controlling.Text = lol;
            lookingatindex = api.SubMemory(pid, 0x00d9f060 + 3, 1, IPS3API.MemoryCondition.Any, (value) =>
            {
                if (value[0] == 0)
                    lol2 = "Looking at: Nothin";
                else if (value[0] == 1)
                    lol2 = "Looking at: Fixed Position";
                else
                    lol2 = "Looking at: Ratchet";
            });
            lookingat.Text = lol2;
        }
        public void updatelabel(object sender, EventArgs e)
        {
            controlling.Text = lol;
            lookingat.Text = lol2;
        }

        private void loadbutton_Click(object sender, EventArgs e)
        {
            if (listbox.SelectedItem != null)
            {
                string temp;
                temp = func.GetConfigData("config.txt", "freecampos" + listbox.SelectedItem.ToString());
                api.WriteMemory(AttachPS3Form.pid, 0x00d9f000, 76, temp);
                System.Threading.Thread.Sleep(10);
                api.WriteMemory(AttachPS3Form.pid, 0x00d9f0bc, 1);
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
                api.WriteMemory(AttachPS3Form.pid, 0x00d9f0c0, 1);
                System.Threading.Thread.Sleep(10);
                
                cuh = api.ReadMemoryStr(AttachPS3Form.pid, 0x00d9f000, 76);
                func.ChangeFileLines("config.txt", cuh, "freecampos" + savebox.Text);
            }
        }

        private void enablebutton_Click(object sender, EventArgs e)
        {
            bool enabled = Convert.ToBoolean(api.ReadMemory(AttachPS3Form.pid, 0x00d9f0b8));
            enabled = !enabled;
            api.WriteMemory(AttachPS3Form.pid, 0x00d9f0b8, Convert.ToUInt32(enabled));
            if (enabled)
                enablebutton.BackColor = Color.LightGreen;
            else
                enablebutton.BackColor = Color.Red;
        }

        //rotspeed = 0.03f;
        //movespeed = 0.05f;
		//airfriction = -0.1f;
		//turnfriction = -0.1f;
        private void mstracker_ValueChanged(object sender, EventArgs e)
        {
           float ba;
           ba =  mstracker.Value / 100.0f;
           mslabel.Text = $"Move speed: {ba}";
           api.WriteMemory(AttachPS3Form.pid, 0x00d9f068, BitConverter.GetBytes(ba).Reverse().ToArray());
        }

        private void mftracker_ValueChanged(object sender, EventArgs e)
        {
            float ba;
            ba = mftracker.Value / 20.0f;
            mflabel.Text = $"Move friction: {-ba * 100}%";
            api.WriteMemory(AttachPS3Form.pid, 0x00d9f09c, BitConverter.GetBytes(ba).Reverse().ToArray());
        }

        private void tftracker_ValueChanged(object sender, EventArgs e)
        {
            float ba;
            ba = tftracker.Value / 20.0f;
            tflabel.Text = $"Turn friction: {-ba * 100}%";
            api.WriteMemory(AttachPS3Form.pid, 0x00d9f098, BitConverter.GetBytes(ba).Reverse().ToArray());
        }

        private void tstracker_ValueChanged(object sender, EventArgs e)
        {
            float ba;
            ba = tstracker.Value / 100.0f;
            tslabel.Text = $"Turn speed: {ba}";
            api.WriteMemory(AttachPS3Form.pid, 0x00d9f06c, BitConverter.GetBytes(ba).Reverse().ToArray());
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
                menu.Items.Add("delete");
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
