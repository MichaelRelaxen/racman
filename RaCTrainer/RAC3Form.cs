using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace racman
{
    public partial class RAC3Form : Form
    {
        public RAC3Form()
        {
            InitializeComponent();
            comboBox2.Text = "1";
            comboBox3.Text = "Veldin";

            if (File.Exists(Environment.CurrentDirectory + @"\config.txt"))
            {
                ip = File.ReadAllText(Environment.CurrentDirectory + @"\config.txt");
            }

            Directory.CreateDirectory(func.ebootPath);

            EBOOTs = Directory.GetFiles(func.ebootPath);
            foreach (string i in EBOOTs)
            {
                string b = i.Substring(i.IndexOf("EBOOTs") + 7);
                ebootSwap.Items.Add(b);
            }
        }


        public static string ip = AttachPS3Form.ip;
        public static int pid = AttachPS3Form.pid;

        public static string saved_pos1;
        public static string saved_pos2;
        public static string saved_pos3;

        public static string[] EBOOTs;

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "1")
            {
                saved_pos1 = func.ReadMemory(ip, pid, rac3.Coordinates, 30);
            }
            if (comboBox2.Text == "2")
            {
                saved_pos2 = func.ReadMemory(ip, pid, rac3.Coordinates, 30);
            }
            if (comboBox2.Text == "3")
            {
                saved_pos3 = func.ReadMemory(ip, pid, rac3.Coordinates, 30);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "1")
            {
                func.WriteMemory(ip, pid, rac3.Coordinates, saved_pos1);
            }
            if (comboBox2.Text == "2")
            {
                func.WriteMemory(ip, pid, rac3.Coordinates, saved_pos2);
            }
            if (comboBox2.Text == "3")
            {
                func.WriteMemory(ip, pid, rac3.Coordinates, saved_pos3);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ghostrac_Click(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac3.GhostRatchet, "2710");
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox43_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            func.WriteMemory(ip, pid, rac3.KlunkTuning, "07");
            func.WriteMemory(ip, pid, rac3.KlunkTuning2, "03");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int x = comboBox3.SelectedIndex + 1; string planet = x.ToString("X2");
            func.WriteMemory(ip, pid, rac3.LoadPlanet, $"00000001000000{planet}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;

            ToolTip tt1 = new ToolTip(); tt1.SetToolTip(savepos, "Hotkey: Q");
            ToolTip tt2 = new ToolTip(); tt1.SetToolTip(loadpos, "Hotkey: W");
            ToolTip tt3 = new ToolTip(); tt1.SetToolTip(killyourself, "Hotkey: E");
            ToolTip tt4 = new ToolTip(); tt1.SetToolTip(ebootSwap, "Put EBOOTs into the EBOOT folder.");
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                savepos.PerformClick();
            }

            if (e.KeyCode == Keys.W)
            {
                loadpos.PerformClick();
            }

            if (e.KeyCode == Keys.E)
            {
                killyourself.PerformClick();
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
            string reset = String.Concat(Enumerable.Repeat("00", 128));
            func.WriteMemory(ip, pid, rac3.TitaniumBoltsStart, reset);
            func.WriteMemory(ip, pid, rac3.TitaniumBoltsStart + 128, reset);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string unlock = String.Concat(Enumerable.Repeat("01", 128));
            func.WriteMemory(ip, pid, rac3.TitaniumBoltsStart, unlock);
            func.WriteMemory(ip, pid, rac3.TitaniumBoltsStart + 128, unlock);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string unlock = String.Concat(Enumerable.Repeat("01", 30));
            func.WriteMemory(ip, pid, rac3.SkillPointsStart, unlock);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string reset = String.Concat(Enumerable.Repeat("00", 30));
            func.WriteMemory(ip, pid, rac3.SkillPointsStart, reset);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string refill = String.Concat(Enumerable.Repeat("000001A4", 32));

            func.WriteMemory(ip, pid, 0xDA527C, refill);
            func.WriteMemory(ip, pid, 0xDA527C + 128, refill);
            func.WriteMemory(ip, pid, 0xDA527C + 256, refill);
            func.WriteMemory(ip, pid, 0xDA527C + 384, refill);
            func.WriteMemory(ip, pid, 0xDA527C + 420, refill);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                uint bolts = UInt32.Parse(textBox1.Text);
                func.WriteMemory(ip, pid, rac3.BoltCount, bolts.ToString("X8"));
            }
            catch
            {

            }

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            while (coordscb.Checked == true)
            {
                ShowCoordinates();
                Application.DoEvents();
            }
        }
        private void ShowCoordinates()
        {
            string result = func.ReadMemory(ip, pid, 0xDA2870, 12);

            float x = func.HexToFloat(result.Substring(0, 8));
            float y = func.HexToFloat(result.Substring(8, 8));
            float z = func.HexToFloat(result.Substring(16, 8));

            label4.Text = $"X: {x}\nY: {y}\nZ: {z}\n";
            Thread.Sleep(250);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (coordscb.Checked == true)
            {
                coordscb.Checked = !coordscb.Checked;
            }
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            currentlyDoing.Text = "uploading...";
            string SwapText = ebootSwap.Text;  
            string path = Environment.CurrentDirectory + $@"\EBOOTs\{SwapText}";

            func.UploadFile(ip, path);
            MessageBox.Show("EBOOT successfully swapped to " + path); currentlyDoing.Text = null;
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}