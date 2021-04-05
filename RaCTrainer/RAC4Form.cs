using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace racman
{
    public partial class RAC4Form : Form
    {
        public RAC4Form()
        {
            InitializeComponent();
        }

        private void RAC4Form_Load(object sender, EventArgs e)
        {

        }

        public static string ip = AttachPS3Form.ip;
        public static int pid = AttachPS3Form.pid;

        private void writetext_CheckedChanged(object sender, EventArgs e)
        {
            while (writetext.Checked)
            {
                Application.DoEvents();
                int frames = Convert.ToInt32(func.ReadMemory(ip, pid, 0x9D2584, 4), 16)+1;
                TimeSpan t = TimeSpan.FromMilliseconds(frames * 1000/60);
                writetext.Text = string.Format("{0:D2}:{1:D2}.{2:D3}", t.Minutes, t.Seconds, t.Milliseconds);

                // byte[] pain = Encoding.ASCII.GetBytes(timer);
                // string final = BitConverter.ToString(pain).Replace("-", string.Empty);
                // string fuck = "3E8000003E8076270000000B3FFF71B23F5071B2FFFFFFFF00000001800000000a"+final;
                // func.WriteMemory(ip, pid, 0x1162BE8, fuck);

                Thread.Sleep(175);


            }

        }

        private void RAC4Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            writetext.Checked = !writetext.Checked;
        }
    }
}
