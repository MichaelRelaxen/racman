using racman;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace racman
{
    public partial class RacketsGUI : Form
    {
        private int pid;
        private string pathToScript;

        public RacketsGUI(rac2 game)
        {
            InitializeComponent();

            var hotkeys = GetConfigData("hotkeysEnabled");
            checkBoxHotkeys.Checked = hotkeys == "true";
            var frameskip = GetConfigData("frameskipEnabled");
            checkBoxFrameskip.Checked = frameskip == "true";

            this.KeyPreview = true;
        }

        private void buttonOpenScript_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Ratchet 2 TAS script file (*.rtas2s)|*.rtas2s|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.pathToScript = openFileDialog.FileName;
                    labelCurrentScript.Text = $"Current Script: {Path.GetFileName(pathToScript)}";
                }
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            Rackets2API.PauseUnpauseRackets(true);
        }

        private void buttonResume_Click(object sender, EventArgs e)
        {
            Rackets2API.PauseUnpauseRackets(false);
        }

        private void buttonAdvance_Click(object sender, EventArgs e)
        {
            Rackets2API.Framestep();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Rackets2API.CancelPlayback();
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            Rackets2API.PauseUnpauseRackets(false);
            Rackets2API.RestartPlayback();
        }

        private void buttonUploadRun_Click(object sender, EventArgs e)
        {
            // api.PauseUnpauseRackets(false);
            var result = Rackets2API.UploadInputsFile(this.pathToScript);
            if (result != null) 
            { 
                MessageBox.Show(result, "Error when uploading!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxHotkeys_CheckedChanged(object sender, EventArgs e)
        {
            SetConfigData("hotkeysEnabled", checkBoxHotkeys.Checked ? "true" : "false");
        }

        private void checkBoxFrameskip_CheckedChanged(object sender, EventArgs e)
        {
            SetConfigData("frameskipEnabled", checkBoxFrameskip.Checked ? "true" : "false");
            Rackets2API.SetRenderingMode(checkBoxFrameskip.Checked, checkBoxRendering.Checked);
        }

        private void checkBoxRendering_CheckedChanged(object sender, EventArgs e)
        {
            Rackets2API.SetRenderingMode(checkBoxFrameskip.Checked, checkBoxRendering.Checked);
        }

        private void checkBoxHideHud_CheckedChanged(object sender, EventArgs e)
        {
            Rackets2API.SetHudStatus(checkBoxHideHud.Checked);
        }

        private void buttonSetAside_Click(object sender, EventArgs e)
        {
            Rackets2API.SetAsideMethod();
        }

        private void buttonLoadSetAside_Click(object sender, EventArgs e)
        {
            Rackets2API.LoadSetAsideMethod();
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (!checkBoxHotkeys.Checked) return;

            if (e.KeyCode == Keys.U)
                buttonUploadRun_Click(sender, e);
            else if (e.KeyCode == Keys.L) 
                buttonRestart_Click(sender, e);
            else if (e.KeyCode == Keys.C)
                buttonCancel_Click(sender, e);
            else if (e.KeyCode == Keys.P)
                buttonPause_Click(sender, e);
            else if (e.KeyCode == Keys.R)
                buttonResume_Click(sender, e);
            else if (e.KeyCode == Keys.S)
                buttonAdvance_Click(sender, e); 
        }

        private void buttonSetPosition_Click(object sender, EventArgs e)
        {
            Rackets2API.SetPositionToLoadMethod();
        }

        private void buttonCopyPos_Click(object sender, EventArgs e)
        {
            Rackets2API.CopyPositionToClipBoardMethod();
        }

        private void buttonSetPlanet_Click(object sender, EventArgs e)
        {
            Rackets2API.SetCurrentLevelMethod((uint)comboLevelSelect.SelectedIndex);
        }

        private void forceAutosaveButton_Click(object sender, EventArgs e)
        {
            Rackets2API.SetSaveModeMethod(3); // 3 is autosave.
        }

        private void pastePositionButton_Click(object sender, EventArgs e)
        {
            Rackets2API.PastePositionFromClipboard();
        }

        private void buttonRecording_Click(object sender, EventArgs e)
        {
            Rackets2API.StartRecording();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }


        public static void SetConfigData(string keyword, string contents)
        {
            ChangeFileLines("config.txt", contents, keyword);
        }

        public static void ChangeFileLines(string filename, string contents, string keyword)
        {
            string[] data = File.ReadAllLines("config.txt");
            bool found = false;

            for (int i = 0; i < data.Length; i++)
            {
                if (Regex.Match(data[i], @"^([\w\-]+)").Value == keyword)
                {
                    data[i] = keyword + " = " + contents;
                    found = true;
                }
            }


            if (!found)
            {
                string[] new_data;
                new_data = new string[data.Length + 1];


                for (int i = 0; i < data.Length; i++)
                {
                    new_data[i] = data[i];
                }
                new_data[data.Length] = keyword + " = " + contents;
                File.WriteAllLines("config.txt", new_data);
            }
            else
            {
                File.WriteAllLines("config.txt", data);
            }
        }

        public static string GetConfigData(string keyword)
        {
            string[] data = File.ReadAllLines("config.txt");

            for (int i = 0; i < data.Length; i++)
            {
                if (Regex.Match(data[i], @"^([\w\-]+)").Value == keyword)
                {
                    int startPos = data[i].IndexOf("=") + 2;
                    return data[i].Substring(startPos, data[i].Length - startPos);
                }
            }

            return "";
        }
    }
}
