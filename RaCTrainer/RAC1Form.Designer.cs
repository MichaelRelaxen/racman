
namespace racman
{
    partial class RAC1Form
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button5 = new System.Windows.Forms.Button();
            this.gbsreset = new System.Windows.Forms.Button();
            this.ghostrac = new System.Windows.Forms.Button();
            this.killyourself = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.loadpos = new System.Windows.Forms.Button();
            this.loadPlanetButton = new System.Windows.Forms.Button();
            this.savepos = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.positions_comboBox = new System.Windows.Forms.ComboBox();
            this.planets_comboBox = new System.Windows.Forms.ComboBox();
            this.unlockGoldBoltsButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.bolts_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.unlocksWindowButton = new System.Windows.Forms.Button();
            this.infHealth = new System.Windows.Forms.CheckBox();
            this.drekSkipCheck = new System.Windows.Forms.CheckBox();
            this.goodiesCheck = new System.Windows.Forms.CheckBox();
            this.lflagresetCb = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hotkeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.planetPosName = new System.Windows.Forms.TextBox();
            this.planetpos_label = new System.Windows.Forms.Label();
            this.addPlanetPos = new System.Windows.Forms.Button();
            this.deletePlanetPosition = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(341, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(150, 100);
            this.splitContainer1.TabIndex = 18;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(0, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 0;
            // 
            // gbsreset
            // 
            this.gbsreset.Location = new System.Drawing.Point(344, 157);
            this.gbsreset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbsreset.Name = "gbsreset";
            this.gbsreset.Size = new System.Drawing.Size(202, 35);
            this.gbsreset.TabIndex = 11;
            this.gbsreset.Text = "Reset All Gold Bolts";
            this.gbsreset.UseVisualStyleBackColor = true;
            this.gbsreset.Click += new System.EventHandler(this.gbsreset_Click);
            // 
            // ghostrac
            // 
            this.ghostrac.Location = new System.Drawing.Point(344, 202);
            this.ghostrac.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ghostrac.Name = "ghostrac";
            this.ghostrac.Size = new System.Drawing.Size(202, 35);
            this.ghostrac.TabIndex = 8;
            this.ghostrac.Text = "Ghost Ratchet";
            this.ghostrac.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ghostrac.UseVisualStyleBackColor = true;
            this.ghostrac.Click += new System.EventHandler(this.ghostrac_Click);
            // 
            // killyourself
            // 
            this.killyourself.Location = new System.Drawing.Point(21, 158);
            this.killyourself.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.killyourself.Name = "killyourself";
            this.killyourself.Size = new System.Drawing.Size(172, 35);
            this.killyourself.TabIndex = 7;
            this.killyourself.Text = "Die";
            this.killyourself.UseVisualStyleBackColor = true;
            this.killyourself.Click += new System.EventHandler(this.killyourself_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 208);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 13;
            // 
            // loadpos
            // 
            this.loadpos.Location = new System.Drawing.Point(21, 114);
            this.loadpos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loadpos.Name = "loadpos";
            this.loadpos.Size = new System.Drawing.Size(172, 35);
            this.loadpos.TabIndex = 1;
            this.loadpos.Text = "Load Position";
            this.loadpos.UseVisualStyleBackColor = true;
            this.loadpos.Click += new System.EventHandler(this.loadPosButton_Click);
            // 
            // loadPlanetButton
            // 
            this.loadPlanetButton.Location = new System.Drawing.Point(202, 234);
            this.loadPlanetButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loadPlanetButton.Name = "loadPlanetButton";
            this.loadPlanetButton.Size = new System.Drawing.Size(112, 35);
            this.loadPlanetButton.TabIndex = 14;
            this.loadPlanetButton.Text = "Load";
            this.loadPlanetButton.UseVisualStyleBackColor = true;
            this.loadPlanetButton.Click += new System.EventHandler(this.loadPlanetButton_Click_1);
            // 
            // savepos
            // 
            this.savepos.Location = new System.Drawing.Point(21, 69);
            this.savepos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.savepos.Name = "savepos";
            this.savepos.Size = new System.Drawing.Size(172, 35);
            this.savepos.TabIndex = 0;
            this.savepos.Text = "Save Position";
            this.savepos.UseVisualStyleBackColor = true;
            this.savepos.Click += new System.EventHandler(this.savePosButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, -3);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 20);
            this.label7.TabIndex = 21;
            // 
            // positions_comboBox
            // 
            this.positions_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.positions_comboBox.FormattingEnabled = true;
            this.positions_comboBox.Location = new System.Drawing.Point(202, 72);
            this.positions_comboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.positions_comboBox.Name = "positions_comboBox";
            this.positions_comboBox.Size = new System.Drawing.Size(110, 28);
            this.positions_comboBox.TabIndex = 27;
            // 
            // planets_comboBox
            // 
            this.planets_comboBox.FormattingEnabled = true;
            this.planets_comboBox.Items.AddRange(new object[] {
            "Veldin",
            "Novalis",
            "Aridia",
            "Kerwan",
            "Eudora",
            "Rilgar",
            "Blarg",
            "Umbris",
            "Batalia",
            "Gaspar",
            "Orxon",
            "Pokitaru",
            "Hoven",
            "Gemlik",
            "Oltanis",
            "Quartu",
            "Kalebo III",
            "Fleet",
            "Veldin 2"});
            this.planets_comboBox.Location = new System.Drawing.Point(26, 237);
            this.planets_comboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.planets_comboBox.Name = "planets_comboBox";
            this.planets_comboBox.Size = new System.Drawing.Size(166, 28);
            this.planets_comboBox.TabIndex = 28;
            // 
            // unlockGoldBoltsButton
            // 
            this.unlockGoldBoltsButton.Location = new System.Drawing.Point(345, 112);
            this.unlockGoldBoltsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.unlockGoldBoltsButton.Name = "unlockGoldBoltsButton";
            this.unlockGoldBoltsButton.Size = new System.Drawing.Size(202, 35);
            this.unlockGoldBoltsButton.TabIndex = 31;
            this.unlockGoldBoltsButton.Text = "Unlock All Gold Bolts";
            this.unlockGoldBoltsButton.UseVisualStyleBackColor = true;
            this.unlockGoldBoltsButton.Click += new System.EventHandler(this.unlockGoldBoltsButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(321, 11);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 20);
            this.label9.TabIndex = 39;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(320, 72);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 20);
            this.label10.TabIndex = 41;
            // 
            // bolts_textBox
            // 
            this.bolts_textBox.Location = new System.Drawing.Point(345, 72);
            this.bolts_textBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bolts_textBox.Name = "bolts_textBox";
            this.bolts_textBox.Size = new System.Drawing.Size(200, 26);
            this.bolts_textBox.TabIndex = 62;
            this.bolts_textBox.TextChanged += new System.EventHandler(this.bolts_textBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(339, 285);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 20);
            this.label3.TabIndex = 63;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 212);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 20);
            this.label6.TabIndex = 67;
            this.label6.Text = "Load Planet:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(342, 51);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 20);
            this.label8.TabIndex = 68;
            this.label8.Text = "Bolt Count:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // unlocksWindowButton
            // 
            this.unlocksWindowButton.Location = new System.Drawing.Point(434, 246);
            this.unlocksWindowButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.unlocksWindowButton.Name = "unlocksWindowButton";
            this.unlocksWindowButton.Size = new System.Drawing.Size(112, 35);
            this.unlocksWindowButton.TabIndex = 72;
            this.unlocksWindowButton.Text = "Unlocks";
            this.unlocksWindowButton.UseVisualStyleBackColor = true;
            this.unlocksWindowButton.Click += new System.EventHandler(this.unlocksWindowButton_Click);
            // 
            // infHealth
            // 
            this.infHealth.AutoSize = true;
            this.infHealth.Location = new System.Drawing.Point(26, 278);
            this.infHealth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.infHealth.Name = "infHealth";
            this.infHealth.Size = new System.Drawing.Size(127, 24);
            this.infHealth.TabIndex = 70;
            this.infHealth.Text = "Infinite Health";
            this.infHealth.UseVisualStyleBackColor = true;
            this.infHealth.CheckedChanged += new System.EventHandler(this.infHealth_Checkbox_Changed);
            // 
            // drekSkipCheck
            // 
            this.drekSkipCheck.AutoSize = true;
            this.drekSkipCheck.Location = new System.Drawing.Point(26, 312);
            this.drekSkipCheck.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.drekSkipCheck.Name = "drekSkipCheck";
            this.drekSkipCheck.Size = new System.Drawing.Size(97, 24);
            this.drekSkipCheck.TabIndex = 73;
            this.drekSkipCheck.Text = "Drek Skip";
            this.drekSkipCheck.UseVisualStyleBackColor = true;
            this.drekSkipCheck.CheckedChanged += new System.EventHandler(this.drekSkipCheck_CheckedChanged);
            // 
            // goodiesCheck
            // 
            this.goodiesCheck.AutoSize = true;
            this.goodiesCheck.Location = new System.Drawing.Point(26, 348);
            this.goodiesCheck.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.goodiesCheck.Name = "goodiesCheck";
            this.goodiesCheck.Size = new System.Drawing.Size(132, 24);
            this.goodiesCheck.TabIndex = 74;
            this.goodiesCheck.Text = "Goodies Menu";
            this.goodiesCheck.UseVisualStyleBackColor = true;
            this.goodiesCheck.CheckedChanged += new System.EventHandler(this.goodiesCheck_CheckedChanged);
            // 
            // lflagresetCb
            // 
            this.lflagresetCb.AutoSize = true;
            this.lflagresetCb.Location = new System.Drawing.Point(202, 278);
            this.lflagresetCb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lflagresetCb.Name = "lflagresetCb";
            this.lflagresetCb.Size = new System.Drawing.Size(144, 24);
            this.lflagresetCb.TabIndex = 75;
            this.lflagresetCb.Text = "Reset level flags";
            this.lflagresetCb.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(626, 25);
            this.menuStrip1.TabIndex = 76;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hotkeysToolStripMenuItem,
            this.switchGameToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 19);
            this.menuToolStripMenuItem.Text = "Menu";
            this.menuToolStripMenuItem.Click += new System.EventHandler(this.menuToolStripMenuItem_Click);
            // 
            // hotkeysToolStripMenuItem
            // 
            this.hotkeysToolStripMenuItem.Name = "hotkeysToolStripMenuItem";
            this.hotkeysToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.hotkeysToolStripMenuItem.Text = "Hotkeys";
            this.hotkeysToolStripMenuItem.Click += new System.EventHandler(this.hotkeysToolStripMenuItem_Click);
            // 
            // switchGameToolStripMenuItem
            // 
            this.switchGameToolStripMenuItem.Name = "switchGameToolStripMenuItem";
            this.switchGameToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.switchGameToolStripMenuItem.Text = "Switch Game";
            this.switchGameToolStripMenuItem.Click += new System.EventHandler(this.switchGameToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(434, 291);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 77;
            this.button1.Text = "hoven hp";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.hovenHPButton_click);
            // 
            // planetPosName
            // 
            this.planetPosName.Location = new System.Drawing.Point(18, 463);
            this.planetPosName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.planetPosName.Name = "planetPosName";
            this.planetPosName.Size = new System.Drawing.Size(148, 26);
            this.planetPosName.TabIndex = 78;
            // 
            // planetpos_label
            // 
            this.planetpos_label.AutoSize = true;
            this.planetpos_label.Location = new System.Drawing.Point(14, 435);
            this.planetpos_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.planetpos_label.Name = "planetpos_label";
            this.planetpos_label.Size = new System.Drawing.Size(114, 20);
            this.planetpos_label.TabIndex = 79;
            this.planetpos_label.Text = "Planet Position";
            // 
            // addPlanetPos
            // 
            this.addPlanetPos.Location = new System.Drawing.Point(178, 463);
            this.addPlanetPos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addPlanetPos.Name = "addPlanetPos";
            this.addPlanetPos.Size = new System.Drawing.Size(63, 35);
            this.addPlanetPos.TabIndex = 80;
            this.addPlanetPos.Text = "Add";
            this.addPlanetPos.UseVisualStyleBackColor = true;
            this.addPlanetPos.Click += new System.EventHandler(this.addPlanetPos_Click);
            // 
            // deletePlanetPosition
            // 
            this.deletePlanetPosition.Location = new System.Drawing.Point(250, 463);
            this.deletePlanetPosition.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.deletePlanetPosition.Name = "deletePlanetPosition";
            this.deletePlanetPosition.Size = new System.Drawing.Size(88, 35);
            this.deletePlanetPosition.TabIndex = 81;
            this.deletePlanetPosition.Text = "Delete";
            this.deletePlanetPosition.UseVisualStyleBackColor = true;
            this.deletePlanetPosition.Click += new System.EventHandler(this.deletePlanetPosition_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(493, 464);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 34);
            this.button2.TabIndex = 82;
            this.button2.Text = "Input display";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // RAC1Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(626, 517);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.deletePlanetPosition);
            this.Controls.Add(this.addPlanetPos);
            this.Controls.Add(this.planetpos_label);
            this.Controls.Add(this.planetPosName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lflagresetCb);
            this.Controls.Add(this.goodiesCheck);
            this.Controls.Add(this.drekSkipCheck);
            this.Controls.Add(this.unlocksWindowButton);
            this.Controls.Add(this.infHealth);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gbsreset);
            this.Controls.Add(this.bolts_textBox);
            this.Controls.Add(this.ghostrac);
            this.Controls.Add(this.killyourself);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.loadpos);
            this.Controls.Add(this.loadPlanetButton);
            this.Controls.Add(this.savepos);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.positions_comboBox);
            this.Controls.Add(this.planets_comboBox);
            this.Controls.Add(this.unlockGoldBoltsButton);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "RAC1Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ratchet & Clank 1 - NPEA00385 (PAL)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button gbsreset;
        private System.Windows.Forms.Button ghostrac;
        private System.Windows.Forms.Button killyourself;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button loadpos;
        private System.Windows.Forms.Button loadPlanetButton;
        private System.Windows.Forms.Button savepos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox positions_comboBox;
        private System.Windows.Forms.ComboBox planets_comboBox;
        private System.Windows.Forms.Button unlockGoldBoltsButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox bolts_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button unlocksWindowButton;
        private System.Windows.Forms.CheckBox infHealth;
        private System.Windows.Forms.CheckBox drekSkipCheck;
        private System.Windows.Forms.CheckBox goodiesCheck;
        private System.Windows.Forms.CheckBox lflagresetCb;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hotkeysToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox planetPosName;
        private System.Windows.Forms.Label planetpos_label;
        private System.Windows.Forms.Button addPlanetPos;
        private System.Windows.Forms.Button deletePlanetPosition;
        private System.Windows.Forms.ToolStripMenuItem switchGameToolStripMenuItem;
        private System.Windows.Forms.Button button2;
    }
}

