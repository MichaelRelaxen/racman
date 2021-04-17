using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace racman
{
    public partial class RAC3Form : Form
    {
        public RAC3Form()
        {
            InitializeComponent();
            positions_ComboBox.Text = "1";
            planets_comboBox.Text = "Veldin";

            if (File.Exists(Environment.CurrentDirectory + @"\config.txt"))
            {
                ip = func.GetConfigData("config.txt", "ip");
            }
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
            "Crash Site",
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

            Directory.CreateDirectory(func.ebootPath);

            EBOOTs = Directory.GetFiles(func.ebootPath);
            foreach (string i in EBOOTs)
            {
                string b = i.Substring(i.IndexOf("EBOOTs") + 7);
                ebootSwap.Items.Add(b);
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    uint bolts = uint.Parse(textBox1.Text);
                    func.WriteMemory(ip, pid, rac3.BoltCount, bolts.ToString("X8"));
                }
                catch
                {
                    MessageBox.Show("Please enter a number", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static string ip = AttachPS3Form.ip;
        public static int pid = AttachPS3Form.pid;
        readonly string[] planets_list;

        public int saved_pos_index = 1;
        public string current_planet;

        public static string[] EBOOTs;

        private void loadPosButton_Click(object sender, EventArgs e)
        {
            string position = func.GetConfigData("config.txt", planets_list[getCurrentPlanetIndex()] + "SavedPos" + Convert.ToString(saved_pos_index));
            if (position != "")
            {
                func.WriteMemory(ip, pid, rac3.Coordinates, position);
            }
        }
        private void savePosButton_Click(object sender, EventArgs e)
        {
            string position = func.ReadMemory(ip, pid, rac3.Coordinates, 30);
            func.ChangeFileLines("config.txt", position, planets_list[getCurrentPlanetIndex()] + "SavedPos" + Convert.ToString(saved_pos_index));
        }

        private void eboots_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ghostrac_Click(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac3.GhostRatchet, "2710");
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void killyourself_Click(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac3.Coordinates + 8, "C2480000");
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
            func.WriteMemory(ip, pid, rac3.KlunkTuning, "07");
            func.WriteMemory(ip, pid, rac3.KlunkTuning2, "03");
        }

        private void loadPlanetButton_Click(object sender, EventArgs e)
        {
            int x = planets_comboBox.SelectedIndex + 1; string planet = x.ToString("X2");
            func.WriteMemory(ip, pid, rac3.LoadPlanet, $"00000001000000{planet}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KeyPreview = true;

            ToolTip tt1 = new ToolTip(); tt1.SetToolTip(savepos, "Hotkey: Shift");
            ToolTip tt2 = new ToolTip(); tt1.SetToolTip(loadpos, "Hotkey: Space");
            ToolTip tt3 = new ToolTip(); tt1.SetToolTip(killyourself, "Hotkey: E");
            ToolTip tt4 = new ToolTip(); tt1.SetToolTip(ebootSwap, "Put EBOOTs into the EBOOT folder.");
        }

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

        static bool qsbool = false;
        private void button9_Click(object sender, EventArgs e)
        {
            qsbool = !qsbool;
            if (qsbool == true) { func.WriteMemory(ip, pid, rac3.QuickSelectPause, "01"); }
            if (qsbool == false) { func.WriteMemory(ip, pid, rac3.QuickSelectPause, "00"); }
        }

        private void tbsreset_Click(object sender, EventArgs e)
        {
            string reset = string.Concat(Enumerable.Repeat("00", 128));
            func.WriteMemory(ip, pid, rac3.TitaniumBoltsStart, reset);
            func.WriteMemory(ip, pid, rac3.TitaniumBoltsStart + 128, reset);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string unlock = string.Concat(Enumerable.Repeat("01", 128));
            func.WriteMemory(ip, pid, rac3.TitaniumBoltsStart, unlock);
            func.WriteMemory(ip, pid, rac3.TitaniumBoltsStart + 128, unlock);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string unlock = string.Concat(Enumerable.Repeat("01", 30));
            func.WriteMemory(ip, pid, rac3.SkillPointsStart, unlock);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string reset = string.Concat(Enumerable.Repeat("00", 30));
            func.WriteMemory(ip, pid, rac3.SkillPointsStart, reset);
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
            func.WriteMemory(ip, pid, rac3.CurrentArmor, armor);
        }

        private void numericUpDown22_ValueChanged(object sender, EventArgs e)
        {
            string chmod = Convert.ToInt32(numericUpDown22.Value).ToString("X4");
            func.WriteMemory(ip, pid, rac3.ChallengeMode, chmod);
        }

        private void checkBox41_CheckedChanged(object sender, EventArgs e)
        {
            if (vc1.Checked)
            {
                func.WriteMemory(ip, pid, rac3.VidComics, "01");
            }
            else
            {
                func.WriteMemory(ip, pid, rac3.VidComics, "00");
            }
        }

        private void checkBox42_CheckedChanged(object sender, EventArgs e)
        {
            if (vc2.Checked)
            {
                func.WriteMemory(ip, pid, rac3.VidComics + 1, "01");
            }
            else
            {
                func.WriteMemory(ip, pid, rac3.VidComics + 1, "00");
            }
        }

        private void checkBox43_CheckedChanged_1(object sender, EventArgs e)
        {

            if (vc3.Checked)
            {
                func.WriteMemory(ip, pid, rac3.VidComics + 2, "01");
            }
            else
            {
                func.WriteMemory(ip, pid, rac3.VidComics + 2, "00");
            }
        }

        private void checkBox44_CheckedChanged(object sender, EventArgs e)
        {
            if (vc4.Checked)
            {
                func.WriteMemory(ip, pid, rac3.VidComics + 3, "01");
            }
            else
            {
                func.WriteMemory(ip, pid, rac3.VidComics + 3, "00");
            }
        }

        private void checkBox45_CheckedChanged(object sender, EventArgs e)
        {
            if (vc5.Checked)
            {
                func.WriteMemory(ip, pid, rac3.VidComics + 4, "01");
            }
            else
            {
                func.WriteMemory(ip, pid, rac3.VidComics + 4, "00");
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
            Application.Exit();
        }

        private void eboots_combobox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            currentlyDoing.Text = "uploading...";
            string SwapText = ebootSwap.Text;
            string path = Environment.CurrentDirectory + $@"\EBOOTs\{SwapText}";

            func.UploadFile(ip, path);
            MessageBox.Show("EBOOT successfully swapped to " + path); currentlyDoing.Text = null;
        }

        public static int getCurrentPlanetIndex()
        {
            string planet = func.ReadMemory(ip, pid, rac3.CurrentPlanet, 4);
            int index = int.Parse(planet, System.Globalization.NumberStyles.HexNumber) - 1;
            return index;
        }
    }
}