using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using System.Collections.Generic;
using System.Diagnostics;

namespace racman
{

    public partial class RAC1Form : Form
    {
        public RAC1Form()
        {
            InitializeComponent();
            positions_comboBox.Text = "1";
            planets_comboBox.Text = "Veldin";
            // are you look for saved_pos_index? Just use positions_comboBox.SelectedIndex
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
        public Form HovenHealthForm;
        public Form InputDisplay;
        public static string ip = AttachPS3Form.ip;
        public static int pid = AttachPS3Form.pid;

        public string current_planet;
        public string[] planets_list;
        public List<string> planet_positions = new List<string>();
        private void loadPlanetPositions()
        {
            planet_positions.Clear();
            positions_comboBox.Items.Clear();
            string planetPositions = func.GetConfigData("config.txt", planets_list[getCurrentPlanetIndex()] + "PosArray");
            if (planetPositions != "")
            {
                for (int i = 0; i < planetPositions.Split(',').Length - 1; i++)
                {
                    positions_comboBox.Items.Add(planetPositions.Split(',')[i]);
                    planet_positions.Add(planetPositions.Split(',')[i]);
                }
            }

            positions_comboBox.Text = planetPositions.Split(',')[0];

            if (positions_comboBox.Items.Count <= 0)
            {
                positions_comboBox.Items.AddRange(new object[] { "1", "2", "3" });
            }

            positions_comboBox.SelectedIndex = 0;
        }
        private void savePlanetPositions(string planet)
        {
            string planetPosStringContents = "";
            foreach (string item in planet_positions)
            {
                planetPosStringContents += item;
                planetPosStringContents += ",";
            }
            func.ChangeFileLines("config.txt", planetPosStringContents, planet + "PosArray");
        }

        private void positions_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //planetPosName.Text = positions_comboBox.Text;
        }

        private void savePosButton_Click(object sender, EventArgs e)
        {
            string position = func.ReadMemory(ip, pid, rac1.player_coords, 30);
            func.ChangeFileLines("config.txt", position, planets_list[getCurrentPlanetIndex()] + "SavedPos" + positions_comboBox.Text);
            //savePlanetPositions(planets_list[getCurrentPlanetIndex()]);
        }

        private void loadPosButton_Click(object sender, EventArgs e)
        {
            if (positions_comboBox.Text == "")
            {
                MessageBox.Show("Position is empty, bruh");
            }
            else
            {
                string position = func.GetConfigData("config.txt", planets_list[getCurrentPlanetIndex()] + "SavedPos" + positions_comboBox.Text);
                if (position != "")
                {
                    func.WriteMemory(ip, pid, rac1.player_coords, position);
                }
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


        private void loadPlanetButton_Click_1(object sender, EventArgs e)
        {
            int x = planets_comboBox.SelectedIndex; string planet = x.ToString("X2");
            if(lflagresetCb.Checked)
                ResetLevelFlags((uint)x);
            func.WriteMemory(ip, pid, rac1.load_planet, $"00000001000000{planet}");

            Thread.Sleep(1000); // lazy
            func.WriteMemory(ip, pid, 0x9645C4, "0000001A0000000400000002"); // Force fast load
            //loadPlanetPositions();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void gbsreset_Click(object sender, EventArgs e)
        {
            string reset = string.Concat(Enumerable.Repeat("00", 80));
            func.WriteMemory(ip, pid, rac1.gold_bolts_array, reset);
        }

        private void unlockGoldBoltsButton_Click(object sender, EventArgs e)
        {
            string unlock = string.Concat(Enumerable.Repeat("01", 80));
            func.WriteMemory(ip, pid, rac1.gold_bolts_array, unlock);
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
        public int getCurrentPlanetIndex()
        {
            string planet = func.ReadMemory(ip, pid, rac1.current_planet, 4);
            return int.Parse(planet, System.Globalization.NumberStyles.HexNumber);
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void hovenHPButton_click(object sender, EventArgs e)
        {
            if (HovenHealthForm == null)
            {
                HovenHealthForm = new HovenHealthForm();
                HovenHealthForm.FormClosed += HovenHealthForm_FormClosed;
                HovenHealthForm.Show();
            }
        }

        private void HovenHealthForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HovenHealthForm = null;
        }

        private void switchGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClosing -= Form1_FormClosing;
            Program.AttachPS3Form.Show();
            Close();
        }

        private void inputdisplay_click(object sender, EventArgs e)
        {
            if (!(func.api is Ratchetron))
            {
                MessageBox.Show("You need to be using the new API to use input display");
                return;
            }

            if (InputDisplay == null)
            {
                InputDisplay = new InputDisplay();
                InputDisplay.FormClosed += InputDisplay_FormClosed;
                InputDisplay.Show();
            }
            /*
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Environment.CurrentDirectory + @"\bins\RatchetInputDisplay.exe";
            startInfo.Arguments = func.api.GetIP();

            Process.Start(startInfo);
            */
        }


        private void InputDisplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            InputDisplay = null;
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