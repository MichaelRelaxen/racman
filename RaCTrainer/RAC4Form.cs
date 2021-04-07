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
using HtmlAgilityPack;

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

        private string leaderboard;
        private string Challenge;
        private string WorldRecord;
        private string tb;

        private void RAC4Form_Load(object sender, EventArgs e)
        {
            leaderboard = func.get_data("https://www.speedrun.com/rac4/individual_levels");
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(leaderboard);

            var headers = doc.DocumentNode.SelectNodes("//tr/th");
            DataTable table = new DataTable();
            foreach (HtmlNode header in headers)
                table.Columns.Add(header.InnerText);

            foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
                table.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());

            foreach (DataRow dataRow in table.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    tb += item;
                    tb = tb.Replace("&#039;", "'").Replace("\t", "").Replace("\n", "-");
                }
            }
        }
        private string GetWorldRecord(string challenge) //scuffed because i couldnt work the api out :/ whatever
        {

            try
            {
                var ChIndex = tb.IndexOf(challenge);
                WorldRecord = tb.Substring(ChIndex + challenge.Length + 1, 6);
                return WorldRecord;
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
                TimeSpan t = TimeSpan.FromMilliseconds(frames * 1000/60);

                writetext.Text = string.Format("{0:D2}:{1:D2}.{2:D3}", t.Minutes, t.Seconds, t.Milliseconds);

                if (frames < 20)
                {
                    levelinfo.Text = Encoding.ASCII.GetString(func.FromHex(func.ReadMemory(ip, pid, SaveInfo, 64).Split(new string[] { "0A" }, StringSplitOptions.None).First())); // lmfao
                    Challenge = levelinfo.Text.Split(new string[] { " -" }, StringSplitOptions.None).First().TrimEnd();
                    wrtext.Text = $"WR: {GetWorldRecord(Challenge)}";
                }
                Thread.Sleep(200);
            }
        }
        private void RAC4Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            writetext.Checked = !writetext.Checked;
        }
    }
}

/*
byte[] pain = Encoding.ASCII.GetBytes(timer);
string final = BitConverter.ToString(pain).Replace("-", string.Empty);
string fuck = "3E8000003E8076270000000B3FFF71B23F5071B2FFFFFFFF00000001800000000a"+final;
func.WriteMemory(ip, pid, 0x1162BE8, fuck);
*/