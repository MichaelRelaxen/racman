
namespace racman
{
    partial class RAC1MpForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RAC1MpForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button5 = new System.Windows.Forms.Button();
            this.killyourself = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.loadpos = new System.Windows.Forms.Button();
            this.loadPlanetButton = new System.Windows.Forms.Button();
            this.savepos = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.positions_comboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.bolts_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.infHealth = new System.Windows.Forms.CheckBox();
            this.goodiesCheck = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buyPremiumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.patchLoaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryUtilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateHeroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateMobysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateParticlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.normalCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.freecamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.freecamCharacterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.ghostCheckbox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.FastLoadToggle = new System.Windows.Forms.CheckBox();
            this.FreezeAmmoCheckbox = new System.Windows.Forms.CheckBox();
            this.platinumLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.planets_comboBox = new System.Windows.Forms.ComboBox();
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
            // killyourself
            // 
            this.killyourself.Location = new System.Drawing.Point(109, 74);
            this.killyourself.Name = "killyourself";
            this.killyourself.Size = new System.Drawing.Size(89, 23);
            this.killyourself.TabIndex = 7;
            this.killyourself.Text = "Die";
            this.killyourself.UseVisualStyleBackColor = true;
            this.killyourself.Click += new System.EventHandler(this.killyourself_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 13;
            // 
            // loadpos
            // 
            this.loadpos.Location = new System.Drawing.Point(14, 74);
            this.loadpos.Name = "loadpos";
            this.loadpos.Size = new System.Drawing.Size(89, 23);
            this.loadpos.TabIndex = 1;
            this.loadpos.Text = "Load Position";
            this.loadpos.UseVisualStyleBackColor = true;
            this.loadpos.Click += new System.EventHandler(this.loadPosButton_Click);
            // 
            // loadPlanetButton
            // 
            this.loadPlanetButton.Location = new System.Drawing.Point(136, 127);
            this.loadPlanetButton.Name = "loadPlanetButton";
            this.loadPlanetButton.Size = new System.Drawing.Size(59, 23);
            this.loadPlanetButton.TabIndex = 14;
            this.loadPlanetButton.Text = "Load";
            this.loadPlanetButton.UseVisualStyleBackColor = true;
            this.loadPlanetButton.Click += new System.EventHandler(this.loadPlanetButton_Click_1);
            // 
            // savepos
            // 
            this.savepos.Location = new System.Drawing.Point(14, 45);
            this.savepos.Name = "savepos";
            this.savepos.Size = new System.Drawing.Size(89, 23);
            this.savepos.TabIndex = 0;
            this.savepos.Text = "Save Position";
            this.savepos.UseVisualStyleBackColor = true;
            this.savepos.Click += new System.EventHandler(this.savePosButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, -2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 21;
            // 
            // positions_comboBox
            // 
            this.positions_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.positions_comboBox.Location = new System.Drawing.Point(109, 47);
            this.positions_comboBox.Name = "positions_comboBox";
            this.positions_comboBox.Size = new System.Drawing.Size(88, 21);
            this.positions_comboBox.TabIndex = 27;
            this.positions_comboBox.SelectedIndexChanged += new System.EventHandler(this.positions_comboBox_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(214, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 39;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(213, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 13);
            this.label10.TabIndex = 41;
            // 
            // bolts_textBox
            // 
            this.bolts_textBox.Location = new System.Drawing.Point(250, 47);
            this.bolts_textBox.Name = "bolts_textBox";
            this.bolts_textBox.Size = new System.Drawing.Size(105, 20);
            this.bolts_textBox.TabIndex = 62;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(224, 259);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 63;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(246, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 68;
            this.label8.Text = "Bolt Count:";
            // 
            // infHealth
            // 
            this.infHealth.AutoSize = true;
            this.infHealth.Location = new System.Drawing.Point(250, 91);
            this.infHealth.Name = "infHealth";
            this.infHealth.Size = new System.Drawing.Size(91, 17);
            this.infHealth.TabIndex = 70;
            this.infHealth.Text = "Infinite Health";
            this.infHealth.UseVisualStyleBackColor = true;
            this.infHealth.CheckedChanged += new System.EventHandler(this.infHealth_Checkbox_Changed);
            // 
            // goodiesCheck
            // 
            this.goodiesCheck.AutoSize = true;
            this.goodiesCheck.Location = new System.Drawing.Point(250, 155);
            this.goodiesCheck.Name = "goodiesCheck";
            this.goodiesCheck.Size = new System.Drawing.Size(95, 17);
            this.goodiesCheck.TabIndex = 74;
            this.goodiesCheck.Text = "Goodies Menu";
            this.goodiesCheck.UseVisualStyleBackColor = true;
            this.goodiesCheck.CheckedChanged += new System.EventHandler(this.goodiesCheck_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(375, 24);
            this.menuStrip1.TabIndex = 76;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.switchGameToolStripMenuItem,
            this.buyPremiumToolStripMenuItem,
            this.toolStripSeparator1,
            this.patchLoaderToolStripMenuItem,
            this.memoryUtilitiesToolStripMenuItem,
            this.toolStripSeparator2});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            this.menuToolStripMenuItem.Click += new System.EventHandler(this.menuToolStripMenuItem_Click);
            // 
            // switchGameToolStripMenuItem
            // 
            this.switchGameToolStripMenuItem.Name = "switchGameToolStripMenuItem";
            this.switchGameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.switchGameToolStripMenuItem.Text = "Switch Game";
            this.switchGameToolStripMenuItem.Click += new System.EventHandler(this.switchGameToolStripMenuItem_Click);
            // 
            // buyPremiumToolStripMenuItem
            // 
            this.buyPremiumToolStripMenuItem.Name = "buyPremiumToolStripMenuItem";
            this.buyPremiumToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.buyPremiumToolStripMenuItem.Text = "Buy Platinum";
            this.buyPremiumToolStripMenuItem.Click += new System.EventHandler(this.buyPremiumToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
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
            this.memoryUtilitiesToolStripMenuItem.Text = "Memory utilities...";
            this.memoryUtilitiesToolStripMenuItem.Click += new System.EventHandler(this.memoryUtilitiesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateHeroToolStripMenuItem,
            this.updateMobysToolStripMenuItem,
            this.updateParticlesToolStripMenuItem,
            this.toolStripSeparator3,
            this.normalCameraToolStripMenuItem,
            this.freecamToolStripMenuItem,
            this.freecamCharacterToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.DropDownOpening += new System.EventHandler(this.debugToolStripMenuItem_DropDownOpening);
            // 
            // updateHeroToolStripMenuItem
            // 
            this.updateHeroToolStripMenuItem.Name = "updateHeroToolStripMenuItem";
            this.updateHeroToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.updateHeroToolStripMenuItem.Text = "Update Hero";
            this.updateHeroToolStripMenuItem.Click += new System.EventHandler(this.updateHeroToolStripMenuItem_Click);
            // 
            // updateMobysToolStripMenuItem
            // 
            this.updateMobysToolStripMenuItem.Name = "updateMobysToolStripMenuItem";
            this.updateMobysToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.updateMobysToolStripMenuItem.Text = "Update Mobys";
            this.updateMobysToolStripMenuItem.Click += new System.EventHandler(this.updateMobysToolStripMenuItem_Click);
            // 
            // updateParticlesToolStripMenuItem
            // 
            this.updateParticlesToolStripMenuItem.Name = "updateParticlesToolStripMenuItem";
            this.updateParticlesToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.updateParticlesToolStripMenuItem.Text = "Update Particles";
            this.updateParticlesToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(181, 6);
            // 
            // normalCameraToolStripMenuItem
            // 
            this.normalCameraToolStripMenuItem.Name = "normalCameraToolStripMenuItem";
            this.normalCameraToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.normalCameraToolStripMenuItem.Text = "Normal Camera";
            this.normalCameraToolStripMenuItem.Click += new System.EventHandler(this.normalCameraToolStripMenuItem_Click);
            // 
            // freecamToolStripMenuItem
            // 
            this.freecamToolStripMenuItem.Name = "freecamToolStripMenuItem";
            this.freecamToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.freecamToolStripMenuItem.Text = "Freecam";
            this.freecamToolStripMenuItem.Click += new System.EventHandler(this.freecamToolStripMenuItem_Click);
            // 
            // freecamCharacterToolStripMenuItem
            // 
            this.freecamCharacterToolStripMenuItem.Name = "freecamCharacterToolStripMenuItem";
            this.freecamCharacterToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.freecamCharacterToolStripMenuItem.Text = "Freecam + Character";
            this.freecamCharacterToolStripMenuItem.Click += new System.EventHandler(this.freecamCharacterToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(17, 188);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(337, 22);
            this.button2.TabIndex = 82;
            this.button2.Text = "Input Display";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.inputdisplay_click);
            // 
            // ghostCheckbox
            // 
            this.ghostCheckbox.AutoSize = true;
            this.ghostCheckbox.Location = new System.Drawing.Point(250, 132);
            this.ghostCheckbox.Name = "ghostCheckbox";
            this.ghostCheckbox.Size = new System.Drawing.Size(95, 17);
            this.ghostCheckbox.TabIndex = 83;
            this.ghostCheckbox.Text = "Ghost Ratchet";
            this.ghostCheckbox.UseVisualStyleBackColor = true;
            this.ghostCheckbox.CheckedChanged += new System.EventHandler(this.ghostCheckbox_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 87;
            this.label4.Text = "Toggles:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // FastLoadToggle
            // 
            this.FastLoadToggle.AutoSize = true;
            this.FastLoadToggle.Location = new System.Drawing.Point(16, 156);
            this.FastLoadToggle.Name = "FastLoadToggle";
            this.FastLoadToggle.Size = new System.Drawing.Size(114, 17);
            this.FastLoadToggle.TabIndex = 89;
            this.FastLoadToggle.Text = "Toggle Fast Loads";
            this.FastLoadToggle.UseVisualStyleBackColor = true;
            this.FastLoadToggle.CheckedChanged += new System.EventHandler(this.FastLoadToggle_CheckedChanged);
            // 
            // FreezeAmmoCheckbox
            // 
            this.FreezeAmmoCheckbox.AutoSize = true;
            this.FreezeAmmoCheckbox.Location = new System.Drawing.Point(250, 111);
            this.FreezeAmmoCheckbox.Name = "FreezeAmmoCheckbox";
            this.FreezeAmmoCheckbox.Size = new System.Drawing.Size(90, 17);
            this.FreezeAmmoCheckbox.TabIndex = 90;
            this.FreezeAmmoCheckbox.Text = "Freeze Ammo";
            this.FreezeAmmoCheckbox.UseVisualStyleBackColor = true;
            this.FreezeAmmoCheckbox.CheckedChanged += new System.EventHandler(this.FreezeAmmoCheckbox_CheckedChanged);
            // 
            // platinumLabel
            // 
            this.platinumLabel.AutoSize = true;
            this.platinumLabel.Location = new System.Drawing.Point(12, 29);
            this.platinumLabel.Name = "platinumLabel";
            this.platinumLabel.Size = new System.Drawing.Size(128, 13);
            this.platinumLabel.TabIndex = 94;
            this.platinumLabel.Text = "PLATINUM+   PREMIUM";
            this.platinumLabel.Visible = false;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 99;
            this.label6.Text = "Load Planet:";
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
            this.planets_comboBox.Location = new System.Drawing.Point(18, 129);
            this.planets_comboBox.Name = "planets_comboBox";
            this.planets_comboBox.Size = new System.Drawing.Size(112, 21);
            this.planets_comboBox.TabIndex = 28;
            this.planets_comboBox.SelectedIndexChanged += new System.EventHandler(this.planets_comboBox_SelectedIndexChanged);
            // 
            // RAC1MpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(375, 223);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.loadPlanetButton);
            this.Controls.Add(this.planets_comboBox);
            this.Controls.Add(this.platinumLabel);
            this.Controls.Add(this.FreezeAmmoCheckbox);
            this.Controls.Add(this.FastLoadToggle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ghostCheckbox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.goodiesCheck);
            this.Controls.Add(this.infHealth);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bolts_textBox);
            this.Controls.Add(this.killyourself);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.loadpos);
            this.Controls.Add(this.savepos);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.positions_comboBox);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "RAC1MpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ratchet & Clank 1 Randomizer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.RAC1MpForm_Load);
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
        private System.Windows.Forms.Button killyourself;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button loadpos;
        private System.Windows.Forms.Button loadPlanetButton;
        private System.Windows.Forms.Button savepos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox positions_comboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox bolts_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox infHealth;
        private System.Windows.Forms.CheckBox goodiesCheck;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchGameToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox ghostCheckbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox FastLoadToggle;
        private System.Windows.Forms.CheckBox FreezeAmmoCheckbox;
        private System.Windows.Forms.ToolStripMenuItem patchLoaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buyPremiumToolStripMenuItem;
        private System.Windows.Forms.Label platinumLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem memoryUtilitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem updateHeroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateMobysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateParticlesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem normalCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem freecamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem freecamCharacterToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox planets_comboBox;
    }
}