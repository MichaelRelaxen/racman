
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
            this.patchLoaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryUtilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutosplitterCheckbox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.bolts_textBox = new System.Windows.Forms.TextBox();
            this.killyourself = new System.Windows.Forms.Button();
            this.botsUnlocksWindowButton = new System.Windows.Forms.Button();
            this.wrsFromSrcSiteCheck = new System.Windows.Forms.CheckBox();
            this.buttonActTune = new System.Windows.Forms.Button();
            this.loadPlannetButton = new System.Windows.Forms.Button();
            this.dreadPoints_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.freezeHealthCheckbox = new System.Windows.Forms.CheckBox();
            this.skinsButton = new System.Windows.Forms.Button();
            this.CM_textBox = new System.Windows.Forms.TextBox();
            this.unlockPlanetsButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.planets_comboBox = new System.Windows.Forms.ComboBox();
            this.loadpos = new System.Windows.Forms.Button();
            this.savepos = new System.Windows.Forms.Button();
            this.skins_comboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // writetext
            // 
            this.writetext.AutoSize = true;
            this.writetext.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writetext.Location = new System.Drawing.Point(15, 35);
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
            this.levelinfo.Location = new System.Drawing.Point(16, 73);
            this.levelinfo.Name = "levelinfo";
            this.levelinfo.Size = new System.Drawing.Size(138, 20);
            this.levelinfo.TabIndex = 1;
            this.levelinfo.Text = "Challenge - Planet";
            // 
            // wrtext
            // 
            this.wrtext.AutoSize = true;
            this.wrtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wrtext.Location = new System.Drawing.Point(39, 100);
            this.wrtext.Name = "wrtext";
            this.wrtext.Size = new System.Drawing.Size(44, 20);
            this.wrtext.TabIndex = 2;
            this.wrtext.Text = "WR: ";
            // 
            // ghostcheck
            // 
            this.ghostcheck.AutoSize = true;
            this.ghostcheck.Location = new System.Drawing.Point(152, 104);
            this.ghostcheck.Name = "ghostcheck";
            this.ghostcheck.Size = new System.Drawing.Size(95, 17);
            this.ghostcheck.TabIndex = 3;
            this.ghostcheck.Text = "Ghost Ratchet";
            this.ghostcheck.UseVisualStyleBackColor = true;
            this.ghostcheck.CheckedChanged += new System.EventHandler(this.ghostcheck_CheckedChanged);
            // 
            // inputdisplaybutton
            // 
            this.inputdisplaybutton.Location = new System.Drawing.Point(239, 271);
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
            this.menuStrip1.Size = new System.Drawing.Size(363, 24);
            this.menuStrip1.TabIndex = 77;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patchLoaderToolStripMenuItem,
            this.memoryUtilitiesToolStripMenuItem,
            this.switchGameToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // patchLoaderToolStripMenuItem
            // 
            this.patchLoaderToolStripMenuItem.Name = "patchLoaderToolStripMenuItem";
            this.patchLoaderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.patchLoaderToolStripMenuItem.Text = "Patch loader...";
            this.patchLoaderToolStripMenuItem.Click += new System.EventHandler(this.patchLoaderToolStripMenuItem_Click);
            // 
            // memoryUtilitiesToolStripMenuItem
            // 
            this.memoryUtilitiesToolStripMenuItem.Name = "memoryUtilitiesToolStripMenuItem";
            this.memoryUtilitiesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.memoryUtilitiesToolStripMenuItem.Text = "Memory utilities";
            this.memoryUtilitiesToolStripMenuItem.Click += new System.EventHandler(this.memoryUtilitiesToolStripMenuItem_Click);
            // 
            // switchGameToolStripMenuItem
            // 
            this.switchGameToolStripMenuItem.Name = "switchGameToolStripMenuItem";
            this.switchGameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.switchGameToolStripMenuItem.Text = "Switch Game";
            this.switchGameToolStripMenuItem.Click += new System.EventHandler(this.switchGameToolStripMenuItem_Click);
            // 
            // AutosplitterCheckbox
            // 
            this.AutosplitterCheckbox.AutoSize = true;
            this.AutosplitterCheckbox.Location = new System.Drawing.Point(15, 152);
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
            this.label8.Location = new System.Drawing.Point(236, 132);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 107;
            this.label8.Text = "Bolt Count:";
            // 
            // bolts_textBox
            // 
            this.bolts_textBox.Location = new System.Drawing.Point(239, 148);
            this.bolts_textBox.Name = "bolts_textBox";
            this.bolts_textBox.Size = new System.Drawing.Size(106, 20);
            this.bolts_textBox.TabIndex = 106;
            // 
            // killyourself
            // 
            this.killyourself.Location = new System.Drawing.Point(239, 213);
            this.killyourself.Name = "killyourself";
            this.killyourself.Size = new System.Drawing.Size(106, 23);
            this.killyourself.TabIndex = 108;
            this.killyourself.Text = "Die";
            this.killyourself.UseVisualStyleBackColor = true;
            this.killyourself.Click += new System.EventHandler(this.killyourself_Click);
            // 
            // botsUnlocksWindowButton
            // 
            this.botsUnlocksWindowButton.Location = new System.Drawing.Point(239, 242);
            this.botsUnlocksWindowButton.Name = "botsUnlocksWindowButton";
            this.botsUnlocksWindowButton.Size = new System.Drawing.Size(106, 23);
            this.botsUnlocksWindowButton.TabIndex = 109;
            this.botsUnlocksWindowButton.Text = "Bots Unlocks";
            this.botsUnlocksWindowButton.UseVisualStyleBackColor = true;
            this.botsUnlocksWindowButton.Click += new System.EventHandler(this.botsUnlocksWindowButton_Click);
            // 
            // wrsFromSrcSiteCheck
            // 
            this.wrsFromSrcSiteCheck.AutoSize = true;
            this.wrsFromSrcSiteCheck.Location = new System.Drawing.Point(15, 128);
            this.wrsFromSrcSiteCheck.Name = "wrsFromSrcSiteCheck";
            this.wrsFromSrcSiteCheck.Size = new System.Drawing.Size(173, 17);
            this.wrsFromSrcSiteCheck.TabIndex = 110;
            this.wrsFromSrcSiteCheck.Text = "Show WRs from speedrun.com";
            this.wrsFromSrcSiteCheck.UseVisualStyleBackColor = true;
            this.wrsFromSrcSiteCheck.CheckedChanged += new System.EventHandler(this.wrsFromSrcSiteCheck_CheckedChanged);
            // 
            // buttonActTune
            // 
            this.buttonActTune.Location = new System.Drawing.Point(127, 271);
            this.buttonActTune.Name = "buttonActTune";
            this.buttonActTune.Size = new System.Drawing.Size(106, 23);
            this.buttonActTune.TabIndex = 111;
            this.buttonActTune.Text = "Act Tune Bosses";
            this.buttonActTune.UseVisualStyleBackColor = true;
            this.buttonActTune.Click += new System.EventHandler(this.buttonActTune_Click);
            // 
            // loadPlannetButton
            // 
            this.loadPlannetButton.Location = new System.Drawing.Point(137, 199);
            this.loadPlannetButton.Name = "loadPlannetButton";
            this.loadPlannetButton.Size = new System.Drawing.Size(85, 23);
            this.loadPlannetButton.TabIndex = 112;
            this.loadPlannetButton.Text = "Load";
            this.loadPlannetButton.UseVisualStyleBackColor = true;
            this.loadPlannetButton.Click += new System.EventHandler(this.loadPlannetButton_Click);
            // 
            // dreadPoints_textBox
            // 
            this.dreadPoints_textBox.Location = new System.Drawing.Point(239, 187);
            this.dreadPoints_textBox.Name = "dreadPoints_textBox";
            this.dreadPoints_textBox.Size = new System.Drawing.Size(106, 20);
            this.dreadPoints_textBox.TabIndex = 113;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(236, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 114;
            this.label1.Text = "Dread Points:";
            // 
            // freezeHealthCheckbox
            // 
            this.freezeHealthCheckbox.AutoSize = true;
            this.freezeHealthCheckbox.Location = new System.Drawing.Point(254, 104);
            this.freezeHealthCheckbox.Name = "freezeHealthCheckbox";
            this.freezeHealthCheckbox.Size = new System.Drawing.Size(92, 17);
            this.freezeHealthCheckbox.TabIndex = 115;
            this.freezeHealthCheckbox.Text = "Freeze Health";
            this.freezeHealthCheckbox.UseVisualStyleBackColor = true;
            this.freezeHealthCheckbox.CheckedChanged += new System.EventHandler(this.freezeHealthCheckbox_CheckedChanged);
            // 
            // skinsButton
            // 
            this.skinsButton.Location = new System.Drawing.Point(137, 237);
            this.skinsButton.Name = "skinsButton";
            this.skinsButton.Size = new System.Drawing.Size(85, 23);
            this.skinsButton.TabIndex = 120;
            this.skinsButton.Text = "Apply Skin";
            this.skinsButton.UseVisualStyleBackColor = true;
            this.skinsButton.Click += new System.EventHandler(this.skinsButton_Click);
            // 
            // CM_textBox
            // 
            this.CM_textBox.Location = new System.Drawing.Point(137, 168);
            this.CM_textBox.Name = "CM_textBox";
            this.CM_textBox.Size = new System.Drawing.Size(85, 20);
            this.CM_textBox.TabIndex = 116;
            // 
            // unlockPlanetsButton
            // 
            this.unlockPlanetsButton.Location = new System.Drawing.Point(15, 271);
            this.unlockPlanetsButton.Name = "unlockPlanetsButton";
            this.unlockPlanetsButton.Size = new System.Drawing.Size(106, 23);
            this.unlockPlanetsButton.TabIndex = 117;
            this.unlockPlanetsButton.Text = "Unlock All Planets";
            this.unlockPlanetsButton.UseVisualStyleBackColor = true;
            this.unlockPlanetsButton.Click += new System.EventHandler(this.unlockPlanetsButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 118;
            this.label2.Text = "Challenge Mode:";
            // 
            // planets_comboBox
            // 
            this.planets_comboBox.FormattingEnabled = true;
            this.planets_comboBox.Items.AddRange(new object[] {
            "DreadZoneStation",
            "CatacromFour",
            "Sarathos",
            "Kronos",
            "Shaar",
            "Orxon",
            "TheValixBelt",
            "Torval",
            "Stygia",
            "Maraxus",
            "GhostStation"});
            this.planets_comboBox.Location = new System.Drawing.Point(15, 199);
            this.planets_comboBox.Name = "planets_comboBox";
            this.planets_comboBox.Size = new System.Drawing.Size(112, 21);
            this.planets_comboBox.TabIndex = 28;
            this.planets_comboBox.SelectedIndexChanged += new System.EventHandler(this.planets_comboBox_SelectedIndexChanged);
            // 
            // loadpos
            // 
            this.loadpos.Location = new System.Drawing.Point(265, 44);
            this.loadpos.Name = "loadpos";
            this.loadpos.Size = new System.Drawing.Size(80, 23);
            this.loadpos.TabIndex = 1;
            this.loadpos.Text = "Load Position";
            this.loadpos.UseVisualStyleBackColor = true;
            this.loadpos.Click += new System.EventHandler(this.loadPosButton_Click);
            // 
            // savepos
            // 
            this.savepos.Location = new System.Drawing.Point(265, 73);
            this.savepos.Name = "savepos";
            this.savepos.Size = new System.Drawing.Size(80, 23);
            this.savepos.TabIndex = 0;
            this.savepos.Text = "Save Position";
            this.savepos.UseVisualStyleBackColor = true;
            this.savepos.Click += new System.EventHandler(this.savePosButton_Click);
            // 
            // skins_comboBox
            // 
            this.skins_comboBox.FormattingEnabled = true;
            this.skins_comboBox.Items.AddRange(new object[] {
            "Marauder",
            "Avenger",
            "Crusader",
            "Vindicator",
            "Liberator",
            "AlphaClank",
            "Squidzor",
            "LandShark",
            "TheMuscle",
            "W3RM",
            "Starshield",
            "KingClaude",
            "Vernon",
            "KidNova",
            "Venus",
            "Jak",
            "Ninja",
            "SaurusRatchet",
            "GenomeRatchet",
            "SantaRatchet",
            "PipoSaruRatchet",
            "Clankchet"});
            this.skins_comboBox.Location = new System.Drawing.Point(15, 239);
            this.skins_comboBox.Name = "skins_comboBox";
            this.skins_comboBox.Size = new System.Drawing.Size(112, 21);
            this.skins_comboBox.TabIndex = 28;
            this.skins_comboBox.SelectedIndexChanged += new System.EventHandler(this.skins_comboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 121;
            this.label3.Text = "Load Planet:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 122;
            this.label4.Text = "Skins:";
            // 
            // RAC4Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 313);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.loadPlannetButton);
            this.Controls.Add(this.wrsFromSrcSiteCheck);
            this.Controls.Add(this.botsUnlocksWindowButton);
            this.Controls.Add(this.killyourself);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.bolts_textBox);
            this.Controls.Add(this.dreadPoints_textBox);
            this.Controls.Add(this.skinsButton);
            this.Controls.Add(this.CM_textBox);
            this.Controls.Add(this.AutosplitterCheckbox);
            this.Controls.Add(this.inputdisplaybutton);
            this.Controls.Add(this.ghostcheck);
            this.Controls.Add(this.wrtext);
            this.Controls.Add(this.levelinfo);
            this.Controls.Add(this.buttonActTune);
            this.Controls.Add(this.freezeHealthCheckbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.unlockPlanetsButton);
            this.Controls.Add(this.skins_comboBox);
            this.Controls.Add(this.loadpos);
            this.Controls.Add(this.savepos);
            this.Controls.Add(this.writetext);
            this.Controls.Add(this.planets_comboBox);
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
        private System.Windows.Forms.ToolStripMenuItem switchGameToolStripMenuItem;
        private System.Windows.Forms.CheckBox AutosplitterCheckbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox bolts_textBox;
        private System.Windows.Forms.Button killyourself;
        private System.Windows.Forms.Button botsUnlocksWindowButton;
        private System.Windows.Forms.CheckBox wrsFromSrcSiteCheck;
        private System.Windows.Forms.Button buttonActTune;
        private System.Windows.Forms.Button loadPlannetButton;
        private System.Windows.Forms.TextBox dreadPoints_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox freezeHealthCheckbox;
        private System.Windows.Forms.Button skinsButton;
        private System.Windows.Forms.TextBox CM_textBox;
        private System.Windows.Forms.Button unlockPlanetsButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox planets_comboBox;
        private System.Windows.Forms.Button loadpos;
        private System.Windows.Forms.Button savepos;
        private System.Windows.Forms.ComboBox skins_comboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
