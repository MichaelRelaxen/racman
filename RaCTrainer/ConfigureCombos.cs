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
            comboActions = new Dictionary<TextBox, Action<int>>
            {
                { loadPlanetTextBox, val => loadPlanetCombo = val },
                { dieTextBox, val => dieCombo = val },
                { loadSetAsideComboTextBox, val => loadSetAsideCombo = val },
                { loadPositionTextBox, val => loadCombo = val },
                { savePositionTextBox, val => saveCombo = val },
                { textBoxRunScript, val => runScriptCombo = val }
            };
            GetCombos();
            UpdateCombos();

            timer.Interval = 100;
            timer.Tick += new EventHandler(UpdateInputs);
        }
        private Dictionary<TextBox, Action<int>> comboActions;

        int confirmedInput = 0;
        int confirmationCounter = 0;
        const int CONFIRMATION_TICKS = 8;

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
            timer.Enabled = false;
        }

        public Timer timer = new Timer();


        public void UpdateInputs(object sender, EventArgs e)
        {
            if (Inputs.RawInputs != confirmedInput)
            {
                confirmedInput = Inputs.RawInputs;
                confirmationCounter = 0;
                return;
            }

            if (Inputs.RawInputs == confirmedInput && confirmedInput != 0)
            {
                var activeTextBox = comboActions.Keys.FirstOrDefault(tb => tb.Text == EnterInput);
                confirmationCounter++;

                if (activeTextBox != null)
                {
                    activeTextBox.Text = String.Join(" + ", Inputs.DecodeMask(confirmedInput));

                    if (confirmationCounter >= CONFIRMATION_TICKS)
                    {
                        comboActions[activeTextBox](confirmedInput);
                        UpdateCombos();
                        confirmationCounter = 0;
                        confirmedInput = 0;
                    }
                }
            }
        }
        private void setInputs(TextBox textBox)
        {
            confirmationCounter = 0;
            confirmedInput = 0;
            textBox.Text = EnterInput;
            timer.Enabled = true;

        }
        private void ConfigureCombos_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateCombos();
        }

        private void textBoxClick(object sender, EventArgs e)
        {
            setInputs((TextBox)sender);
        }
        public void ConfigureCombos_Load(object sender, EventArgs e)
        {
            infoText.Text = "To edit a combo, simply click on\nthe box you want to change,\nthen press inputs on your controller";
        }
    }
}
