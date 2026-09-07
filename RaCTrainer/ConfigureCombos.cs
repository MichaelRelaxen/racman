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
            comboValues = new Dictionary<TextBox, Func<int>>
{
                { loadPlanetTextBox, () => loadPlanetCombo },
                { dieTextBox, () => dieCombo },
                { loadSetAsideComboTextBox, () => loadSetAsideCombo },
                { loadPositionTextBox, () => loadCombo },
                { savePositionTextBox, () => saveCombo },
                { textBoxRunScript, () => runScriptCombo }
            };
            GetCombos();
            UpdateCombos();

            timer.Interval = 100;
            timer.Tick += new EventHandler(UpdateInputs);
        }
        private Dictionary<TextBox, Action<int>> comboActions;
        private Dictionary<TextBox, Func<int>> comboValues;

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

        private static string DescribeCombo(int rawInput) {
            return String.Join(" + ", Inputs.DecodeMask(rawInput));
        }
        private void ShowStoredCombo(TextBox textBox) {
            Func<int> read;
            if (textBox != null && comboValues.TryGetValue(textBox, out read)) {
                textBox.Text = DescribeCombo(read());
            }
        }

        public Timer timer = new Timer();


        private TextBox capturingTextBox;
        public void UpdateInputs(object sender, EventArgs e) {
            TextBox activeTextBox = capturingTextBox;

            if (Inputs.RawInputs != confirmedInput) {
                confirmedInput = Inputs.RawInputs;
                confirmationCounter = 0;
                ShowHoldProgress(activeTextBox);
                return;
            }

            if (confirmedInput != 0) {
                if (activeTextBox != null) {
                    confirmationCounter++;
                    if (confirmationCounter >= CONFIRMATION_TICKS) {
                        comboActions[activeTextBox](confirmedInput);

                        UpdateCombos();

                        confirmationCounter = 0;
                        confirmedInput = 0;
                        return;
                    }

                    ShowHoldProgress(activeTextBox);
                }
            }
        }

        private void ShowHoldProgress(TextBox textBox) {
            if (textBox == null) return;

            if (confirmedInput == 0) {
                textBox.Text = EnterInput;
                return;
            }

            int ticksLeft = CONFIRMATION_TICKS - confirmationCounter;
            if (ticksLeft < 0) ticksLeft = 0;

            double secondsLeft = ticksLeft * timer.Interval / 1000.0;
            textBox.Text = $"{DescribeCombo(confirmedInput)} · hold {secondsLeft:0.0} s";
        }

        private void setInputs(TextBox textBox) {
            if (capturingTextBox != null && capturingTextBox != textBox) {
                ShowStoredCombo(capturingTextBox);
            }

            confirmationCounter = 0;
            confirmedInput = 0;
            capturingTextBox = textBox;
            textBox.Text = EnterInput;
            timer.Enabled = true;

        }
        private void ConfigureCombos_FormClosing(object sender, FormClosingEventArgs e) {
            UpdateCombos();
        }

        private void textBoxClick(object sender, EventArgs e) {
            setInputs((TextBox)sender);
        }
        public void ConfigureCombos_Load(object sender, EventArgs e) {
            infoText.Text = "To edit a combo, simply click on\nthe box you want to change,\nthen press inputs on your controller\nand hold them until the timer runs out.";
        }
    }
}