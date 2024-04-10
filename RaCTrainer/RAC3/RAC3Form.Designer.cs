
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
            this.configureButtonCombosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.patchLoaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryUtilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autosplitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editRouteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.setAsideButton = new System.Windows.Forms.Button();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.toggleQS = new System.Windows.Forms.Button();
            this.coordsComboBox = new System.Windows.Forms.CheckBox();
            this.coordsLabel = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.shipColourComboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonUnlocks = new System.Windows.Forms.Button();
            this.buttonSetup = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.flagViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            "StarshipPhoenix",
            "Marcadia",
            "Daxx",
            "ObaniGemini",
            "BlackwaterCity",
            "AnnihilationNation ",
            "Aquatos",
            "AquatosClank",
            "AquatosSewers",
            "Tyhrranosis",
            "ZeldrinStarport",
            "Holostar",
            "HolostarClank",
            "ObaniDraco",
            "ZeldrinStarport",
            "Metropolis",
            "CrashSite",
            "Aridia",
            "QwarksHideout",
            "PhoenixRescue",
            "Koros ",
            "CommandCenter",
            "LaunchSite",
            "InsomniacMuseum",
            "TyhrranosisRangers",
            "MetropolisRangers",
            "VidComic1",
            "VidComic2",
            "VidComic3",
            "VidComic4",
            "VidComic5",
            "VidComic6",
            "VidComic1SpecialEdition",
            "Unknown",
            "Unknown2"});
            this.planets_comboBox.Location = new System.Drawing.Point(15, 179);
            this.planets_comboBox.Name = "planets_comboBox";
            this.planets_comboBox.Size = new System.Drawing.Size(112, 21);
            this.planets_comboBox.TabIndex = 28;
            this.planets_comboBox.SelectedIndexChanged += new System.EventHandler(this.planets_comboBox_SelectedIndexChanged);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(237, 279);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(132, 23);
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
            this.textBox1.Location = new System.Drawing.Point(236, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(135, 20);
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
            this.label8.Location = new System.Drawing.Point(234, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 68;
            this.label8.Text = "Bolt Count:";
            // 
            // currentlyDoing
            // 
            this.currentlyDoing.AutoSize = true;
            this.currentlyDoing.Location = new System.Drawing.Point(303, 163);
            this.currentlyDoing.Name = "currentlyDoing";
            this.currentlyDoing.Size = new System.Drawing.Size(0, 13);
            this.currentlyDoing.TabIndex = 73;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.autosplitterToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(385, 24);
            this.menuStrip1.TabIndex = 75;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.switchGameToolStripMenuItem,
            this.configureButtonCombosToolStripMenuItem,
            this.toolStripSeparator1,
            this.flagViewerToolStripMenuItem,
            this.patchLoaderToolStripMenuItem,
            this.memoryUtilitiesToolStripMenuItem});
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
            // patchLoaderToolStripMenuItem
            // 
            this.patchLoaderToolStripMenuItem.Name = "patchLoaderToolStripMenuItem";
            this.patchLoaderToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.patchLoaderToolStripMenuItem.Text = "Mods and patches...";
            this.patchLoaderToolStripMenuItem.Click += new System.EventHandler(this.patchLoaderToolStripMenuItem_Click);
            // 
            // memoryUtilitiesToolStripMenuItem
            // 
            this.memoryUtilitiesToolStripMenuItem.Name = "memoryUtilitiesToolStripMenuItem";
            this.memoryUtilitiesToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.memoryUtilitiesToolStripMenuItem.Text = "Memory utilities";
            this.memoryUtilitiesToolStripMenuItem.Click += new System.EventHandler(this.memoryUtilitiesToolStripMenuItem_Click);
            // 
            // autosplitterToolStripMenuItem
            // 
            this.autosplitterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editRouteToolStripMenuItem});
            this.autosplitterToolStripMenuItem.Name = "autosplitterToolStripMenuItem";
            this.autosplitterToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.autosplitterToolStripMenuItem.Text = "Autosplitter";
            // 
            // editRouteToolStripMenuItem
            // 
            this.editRouteToolStripMenuItem.Name = "editRouteToolStripMenuItem";
            this.editRouteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editRouteToolStripMenuItem.Text = "Edit route...";
            this.editRouteToolStripMenuItem.Click += new System.EventHandler(this.editRouteToolStripMenuItem_Click);
            // 
            // inputdisplay
            // 
            this.inputdisplay.Location = new System.Drawing.Point(236, 338);
            this.inputdisplay.Name = "inputdisplay";
            this.inputdisplay.Size = new System.Drawing.Size(133, 23);
            this.inputdisplay.TabIndex = 76;
            this.inputdisplay.Text = "Input Display";
            this.inputdisplay.UseVisualStyleBackColor = true;
            this.inputdisplay.Click += new System.EventHandler(this.inputdisplay_Click);
            // 
            // OHKOCheckBox
            // 
            this.OHKOCheckBox.AutoSize = true;
            this.OHKOCheckBox.Location = new System.Drawing.Point(16, 239);
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
            this.ghostCheckbox.Location = new System.Drawing.Point(16, 223);
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
            this.freezeAmmoCheckBox.Location = new System.Drawing.Point(16, 255);
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
            this.unlockTitaniumBoltsButton.Location = new System.Drawing.Point(236, 158);
            this.unlockTitaniumBoltsButton.Margin = new System.Windows.Forms.Padding(2);
            this.unlockTitaniumBoltsButton.Name = "unlockTitaniumBoltsButton";
            this.unlockTitaniumBoltsButton.Size = new System.Drawing.Size(135, 23);
            this.unlockTitaniumBoltsButton.TabIndex = 82;
            this.unlockTitaniumBoltsButton.Text = "Unlock All Titanium Bolts";
            this.unlockTitaniumBoltsButton.UseVisualStyleBackColor = true;
            this.unlockTitaniumBoltsButton.Click += new System.EventHandler(this.unlockTitaniumBoltsButton_Click);
            // 
            // resetTitaniumBoltsButton
            // 
            this.resetTitaniumBoltsButton.Location = new System.Drawing.Point(236, 185);
            this.resetTitaniumBoltsButton.Margin = new System.Windows.Forms.Padding(2);
            this.resetTitaniumBoltsButton.Name = "resetTitaniumBoltsButton";
            this.resetTitaniumBoltsButton.Size = new System.Drawing.Size(135, 23);
            this.resetTitaniumBoltsButton.TabIndex = 83;
            this.resetTitaniumBoltsButton.Text = "Reset All Titanium Bolts";
            this.resetTitaniumBoltsButton.UseVisualStyleBackColor = true;
            this.resetTitaniumBoltsButton.Click += new System.EventHandler(this.resetTitaniumBoltsButton_Click);
            // 
            // unlockSkillPointsButton
            // 
            this.unlockSkillPointsButton.Location = new System.Drawing.Point(236, 219);
            this.unlockSkillPointsButton.Margin = new System.Windows.Forms.Padding(2);
            this.unlockSkillPointsButton.Name = "unlockSkillPointsButton";
            this.unlockSkillPointsButton.Size = new System.Drawing.Size(135, 23);
            this.unlockSkillPointsButton.TabIndex = 84;
            this.unlockSkillPointsButton.Text = "Unlock All Skill Points";
            this.unlockSkillPointsButton.UseVisualStyleBackColor = true;
            this.unlockSkillPointsButton.Click += new System.EventHandler(this.unlockSkillPointsButton_Click);
            // 
            // resetSkillPointsButton
            // 
            this.resetSkillPointsButton.Location = new System.Drawing.Point(236, 246);
            this.resetSkillPointsButton.Margin = new System.Windows.Forms.Padding(2);
            this.resetSkillPointsButton.Name = "resetSkillPointsButton";
            this.resetSkillPointsButton.Size = new System.Drawing.Size(135, 23);
            this.resetSkillPointsButton.TabIndex = 85;
            this.resetSkillPointsButton.Text = "Reset All Skill Points";
            this.resetSkillPointsButton.UseVisualStyleBackColor = true;
            this.resetSkillPointsButton.Click += new System.EventHandler(this.resetSkillPointsButton_Click);
            // 
            // challengeModeInput
            // 
            this.challengeModeInput.Location = new System.Drawing.Point(236, 86);
            this.challengeModeInput.Margin = new System.Windows.Forms.Padding(2);
            this.challengeModeInput.Name = "challengeModeInput";
            this.challengeModeInput.Size = new System.Drawing.Size(133, 20);
            this.challengeModeInput.TabIndex = 86;
            this.challengeModeInput.ValueChanged += new System.EventHandler(this.challengeModeInput_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 71);
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
            this.armorComboBox.Location = new System.Drawing.Point(236, 121);
            this.armorComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.armorComboBox.Name = "armorComboBox";
            this.armorComboBox.Size = new System.Drawing.Size(135, 21);
            this.armorComboBox.TabIndex = 88;
            this.armorComboBox.SelectedIndexChanged += new System.EventHandler(this.armorComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 89;
            this.label4.Text = "Armor:";
            // 
            // vidComicCheckedListBox
            // 
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
            this.vidComicCheckedListBox.Location = new System.Drawing.Point(132, 221);
            this.vidComicCheckedListBox.Margin = new System.Windows.Forms.Padding(5);
            this.vidComicCheckedListBox.Name = "vidComicCheckedListBox";
            this.vidComicCheckedListBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.vidComicCheckedListBox.Size = new System.Drawing.Size(85, 75);
            this.vidComicCheckedListBox.TabIndex = 90;
            this.vidComicCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.vidComicCheckedListBox_ItemCheck);
            this.vidComicCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.vidComicCheckedListBox_SelectedIndexChanged);
            // 
            // freezeHealthCheck
            // 
            this.freezeHealthCheck.AutoSize = true;
            this.freezeHealthCheck.Location = new System.Drawing.Point(16, 271);
            this.freezeHealthCheck.Name = "freezeHealthCheck";
            this.freezeHealthCheck.Size = new System.Drawing.Size(90, 17);
            this.freezeHealthCheck.TabIndex = 91;
            this.freezeHealthCheck.Text = "Freeze health";
            this.freezeHealthCheck.UseVisualStyleBackColor = true;
            this.freezeHealthCheck.CheckedChanged += new System.EventHandler(this.freezeHealthCheck_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 203);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 93;
            this.label5.Text = "Toggles:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(130, 203);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 95;
            this.label12.Text = "Vid Comics:";
            // 
            // setAsideButton
            // 
            this.setAsideButton.Enabled = false;
            this.setAsideButton.Location = new System.Drawing.Point(136, 74);
            this.setAsideButton.Name = "setAsideButton";
            this.setAsideButton.Size = new System.Drawing.Size(75, 23);
            this.setAsideButton.TabIndex = 97;
            this.setAsideButton.Text = "Set aside file";
            this.setAsideButton.UseVisualStyleBackColor = true;
            this.setAsideButton.Click += new System.EventHandler(this.setAsideButton_Click);
            // 
            // loadFileButton
            // 
            this.loadFileButton.Enabled = false;
            this.loadFileButton.Location = new System.Drawing.Point(136, 103);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(75, 23);
            this.loadFileButton.TabIndex = 98;
            this.loadFileButton.Text = "Load file";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // toggleQS
            // 
            this.toggleQS.Location = new System.Drawing.Point(236, 308);
            this.toggleQS.Name = "toggleQS";
            this.toggleQS.Size = new System.Drawing.Size(133, 23);
            this.toggleQS.TabIndex = 99;
            this.toggleQS.Text = "Toggle QS Pause";
            this.toggleQS.UseVisualStyleBackColor = true;
            this.toggleQS.Click += new System.EventHandler(this.toggleQS_Click);
            // 
            // coordsComboBox
            // 
            this.coordsComboBox.AutoSize = true;
            this.coordsComboBox.Location = new System.Drawing.Point(16, 289);
            this.coordsComboBox.Name = "coordsComboBox";
            this.coordsComboBox.Size = new System.Drawing.Size(112, 17);
            this.coordsComboBox.TabIndex = 100;
            this.coordsComboBox.Text = "Show Coordinates";
            this.coordsComboBox.UseVisualStyleBackColor = true;
            this.coordsComboBox.CheckedChanged += new System.EventHandler(this.coordsComboBox_CheckedChanged);
            // 
            // coordsLabel
            // 
            this.coordsLabel.AutoSize = true;
            this.coordsLabel.Location = new System.Drawing.Point(13, 424);
            this.coordsLabel.Name = "coordsLabel";
            this.coordsLabel.Size = new System.Drawing.Size(123, 13);
            this.coordsLabel.TabIndex = 101;
            this.coordsLabel.Text = "Co-ordinates: <disabled>";
            this.coordsLabel.Click += new System.EventHandler(this.coordsLabel_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(16, 308);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(91, 17);
            this.checkBox1.TabIndex = 102;
            this.checkBox1.Text = "Untune Klunk";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // shipColourComboBox
            // 
            this.shipColourComboBox.FormattingEnabled = true;
            this.shipColourComboBox.Items.AddRange(new object[] {
            "Blargian Red",
            "Orxon Green ",
            "Bogon Blue",
            "Insomniac Special",
            "Dark Nebula",
            "Drek\'s Black Heart",
            "Space Storm",
            "Lunar Eclipse",
            "Plaidtastic",
            "Supernova",
            "Solar Wind",
            "Clowner",
            "Silent Strike",
            "Lombax Orange",
            "Neutron Star",
            "Star Traveller",
            "Hooked on Onyx",
            "Tyhrranoid Void",
            "Zeldrin Sunset",
            "Ghost Pirate Purple",
            "Qwark Green",
            "Agent Orange",
            "Helga Hues",
            "Amoeboid Green",
            "Obani Orange",
            "Pulsing Purple",
            "Low Rider",
            "Black Hole",
            "Sun Storm",
            "Sasha Scarlet",
            "Florana Breeze",
            "Ozzy Kamikaze"});
            this.shipColourComboBox.Location = new System.Drawing.Point(237, 390);
            this.shipColourComboBox.Name = "shipColourComboBox";
            this.shipColourComboBox.Size = new System.Drawing.Size(121, 21);
            this.shipColourComboBox.TabIndex = 103;
            this.shipColourComboBox.SelectedIndexChanged += new System.EventHandler(this.shipColourComboBox_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(234, 374);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 13);
            this.label11.TabIndex = 104;
            this.label11.Text = "Ship Colour:";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 374);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 13);
            this.label13.TabIndex = 106;
            this.label13.Text = "File Time:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(15, 390);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(193, 20);
            this.textBox2.TabIndex = 105;
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // buttonUnlocks
            // 
            this.buttonUnlocks.Location = new System.Drawing.Point(12, 331);
            this.buttonUnlocks.Name = "buttonUnlocks";
            this.buttonUnlocks.Size = new System.Drawing.Size(113, 40);
            this.buttonUnlocks.TabIndex = 107;
            this.buttonUnlocks.Text = "Unlocks";
            this.buttonUnlocks.UseVisualStyleBackColor = true;
            this.buttonUnlocks.Click += new System.EventHandler(this.buttonUnlocks_Click);
            // 
            // buttonSetup
            // 
            this.buttonSetup.Location = new System.Drawing.Point(131, 331);
            this.buttonSetup.Name = "buttonSetup";
            this.buttonSetup.Size = new System.Drawing.Size(83, 40);
            this.buttonSetup.TabIndex = 108;
            this.buttonSetup.Text = "Setup NG+ or No QE File";
            this.buttonSetup.UseVisualStyleBackColor = true;
            this.buttonSetup.Click += new System.EventHandler(this.buttonSetup_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(101, 561);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(156, 13);
            this.label14.TabIndex = 109;
            this.label14.Text = "what are you doing down here?";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(157, 696);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(212, 13);
            this.label15.TabIndex = 110;
            this.label15.Text = "leave. there\'s nothing for you here. unless...";
            // 
            // flagViewerToolStripMenuItem
            // 
            this.flagViewerToolStripMenuItem.Name = "flagViewerToolStripMenuItem";
            this.flagViewerToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.flagViewerToolStripMenuItem.Text = "Level flag viewer";
            this.flagViewerToolStripMenuItem.Click += new System.EventHandler(this.flagViewerToolStripMenuItem_Click);
            // 
            // RAC3Form
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(385, 424);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.buttonSetup);
            this.Controls.Add(this.buttonUnlocks);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.shipColourComboBox);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.coordsLabel);
            this.Controls.Add(this.coordsComboBox);
            this.Controls.Add(this.toggleQS);
            this.Controls.Add(this.loadFileButton);
            this.Controls.Add(this.setAsideButton);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label5);
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button setAsideButton;
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.Button toggleQS;
        private System.Windows.Forms.CheckBox coordsComboBox;
        private System.Windows.Forms.Label coordsLabel;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox shipColourComboBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem memoryUtilitiesToolStripMenuItem;
        private System.Windows.Forms.Button buttonUnlocks;
        private System.Windows.Forms.Button buttonSetup;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ToolStripMenuItem autosplitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editRouteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flagViewerToolStripMenuItem;
    }
}

