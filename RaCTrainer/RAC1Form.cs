using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace racman
{

    public partial class RAC1Form : Form
    {
        public RAC1Form()
        {
            InitializeComponent();
            positions_comboBox.Text = "1";
            planets_comboBox.Text = "Veldin";

            bolts_textBox.KeyDown += bolts_TextBox_KeyDown;
            positions_comboBox.SelectedIndexChanged += positions_ComboBox_SelectedIndexChanged;

            if (File.Exists(Environment.CurrentDirectory + @"\config.txt"))
            {
                ip = func.GetConfigData("config.txt", "ip");
            }

            planets_list = new string[] {
                "Veldin",
                "Novalis",
                "Aridia",
                "Kerwan",
                "Eudora",
                "Rilgar",
                "Blarg",
                "Umbris",
                "Batalia",
                "Gaspar",
                "Orxon",
                "Pokitaru",
                "Hoven",
                "Gemlik",
                "Oltanis",
                "Quartu",
                "Kalebo3",
                "Fleet",
                "Veldin2"
            };

            goodiesCheck.Checked = Convert.ToBoolean(int.Parse(func.ReadMemory(ip, pid, rac1.GoodiesMenu, 1)));
            drekSkipCheck.Checked = Convert.ToBoolean(int.Parse(func.ReadMemory(ip, pid, rac1.DrekSkip, 1)));
        }


        private void bolts_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    uint bolts = uint.Parse(bolts_textBox.Text);
                    func.WriteMemory(ip, pid, rac1.BoltCount, bolts.ToString("X8"));
                }
                catch
                {
                    MessageBox.Show("Please enter a number", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        public Form UnlocksWindow;
        public static string ip = AttachPS3Form.ip;
        public static int pid = AttachPS3Form.pid;


        public int saved_pos_index = 1;
        public string current_planet;
        public string[] planets_list;



        private void positions_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            saved_pos_index = positions_comboBox.SelectedIndex;
        }

        private void savePosButton_Click(object sender, EventArgs e)
        {
            string position = func.ReadMemory(ip, pid, rac1.Coordinates, 30);
            func.ChangeFileLines("config.txt", position, planets_list[getCurrentPlanetIndex()] + "SavedPos" + Convert.ToString(saved_pos_index));
        }

        private void loadPosButton_Click(object sender, EventArgs e)
        {
            string position = func.GetConfigData("config.txt", planets_list[getCurrentPlanetIndex()] + "SavedPos" + Convert.ToString(saved_pos_index));
            if (position != "")
            {
                func.WriteMemory(ip, pid, rac1.Coordinates, position);
            }

        }


        private void ghostrac_Click(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac1.GhostRatchet, "2710");
        }

        private void killyourself_Click(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac1.Coordinates + 8, "C2480000");
        }

        private void planets_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void loadPlanetButton_Click_1(object sender, EventArgs e)
        {
            int x = planets_comboBox.SelectedIndex; string planet = x.ToString("X2");
            func.WriteMemory(ip, pid, rac1.LoadPlanet, $"00000001000000{planet}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KeyPreview = true;

            ToolTip tt1 = new ToolTip(); tt1.SetToolTip(savepos, "Hotkey: Shift");
            ToolTip tt2 = new ToolTip(); tt1.SetToolTip(loadpos, "Hotkey: Space");
            ToolTip tt3 = new ToolTip(); tt1.SetToolTip(killyourself, "Hotkey: E");

        }


        //Method that checks if keys are being pressed
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                savepos.PerformClick();
            }

            if (e.KeyCode == Keys.Space)
            {
                loadpos.PerformClick();
            }

            if (e.KeyCode == Keys.E)
            {
                killyourself.PerformClick();
            }

            if (e.KeyCode == Keys.D1)
            {
                saved_pos_index = 0;
            }
            if (e.KeyCode == Keys.D2)
            {
                saved_pos_index = 1;
            }
            if (e.KeyCode == Keys.D3)
            {
                saved_pos_index = 2;
            }
        }

        private void gbsreset_Click(object sender, EventArgs e)
        {
            string reset = string.Concat(Enumerable.Repeat("00", 128));
            func.WriteMemory(ip, pid, rac1.GoldBoltsStart, reset);
            func.WriteMemory(ip, pid, rac1.GoldBoltsStart + 128, reset);
        }

        private void unlockGoldBoltsButton_Click(object sender, EventArgs e)
        {
            string unlock = string.Concat(Enumerable.Repeat("01", 128));
            func.WriteMemory(ip, pid, rac1.GoldBoltsStart, unlock);
            func.WriteMemory(ip, pid, rac1.GoldBoltsStart + 128, unlock);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string refill = string.Concat(Enumerable.Repeat("000001A4", 32));

            func.WriteMemory(ip, pid, 0xDA527C, refill);
            func.WriteMemory(ip, pid, 0xDA527C + 128, refill);
            func.WriteMemory(ip, pid, 0xDA527C + 256, refill);
            func.WriteMemory(ip, pid, 0xDA527C + 384, refill);
            func.WriteMemory(ip, pid, 0xDA527C + 420, refill);
        }

        private void bolts_textBox_TextChanged(object sender, EventArgs e)
        {

        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac1.Health, "00000003");
        }

        private void infHealth_Checkbox_Changed(object sender, EventArgs e)
        {
            if (infHealth.Checked)
            {
                func.WriteMemory(ip, pid, rac1.Health, "11111111");
            }
            else
            {
                func.WriteMemory(ip, pid, rac1.Health, "00000004");
            }
        }

        private void unlocksWindowButton_Click(object sender, EventArgs e)
        {
            if (UnlocksWindow == null)
            {
                UnlocksWindow = new UnlocksWindow();
                UnlocksWindow.FormClosed += UnlocksWindow_FormClosed;
                UnlocksWindow.Show();
            }

        }

        private void UnlocksWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnlocksWindow = null;
        }

        private void currentPlanetView()
        {
            string planet = func.ReadMemory(ip, pid, rac1.CurrentPlanet, 30);
        }

        public static int getCurrentPlanetIndex()
        {
            string planet = func.ReadMemory(ip, pid, rac1.CurrentPlanet, 4);
            int index = int.Parse(planet, System.Globalization.NumberStyles.HexNumber);
            return index;
        }
        private void drekSkipCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (drekSkipCheck.Checked)
            {
                func.WriteMemory(ip, pid, rac1.DrekSkip, "01");
            }
            else
            {
                func.WriteMemory(ip, pid, rac1.DrekSkip, "00");
            }
        }
        private void goodiesCheck_CheckedChanged(object sender, EventArgs e)
        {
            //Would want to make it so it can check the byte in memory first then go off of that... a button might be better
            if (goodiesCheck.Checked)
            {
                func.WriteMemory(ip, pid, rac1.GoodiesMenu, "01");
            }
            else
            {
                func.WriteMemory(ip, pid, rac1.GoodiesMenu, "00");
            }
        }
    }
}