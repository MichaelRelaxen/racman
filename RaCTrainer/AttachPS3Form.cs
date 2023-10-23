using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using racman.Memory;

namespace racman
{
    public partial class AttachPS3Form : Form
    {
        bool useOldAPI = false;

        public static RacManConsole console;

        public static RacmanScripting scripting;

        static ModLoaderForm modLoaderForm;

        public AttachPS3Form()
        {
            InitializeComponent();

            RacManConsole.RedirectOutput();

            console = new RacManConsole();
            scripting = new RacmanScripting();

            currentVerLabel.Text = "v" + Assembly.GetEntryAssembly().GetName().Version.ToString(3);

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
        public static string gameName;

        private int pleaseStartTheGame = 1;

        private string[] startGameText = {
                "You need to start the game first." ,
                "Bro, you need to start the game first.",
                "You're not in a game. You need to be in a game to attach RaCMAN.",
                "Are you even reading the error messages? Please start the game.",
                "What the fuck? Can you please start the game before hitting \"Attach\"?",
                "???",
                "Fr, start the game on your PS3.",
                "Why? What's your problem?",
                "Fuck you",
                "This is getting ridiculous.",
                "I'm begging you, start the game.",
        };


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

            Attach(func.api);
        }

        private void Attach(IPS3API api)
        {
            if (!api.Connect())
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

            if (pid == 0)
            {
                MessageBox.Show(startGameText[pleaseStartTheGame-1], "Game is not running");

                if (pleaseStartTheGame < startGameText.Length)
                {
                    pleaseStartTheGame += 1;
                }

                return;
            }

            if (game == "BCES01503")
            {
                var diskGameSelector = new DiskGameSelector();
                if (diskGameSelector.ShowDialog() == DialogResult.OK)
                {
                    switch (diskGameSelector.GetSelectedVersion())
                    {
                        case 0:
                            game = "NPEA00385"; // RAC 1
                            gameName = "RAC 1";
                            break;
                        case 1:
                            game = "NPEA00386"; // RAC 2
                            gameName = "RAC 2";
                            break;
                        case 2:
                            game = "NPEA00387"; // RAC 3
                            gameName = "RAC 3";
                            break;  
                    }
                } else
                {
                    return;
                }
            } // if disk version was found, the following code can be executed as if this check never happened

            if (game == "NPEA00385")
            {
                Hide();
                func.api.Notify("RaCMAN connected!");
                RAC1Form rac1 = new RAC1Form(new rac1(func.api));
                gameName = "RAC 1";
                rac1.ShowDialog();
            }
            else if (game == "NPEA00386")
            {
                Hide();
                func.api.Notify("RaCMAN connected!");
                RAC2Form rac2 = new RAC2Form(new rac2(func.api));
                gameName = "RAC 2";
                rac2.ShowDialog();
            }
            else if (game == "NPJA40002")
            {
                Hide();
                func.api.Notify("RaCMAN connected!");
                RAC2JPForm rac2jp = new RAC2JPForm(new rac2jp(func.api));
                gameName = "RAC 2 (JP)";
                rac2jp.ShowDialog();
            }
            else if (game == "NPEA00387")
            {
                Hide();
                func.api.Notify("RaCMAN connected!");
                RAC3Form rac3 = new RAC3Form(new rac3(func.api));
                gameName = "RAC 3";
                rac3.ShowDialog();
            }
            else if (game == "NPEA00423")
            {
                Hide();
                func.api.Notify("RaCMAN connected!");
                RAC4Form rac4 = new RAC4Form(new rac4(func.api));
                gameName = "RAC 4";
                rac4.ShowDialog();
            }
            else if (game == "NPUA80966" || game == "NPEA00453" || game == "BCES00511" || game == "BCES00726")
            {
                Hide();
                func.api.Notify("RaCMAN connected!");
                ACITForm acit = new ACITForm(new acit(func.api));
                gameName = "ACIT";
                acit.ShowDialog();
            }
            else
            {
                if (game.Length > 0)
                {
                    MessageBox.Show($"{game} isn't supported yet. You can still apply mods if you have any.");

                    if ((Application.OpenForms["ModLoaderForm"] as ModLoaderForm) != null)
                    {
                        modLoaderForm.Activate();
                    }
                    else
                    {
                        modLoaderForm = new ModLoaderForm();
                        modLoaderForm.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Game isn't running or isn't supported yet.");
                }
            }
        }

        private void currentVerLabel_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.useOldAPI = ((CheckBox)sender).Checked;
        }

        private void IPTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                attachButton_Click(IPTextBox, e);
            }
        }

        private void AttachRPCS3Button_Click(object sender, EventArgs e)
        {
            func.api = new RPCS3("FUCK");

            Attach(func.api);
        }
    }
}
