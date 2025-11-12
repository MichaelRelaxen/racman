
namespace racman
{
    partial class RAC4Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RAC4Form));
            this.writetext = new System.Windows.Forms.CheckBox();
            this.levelinfo = new System.Windows.Forms.Label();
            this.wrtext = new System.Windows.Forms.Label();
            this.ghostcheck = new System.Windows.Forms.CheckBox();
            this.inputdisplaybutton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureButtonCombosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.memoryUtilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchLoaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutosplitterCheckbox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.bolts_textBox = new System.Windows.Forms.TextBox();
            this.killyourself = new System.Windows.Forms.Button();
            this.botsUnlocksWindowButton = new System.Windows.Forms.Button();
            this.wrsFromSrcSiteCheck = new System.Windows.Forms.CheckBox();
            this.buttonActTune = new System.Windows.Forms.Button();
            this.checkBoxSoftlocks = new System.Windows.Forms.CheckBox();
            this.setAsideFileButton = new System.Windows.Forms.Button();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.savepos = new System.Windows.Forms.Button();
            this.loadpos = new System.Windows.Forms.Button();
            this.positions_comboBox = new System.Windows.Forms.ComboBox();
            this.CComboCheckBox = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // writetext
            // 
            this.writetext.AutoSize = true;
            this.writetext.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writetext.Location = new System.Drawing.Point(6, 19);
            this.writetext.Name = "writetext";
            this.writetext.Size = new System.Drawing.Size(151, 35);
            this.writetext.TabIndex = 0;
            this.writetext.Text = "Level IGT";
            this.writetext.UseVisualStyleBackColor = true;
            this.writetext.CheckedChanged += new System.EventHandler(this.writetext_CheckedChanged);
            // 
            // levelinfo
            // 
            this.levelinfo.AutoSize = true;
            this.levelinfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelinfo.Location = new System.Drawing.Point(25, 57);
            this.levelinfo.Name = "levelinfo";
            this.levelinfo.Size = new System.Drawing.Size(138, 20);
            this.levelinfo.TabIndex = 1;
            this.levelinfo.Text = "Challenge - Planet";
            // 
            // wrtext
            // 
            this.wrtext.AutoSize = true;
            this.wrtext.Enabled = false;
            this.wrtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wrtext.Location = new System.Drawing.Point(8, 414);
            this.wrtext.Name = "wrtext";
            this.wrtext.Size = new System.Drawing.Size(44, 20);
            this.wrtext.TabIndex = 2;
            this.wrtext.Text = "WR: ";
            // 
            // ghostcheck
            // 
            this.ghostcheck.AutoSize = true;
            this.ghostcheck.Location = new System.Drawing.Point(139, 261);
            this.ghostcheck.Name = "ghostcheck";
            this.ghostcheck.Size = new System.Drawing.Size(90, 17);
            this.ghostcheck.TabIndex = 3;
            this.ghostcheck.Text = "Ghost ratchet";
            this.ghostcheck.UseVisualStyleBackColor = true;
            this.ghostcheck.CheckedChanged += new System.EventHandler(this.ghostcheck_CheckedChanged);
            // 
            // inputdisplaybutton
            // 
            this.inputdisplaybutton.Location = new System.Drawing.Point(269, 234);
            this.inputdisplaybutton.Name = "inputdisplaybutton";
            this.inputdisplaybutton.Size = new System.Drawing.Size(106, 23);
            this.inputdisplaybutton.TabIndex = 4;
            this.inputdisplaybutton.Text = "Input display";
            this.inputdisplaybutton.UseVisualStyleBackColor = true;
            this.inputdisplaybutton.Click += new System.EventHandler(this.inputdisplaybutton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(400, 24);
            this.menuStrip1.TabIndex = 77;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureButtonCombosToolStripMenuItem,
            this.toolStripSeparator1,
            this.memoryUtilitiesToolStripMenuItem,
            this.patchLoaderToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // configureButtonCombosToolStripMenuItem
            // 
            this.configureButtonCombosToolStripMenuItem.Name = "configureButtonCombosToolStripMenuItem";
            this.configureButtonCombosToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.configureButtonCombosToolStripMenuItem.Text = "Configure button combos";
            this.configureButtonCombosToolStripMenuItem.Click += new System.EventHandler(this.configureButtonCombosToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(209, 6);
            // 
            // memoryUtilitiesToolStripMenuItem
            // 
            this.memoryUtilitiesToolStripMenuItem.Name = "memoryUtilitiesToolStripMenuItem";
            this.memoryUtilitiesToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.memoryUtilitiesToolStripMenuItem.Text = "Memory utilities";
            this.memoryUtilitiesToolStripMenuItem.Click += new System.EventHandler(this.memoryUtilitiesToolStripMenuItem_Click);
            // 
            // patchLoaderToolStripMenuItem
            // 
            this.patchLoaderToolStripMenuItem.Name = "patchLoaderToolStripMenuItem";
            this.patchLoaderToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.patchLoaderToolStripMenuItem.Text = "Patch loader...";
            this.patchLoaderToolStripMenuItem.Click += new System.EventHandler(this.patchLoaderToolStripMenuItem_Click);
            // 
            // AutosplitterCheckbox
            // 
            this.AutosplitterCheckbox.AutoSize = true;
            this.AutosplitterCheckbox.Location = new System.Drawing.Point(139, 284);
            this.AutosplitterCheckbox.Name = "AutosplitterCheckbox";
            this.AutosplitterCheckbox.Size = new System.Drawing.Size(78, 17);
            this.AutosplitterCheckbox.TabIndex = 104;
            this.AutosplitterCheckbox.Text = "Autosplitter";
            this.AutosplitterCheckbox.UseVisualStyleBackColor = true;
            this.AutosplitterCheckbox.CheckedChanged += new System.EventHandler(this.AutosplitterCheckbox_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(266, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 107;
            this.label8.Text = "Bolt Count:";
            // 
            // bolts_textBox
            // 
            this.bolts_textBox.Location = new System.Drawing.Point(269, 147);
            this.bolts_textBox.Name = "bolts_textBox";
            this.bolts_textBox.Size = new System.Drawing.Size(110, 20);
            this.bolts_textBox.TabIndex = 106;
            // 
            // killyourself
            // 
            this.killyourself.Location = new System.Drawing.Point(12, 184);
            this.killyourself.Name = "killyourself";
            this.killyourself.Size = new System.Drawing.Size(115, 23);
            this.killyourself.TabIndex = 108;
            this.killyourself.Text = "Die";
            this.killyourself.UseVisualStyleBackColor = true;
            this.killyourself.Click += new System.EventHandler(this.killyourself_Click);
            // 
            // botsUnlocksWindowButton
            // 
            this.botsUnlocksWindowButton.Location = new System.Drawing.Point(269, 173);
            this.botsUnlocksWindowButton.Name = "botsUnlocksWindowButton";
            this.botsUnlocksWindowButton.Size = new System.Drawing.Size(110, 34);
            this.botsUnlocksWindowButton.TabIndex = 109;
            this.botsUnlocksWindowButton.Text = "Bots Unlocks";
            this.botsUnlocksWindowButton.UseVisualStyleBackColor = true;
            this.botsUnlocksWindowButton.Click += new System.EventHandler(this.botsUnlocksWindowButton_Click);
            // 
            // wrsFromSrcSiteCheck
            // 
            this.wrsFromSrcSiteCheck.AutoSize = true;
            this.wrsFromSrcSiteCheck.Enabled = false;
            this.wrsFromSrcSiteCheck.Location = new System.Drawing.Point(12, 394);
            this.wrsFromSrcSiteCheck.Name = "wrsFromSrcSiteCheck";
            this.wrsFromSrcSiteCheck.Size = new System.Drawing.Size(173, 17);
            this.wrsFromSrcSiteCheck.TabIndex = 110;
            this.wrsFromSrcSiteCheck.Text = "Show WRs from speedrun.com";
            this.wrsFromSrcSiteCheck.UseVisualStyleBackColor = true;
            this.wrsFromSrcSiteCheck.CheckedChanged += new System.EventHandler(this.wrsFromSrcSiteCheck_CheckedChanged);
            // 
            // buttonActTune
            // 
            this.buttonActTune.Location = new System.Drawing.Point(17, 238);
            this.buttonActTune.Name = "buttonActTune";
            this.buttonActTune.Size = new System.Drawing.Size(110, 86);
            this.buttonActTune.TabIndex = 111;
            this.buttonActTune.Text = "Act Tune Bosses";
            this.buttonActTune.UseVisualStyleBackColor = true;
            this.buttonActTune.Click += new System.EventHandler(this.buttonActTune_Click);
            // 
            // checkBoxSoftlocks
            // 
            this.checkBoxSoftlocks.AutoSize = true;
            this.checkBoxSoftlocks.Location = new System.Drawing.Point(139, 307);
            this.checkBoxSoftlocks.Name = "checkBoxSoftlocks";
            this.checkBoxSoftlocks.Size = new System.Drawing.Size(110, 17);
            this.checkBoxSoftlocks.TabIndex = 112;
            this.checkBoxSoftlocks.Text = "Fix reset softlocks";
            this.checkBoxSoftlocks.UseVisualStyleBackColor = true;
            this.checkBoxSoftlocks.CheckedChanged += new System.EventHandler(this.checkBoxSoftlocks_CheckedChanged);
            // 
            // setAsideFileButton
            // 
            this.setAsideFileButton.Enabled = false;
            this.setAsideFileButton.Location = new System.Drawing.Point(133, 155);
            this.setAsideFileButton.Name = "setAsideFileButton";
            this.setAsideFileButton.Size = new System.Drawing.Size(96, 23);
            this.setAsideFileButton.TabIndex = 113;
            this.setAsideFileButton.Text = "Set Aside File";
            this.setAsideFileButton.UseVisualStyleBackColor = true;
            this.setAsideFileButton.Click += new System.EventHandler(this.setAsideFileButton_Click);
            // 
            // loadFileButton
            // 
            this.loadFileButton.Enabled = false;
            this.loadFileButton.Location = new System.Drawing.Point(133, 184);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(96, 23);
            this.loadFileButton.TabIndex = 114;
            this.loadFileButton.Text = "Load File";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.writetext);
            this.groupBox1.Controls.Add(this.levelinfo);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 83);
            this.groupBox1.TabIndex = 115;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Individual Levels";
            // 
            // savepos
            // 
            this.savepos.Enabled = false;
            this.savepos.Location = new System.Drawing.Point(12, 126);
            this.savepos.Name = "savepos";
            this.savepos.Size = new System.Drawing.Size(115, 23);
            this.savepos.TabIndex = 116;
            this.savepos.Text = "Save Position";
            this.savepos.UseVisualStyleBackColor = true;
            // 
            // loadpos
            // 
            this.loadpos.Enabled = false;
            this.loadpos.Location = new System.Drawing.Point(12, 155);
            this.loadpos.Name = "loadpos";
            this.loadpos.Size = new System.Drawing.Size(115, 23);
            this.loadpos.TabIndex = 117;
            this.loadpos.Text = "Load Position";
            this.loadpos.UseVisualStyleBackColor = true;
            // 
            // positions_comboBox
            // 
            this.positions_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.positions_comboBox.Enabled = false;
            this.positions_comboBox.FormattingEnabled = true;
            this.positions_comboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.positions_comboBox.Location = new System.Drawing.Point(133, 128);
            this.positions_comboBox.Name = "positions_comboBox";
            this.positions_comboBox.Size = new System.Drawing.Size(96, 21);
            this.positions_comboBox.TabIndex = 118;
            // 
            // CComboCheckBox
            // 
            this.CComboCheckBox.AutoSize = true;
            this.CComboCheckBox.Location = new System.Drawing.Point(139, 238);
            this.CComboCheckBox.Name = "CComboCheckBox";
            this.CComboCheckBox.Size = new System.Drawing.Size(99, 17);
            this.CComboCheckBox.TabIndex = 119;
            this.CComboCheckBox.Text = "Enable combos";
            this.CComboCheckBox.UseVisualStyleBackColor = true;
            this.CComboCheckBox.CheckedChanged += new System.EventHandler(this.CComboCheckBox_CheckedChanged);
            // 
            // RAC4Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 384);
            this.Controls.Add(this.CComboCheckBox);
            this.Controls.Add(this.positions_comboBox);
            this.Controls.Add(this.loadpos);
            this.Controls.Add(this.savepos);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.loadFileButton);
            this.Controls.Add(this.wrtext);
            this.Controls.Add(this.setAsideFileButton);
            this.Controls.Add(this.checkBoxSoftlocks);
            this.Controls.Add(this.buttonActTune);
            this.Controls.Add(this.wrsFromSrcSiteCheck);
            this.Controls.Add(this.botsUnlocksWindowButton);
            this.Controls.Add(this.killyourself);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.bolts_textBox);
            this.Controls.Add(this.AutosplitterCheckbox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.inputdisplaybutton);
            this.Controls.Add(this.ghostcheck);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RAC4Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ratchet: Deadlocked (PAL) - NPEA00423";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RAC4Form_FormClosing);
            this.Load += new System.EventHandler(this.RAC4Form_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox writetext;
        private System.Windows.Forms.Label levelinfo;
        private System.Windows.Forms.Label wrtext;
        private System.Windows.Forms.CheckBox ghostcheck;
        private System.Windows.Forms.Button inputdisplaybutton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patchLoaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memoryUtilitiesToolStripMenuItem;
        private System.Windows.Forms.CheckBox AutosplitterCheckbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox bolts_textBox;
        private System.Windows.Forms.Button killyourself;
        private System.Windows.Forms.Button botsUnlocksWindowButton;
        private System.Windows.Forms.CheckBox wrsFromSrcSiteCheck;
        private System.Windows.Forms.Button buttonActTune;
        private System.Windows.Forms.CheckBox checkBoxSoftlocks;
        private System.Windows.Forms.Button setAsideFileButton;
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button savepos;
        private System.Windows.Forms.Button loadpos;
        private System.Windows.Forms.ComboBox positions_comboBox;
        private System.Windows.Forms.ToolStripMenuItem configureButtonCombosToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.CheckBox CComboCheckBox;
    }
}
