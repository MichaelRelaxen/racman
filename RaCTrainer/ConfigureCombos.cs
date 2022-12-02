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
        public static string EnterInput = "Enter Controller combo...";

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

        public static int loadCombo, saveCombo, loadSetAsideCombo, dieCombo, loadPlanetCombo, runScriptCombo;
        public static void GetCombos()
        {
            try
            {
                loadCombo = Convert.ToInt32(func.GetConfigData("config.txt", "loadPosCombo"));
                saveCombo = Convert.ToInt32(func.GetConfigData("config.txt", "savePosCombo"));
                loadSetAsideCombo = Convert.ToInt32(func.GetConfigData("config.txt", "loadSetAsideCombo"));
                dieCombo = Convert.ToInt32(func.GetConfigData("config.txt", "dieCombo"));
                loadPlanetCombo = Convert.ToInt32(func.GetConfigData("config.txt", "loadPlanetCombo"));
                runScriptCombo = Convert.ToInt32(func.GetConfigData("config.txt", "runScriptCombo"));
            }
            catch
            {
                // Keep the ones that successfully loaded.
                saveCombo = saveCombo == 0 ? 0xb : saveCombo;
                loadCombo = loadCombo == 0 ? 0x7 : loadCombo;
                dieCombo = dieCombo == 0 ? 0x5 : dieCombo;
                loadPlanetCombo = loadPlanetCombo == 0 ? 0x600 : loadPlanetCombo;
                loadSetAsideCombo = loadSetAsideCombo == 0 ? 0x100 : loadSetAsideCombo;
                runScriptCombo = runScriptCombo == 0 ? 0xFF : runScriptCombo;
            }
        }
        public void UpdateCombos()
        {
            loadPlanetTextBox.Text = String.Join(" + ", Inputs.DecodeMask(loadPlanetCombo));
            dieTextBox.Text = String.Join(" + ", Inputs.DecodeMask(dieCombo));
            loadSetAsideComboTextBox.Text = String.Join(" + ", Inputs.DecodeMask(loadSetAsideCombo));
            loadPositionTextBox.Text = String.Join(" + ", Inputs.DecodeMask(loadCombo));
            savePositionTextBox.Text = String.Join(" + ", Inputs.DecodeMask(saveCombo));
            textBoxRunScript.Text = String.Join(" + ", Inputs.DecodeMask(runScriptCombo));

            func.ChangeFileLines("config.txt", loadCombo.ToString(), "loadPosCombo");
            func.ChangeFileLines("config.txt", saveCombo.ToString(), "savePosCombo");
            func.ChangeFileLines("config.txt", loadSetAsideCombo.ToString(), "loadSetAsideCombo");
            func.ChangeFileLines("config.txt", dieCombo.ToString(), "dieCombo");
            func.ChangeFileLines("config.txt", loadPlanetCombo.ToString(), "loadPlanetCombo");
            func.ChangeFileLines("config.txt", runScriptCombo.ToString(), "runScriptCombo");
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
            if(loadPlanetTextBox.Text == EnterInput && input == true)
            {
                loadPlanetCombo = Inputs.RawInputs;
                UpdateCombos();
            }
            if (dieTextBox.Text == EnterInput && input == true)
            {
                dieCombo = Inputs.RawInputs;
                UpdateCombos();
            }
            if (loadSetAsideComboTextBox.Text == EnterInput && input == true)
            {
                loadSetAsideCombo = Inputs.RawInputs;
                UpdateCombos();
            }
            if (loadPositionTextBox.Text == EnterInput && input == true)
            {
                loadCombo = Inputs.RawInputs;
                UpdateCombos();
            }
            if (savePositionTextBox.Text == EnterInput && input == true)
            {
                saveCombo = Inputs.RawInputs;
                UpdateCombos();
            }
            if (textBoxRunScript.Text == EnterInput && input)
            {
                runScriptCombo = Inputs.RawInputs;
                UpdateCombos();
            } 
        }

        private void loadPlanetTextBox_Click(object sender, EventArgs e)
        {
            loadPlanetTextBox.Text = EnterInput;
            timer.Enabled = true;
        }

        private void ConfigureCombos_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateCombos();
        }

        private void textBoxRunScript_Click(object sender, EventArgs e)
        {
            textBoxRunScript.Text = EnterInput;
            timer.Enabled = true;
        }

        private void dieTextBox_Click(object sender, EventArgs e)
        {
            dieTextBox.Text = EnterInput;
            timer.Enabled = true;
        }

        private void switchPositionTextBox_Click(object sender, EventArgs e)
        {
            loadSetAsideComboTextBox.Text = EnterInput;
            timer.Enabled = true;
        }

        private void loadPositionTextBox_Click(object sender, EventArgs e)
        {
            loadPositionTextBox.Text = EnterInput;
            timer.Enabled = true;
        }

        private void savePositionTextBox_Click(object sender, EventArgs e)
        {
            savePositionTextBox.Text = EnterInput;
            timer.Enabled = true;
        }

        public void ConfigureCombos_Load(object sender, EventArgs e)
        {
            infoText.Text = "To edit a combo, simply click on\nthe box you want to change,\nthen press inputs on your controller";
        }
    }
}
