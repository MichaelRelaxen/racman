using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace racman
{
    public partial class RAC4Form : Form
    {
        public RAC4Form()
        {
            InitializeComponent();
        }
        public static string ip = AttachPS3Form.ip;
        public static int pid = AttachPS3Form.pid;
        public static uint SaveInfo = 0x11B1BD8;
        public static uint FrameCounter = 0xB3C59C;

        private string Challenge;
        private string tb;

        private void RAC4Form_Load(object sender, EventArgs e)
        {
            tb = func.get_data("https://www.speedrun.com/rac4/individual_levels");
        }

        private string GetWorldRecord(string challenge)
        {

            try
            {
                int ChIndex = tb.IndexOf($"{challenge}</a>");
                string a = tb.Substring(ChIndex + challenge.Length, 512);

                return a.Substring(a.IndexOf("<span class=\"nobr\">") + 19).Replace("<small>", string.Empty).Replace("</small>", string.Empty).Substring(0, 6);
            }
            catch
            {
                return null;
            }
        }
        private void writetext_CheckedChanged(object sender, EventArgs e)
        {

            while (writetext.Checked)
            {
                Application.DoEvents();
                int frames = Convert.ToInt32(func.ReadMemory(ip, pid, FrameCounter, 4), 16);
                TimeSpan t = TimeSpan.FromMilliseconds(frames * 1000 / 60);

                writetext.Text = string.Format("{0:D2}:{1:D2}.{2:D3}", t.Minutes, t.Seconds, t.Milliseconds);

                if (frames < 20)
                {
                    levelinfo.Text = Encoding.ASCII.GetString(func.FromHex(func.ReadMemory(ip, pid, SaveInfo, 64).Split(new string[] { "0A" }, StringSplitOptions.None).First())); // lmfao
                    Challenge = levelinfo.Text.Split(new string[] { " -" }, StringSplitOptions.None).First().TrimEnd();
                    wrtext.Text = $"WR: {GetWorldRecord(Challenge)}";

                    if(ghostcheck.Checked)
                        func.WriteMemory(ip, pid, 0x10D47D0, "3500");
                }
                Thread.Sleep(200);
            }
        }
        private void RAC4Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            writetext.Checked = false;
            Application.Exit();
        }

        private void ghostcheck_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

/*
byte[] pain = Encoding.ASCII.GetBytes(timer);
string final = BitConverter.ToString(pain).Replace("-", string.Empty);
string fuck = "3E8000003E8076270000000B3FFF71B23F5071B2FFFFFFFF00000001800000000a"+final;
func.WriteMemory(ip, pid, 0x1162BE8, fuck);
*/