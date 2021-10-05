
namespace racman
{
    //

    partial class RAC3Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RAC3Form));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button5 = new System.Windows.Forms.Button();
            this.killyourself = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.loadpos = new System.Windows.Forms.Button();
            this.loadPlanetButton = new System.Windows.Forms.Button();
            this.savepos = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.positions_ComboBox = new System.Windows.Forms.ComboBox();
            this.planets_comboBox = new System.Windows.Forms.ComboBox();
            this.button6 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.currentlyDoing = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchLoaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.inputdisplay = new System.Windows.Forms.Button();
            this.OHKOCheckBox = new System.Windows.Forms.CheckBox();
            this.ghostCheckbox = new System.Windows.Forms.CheckBox();
            this.controllerCombosCheckbox = new System.Windows.Forms.CheckBox();
            this.freezeAmmoCheckBox = new System.Windows.Forms.CheckBox();
            this.unlockTitaniumBoltsButton = new System.Windows.Forms.Button();
            this.resetTitaniumBoltsButton = new System.Windows.Forms.Button();
            this.unlockSkillPointsButton = new System.Windows.Forms.Button();
            this.resetSkillPointsButton = new System.Windows.Forms.Button();
            this.challengeModeInput = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.armorComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.vidComicCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.freezeHealthCheck = new System.Windows.Forms.CheckBox();
            this.configureButtonCombosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.challengeModeInput)).BeginInit();
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
            this.killyourself.Location = new System.Drawing.Point(15, 103);
            this.killyourself.Name = "killyourself";
            this.killyourself.Size = new System.Drawing.Size(115, 23);
            this.killyourself.TabIndex = 7;
            this.killyourself.Text = "Die";
            this.killyourself.UseVisualStyleBackColor = true;
            this.killyourself.Click += new System.EventHandler(this.killyourself_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 13;
            // 
            // loadpos
            // 
            this.loadpos.Location = new System.Drawing.Point(15, 74);
            this.loadpos.Name = "loadpos";
            this.loadpos.Size = new System.Drawing.Size(115, 23);
            this.loadpos.TabIndex = 1;
            this.loadpos.Text = "Load Position";
            this.loadpos.UseVisualStyleBackColor = true;
            this.loadpos.Click += new System.EventHandler(this.loadPosButton_Click);
            // 
            // loadPlanetButton
            // 
            this.loadPlanetButton.Location = new System.Drawing.Point(133, 177);
            this.loadPlanetButton.Name = "loadPlanetButton";
            this.loadPlanetButton.Size = new System.Drawing.Size(75, 23);
            this.loadPlanetButton.TabIndex = 14;
            this.loadPlanetButton.Text = "Load";
            this.loadPlanetButton.UseVisualStyleBackColor = true;
            this.loadPlanetButton.Click += new System.EventHandler(this.loadPlanetButton_Click);
            // 
            // savepos
            // 
            this.savepos.Location = new System.Drawing.Point(15, 45);
            this.savepos.Name = "savepos";
            this.savepos.Size = new System.Drawing.Size(115, 23);
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
            // positions_ComboBox
            // 
            this.positions_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.positions_ComboBox.FormattingEnabled = true;
            this.positions_ComboBox.Items.AddRange(new object[] {
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
            this.positions_ComboBox.Location = new System.Drawing.Point(136, 47);
            this.positions_ComboBox.Name = "positions_ComboBox";
            this.positions_ComboBox.Size = new System.Drawing.Size(75, 21);
            this.positions_ComboBox.TabIndex = 27;
            this.positions_ComboBox.SelectedIndexChanged += new System.EventHandler(this.positions_ComboBox_SelectedIndexChanged);
            // 
            // planets_comboBox
            // 
            this.planets_comboBox.FormattingEnabled = true;
            this.planets_comboBox.Items.AddRange(new object[] {
            "Veldin",
            "Florana",
            "Starship Phoenix",
            "Marcadia",
            "Daxx",
            "Phoenix Rescue",
            "Annihilation Nation",
            "Aquatos",
            "Tyhrranosis",
            "Zeldrin Starport",
            "Obani Gemini",
            "Blackwater City",
            "Holostar",
            "Koros",
            "Unknown",
            "Metropolis",
            "Crash Site",
            "Aridia",
            "Qwarks Hideout",
            "Launch Site",
            "Obani Draco",
            "Command Center",
            "Holostar 2",
            "Insomniac Museum",
            "Unknown",
            "Metropolis Rangers",
            "Aquatos Clank",
            "Aquatos Sewers",
            "Tyhrranosis Rangers",
            "Vid Comic 6",
            "Vid Comic 1",
            "Vid Comic 4",
            "Vid Comic 2",
            "Vid Comic 3",
            "Vid Comic 5",
            "Vid Comic 1 Special Edition"});
            this.planets_comboBox.Location = new System.Drawing.Point(15, 179);
            this.planets_comboBox.Name = "planets_comboBox";
            this.planets_comboBox.Size = new System.Drawing.Size(112, 21);
            this.planets_comboBox.TabIndex = 28;
            this.planets_comboBox.SelectedIndexChanged += new System.EventHandler(this.planets_comboBox_SelectedIndexChanged);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Location = new System.Drawing.Point(360, 158);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(122, 23);
            this.button6.TabIndex = 29;
            this.button6.Text = "Setup No QE";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            this.button6.MouseHover += new System.EventHandler(this.button6_MouseHover);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(355, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 35;
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(228, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(112, 20);
            this.textBox1.TabIndex = 62;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 67;
            this.label6.Text = "Load Planet:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(226, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 68;
            this.label8.Text = "Bolt Count:";
            // 
            // currentlyDoing
            // 
            this.currentlyDoing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.currentlyDoing.AutoSize = true;
            this.currentlyDoing.Location = new System.Drawing.Point(420, 46);
            this.currentlyDoing.Name = "currentlyDoing";
            this.currentlyDoing.Size = new System.Drawing.Size(0, 13);
            this.currentlyDoing.TabIndex = 73;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(489, 24);
            this.menuStrip1.TabIndex = 75;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.switchGameToolStripMenuItem,
            this.patchLoaderToolStripMenuItem,
            this.configureButtonCombosToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // switchGameToolStripMenuItem
            // 
            this.switchGameToolStripMenuItem.Name = "switchGameToolStripMenuItem";
            this.switchGameToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.switchGameToolStripMenuItem.Text = "Switch Game";
            this.switchGameToolStripMenuItem.Click += new System.EventHandler(this.switchGameToolStripMenuItem_Click);
            // 
            // patchLoaderToolStripMenuItem
            // 
            this.patchLoaderToolStripMenuItem.Name = "patchLoaderToolStripMenuItem";
            this.patchLoaderToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.patchLoaderToolStripMenuItem.Text = "Patch loader...";
            this.patchLoaderToolStripMenuItem.Click += new System.EventHandler(this.patchLoaderToolStripMenuItem_Click);
            // 
            // inputdisplay
            // 
            this.inputdisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.inputdisplay.Location = new System.Drawing.Point(370, 292);
            this.inputdisplay.Name = "inputdisplay";
            this.inputdisplay.Size = new System.Drawing.Size(112, 23);
            this.inputdisplay.TabIndex = 76;
            this.inputdisplay.Text = "Input Display";
            this.inputdisplay.UseVisualStyleBackColor = true;
            this.inputdisplay.Click += new System.EventHandler(this.inputdisplay_Click);
            // 
            // OHKOCheckBox
            // 
            this.OHKOCheckBox.AutoSize = true;
            this.OHKOCheckBox.Location = new System.Drawing.Point(15, 229);
            this.OHKOCheckBox.Name = "OHKOCheckBox";
            this.OHKOCheckBox.Size = new System.Drawing.Size(80, 17);
            this.OHKOCheckBox.TabIndex = 77;
            this.OHKOCheckBox.Text = "One-Hit KO";
            this.OHKOCheckBox.UseVisualStyleBackColor = true;
            this.OHKOCheckBox.CheckedChanged += new System.EventHandler(this.OHKOCheckBox_CheckedChanged);
            // 
            // ghostCheckbox
            // 
            this.ghostCheckbox.AutoSize = true;
            this.ghostCheckbox.Location = new System.Drawing.Point(15, 206);
            this.ghostCheckbox.Name = "ghostCheckbox";
            this.ghostCheckbox.Size = new System.Drawing.Size(95, 17);
            this.ghostCheckbox.TabIndex = 78;
            this.ghostCheckbox.Text = "Ghost Ratchet";
            this.ghostCheckbox.UseVisualStyleBackColor = true;
            this.ghostCheckbox.CheckedChanged += new System.EventHandler(this.ghostCheckbox_CheckedChanged);
            // 
            // controllerCombosCheckbox
            // 
            this.controllerCombosCheckbox.AutoSize = true;
            this.controllerCombosCheckbox.Location = new System.Drawing.Point(15, 131);
            this.controllerCombosCheckbox.Name = "controllerCombosCheckbox";
            this.controllerCombosCheckbox.Size = new System.Drawing.Size(110, 17);
            this.controllerCombosCheckbox.TabIndex = 79;
            this.controllerCombosCheckbox.Text = "Controller combos";
            this.controllerCombosCheckbox.UseVisualStyleBackColor = true;
            this.controllerCombosCheckbox.CheckedChanged += new System.EventHandler(this.controllerCombosCheckbox_CheckedChanged);
            // 
            // freezeAmmoCheckBox
            // 
            this.freezeAmmoCheckBox.AutoSize = true;
            this.freezeAmmoCheckBox.Location = new System.Drawing.Point(15, 251);
            this.freezeAmmoCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.freezeAmmoCheckBox.Name = "freezeAmmoCheckBox";
            this.freezeAmmoCheckBox.Size = new System.Drawing.Size(89, 17);
            this.freezeAmmoCheckBox.TabIndex = 81;
            this.freezeAmmoCheckBox.Text = "Freeze ammo";
            this.freezeAmmoCheckBox.UseVisualStyleBackColor = true;
            this.freezeAmmoCheckBox.CheckedChanged += new System.EventHandler(this.freezeAmmoCheckBox_CheckedChanged);
            // 
            // unlockTitaniumBoltsButton
            // 
            this.unlockTitaniumBoltsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.unlockTitaniumBoltsButton.Location = new System.Drawing.Point(360, 41);
            this.unlockTitaniumBoltsButton.Margin = new System.Windows.Forms.Padding(2);
            this.unlockTitaniumBoltsButton.Name = "unlockTitaniumBoltsButton";
            this.unlockTitaniumBoltsButton.Size = new System.Drawing.Size(124, 23);
            this.unlockTitaniumBoltsButton.TabIndex = 82;
            this.unlockTitaniumBoltsButton.Text = "Unlock Titanium Bolts";
            this.unlockTitaniumBoltsButton.UseVisualStyleBackColor = true;
            this.unlockTitaniumBoltsButton.Click += new System.EventHandler(this.unlockTitaniumBoltsButton_Click);
            // 
            // resetTitaniumBoltsButton
            // 
            this.resetTitaniumBoltsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetTitaniumBoltsButton.Location = new System.Drawing.Point(360, 68);
            this.resetTitaniumBoltsButton.Margin = new System.Windows.Forms.Padding(2);
            this.resetTitaniumBoltsButton.Name = "resetTitaniumBoltsButton";
            this.resetTitaniumBoltsButton.Size = new System.Drawing.Size(124, 23);
            this.resetTitaniumBoltsButton.TabIndex = 83;
            this.resetTitaniumBoltsButton.Text = "Reset Titanium Bolts";
            this.resetTitaniumBoltsButton.UseVisualStyleBackColor = true;
            this.resetTitaniumBoltsButton.Click += new System.EventHandler(this.resetTitaniumBoltsButton_Click);
            // 
            // unlockSkillPointsButton
            // 
            this.unlockSkillPointsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.unlockSkillPointsButton.Location = new System.Drawing.Point(360, 102);
            this.unlockSkillPointsButton.Margin = new System.Windows.Forms.Padding(2);
            this.unlockSkillPointsButton.Name = "unlockSkillPointsButton";
            this.unlockSkillPointsButton.Size = new System.Drawing.Size(124, 23);
            this.unlockSkillPointsButton.TabIndex = 84;
            this.unlockSkillPointsButton.Text = "Unlock Skill Points";
            this.unlockSkillPointsButton.UseVisualStyleBackColor = true;
            this.unlockSkillPointsButton.Click += new System.EventHandler(this.unlockSkillPointsButton_Click);
            // 
            // resetSkillPointsButton
            // 
            this.resetSkillPointsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetSkillPointsButton.Location = new System.Drawing.Point(360, 129);
            this.resetSkillPointsButton.Margin = new System.Windows.Forms.Padding(2);
            this.resetSkillPointsButton.Name = "resetSkillPointsButton";
            this.resetSkillPointsButton.Size = new System.Drawing.Size(124, 23);
            this.resetSkillPointsButton.TabIndex = 85;
            this.resetSkillPointsButton.Text = "Reset Skill Points";
            this.resetSkillPointsButton.UseVisualStyleBackColor = true;
            this.resetSkillPointsButton.Click += new System.EventHandler(this.resetSkillPointsButton_Click);
            // 
            // challengeModeInput
            // 
            this.challengeModeInput.Location = new System.Drawing.Point(228, 86);
            this.challengeModeInput.Margin = new System.Windows.Forms.Padding(2);
            this.challengeModeInput.Name = "challengeModeInput";
            this.challengeModeInput.Size = new System.Drawing.Size(110, 20);
            this.challengeModeInput.TabIndex = 86;
            this.challengeModeInput.ValueChanged += new System.EventHandler(this.challengeModeInput_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(226, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 87;
            this.label3.Text = "Challenge Mode:";
            // 
            // armorComboBox
            // 
            this.armorComboBox.FormattingEnabled = true;
            this.armorComboBox.Items.AddRange(new object[] {
            "Alpha Combat Suit",
            "Magnaplate Armor",
            "Adamantine Armor",
            "Aegis Mark V Armor",
            "Infernox Armor",
            "OG Ratchet Skin",
            "Snowman Skin",
            "Tux Skin"});
            this.armorComboBox.Location = new System.Drawing.Point(228, 121);
            this.armorComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.armorComboBox.Name = "armorComboBox";
            this.armorComboBox.Size = new System.Drawing.Size(112, 21);
            this.armorComboBox.TabIndex = 88;
            this.armorComboBox.SelectedIndexChanged += new System.EventHandler(this.armorComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(226, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 89;
            this.label4.Text = "Armor:";
            // 
            // vidComicCheckedListBox
            // 
            this.vidComicCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.vidComicCheckedListBox.BackColor = System.Drawing.SystemColors.Menu;
            this.vidComicCheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.vidComicCheckedListBox.CheckOnClick = true;
            this.vidComicCheckedListBox.FormattingEnabled = true;
            this.vidComicCheckedListBox.Items.AddRange(new object[] {
            "Vid Comic 1",
            "Vid Comic 2",
            "Vid Comic 3",
            "Vid Comic 4",
            "Vid Comic 5"});
            this.vidComicCheckedListBox.Location = new System.Drawing.Point(360, 191);
            this.vidComicCheckedListBox.Margin = new System.Windows.Forms.Padding(2);
            this.vidComicCheckedListBox.Name = "vidComicCheckedListBox";
            this.vidComicCheckedListBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.vidComicCheckedListBox.Size = new System.Drawing.Size(124, 75);
            this.vidComicCheckedListBox.TabIndex = 90;
            this.vidComicCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.vidComicCheckedListBox_ItemCheck);
            this.vidComicCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.vidComicCheckedListBox_SelectedIndexChanged);
            // 
            // freezeHealthCheck
            // 
            this.freezeHealthCheck.AutoSize = true;
            this.freezeHealthCheck.Location = new System.Drawing.Point(15, 274);
            this.freezeHealthCheck.Name = "freezeHealthCheck";
            this.freezeHealthCheck.Size = new System.Drawing.Size(90, 17);
            this.freezeHealthCheck.TabIndex = 91;
            this.freezeHealthCheck.Text = "Freeze health";
            this.freezeHealthCheck.UseVisualStyleBackColor = true;
            this.freezeHealthCheck.CheckedChanged += new System.EventHandler(this.freezeHealthCheck_CheckedChanged);
            // 
            // configureButtonCombosToolStripMenuItem
            // 
            this.configureButtonCombosToolStripMenuItem.Name = "configureButtonCombosToolStripMenuItem";
            this.configureButtonCombosToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.configureButtonCombosToolStripMenuItem.Text = "Configure button combos";
            this.configureButtonCombosToolStripMenuItem.Click += new System.EventHandler(this.configureButtonCombosToolStripMenuItem_Click);
            // 
            // RAC3Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(489, 322);
            this.Controls.Add(this.freezeHealthCheck);
            this.Controls.Add(this.vidComicCheckedListBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.armorComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.challengeModeInput);
            this.Controls.Add(this.resetSkillPointsButton);
            this.Controls.Add(this.unlockSkillPointsButton);
            this.Controls.Add(this.resetTitaniumBoltsButton);
            this.Controls.Add(this.unlockTitaniumBoltsButton);
            this.Controls.Add(this.freezeAmmoCheckBox);
            this.Controls.Add(this.controllerCombosCheckbox);
            this.Controls.Add(this.ghostCheckbox);
            this.Controls.Add(this.OHKOCheckBox);
            this.Controls.Add(this.inputdisplay);
            this.Controls.Add(this.currentlyDoing);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.killyourself);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.loadpos);
            this.Controls.Add(this.loadPlanetButton);
            this.Controls.Add(this.savepos);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.positions_ComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.planets_comboBox);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "RAC3Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ratchet & Clank 3 - NPEA00387 (PAL)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.challengeModeInput)).EndInit();
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
        private System.Windows.Forms.ComboBox positions_ComboBox;
        private System.Windows.Forms.ComboBox planets_comboBox;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label currentlyDoing;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchGameToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button inputdisplay;
        private System.Windows.Forms.CheckBox OHKOCheckBox;
        private System.Windows.Forms.CheckBox ghostCheckbox;
        private System.Windows.Forms.CheckBox controllerCombosCheckbox;
        private System.Windows.Forms.CheckBox freezeAmmoCheckBox;
        private System.Windows.Forms.Button unlockTitaniumBoltsButton;
        private System.Windows.Forms.Button resetTitaniumBoltsButton;
        private System.Windows.Forms.Button unlockSkillPointsButton;
        private System.Windows.Forms.Button resetSkillPointsButton;
        private System.Windows.Forms.NumericUpDown challengeModeInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox armorComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox vidComicCheckedListBox;
        private System.Windows.Forms.CheckBox freezeHealthCheck;
        private System.Windows.Forms.ToolStripMenuItem patchLoaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureButtonCombosToolStripMenuItem;
    }
}

