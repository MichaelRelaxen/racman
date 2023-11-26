using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace racman
{
    public partial class RAC4Form : Form
    {
        public rac4 game;
        private static ModLoaderForm modLoaderForm;
        private AutosplitterHelper autosplitterHelper;

        public RAC4Form(rac4 game)
        {
            this.game = game;

            game.SetupInputDisplayMemorySubs();


            InitializeComponent();
            bolts_textBox.KeyDown += bolts_TextBox_KeyDown;
            AutosplitterCheckbox.Checked = true;
        }

        public Form InputDisplay;
        public static string ip = AttachPS3Form.ip;
        public static int pid = AttachPS3Form.pid;
        public static uint SaveInfo = 0x11B1BD8;
        public static uint FrameCounter = 0xB3C59C;
        public static uint GhostRatchet = 0x10D47D0;

        private string Challenge;
        private string src;

        private void RAC4Form_Load(object sender, EventArgs e)
        {
            List<string> x = new List<string>();
            for (int i = 0; i <= 80; i = i + 20)
                x.Add(func.client.DownloadString("https://www.speedrun.com/api/v1/games/k6qwo06g/records?top=1&offset="+i));
            src = string.Join(",", x);
        }

        private string GetWorldRecord(string challenge)
        {
            string a = src.Substring(src.IndexOf($"{Challenge.Replace(' ', '_')}#Solo"));
            string b = a.Substring(a.IndexOf("realtime_t") + 12,3).Trim(new char[] { ',', '"' }); // ye
            TimeSpan t = TimeSpan.FromSeconds(int.Parse(b));

            return string.Format("{0:D1}:{1:D2}", t.Minutes, t.Seconds);
        }

        private Timer timer = new Timer();

        public void Init_timer()
        {
            timer.Interval = (int)16.66667;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;
        }
        private void writetext_CheckedChanged(object sender, EventArgs e)
        {

            if (writetext.Checked)
            {
                Init_timer();
            }
            if (!writetext.Checked)
                timer.Enabled = false;
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            int frames = Convert.ToInt32(func.ReadMemory(ip, pid, FrameCounter, 4), 16);
            TimeSpan t = TimeSpan.FromMilliseconds(frames * 1000 / 60);

            writetext.Text = string.Format("{0:D2}:{1:D2}.{2:D3}", t.Minutes, t.Seconds, t.Milliseconds);

            if (frames <= 5)
            {
                levelinfo.Text = Encoding.ASCII.GetString(func.FromHex(func.ReadMemory(ip, pid, SaveInfo, 64))).Split(new string[] { "Time" }, StringSplitOptions.None).First(); // lmfao
                Challenge = levelinfo.Text.Split(new string[] { "-" }, StringSplitOptions.None).First().TrimEnd();
                wrtext.Text = $"WR: {GetWorldRecord(Challenge)}";

                if (ghostcheck.Checked) func.WriteMemory(ip, pid, GhostRatchet, "3500");
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

        private void inputdisplaybutton_Click(object sender, EventArgs e)
        {
            if (InputDisplay == null)
            {
                InputDisplay = new InputDisplay();
                InputDisplay.FormClosed += InputDisplay_FormClosed;
                InputDisplay.Show();
            }
        }

        private void InputDisplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            InputDisplay = null;
        }

        private void patchLoaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

        private void memoryUtilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryForm memoryForm = new MemoryForm();
            memoryForm.Show();
        }

        private void AutosplitterCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!AutosplitterCheckbox.Checked)
            {
                // Disable autosplitter.
                autosplitterHelper.Stop();
                autosplitterHelper = null;
            }
            else
            {
                // Enable auotpslitter
                Console.WriteLine("Autosplitter starting!");
                autosplitterHelper = new AutosplitterHelper();
                autosplitterHelper.StartAutosplitterForGame(this.game);
            }
        }

        private void bolts_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    game.SetBoltCount(uint.Parse(bolts_textBox.Text));
                }
                catch
                {
                    MessageBox.Show("Please enter a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void killyourself_Click(object sender, EventArgs e)
        {
            KillYourself();
        }

        private void KillYourself()
        {
            game.KillYourself();
        }

        private void botsUnlocksWindowButton_Click(object sender, EventArgs e)
        {
            RAC4BotsUnlocks unlocks = new RAC4BotsUnlocks(game);
            unlocks.Show();
        }
    }
}