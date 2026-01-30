namespace racman.TOD
{
    partial class TODForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchLoaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryUtilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label6 = new System.Windows.Forms.Label();
            this.planets_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonStartAutosplitter = new System.Windows.Forms.Button();
            this.labelAutosplitterStatus = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.labelSplitterRoute = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(960, 54);
            this.menuStrip1.TabIndex = 76;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patchLoaderToolStripMenuItem,
            this.memoryUtilitiesToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(107, 41);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // patchLoaderToolStripMenuItem
            // 
            this.patchLoaderToolStripMenuItem.Name = "patchLoaderToolStripMenuItem";
            this.patchLoaderToolStripMenuItem.Size = new System.Drawing.Size(402, 48);
            this.patchLoaderToolStripMenuItem.Text = "Mods and patches...";
            this.patchLoaderToolStripMenuItem.Click += new System.EventHandler(this.patchLoaderToolStripMenuItem_Click);
            // 
            // memoryUtilitiesToolStripMenuItem
            // 
            this.memoryUtilitiesToolStripMenuItem.Name = "memoryUtilitiesToolStripMenuItem";
            this.memoryUtilitiesToolStripMenuItem.Size = new System.Drawing.Size(402, 48);
            this.memoryUtilitiesToolStripMenuItem.Text = "Memory utilities";
            this.memoryUtilitiesToolStripMenuItem.Click += new System.EventHandler(this.memoryUtilitiesToolStripMenuItem_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 426);
            this.label6.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 29);
            this.label6.TabIndex = 99;
            this.label6.Text = "Load Planet:";
            // 
            // planets_comboBox
            // 
            this.planets_comboBox.FormattingEnabled = true;
            this.planets_comboBox.Items.AddRange(new object[] {
            "Kerwan",
            "Cobalia",
            "Kortog",
            "Fastoon",
            "Voron",
            "Mukow",
            "Nundac",
            "Ardolis",
            "Rakar",
            "Rykan V",
            "Sargasso",
            "Kreeli",
            "Viceron",
            "Verdigris",
            "Jasindu",
            "Ublik ",
            "Reepor",
            "Igliak",
            "Fastoon 2"});
            this.planets_comboBox.Location = new System.Drawing.Point(42, 462);
            this.planets_comboBox.Margin = new System.Windows.Forms.Padding(7);
            this.planets_comboBox.Name = "planets_comboBox";
            this.planets_comboBox.Size = new System.Drawing.Size(263, 37);
            this.planets_comboBox.TabIndex = 98;
            this.planets_comboBox.SelectedIndexChanged += new System.EventHandler(this.planets_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(37, 506);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(469, 29);
            this.label1.TabIndex = 100;
            this.label1.Text = "To change planets, save and load your file.\r\n";
            // 
            // buttonStartAutosplitter
            // 
            this.buttonStartAutosplitter.Location = new System.Drawing.Point(33, 100);
            this.buttonStartAutosplitter.Margin = new System.Windows.Forms.Padding(7);
            this.buttonStartAutosplitter.Name = "buttonStartAutosplitter";
            this.buttonStartAutosplitter.Size = new System.Drawing.Size(268, 51);
            this.buttonStartAutosplitter.TabIndex = 104;
            this.buttonStartAutosplitter.Text = "Start Autosplitter";
            this.buttonStartAutosplitter.UseVisualStyleBackColor = true;
            this.buttonStartAutosplitter.Click += new System.EventHandler(this.buttonStartAutosplitter_Click);
            // 
            // labelAutosplitterStatus
            // 
            this.labelAutosplitterStatus.AutoSize = true;
            this.labelAutosplitterStatus.ForeColor = System.Drawing.Color.Red;
            this.labelAutosplitterStatus.Location = new System.Drawing.Point(315, 111);
            this.labelAutosplitterStatus.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.labelAutosplitterStatus.Name = "labelAutosplitterStatus";
            this.labelAutosplitterStatus.Size = new System.Drawing.Size(219, 29);
            this.labelAutosplitterStatus.TabIndex = 105;
            this.labelAutosplitterStatus.Text = "Autosplitter disbled";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(626, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(268, 51);
            this.button1.TabIndex = 106;
            this.button1.Text = "Player Values";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.PlayerValuesFormClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(33, 165);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(268, 51);
            this.button2.TabIndex = 107;
            this.button2.Text = "Save Position";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SavePositionClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(33, 230);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(268, 51);
            this.button3.TabIndex = 108;
            this.button3.Text = "Load Position";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.LoadPositionClick);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(33, 360);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(268, 51);
            this.button4.TabIndex = 109;
            this.button4.Text = "Die";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.DieButtonClick);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(42, 555);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(268, 51);
            this.button5.TabIndex = 110;
            this.button5.Text = "Reset All Gold Bolts";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.ResetAllGoldBoltsClick);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(42, 620);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(268, 51);
            this.button6.TabIndex = 111;
            this.button6.Text = "Weapons";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.WeaponsFormClick);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(626, 295);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(268, 51);
            this.button11.TabIndex = 116;
            this.button11.Text = "Change Armor/Skins";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.ArmorSkinsFormClick);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(42, 750);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(268, 51);
            this.button13.TabIndex = 118;
            this.button13.Text = "Challenge Mode";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.ChallegeModeButtonClick);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(42, 685);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(268, 51);
            this.button14.TabIndex = 119;
            this.button14.Text = "Gadget Unlocks";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.GadgetsFormClick);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(626, 230);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(268, 51);
            this.button7.TabIndex = 120;
            this.button7.Text = "God Ratchet";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.GodRatchetClick);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(42, 815);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(268, 51);
            this.button8.TabIndex = 121;
            this.button8.Text = "Reset RYNO Plans";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.ResetAllRYNOPlans);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(626, 360);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(268, 76);
            this.button12.TabIndex = 124;
            this.button12.Text = "Reset Groovitron Storage";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.ResetGroovitronStorageClick);
            // 
            // labelSplitterRoute
            // 
            this.labelSplitterRoute.AutoSize = true;
            this.labelSplitterRoute.ForeColor = System.Drawing.Color.Gray;
            this.labelSplitterRoute.Location = new System.Drawing.Point(315, 68);
            this.labelSplitterRoute.Name = "labelSplitterRoute";
            this.labelSplitterRoute.Size = new System.Drawing.Size(232, 29);
            this.labelSplitterRoute.TabIndex = 106;
            this.labelSplitterRoute.Text = "Autosplitter disabled";
            this.labelSplitterRoute.Visible = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(320, 174);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(57, 33);
            this.radioButton1.TabIndex = 125;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(383, 174);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(57, 33);
            this.radioButton2.TabIndex = 126;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(449, 174);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(57, 33);
            this.radioButton3.TabIndex = 127;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "3";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // TODForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 916);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelSplitterRoute);
            this.Controls.Add(this.labelAutosplitterStatus);
            this.Controls.Add(this.buttonStartAutosplitter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.planets_comboBox);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "TODForm";
            this.Text = "Ratchet & Clank: Tools of Destruction - NPEA00452 (PAL)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TODForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patchLoaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memoryUtilitiesToolStripMenuItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox planets_comboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonStartAutosplitter;
        private System.Windows.Forms.Label labelAutosplitterStatus;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Label labelSplitterRoute;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
    }
}