using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace racman
{
    public partial class AttachPS3Form : Form
    {
        public AttachPS3Form()
        {
            InitializeComponent();

            currentVerLabel.Text = "v" + Assembly.GetEntryAssembly().GetName().Version;

            if (File.Exists(Environment.CurrentDirectory + @"\config.txt"))
            {
                ip = func.GetConfigData("config.txt", "ip");//ip = File.ReadAllText(Environment.CurrentDirectory + @"\config.txt");
            }
            else
            {
                var config = File.Create("config.txt");
                config.Close();
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

            if (game == "NPEA00385") // I'm sure there's a way better way of doing this.
            {
                Hide();
                RAC1Form rac1 = new RAC1Form
                {
                    TopMost = true
                };
                rac1.ShowDialog();
            }
            else if (game == "NPEA00387")
            {
                Hide();
                RAC3Form rac3 = new RAC3Form
                {
                    TopMost = true
                };
                rac3.ShowDialog();
            }
            else if (game == "NPEA00423")
            {
                Hide();
                RAC4Form rac4 = new RAC4Form
                {
                    TopMost = true
                };
                rac4.ShowDialog();
            }
            else
            {
                MessageBox.Show("Game isn't running or isn't supported yet.");
            }
        }

        private void currentVerLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
