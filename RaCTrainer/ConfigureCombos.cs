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
using Timer = System.Windows.Forms.Timer;

namespace racman
{

    public partial class ConfigureCombos : Form
    {
        public ConfigureCombos()
        {
            InitializeComponent();
            GetCombos();
            UpdateCombos();

            timer.Interval = 100;
            timer.Tick += new EventHandler(UpdateInputs);
        }

        int currentInput;
        bool input = false;

        public static int loadCombo, saveCombo, switchCombo, dieCombo, loadPlanetCombo;
        public static void GetCombos()
        {
            try
            {
                loadCombo = Convert.ToInt32(func.GetConfigData("config.txt", "loadPosCombo"));
                saveCombo = Convert.ToInt32(func.GetConfigData("config.txt", "savePosCombo"));
                switchCombo = Convert.ToInt32(func.GetConfigData("config.txt", "switchCombo"));
                dieCombo = Convert.ToInt32(func.GetConfigData("config.txt", "dieCombo"));
                loadPlanetCombo = Convert.ToInt32(func.GetConfigData("config.txt", "loadPlanetCombo"));
            }
            catch
            {
                saveCombo = 0xb;
                loadCombo = 0x7;
                dieCombo = 0x5;
                loadPlanetCombo = 0x600;
                switchCombo = 0;
            }
        }
        public void UpdateCombos()
        {
            loadPlanetTextBox.Text = String.Join(" + ", Inputs.DecodeMask(loadPlanetCombo));
            dieTextBox.Text = String.Join(" + ", Inputs.DecodeMask(dieCombo));
            switchPositionTextBox.Text = String.Join(" + ", Inputs.DecodeMask(switchCombo));
            loadPositionTextBox.Text = String.Join(" + ", Inputs.DecodeMask(loadCombo));
            savePositionTextBox.Text = String.Join(" + ", Inputs.DecodeMask(saveCombo));

            func.ChangeFileLines("config.txt", loadCombo.ToString(), "loadPosCombo");
            func.ChangeFileLines("config.txt", saveCombo.ToString(), "savePosCombo");
            func.ChangeFileLines("config.txt", switchCombo.ToString(), "switchCombo");
            func.ChangeFileLines("config.txt", dieCombo.ToString(), "dieCombo");
            func.ChangeFileLines("config.txt", loadPlanetCombo.ToString(), "loadPlanetCombo");
            input = false;
            timer.Enabled = false;
        }

        public Timer timer = new Timer();

        public void UpdateInputs(object sender, EventArgs e)
        {
            if(currentInput == Inputs.RawInputs)
            {
                currentInput = Inputs.RawInputs;
            }
            if (currentInput != Inputs.RawInputs)
            {
                input = true;
            }
            if(loadPlanetTextBox.Text == "Enter Controller input..." && input == true)
            {
                loadPlanetCombo = Inputs.RawInputs;
                UpdateCombos();
            }
            if (dieTextBox.Text == "Enter Controller input..." && input == true)
            {
                dieCombo = Inputs.RawInputs;
                UpdateCombos();
            }
            if (switchPositionTextBox.Text == "Enter Controller input..." && input == true)
            {
                switchCombo = Inputs.RawInputs;
                UpdateCombos();
            }
            if (loadPositionTextBox.Text == "Enter Controller input..." && input == true)
            {
                loadCombo = Inputs.RawInputs;
                UpdateCombos();
            }
            if (savePositionTextBox.Text == "Enter Controller input..." && input == true)
            {
                saveCombo = Inputs.RawInputs;
                UpdateCombos();
            }
        }

        private void loadPlanetTextBox_Click(object sender, EventArgs e)
        {
            loadPlanetTextBox.Text = "Enter Controller input...";
            timer.Enabled = true;
        }

        private void ConfigureCombos_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateCombos();
        }

        private void dieTextBox_Click(object sender, EventArgs e)
        {
            dieTextBox.Text = "Enter Controller input...";
            timer.Enabled = true;
        }

        private void switchPositionTextBox_Click(object sender, EventArgs e)
        {
            switchPositionTextBox.Text = "Enter Controller input...";
            timer.Enabled = true;
        }

        private void loadPositionTextBox_Click(object sender, EventArgs e)
        {
            loadPositionTextBox.Text = "Enter Controller input...";
            timer.Enabled = true;
        }

        private void savePositionTextBox_Click(object sender, EventArgs e)
        {
            savePositionTextBox.Text = "Enter Controller input...";
            timer.Enabled = true;
        }

        public void ConfigureCombos_Load(object sender, EventArgs e)
        {
            infoText.Text = "To edit a combo, simply click on\nthe box you want to change,\nthen press inputs on your controller";
        }
    }
}
