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
using System.Net.NetworkInformation;

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
        public static string game_id;
        public string ps3path => $"ftp://{AttachPS3Form.ip}:21/dev_hdd0/game/{game_id}/USRDIR/tempsave";
        public string path;
        public string filename;

        public static uint api_savefile;
        public static uint api_loadfile;

        public static void Initialize(uint _api_savefile, uint _api_loadfile, string _game_id)
        {
            api_savefile = _api_savefile;
            api_loadfile = _api_loadfile;
            game_id = _game_id;
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            if(string.Empty != nameinput.Text)
            {
                savelist.Items.Add(nameinput.Text);
                savelist.SelectedIndex = savelist.Items.Count - 1;
                filename = path + $"/{savelist.SelectedItem.ToString()}";
                api.WriteMemory(AttachPS3Form.pid, api_savefile, new byte[] { 0x01 });
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
            if (savelist.SelectedItem != null)
            {
                filename = path + $"/{savelist.SelectedItem.ToString()}";
                api.WriteFile($"/dev_hdd0/game/{game_id}/USRDIR/tempsave", filename);
                api.WriteMemory(AttachPS3Form.pid, api_loadfile, new byte[] { 0x01 });
            }
            else
            {
                MessageBox.Show("No savefile selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
