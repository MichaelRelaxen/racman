using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace racman
{
    public partial class SavefileLoader : Form
    {
        public rac3 game;
        private static IPS3API api = func.api;
        public static WebClient Client = new WebClient();
        public SavefileLoader()
        {
            InitializeComponent();
            catdropdown.SelectedIndex = 0;
        }

        public string ps3path = $"ftp://{AttachPS3Form.ip}:21/dev_hdd0/game/NPEA00387/USRDIR/tempsave";
        public string path;
        public string filename;

        private void savebutton_Click(object sender, EventArgs e)
        {
            if(string.Empty != nameinput.Text)
            {
                savelist.Items.Add(nameinput.Text);
                savelist.SelectedIndex = savelist.Items.Count - 1;
                filename = path + $"/{savelist.SelectedItem.ToString()}";
                api.WriteMemory(AttachPS3Form.pid, 0xD9FF03, new byte[] { 0x01 });
                System.Threading.Thread.Sleep(2000);
                try
                {
                    Client.DownloadFile(ps3path, filename);
                }
                catch
                {
                    System.Threading.Thread.Sleep(2020);
                    Client.DownloadFile(ps3path, filename);
                }
            }
        }

        private void loadbutton_Click(object sender, EventArgs e)
        {
            //should load Highlighted File from savelist
            filename = path + $"/{savelist.SelectedItem.ToString()}";
            api.WriteFile("/dev_hdd0/game/NPEA00387/USRDIR/tempsave", filename);
            api.WriteMemory(AttachPS3Form.pid, 0xD9FF04, new byte[] { 0x01 });
        }

        private void catdropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            path = Environment.CurrentDirectory + $"/savefiles/{AttachPS3Form.game}/{catdropdown.Text}";
            Directory.CreateDirectory(path);
            savelist.Items.Clear();
            foreach(string file in Directory.GetFiles(path))
            {
                savelist.Items.Add(Path.GetFileName(file));
            }
        }

        private void nameinput_Click(object sender, EventArgs e)
        {
            nameinput.Text = "";
        }

        private void SavefileLoader_Load(object sender, EventArgs e)
        {

        }
    }
}
