using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;


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

            goodiesCheck.Checked = Convert.ToBoolean(int.Parse(func.ReadMemory(ip, pid, rac1.goodies_menu, 1)));
            drekSkipCheck.Checked = Convert.ToBoolean(int.Parse(func.ReadMemory(ip, pid, rac1.drek_skip, 1)));
        }


        private void bolts_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    uint bolts = uint.Parse(bolts_textBox.Text);
                    func.WriteMemory(ip, pid, rac1.bolt_count, bolts.ToString("X8"));
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
            string position = func.ReadMemory(ip, pid, rac1.player_coords, 30);
            func.ChangeFileLines("config.txt", position, planets_list[getCurrentPlanetIndex()] + "SavedPos" + Convert.ToString(saved_pos_index));
        }

        private void loadPosButton_Click(object sender, EventArgs e)
        {
            string position = func.GetConfigData("config.txt", planets_list[getCurrentPlanetIndex()] + "SavedPos" + Convert.ToString(saved_pos_index));
            if (position != "")
            {
                func.WriteMemory(ip, pid, rac1.player_coords, position);
            }

        }


        private void ghostrac_Click(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac1.ghost_timer, "2710");
        }

        private void killyourself_Click(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac1.player_coords + 8, "C2480000");
            /*if (lflagresetCb.Checked)
                ResetLevelFlags((uint)planets_comboBox.SelectedIndex);*/
        }

        private void planets_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void loadPlanetButton_Click_1(object sender, EventArgs e)
        {
            int x = planets_comboBox.SelectedIndex; string planet = x.ToString("X2");
            if(lflagresetCb.Checked)
                ResetLevelFlags((uint)x);
            func.WriteMemory(ip, pid, rac1.load_planet, $"00000001000000{planet}");

            Thread.Sleep(1000); // lazy
            func.WriteMemory(ip, pid, 0x9645C4, "0000001A0000000400000002"); // Force fast load
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
            func.WriteMemory(ip, pid, rac1.gold_bolts_array, reset);
            func.WriteMemory(ip, pid, rac1.gold_bolts_array + 128, reset);
        }

        private void unlockGoldBoltsButton_Click(object sender, EventArgs e)
        {
            string unlock = string.Concat(Enumerable.Repeat("01", 128));
            func.WriteMemory(ip, pid, rac1.gold_bolts_array, unlock);
            func.WriteMemory(ip, pid, rac1.gold_bolts_array + 128, unlock);
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
            func.WriteMemory(ip, pid, rac1.player_health, "00000003");
        }

        private void infHealth_Checkbox_Changed(object sender, EventArgs e)
        {
            if (infHealth.Checked)
            {
                func.WriteMemory(ip, pid, rac1.player_health, "11111111");
            }
            else
            {
                func.WriteMemory(ip, pid, rac1.player_health, "00000004");
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
        public static int getCurrentPlanetIndex()
        {
            string planet = func.ReadMemory(ip, pid, rac1.current_planet, 4);
            return int.Parse(planet, System.Globalization.NumberStyles.HexNumber);
        }
        private void drekSkipCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (drekSkipCheck.Checked)
            {
                func.WriteMemory(ip, pid, rac1.drek_skip, "01");
            }
            else
            {
                func.WriteMemory(ip, pid, rac1.drek_skip, "00");
            }
        }
        private void goodiesCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (goodiesCheck.Checked)
            {
                func.WriteMemory(ip, pid, rac1.goodies_menu, "01");
            }
            else
            {
                func.WriteMemory(ip, pid, rac1.goodies_menu, "00");
            }
        }
        private void ResetLevelFlags(uint planetIndex)
        {
            //thank you dtu!
            func.WriteMemory(ip, pid, rac1.level_flags + 0x10 * planetIndex, string.Concat(Enumerable.Repeat("00", 0x10)));
            func.WriteMemory(ip, pid, rac1.misc_level_flags + 0x100 * planetIndex, string.Concat(Enumerable.Repeat("00", 0x100)));
            func.WriteMemory(ip, pid, rac1.infobot_flags, func.strarr("00", 0x20));
            func.WriteMemory(ip, pid, rac1.watched_ilms_array, func.strarr("00", 0x100));

            
            // Gadget unlocks and more misc level flags
            if(planetIndex == 3) // Kerwan
            {
                func.WriteMemory(ip, pid, 0x96C378, func.strarr("00", 0xF0));
                func.WriteMemory(ip, pid, rac1.unlock_array + 2, "00"); // Heli-Pack
                func.WriteMemory(ip, pid, rac1.unlock_array + 12, "00"); // Swingshot
            }
            if(planetIndex == 4) // Eudora
            {
                func.WriteMemory(ip, pid, 0x96C468, func.strarr("00", 0x40));
                func.WriteMemory(ip, pid, rac1.unlock_array + 9, "00"); // Suck Cannon
            }
            if(planetIndex == 5) // Rilgar
            {
                func.WriteMemory(ip, pid, 0x96C498, func.strarr("00", 0xA0)); 
            }
            if(planetIndex == 6) // Blarg Station
            {
                func.WriteMemory(ip, pid, rac1.unlock_array + 29, "00");
            }
            if(planetIndex == 8) // Batalia
            {
                func.WriteMemory(ip, pid, 0x96C5A8, func.strarr("00", 0x40));
            }
            if(planetIndex == 9) // Gaspar
            {
                func.WriteMemory(ip, pid, 0x96C5E8, func.strarr("00", 0x20));
                func.WriteMemory(ip, pid, rac1.unlock_array + 7, "00"); // Pilot's Helmet
            }
            if(planetIndex == 10) // Orxon
            {
                func.WriteMemory(ip, pid, rac1.unlock_array + 28, "00"); // Magneboots
            }
            if(planetIndex == 11) // Pokitaru
            {
                func.WriteMemory(ip, pid, rac1.unlock_array + 3, "00"); // Thruster-Pack
                func.WriteMemory(ip, pid, rac1.unlock_array + 6, "00"); // O2 Mask
            }
        }

    }
}