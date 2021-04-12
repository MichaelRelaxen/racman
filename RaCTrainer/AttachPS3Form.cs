using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace racman
{
    public partial class AttachPS3Form : Form
    {
        public AttachPS3Form()
        {
            InitializeComponent();

            if (File.Exists(Environment.CurrentDirectory + @"\config.txt"))
            {
                ip = func.GetConfigData("config.txt", "ip");//ip = File.ReadAllText(Environment.CurrentDirectory + @"\config.txt");
            }
            else
            {
                File.Create("config.txt");
            }
            IPTextBox.Text = ip;
        }

        public static string ip;
        public static int pid;
        public static string game;


        private void AttachPS3Form_Load(object sender, EventArgs e)
        {

        }

        private void attachButton_Click(object sender, EventArgs e)
        {
            ip = IPTextBox.Text;
            //File.WriteAllText(Environment.CurrentDirectory + @"\config.txt", ip);
            func.ChangeFileLines("config.txt", Convert.ToString(ip), "ip");
            try
            {
                game = func.current_game(ip);
                pid = func.current_pid(ip);
            }
            catch
            {
                MessageBox.Show("invalid ip/web exception.");
            }

            if (game == "NPEA00385")
            {
                RAC1Form rac1 = new RAC1Form();
                rac1.TopMost = true;
                rac1.ShowDialog();
            }
            else if (game == "NPEA00387")
            {
                RAC3Form rac3 = new RAC3Form();
                rac3.TopMost = true;
                rac3.ShowDialog();
            }
            else if (game == "NPEA00423")
            {
                RAC4Form rac4 = new RAC4Form();
                rac4.TopMost = true;
                rac4.ShowDialog();
            }
            else
            {
                MessageBox.Show("Game isn't running or isn't supported yet.");
            }
        }
    }
}
