using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace racman
{
    public partial class AttachPS3Form : Form
    {
        bool useOldAPI = false;

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

            ConfigureCombos.GetCombos();
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

            func.api = this.useOldAPI ? (IPS3API)new WebMAN(ip) : (IPS3API)new Ratchetron(ip);

            if (!this.useOldAPI)
            {
                if (!func.PrepareRatchetron(ip))
                {
                    return;
                }
            }

            if (!func.api.Connect())
            {
                MessageBox.Show("Couldn't connect to the game.");
                return;
            }

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
                Hide();
                func.api.Notify("RaCMAN connected!");
                RAC1Form rac1 = new RAC1Form(new rac1((Ratchetron)func.api))
                {
                    TopMost = true
                };
                rac1.ShowDialog();
            }
            else if (game == "NPEA00386")
            {
                Hide();
                func.api.Notify("RaCMAN connected to rac2");
                RAC2Form rac2 = new RAC2Form(new rac2((Ratchetron)func.api));
                {
                    TopMost = true;
                }
                rac2.ShowDialog();
            }
            else if (game == "NPEA00387")
            {
                Hide();
                func.api.Notify("RaCMAN connected!");
                RAC3Form rac3 = new RAC3Form(new rac3((Ratchetron)func.api))
                {
                    TopMost = true
                };
                rac3.ShowDialog();
            }
            else if (game == "NPEA00423")
            {
                Hide();
                func.api.Notify("RaCMAN connected!");
                RAC4Form rac4 = new RAC4Form(new rac4((Ratchetron)func.api))
                {
                    TopMost = true
                };
                rac4.ShowDialog();
            }
            else if (game == "NPUA80966" || game == "NPEA00453" || game == "BCES00511" || game == "BCES00726")
            {
                Hide();
                func.api.Notify("RaCMAN connected!");
                ACITForm acit = new ACITForm(new acit((Ratchetron)func.api))
                {
                    TopMost = true
                };
                acit.ShowDialog();
            }
            else
            {
                MessageBox.Show("Game isn't running or isn't supported yet.");
            }
        }

        private void currentVerLabel_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.useOldAPI = ((CheckBox)sender).Checked;
        }
    }
}
