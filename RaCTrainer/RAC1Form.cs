using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace racman
{

    public partial class RAC1Form : Form
    {
        public RAC1Form()
        {
            InitializeComponent();
            comboBox2.Text = "1";
            comboBox3.Text = "Veldin";

            textBox1.KeyDown += TextBox1_KeyDown;
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;

            if (File.Exists(Environment.CurrentDirectory + @"\config.txt"))
            {
                ip = func.GetConfigData("config.txt","ip");
            }

            saved_pos = new string[3];


            for(int i = 0; i < 3; i++)
            {
                string savedPosTest = func.GetConfigData("config.txt", "savedPos" + Convert.ToString(i));
                if (savedPosTest != "")
                {
                    saved_pos[i] = savedPosTest;
                }
            }



        }



        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                try{
                    uint bolts = UInt32.Parse(textBox1.Text);
                    func.WriteMemory(ip, pid, rac1.BoltCount, bolts.ToString("X8"));
                }
                catch{
                    MessageBox.Show("Please enter a number", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        public Form UnlocksWindow; 
        public static string ip = AttachPS3Form.ip;
        public static int pid = AttachPS3Form.pid;


        public int saved_pos_index = 1;
        public static string[] saved_pos;



        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            saved_pos_index = comboBox2.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            saved_pos[saved_pos_index] = func.ReadMemory(ip, pid, rac1.Coordinates, 30);
            func.ChangeFileLines("config.txt", saved_pos[saved_pos_index], "savedPos" + Convert.ToString(saved_pos_index));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (func.GetConfigData("config.txt", "savedPos" + Convert.ToString(saved_pos_index)) != "")
            {
                func.WriteMemory(ip, pid, rac1.Coordinates, saved_pos[saved_pos_index]);
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

            ToolTip tt1 = new ToolTip(); tt1.SetToolTip(savepos, "Hotkey: Shift");
            ToolTip tt2 = new ToolTip(); tt1.SetToolTip(loadpos, "Hotkey: Space");
            ToolTip tt3 = new ToolTip(); tt1.SetToolTip(killyourself, "Hotkey: E");
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

            if(e.KeyCode == Keys.D1)
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
            if(UnlocksWindow == null)
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
    }
}