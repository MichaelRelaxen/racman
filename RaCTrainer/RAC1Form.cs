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
            comboBox2.Text = "1";
            comboBox3.Text = "Veldin";

            if (File.Exists(Environment.CurrentDirectory + @"\config.txt"))
            {
                ip = File.ReadAllText(Environment.CurrentDirectory + @"\config.txt");
            }
        }


        public static string ip = AttachPS3Form.ip;
        public static int pid = AttachPS3Form.pid;

        public static string saved_pos1;
        public static string saved_pos2;
        public static string saved_pos3;

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "1")
            {
                saved_pos1 = func.ReadMemory(ip, pid, rac1.Coordinates, 30);
            }
            if (comboBox2.Text == "2")
            {
                saved_pos2 = func.ReadMemory(ip, pid, rac1.Coordinates, 30);
            }
            if (comboBox2.Text == "3")
            {
                saved_pos3 = func.ReadMemory(ip, pid, rac1.Coordinates, 30);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "1")
            {
                func.WriteMemory(ip, pid, rac1.Coordinates, saved_pos1);
            }
            if (comboBox2.Text == "2")
            {
                func.WriteMemory(ip, pid, rac1.Coordinates, saved_pos2);
            }
            if (comboBox2.Text == "3")
            {
                func.WriteMemory(ip, pid, rac1.Coordinates, saved_pos3);
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
            func.WriteMemory(ip, pid, rac1.GhostRatchet, "2710");
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
            func.WriteMemory(ip, pid, rac1.Coordinates + 8, "C2480000");
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            int x = comboBox3.SelectedIndex; string planet = x.ToString("X2");
            func.WriteMemory(ip, pid, rac1.LoadPlanet, $"00000001000000{planet}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;

            ToolTip tt1 = new ToolTip(); tt1.SetToolTip(savepos, "Hotkey: Q");
            ToolTip tt2 = new ToolTip(); tt1.SetToolTip(loadpos, "Hotkey: W");
            ToolTip tt3 = new ToolTip(); tt1.SetToolTip(killyourself, "Hotkey: E");
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

        private void tbsreset_Click(object sender, EventArgs e)
        {
            string reset = String.Concat(Enumerable.Repeat("00", 128));
            func.WriteMemory(ip, pid, rac1.TitaniumBoltsStart, reset);
            func.WriteMemory(ip, pid, rac1.TitaniumBoltsStart + 128, reset);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string unlock = String.Concat(Enumerable.Repeat("01", 128));
            func.WriteMemory(ip, pid, rac1.TitaniumBoltsStart, unlock);
            func.WriteMemory(ip, pid, rac1.TitaniumBoltsStart + 128, unlock);
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
            uint bolts = UInt32.Parse(textBox1.Text);
            func.WriteMemory(ip, pid, rac1.BoltCount, bolts.ToString("X8"));
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
    }
}