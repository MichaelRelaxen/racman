namespace racman
{
    partial class RacketsGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonUploadRun = new System.Windows.Forms.Button();
            this.buttonAdvance = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.labelCurrentScript = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bufferChargeCheckbox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOpenScript = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonRecording = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonResume = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxHotkeys = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxHideHud = new System.Windows.Forms.CheckBox();
            this.checkBoxRendering = new System.Windows.Forms.CheckBox();
            this.checkBoxFrameskip = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.forceAutosaveButton = new System.Windows.Forms.Button();
            this.buttonLoadSetAside = new System.Windows.Forms.Button();
            this.buttonSetAside = new System.Windows.Forms.Button();
            this.buttonSetPosition = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.pastePositionButton = new System.Windows.Forms.Button();
            this.buttonCopyPos = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.buttonSetPlanet = new System.Windows.Forms.Button();
            this.comboLevelSelect = new System.Windows.Forms.ComboBox();
            this.reloadUploadCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonUploadRun
            // 
            this.buttonUploadRun.Location = new System.Drawing.Point(6, 78);
            this.buttonUploadRun.Name = "buttonUploadRun";
            this.buttonUploadRun.Size = new System.Drawing.Size(129, 27);
            this.buttonUploadRun.TabIndex = 0;
            this.buttonUploadRun.Text = "Compile, Upload && Run";
            this.buttonUploadRun.UseVisualStyleBackColor = true;
            this.buttonUploadRun.Click += new System.EventHandler(this.buttonUploadRun_Click);
            // 
            // buttonAdvance
            // 
            this.buttonAdvance.Location = new System.Drawing.Point(6, 209);
            this.buttonAdvance.Name = "buttonAdvance";
            this.buttonAdvance.Size = new System.Drawing.Size(129, 27);
            this.buttonAdvance.TabIndex = 2;
            this.buttonAdvance.Text = "Advance Frame";
            this.buttonAdvance.UseVisualStyleBackColor = true;
            this.buttonAdvance.Click += new System.EventHandler(this.buttonAdvance_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Location = new System.Drawing.Point(6, 176);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(129, 27);
            this.buttonPause.TabIndex = 5;
            this.buttonPause.Text = "Pause";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // labelCurrentScript
            // 
            this.labelCurrentScript.AutoSize = true;
            this.labelCurrentScript.Location = new System.Drawing.Point(6, 49);
            this.labelCurrentScript.Name = "labelCurrentScript";
            this.labelCurrentScript.Size = new System.Drawing.Size(105, 13);
            this.labelCurrentScript.TabIndex = 6;
            this.labelCurrentScript.Text = "Current Script: <null>";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bufferChargeCheckbox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.buttonOpenScript);
            this.groupBox1.Controls.Add(this.labelCurrentScript);
            this.groupBox1.Controls.Add(this.buttonUploadRun);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 136);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scripting Options";
            // 
            // bufferChargeCheckbox
            // 
            this.bufferChargeCheckbox.AutoSize = true;
            this.bufferChargeCheckbox.Location = new System.Drawing.Point(8, 111);
            this.bufferChargeCheckbox.Name = "bufferChargeCheckbox";
            this.bufferChargeCheckbox.Size = new System.Drawing.Size(91, 17);
            this.bufferChargeCheckbox.TabIndex = 13;
            this.bufferChargeCheckbox.Text = "Buffer Charge";
            this.bufferChargeCheckbox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label3.Location = new System.Drawing.Point(141, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "(u)";
            // 
            // buttonOpenScript
            // 
            this.buttonOpenScript.Location = new System.Drawing.Point(6, 19);
            this.buttonOpenScript.Name = "buttonOpenScript";
            this.buttonOpenScript.Size = new System.Drawing.Size(129, 27);
            this.buttonOpenScript.TabIndex = 7;
            this.buttonOpenScript.Text = "Open Script File";
            this.buttonOpenScript.UseVisualStyleBackColor = true;
            this.buttonOpenScript.Click += new System.EventHandler(this.buttonOpenScript_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonRecording);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.buttonRestart);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.buttonResume);
            this.groupBox2.Controls.Add(this.buttonCancel);
            this.groupBox2.Controls.Add(this.buttonPause);
            this.groupBox2.Controls.Add(this.buttonAdvance);
            this.groupBox2.Location = new System.Drawing.Point(12, 156);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(183, 259);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Playback Options";
            // 
            // buttonRecording
            // 
            this.buttonRecording.Location = new System.Drawing.Point(9, 52);
            this.buttonRecording.Name = "buttonRecording";
            this.buttonRecording.Size = new System.Drawing.Size(129, 27);
            this.buttonRecording.TabIndex = 17;
            this.buttonRecording.Text = "Recording Mode";
            this.buttonRecording.UseVisualStyleBackColor = true;
            this.buttonRecording.Click += new System.EventHandler(this.buttonRecording_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label6.Location = new System.Drawing.Point(144, 27);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "(l)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label5.Location = new System.Drawing.Point(144, 93);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "(c)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label4.Location = new System.Drawing.Point(141, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "(r)";
            // 
            // buttonRestart
            // 
            this.buttonRestart.Location = new System.Drawing.Point(9, 19);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(129, 27);
            this.buttonRestart.TabIndex = 15;
            this.buttonRestart.Text = "Restart Playback";
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label1.Location = new System.Drawing.Point(141, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "(s)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label2.Location = new System.Drawing.Point(141, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "(p)";
            // 
            // buttonResume
            // 
            this.buttonResume.Location = new System.Drawing.Point(6, 143);
            this.buttonResume.Name = "buttonResume";
            this.buttonResume.Size = new System.Drawing.Size(129, 27);
            this.buttonResume.TabIndex = 6;
            this.buttonResume.Text = "Resume";
            this.buttonResume.UseVisualStyleBackColor = true;
            this.buttonResume.Click += new System.EventHandler(this.buttonResume_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(9, 85);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(129, 27);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Stop";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxHotkeys
            // 
            this.checkBoxHotkeys.AutoSize = true;
            this.checkBoxHotkeys.Location = new System.Drawing.Point(6, 19);
            this.checkBoxHotkeys.Name = "checkBoxHotkeys";
            this.checkBoxHotkeys.Size = new System.Drawing.Size(101, 17);
            this.checkBoxHotkeys.TabIndex = 9;
            this.checkBoxHotkeys.Text = "Enable Hotkeys";
            this.checkBoxHotkeys.UseVisualStyleBackColor = true;
            this.checkBoxHotkeys.CheckedChanged += new System.EventHandler(this.checkBoxHotkeys_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.reloadUploadCheckBox);
            this.groupBox3.Controls.Add(this.checkBoxHideHud);
            this.groupBox3.Controls.Add(this.checkBoxRendering);
            this.groupBox3.Controls.Add(this.checkBoxFrameskip);
            this.groupBox3.Controls.Add(this.checkBoxHotkeys);
            this.groupBox3.Location = new System.Drawing.Point(201, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(144, 136);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Global Options";
            // 
            // checkBoxHideHud
            // 
            this.checkBoxHideHud.AutoSize = true;
            this.checkBoxHideHud.Location = new System.Drawing.Point(6, 88);
            this.checkBoxHideHud.Name = "checkBoxHideHud";
            this.checkBoxHideHud.Size = new System.Drawing.Size(99, 17);
            this.checkBoxHideHud.TabIndex = 12;
            this.checkBoxHideHud.Text = "Hide TAS HUD";
            this.checkBoxHideHud.UseVisualStyleBackColor = true;
            this.checkBoxHideHud.CheckedChanged += new System.EventHandler(this.checkBoxHideHud_CheckedChanged);
            // 
            // checkBoxRendering
            // 
            this.checkBoxRendering.AutoSize = true;
            this.checkBoxRendering.Location = new System.Drawing.Point(6, 65);
            this.checkBoxRendering.Name = "checkBoxRendering";
            this.checkBoxRendering.Size = new System.Drawing.Size(113, 17);
            this.checkBoxRendering.TabIndex = 11;
            this.checkBoxRendering.Text = "Disable Rendering";
            this.checkBoxRendering.UseVisualStyleBackColor = true;
            this.checkBoxRendering.CheckedChanged += new System.EventHandler(this.checkBoxRendering_CheckedChanged);
            // 
            // checkBoxFrameskip
            // 
            this.checkBoxFrameskip.AutoSize = true;
            this.checkBoxFrameskip.Location = new System.Drawing.Point(6, 42);
            this.checkBoxFrameskip.Name = "checkBoxFrameskip";
            this.checkBoxFrameskip.Size = new System.Drawing.Size(130, 17);
            this.checkBoxFrameskip.TabIndex = 10;
            this.checkBoxFrameskip.Text = "Enable Frameskipping";
            this.checkBoxFrameskip.UseVisualStyleBackColor = true;
            this.checkBoxFrameskip.CheckedChanged += new System.EventHandler(this.checkBoxFrameskip_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.forceAutosaveButton);
            this.groupBox4.Controls.Add(this.buttonLoadSetAside);
            this.groupBox4.Controls.Add(this.buttonSetAside);
            this.groupBox4.Location = new System.Drawing.Point(201, 156);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(144, 128);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Savefile Options";
            // 
            // forceAutosaveButton
            // 
            this.forceAutosaveButton.Location = new System.Drawing.Point(6, 85);
            this.forceAutosaveButton.Name = "forceAutosaveButton";
            this.forceAutosaveButton.Size = new System.Drawing.Size(129, 27);
            this.forceAutosaveButton.TabIndex = 19;
            this.forceAutosaveButton.Text = "Force Autosave";
            this.forceAutosaveButton.UseVisualStyleBackColor = true;
            this.forceAutosaveButton.Click += new System.EventHandler(this.forceAutosaveButton_Click);
            // 
            // buttonLoadSetAside
            // 
            this.buttonLoadSetAside.Location = new System.Drawing.Point(6, 52);
            this.buttonLoadSetAside.Name = "buttonLoadSetAside";
            this.buttonLoadSetAside.Size = new System.Drawing.Size(129, 27);
            this.buttonLoadSetAside.TabIndex = 18;
            this.buttonLoadSetAside.Text = "Load File";
            this.buttonLoadSetAside.UseVisualStyleBackColor = true;
            this.buttonLoadSetAside.Click += new System.EventHandler(this.buttonLoadSetAside_Click);
            // 
            // buttonSetAside
            // 
            this.buttonSetAside.Location = new System.Drawing.Point(6, 19);
            this.buttonSetAside.Name = "buttonSetAside";
            this.buttonSetAside.Size = new System.Drawing.Size(129, 27);
            this.buttonSetAside.TabIndex = 17;
            this.buttonSetAside.Text = "Set Aside File";
            this.buttonSetAside.UseVisualStyleBackColor = true;
            this.buttonSetAside.Click += new System.EventHandler(this.buttonSetAside_Click);
            // 
            // buttonSetPosition
            // 
            this.buttonSetPosition.Location = new System.Drawing.Point(6, 19);
            this.buttonSetPosition.Name = "buttonSetPosition";
            this.buttonSetPosition.Size = new System.Drawing.Size(129, 27);
            this.buttonSetPosition.TabIndex = 20;
            this.buttonSetPosition.Text = "Set Position From Game";
            this.buttonSetPosition.UseVisualStyleBackColor = true;
            this.buttonSetPosition.Click += new System.EventHandler(this.buttonSetPosition_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.pastePositionButton);
            this.groupBox5.Controls.Add(this.buttonCopyPos);
            this.groupBox5.Controls.Add(this.buttonSetPosition);
            this.groupBox5.Location = new System.Drawing.Point(201, 290);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(144, 125);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Position Options";
            // 
            // pastePositionButton
            // 
            this.pastePositionButton.Location = new System.Drawing.Point(6, 85);
            this.pastePositionButton.Name = "pastePositionButton";
            this.pastePositionButton.Size = new System.Drawing.Size(129, 27);
            this.pastePositionButton.TabIndex = 22;
            this.pastePositionButton.Text = "Set Position From Paste";
            this.pastePositionButton.UseVisualStyleBackColor = true;
            this.pastePositionButton.Click += new System.EventHandler(this.pastePositionButton_Click);
            // 
            // buttonCopyPos
            // 
            this.buttonCopyPos.Location = new System.Drawing.Point(6, 52);
            this.buttonCopyPos.Name = "buttonCopyPos";
            this.buttonCopyPos.Size = new System.Drawing.Size(129, 27);
            this.buttonCopyPos.TabIndex = 21;
            this.buttonCopyPos.Text = "Copy Saved Position";
            this.buttonCopyPos.UseVisualStyleBackColor = true;
            this.buttonCopyPos.Click += new System.EventHandler(this.buttonCopyPos_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.buttonSetPlanet);
            this.groupBox6.Controls.Add(this.comboLevelSelect);
            this.groupBox6.Location = new System.Drawing.Point(12, 421);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(144, 86);
            this.groupBox6.TabIndex = 22;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Level Options";
            // 
            // buttonSetPlanet
            // 
            this.buttonSetPlanet.Location = new System.Drawing.Point(8, 47);
            this.buttonSetPlanet.Name = "buttonSetPlanet";
            this.buttonSetPlanet.Size = new System.Drawing.Size(127, 27);
            this.buttonSetPlanet.TabIndex = 22;
            this.buttonSetPlanet.Text = "Set Current Planet";
            this.buttonSetPlanet.UseVisualStyleBackColor = true;
            this.buttonSetPlanet.Click += new System.EventHandler(this.buttonSetPlanet_Click);
            // 
            // comboLevelSelect
            // 
            this.comboLevelSelect.FormattingEnabled = true;
            this.comboLevelSelect.Items.AddRange(new object[] {
            "Aranos",
            "Oozla",
            "Maktar",
            "Endako",
            "Barlow",
            "Feltzin",
            "Notak",
            "Siberius",
            "Tabora",
            "Dobbo",
            "Hrugis",
            "Joba",
            "Todano",
            "Boldan",
            "Aranos 2",
            "Gorn",
            "Snivelak",
            "Smolg",
            "Damosel",
            "Grelbin",
            "Yeedil",
            "Insomniac Museum",
            "Dobbo Orbit",
            "Damosel Orbit",
            "Slim Cognito",
            "Wupash",
            "Jamming Array"});
            this.comboLevelSelect.Location = new System.Drawing.Point(8, 20);
            this.comboLevelSelect.Name = "comboLevelSelect";
            this.comboLevelSelect.Size = new System.Drawing.Size(127, 21);
            this.comboLevelSelect.TabIndex = 0;
            // 
            // reloadUploadCheckBox
            // 
            this.reloadUploadCheckBox.AutoSize = true;
            this.reloadUploadCheckBox.Location = new System.Drawing.Point(6, 111);
            this.reloadUploadCheckBox.Name = "reloadUploadCheckBox";
            this.reloadUploadCheckBox.Size = new System.Drawing.Size(110, 17);
            this.reloadUploadCheckBox.TabIndex = 13;
            this.reloadUploadCheckBox.Text = "Reload on upload";
            this.reloadUploadCheckBox.UseVisualStyleBackColor = true;
            // 
            // RacketsGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 519);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "RacketsGUI";
            this.ShowIcon = false;
            this.Text = "Rackets2 GUI";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonUploadRun;
        private System.Windows.Forms.Button buttonAdvance;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.Label labelCurrentScript;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonOpenScript;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonResume;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxHotkeys;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.CheckBox checkBoxRendering;
        private System.Windows.Forms.CheckBox checkBoxFrameskip;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonLoadSetAside;
        private System.Windows.Forms.Button buttonSetAside;
        private System.Windows.Forms.CheckBox checkBoxHideHud;
        private System.Windows.Forms.Button forceAutosaveButton;
        private System.Windows.Forms.Button buttonSetPosition;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonCopyPos;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button buttonSetPlanet;
        private System.Windows.Forms.ComboBox comboLevelSelect;
        private System.Windows.Forms.Button pastePositionButton;
        private System.Windows.Forms.Button buttonRecording;
        private System.Windows.Forms.CheckBox bufferChargeCheckbox;
        private System.Windows.Forms.CheckBox reloadUploadCheckBox;
    }
}