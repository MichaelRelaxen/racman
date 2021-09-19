using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Ratchetron
{
    public partial class RAC3Form : Form
    {
        public System.Windows.Forms.Timer ohkoTimer;

        public RAC3Form()
        {
            InitializeComponent();
            positions_ComboBox.Text = "1";
            planets_comboBox.Text = "Veldin";
            textBox1.KeyDown += TextBox1_KeyDown;

            planets_list = new string[] {
            "Rac3Veldin",
            "Florana",
            "StarshipPhoenix",
            "Marcadia",
            "Daxx",
            "PhoenixRescue",
            "AnnihilationNation",
            "Aquatos",
            "Tyhrranosis",
            "ZeldrinStarport",
            "ObaniGemini",
            "BlackwaterCity",
            "Holostar",
            "Koros",
            "Unknown",
            "Metropolis",
            "CrashSite",
            "Aridia",
            "QwarksHideout",
            "LaunchSite",
            "ObaniDraco",
            "CommandCenter",
            "Holostar2",
            "InsomniacMuseum",
            "Unknown2",
            "MetropolisRangers",
            "AquatosClank",
            "AquatosSewers",
            "TyhrranosisRangers",
            "VidComic6",
            "VidComic1",
            "VidComic4",
            "VidComic2",
            "VidComic3",
            "VidComic5",
            "VidComic1SpecialEdition"
            };
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    uint bolts = uint.Parse(textBox1.Text);
                    func.WriteMemory(ip, pid, rac3.bolt_count, bolts.ToString("X8"));
                }
                catch
                {
                    MessageBox.Show("Please enter a number", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public Form InputDisplay;
        public static string ip = AttachPS3Form.ip;
        public static int pid = AttachPS3Form.pid;
        readonly string[] planets_list;

        public int saved_pos_index = 1;
        public string current_planet;


        private void loadPosButton_Click(object sender, EventArgs e)
        {
            string position = func.GetConfigData("config.txt", planets_list[getCurrentPlanetIndex()] + "SavedPos" + Convert.ToString(saved_pos_index));
            if (position != "")
            {
                func.WriteMemory(ip, pid, rac3.player_coords, position);
            }
        }
        private void savePosButton_Click(object sender, EventArgs e)
        {
            string position = func.ReadMemory(ip, pid, rac3.player_coords, 30);
            func.ChangeFileLines("config.txt", position, planets_list[getCurrentPlanetIndex()] + "SavedPos" + Convert.ToString(saved_pos_index));
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ghostrac_Click(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac3.ghost_timer, "2710");
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void killyourself_Click(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac3.player_coords + 8, "C2480000");
        }

        private void positions_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            saved_pos_index = positions_ComboBox.SelectedIndex;
        }

        private void planets_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac3.klunk_tuning_var1, "07");
            func.WriteMemory(ip, pid, rac3.klunk_tuning_var2, "03");
            func.WriteMemory(ip, pid, rac3.vid_comic_menu, "00000002");
            func.WriteMemory(ip, pid, rac3.cc_help_text, "00000001");
        }

        private void loadPlanetButton_Click(object sender, EventArgs e)
        {
            int x = planets_comboBox.SelectedIndex + 1; string planet = x.ToString("X2");
            func.WriteMemory(ip, pid, rac3.force_load_planet, $"00000001000000{planet}");

            FastLoads();

        }

        private void FastLoads()
        {
            int x = planets_comboBox.SelectedIndex + 1; string planet = x.ToString("X2");

            func.WriteMemory(ip, pid, rac3.fast_load, "00000003"); // Force third load screen
            Thread.Sleep(200);
            if (x != 26 || x != 20 || x != 29) // Launch Site, Metro Rangers, Tyhrranosis Rangers
                func.WriteMemory(ip, pid, rac3.fast_load2, "0101"); // Make load one seg
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        static bool qsbool = false;
        private void button9_Click(object sender, EventArgs e)
        {
            qsbool = !qsbool;
            if (qsbool == true) { func.WriteMemory(ip, pid, rac3.quick_select_pause, "01"); }
            if (qsbool == false) { func.WriteMemory(ip, pid, rac3.quick_select_pause, "00"); }
        }

        private void tbsreset_Click(object sender, EventArgs e)
        {
            string reset = string.Concat(Enumerable.Repeat("00", 128));
            func.WriteMemory(ip, pid, rac3.titanium_bolts_array, reset);
            func.WriteMemory(ip, pid, rac3.titanium_bolts_array + 128, reset);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string unlock = string.Concat(Enumerable.Repeat("01", 128));
            func.WriteMemory(ip, pid, rac3.titanium_bolts_array, unlock);
            func.WriteMemory(ip, pid, rac3.titanium_bolts_array + 128, unlock);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string unlock = string.Concat(Enumerable.Repeat("01", 30));
            func.WriteMemory(ip, pid, rac3.skill_points_array, unlock);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string reset = string.Concat(Enumerable.Repeat("00", 30));
            func.WriteMemory(ip, pid, rac3.skill_points_array, reset);
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


        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string armor = comboBox4.SelectedIndex.ToString("X4");
            func.WriteMemory(ip, pid, rac3.current_armor, armor);
        }

        private void numericUpDown22_ValueChanged(object sender, EventArgs e)
        {
            string chmod = Convert.ToInt32(numericUpDown22.Value).ToString("X4");
            func.WriteMemory(ip, pid, rac3.challenge_mode, chmod);
        }

        private void checkBox41_CheckedChanged(object sender, EventArgs e)
        {
            if (vc1.Checked)
            {
                func.WriteMemory(ip, pid, rac3.vid_comics, "01");
            }
            else
            {
                func.WriteMemory(ip, pid, rac3.vid_comics, "00");
            }
        }

        private void checkBox42_CheckedChanged(object sender, EventArgs e)
        {
            if (vc2.Checked)
            {
                func.WriteMemory(ip, pid, rac3.vid_comics + 1, "01");
            }
            else
            {
                func.WriteMemory(ip, pid, rac3.vid_comics + 1, "00");
            }
        }

        private void checkBox43_CheckedChanged_1(object sender, EventArgs e)
        {

            if (vc3.Checked)
            {
                func.WriteMemory(ip, pid, rac3.vid_comics + 2, "01");
            }
            else
            {
                func.WriteMemory(ip, pid, rac3.vid_comics + 2, "00");
            }
        }

        private void checkBox44_CheckedChanged(object sender, EventArgs e)
        {
            if (vc4.Checked)
            {
                func.WriteMemory(ip, pid, rac3.vid_comics + 3, "01");
            }
            else
            {
                func.WriteMemory(ip, pid, rac3.vid_comics + 3, "00");
            }
        }

        private void checkBox45_CheckedChanged(object sender, EventArgs e)
        {
            if (vc5.Checked)
            {
                func.WriteMemory(ip, pid, rac3.vid_comics + 4, "01");
            }
            else
            {
                func.WriteMemory(ip, pid, rac3.vid_comics + 4, "00");
            }
        }
        private void ShowCoordinates(object sender, EventArgs e)
        {
            string result = func.ReadMemory(ip, pid, 0xDA2870, 12);

            float x = func.HexToFloat(result.Substring(0, 8));
            float y = func.HexToFloat(result.Substring(8, 8));
            float z = func.HexToFloat(result.Substring(16, 8));

            label4.Text = $"X: {x}\nY: {y}\nZ: {z}\n";
        }
        private Timer timer = new Timer();
        public void Init_timer()
        {
            timer.Interval = 250;
            timer.Tick += new EventHandler(ShowCoordinates);
            timer.Enabled = true;
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Tunes klunk, positions vid comic ingame menu to 'Ende' (end) and\n removes help desk message in command center at the thyrra button.", button6);
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void ghostrac_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Enables ghost ratchet.", ghostrac);
        }

        private void inputdisplay_Click(object sender, EventArgs e)
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

        private void OHKOCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var isChecked = ((CheckBox)sender).Checked;

            if (!isChecked && ohkoTimer != null)
            {
                ohkoTimer.Enabled = false;
            } else
            {
                ohkoTimer = new System.Windows.Forms.Timer();
                ohkoTimer.Interval = (int)16.66667;
                ohkoTimer.Tick += new EventHandler(OHKOTimer_Tick);
                ohkoTimer.Start();
            }
        }

        public void OHKOTimer_Tick(object sender, EventArgs e)
        {
            var health = Convert.ToInt32(func.ReadMemory(AttachPS3Form.ip, AttachPS3Form.pid, rac3.health, 4), 16);

            if (health > 1) {
                func.api.WriteMemory(AttachPS3Form.pid, rac3.health, 4, new byte[] { 0x00, 0x00, 0x00, 0x01});
                //func.WriteMemory(AttachPS3Form.ip, AttachPS3Form.pid, rac3.health, "01");
            }
        }

        private void switchGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClosing -= Form1_FormClosing;
            Program.AttachPS3Form.Show();
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (coordscb.Checked)
            {
                Init_timer();
            }
            if (!coordscb.Checked)
                timer.Enabled = false;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (coordscb.Checked == true)
            {
                coordscb.Checked = !coordscb.Checked;
            }
            func.api.Disconnect();
            Application.Exit();
        }

        public static int getCurrentPlanetIndex()
        {
            string planet = func.ReadMemory(ip, pid, rac3.current_planet, 4);
            int index = int.Parse(planet, System.Globalization.NumberStyles.HexNumber) - 1;
            return index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FastLoads();
        }
    }
}