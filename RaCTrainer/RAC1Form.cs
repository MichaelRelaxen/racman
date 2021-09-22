using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace racman
{

    public partial class RAC1Form : Form
    {
        public Form UnlocksWindow;
        public Form HovenHealthForm;
        public Form InputDisplay;
        public static string ip = AttachPS3Form.ip;
        public static int pid = AttachPS3Form.pid;
        private static Timer ForceLoadTimer = new Timer();
        private static Timer InputsTimer = new Timer();

        public RAC1Form()
        {
            InitializeComponent();
            positions_comboBox.Text = "1";
            bolts_textBox.KeyDown += bolts_TextBox_KeyDown;

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




            if (func.api is Ratchetron)
            {
                Ratchetron api = (Ratchetron)func.api;

                api.OpenDataChannel();

                Inputs.GetInputs();

                InputsTimer.Interval = (int)16.66667;
                InputsTimer.Tick += new EventHandler(CheckInputs);

                ForceLoadTimer.Interval = 1000;
                ForceLoadTimer.Tick += new EventHandler(GetPlanet);
                //ForceLoadTimer.Enabled = true;

                int buttonMaskSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, rac1.current_planet, 4, (value) =>
                {
                    planetIndex = BitConverter.ToInt32(value, 0);
                });

            }
        }

        int planetIndex;

        private void GetPlanet(object sender, EventArgs e)
        {
            if(planetIndex == 0 || planetIndex == 1)
            {
                FastLoadToggle.Checked = false;
            }
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

        public string current_planet;
        public string[] planets_list;
        public List<string> planet_positions = new List<string>();
        /*private void loadPlanetPositions()
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
        }*/
        bool inputCheck = true;
        private void CheckInputs(object sender, EventArgs e)
        {

            if (Inputs.RawInputs == 0xB && inputCheck)
            {
                SavePosition();
                inputCheck = false;
            }
            if (Inputs.RawInputs == 0x7 && inputCheck)
            {
                LoadPosition();
                inputCheck = false;
            }
            if (Inputs.RawInputs == 0x5 && inputCheck) 
            {
                KillYourself();
                inputCheck = false;
            }
            if (Inputs.RawInputs == 0x600 & inputCheck)
            {
                LoadPlanet();
                inputCheck = false;
            }
            if(Inputs.RawInputs == 0x00 & !inputCheck)
            {
                inputCheck = true;
            }

        }
        private void savePosButton_Click(object sender, EventArgs e)
        {
            SavePosition();
        }
        private void loadPosButton_Click(object sender, EventArgs e)
        {
            LoadPosition();
        }
        private void killyourself_Click(object sender, EventArgs e)
        {
            KillYourself();
        }
        private void loadPlanetButton_Click_1(object sender, EventArgs e)
        {
            LoadPlanet();
        }
        private void SavePosition()
        {
            string position = func.ReadMemory(ip, pid, rac1.player_coords, 30);
            func.ChangeFileLines("config.txt", position, planets_list[planetIndex] + "SavedPos" + positions_comboBox.Text);
        }
        private void LoadPosition()
        {
            if (positions_comboBox.Text == "")
            {
                MessageBox.Show("Position is empty, bruh");
            }
            else
            {
                string position = func.GetConfigData("config.txt", planets_list[planetIndex] + "SavedPos" + positions_comboBox.Text);
                if (position != "")
                {
                    func.WriteMemory(ip, pid, rac1.player_coords, position);
                }
            }
        }
        private void KillYourself()
        {
            func.WriteMemory(ip, pid, rac1.player_coords + 8, "C2480000");
        }
        private void LoadPlanet()
        {
            int x = planets_comboBox.SelectedIndex; string planet = x.ToString("X2");

            if (lflagresetCb.Checked)
                ResetLevelFlags((uint)x);

            if (resetGB_checkbox.Checked)
                ResetGBs();

            func.WriteMemory(ip, pid, rac1.load_planet, $"00000001000000{planet}");

            //loadPlanetPositions();
        }

        private void ToggleFastLoad(bool toggle)
        {
            if (toggle)
            {
                func.WriteMemory(ip, pid, 0x0DF254, "60000000");
                func.WriteMemory(ip, pid, 0x165450, "2C03FFFF");
            }
            else
            {
                func.WriteMemory(ip, pid, 0x0DF254, "40820188");
                func.WriteMemory(ip, pid, 0x165450, "2c030000");
            }
        }
        private void ForceOkayLoad(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, 0x9645C4, "0000001A0000000400000002");
            ForceLoadTimer.Enabled = false;
        }
        private void ResetGBs()
        {
            string reset = string.Concat(Enumerable.Repeat("00", 80));
            func.WriteMemory(ip, pid, rac1.gold_bolts_array, reset);
        }

        static byte[] junk = new byte[] { 0x63, 0x9a, 0x4d, 0xa2, 0x66, 0x19, 0xaa, 0xff, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        System.IO.MemoryMappedFiles.MemoryMappedFile mmfFile;
        System.IO.MemoryMappedFiles.MemoryMappedViewStream mmfStream;

        private void Form1_Load(object sender, EventArgs e)
        {
            mmfFile = System.IO.MemoryMappedFiles.MemoryMappedFile.CreateNew("racman-autosplitter", 20);
            {
                mmfStream = mmfFile.CreateViewStream();
                {
                    BinaryWriter writer = new BinaryWriter(mmfStream);


                    int destinationPlanetSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, rac1.destination_planet, 4, (value) => {
                        junk[8] = value[0];
                        writer.Seek(0, SeekOrigin.Begin);
                        writer.Write(junk, 0, 20);
                    });

                    int playerStateSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, rac1.player_state, 4, (value) =>
                    {
                        junk[10] = value[0];
                        junk[11] = value[1];
                        writer.Seek(0, SeekOrigin.Begin);
                        writer.Write(junk, 0, 20);
                    });

                    int planetFrameCountSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, 0xA10710, 4, (value) =>
                    {

                        junk[12] = value[0];
                        junk[13] = value[1];
                        junk[14] = value[2];
                        junk[15] = value[3];

                        writer.Seek(0, SeekOrigin.Begin);

                        writer.Write(junk, 0, 20);
                    });

                    int gameStateSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, 0x00A10708, 4, (value) =>
                    {
                        junk[16] = value[0];
                        junk[17] = value[1];
                        junk[18] = value[2];
                        junk[19] = value[3];
                        writer.Seek(0, SeekOrigin.Begin);
                        writer.Write(junk, 0, 20);
                    });

                    int currentPlanetSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, rac1.current_planet, 4, (value) =>
                    {
                        planetIndex = BitConverter.ToInt32(value, 0);
                        writer.Seek(0, SeekOrigin.Begin);
                        junk[9] = value[0];

                        writer.Write(junk, 0, 20);

                        /*this.Invoke(new Action(() => {
                            planets_comboBox.SelectedIndex = planetIndex;
                        }));*/
                    });
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FreezeAmmoCheckbox.Checked = false;
            infHealth.Checked = false;
            FastLoadToggle.Checked = false;
            func.api.Disconnect();
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac1.player_health, "00000003");
        }
        private void infHealth_Checkbox_Changed(object sender, EventArgs e)
        {
            if (infHealth.Checked)
                func.WriteMemory(ip, pid, 0x7F558, "30640000");
            else
                func.WriteMemory(ip, pid, 0x7F558, "30649CE0");
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
        }
        private void InputDisplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            InputDisplay = null;
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
            //thank you username!
            func.WriteMemory(ip, pid, rac1.level_flags + 0x10 * planetIndex, string.Concat(Enumerable.Repeat("00", 0x10)));
            func.WriteMemory(ip, pid, rac1.misc_level_flags + 0x100 * planetIndex, string.Concat(Enumerable.Repeat("00", 0x100)));
            func.WriteMemory(ip, pid, rac1.infobot_flags, func.strarr("00", 0x20));
            func.WriteMemory(ip, pid, rac1.watched_ilms_array, func.strarr("00", 0xc0));


            // Gadget unlocks and more misc level flags
            if (planetIndex == 3) // Kerwan
            {
                func.WriteMemory(ip, pid, 0x96C378, func.strarr("00", 0xF0));
                func.WriteMemory(ip, pid, rac1.unlock_array + 2, "00"); // Heli-Pack
                func.WriteMemory(ip, pid, rac1.unlock_array + 12, "00"); // Swingshot
            }
            if (planetIndex == 4) // Eudora
            {
                func.WriteMemory(ip, pid, 0x96C468, func.strarr("00", 0x40));
                func.WriteMemory(ip, pid, rac1.unlock_array + 9, "00"); // Suck Cannon
            }
            if (planetIndex == 5) // Rilgar
            {
                func.WriteMemory(ip, pid, 0x96C498, func.strarr("00", 0xA0));
            }
            if (planetIndex == 6) // Blarg Station
            {
                func.WriteMemory(ip, pid, rac1.unlock_array + 29, "00");
            }
            if (planetIndex == 8) // Batalia
            {
                func.WriteMemory(ip, pid, 0x96C5A8, func.strarr("00", 0x40));
            }
            if (planetIndex == 9) // Gaspar
            {
                func.WriteMemory(ip, pid, 0x96C5E8, func.strarr("00", 0x20));
                func.WriteMemory(ip, pid, rac1.unlock_array + 7, "00"); // Pilot's Helmet
            }
            if (planetIndex == 10) // Orxon
            {
                func.WriteMemory(ip, pid, rac1.unlock_array + 28, "00"); // Magneboots

                if (Convert.ToInt32(func.ReadMemory(ip, pid, rac1.unlock_array + 6, 1),16) == 1)
                {
                    // Resetting enemies and poki infobot. Need to reset door somehow
                    func.WriteMemory(ip, pid, rac1.level_flags + 0x10 * 10, "0000FF0000FF00000000000000000000000000000000FF");
                    func.WriteMemory(ip, pid, rac1.infobot_flags + 11, "01");
                }
            }
            if (planetIndex == 11) // Pokitaru
            {
                func.WriteMemory(ip, pid, rac1.unlock_array + 3, "00"); // Thruster-Pack
                func.WriteMemory(ip, pid, rac1.unlock_array + 6, "00"); // O2 Mask
            }
        }

        private void ghostCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (func.api is Ratchetron)
            {
                Ratchetron api = (Ratchetron)func.api;
                if (ghostCheckbox.Checked)
                {
                    api.FreezeMemory(pid, rac1.ghost_timer, 10);
                }
                else
                {
                    api.ReleaseSubID(api.MemSubIDForAddress(rac1.ghost_timer));
                }
            }
        }

        private void drekskip_Click(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac1.drek_skip, "01");
        }

        private void CComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CComboCheckBox.Checked)
                InputsTimer.Enabled = true;
            else
                InputsTimer.Enabled = false;
        }

        private void FastLoadToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (FastLoadToggle.Checked)
                ToggleFastLoad(true);
            if (!FastLoadToggle.Checked)
                ToggleFastLoad(false);
        }

        private void FreezeAmmoCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (func.api is Ratchetron)
            {
                Ratchetron api = (Ratchetron)func.api;

                if(FreezeAmmoCheckbox.Checked)
                {
                    func.WriteMemory(ip, pid, 0xAA2DC, "60000000");
                }
                else
                {
                    func.WriteMemory(ip, pid, 0xAA2DC, "7D05392E");
                }

            }
        }
    }
}