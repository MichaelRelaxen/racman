
namespace racman
{
    partial class RAC2Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RAC2Form));
            this.coordsComboBox = new System.Windows.Forms.CheckBox();
            this.coordsLabel = new System.Windows.Forms.Label();
            this.CComboCheckBox = new System.Windows.Forms.CheckBox();
            this.killyourself = new System.Windows.Forms.Button();
            this.loadpos = new System.Windows.Forms.Button();
            this.savepos = new System.Windows.Forms.Button();
            this.positions_comboBox = new System.Windows.Forms.ComboBox();
            this.loadPlanetButton = new System.Windows.Forms.Button();
            this.planets_comboBox = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.bolts_textBox = new System.Windows.Forms.TextBox();
            this.ghostCheckbox = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureButtonCombosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.levelFlagViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchLoaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryUtilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugFeaturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activateQEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutosplitterCheckbox = new System.Windows.Forms.CheckBox();
            this.freezeAmmoCheckbox = new System.Windows.Forms.CheckBox();
            this.freezeHealthCheckbox = new System.Windows.Forms.CheckBox();
            this.raritaniumTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.challengeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.resetFileManipButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonSwingshot = new System.Windows.Forms.Button();
            this.checkBoxExp = new System.Windows.Forms.CheckBox();
            this.buttonRaceStorage = new System.Windows.Forms.Button();
            this.SetFastLoadCheckbox = new System.Windows.Forms.CheckBox();
            this.buttonNGPlusMenu = new System.Windows.Forms.Button();
            this.buttonNoIMGMenu = new System.Windows.Forms.Button();
            this.checkBoxResetFlags = new System.Windows.Forms.CheckBox();
            this.buttonSetupAllMissions = new System.Windows.Forms.Button();
            this.labelLap = new System.Windows.Forms.Label();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.setAsideFileButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxAutoReset = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxHealthXP = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonRespawn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonCosmetics = new System.Windows.Forms.Button();
            this.checkBox_autoResetAnyPercent = new System.Windows.Forms.CheckBox();
            this.resetBossesComboBox = new System.Windows.Forms.CheckBox();
            this.platBoltsCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // coordsComboBox
            // 
            this.coordsComboBox.AutoSize = true;
            this.coordsComboBox.Location = new System.Drawing.Point(13, 441);
            this.coordsComboBox.Name = "coordsComboBox";
            this.coordsComboBox.Size = new System.Drawing.Size(112, 17);
            this.coordsComboBox.TabIndex = 120;
            this.coordsComboBox.Text = "Show Coordinates";
            this.coordsComboBox.UseVisualStyleBackColor = true;
            this.coordsComboBox.CheckedChanged += new System.EventHandler(this.coordsComboBox_CheckedChanged);
            // 
            // coordsLabel
            // 
            this.coordsLabel.AutoSize = true;
            this.coordsLabel.Location = new System.Drawing.Point(12, 475);
            this.coordsLabel.Name = "coordsLabel";
            this.coordsLabel.Size = new System.Drawing.Size(123, 13);
            this.coordsLabel.TabIndex = 119;
            this.coordsLabel.Text = "Co-ordinates: <disabled>";
            this.coordsLabel.Visible = false;
            // 
            // CComboCheckBox
            // 
            this.CComboCheckBox.AutoSize = true;
            this.CComboCheckBox.Location = new System.Drawing.Point(133, 126);
            this.CComboCheckBox.Name = "CComboCheckBox";
            this.CComboCheckBox.Size = new System.Drawing.Size(100, 17);
            this.CComboCheckBox.TabIndex = 93;
            this.CComboCheckBox.Text = "Enable Combos";
            this.CComboCheckBox.UseVisualStyleBackColor = true;
            this.CComboCheckBox.CheckedChanged += new System.EventHandler(this.CComboCheckBox_CheckedChanged);
            // 
            // killyourself
            // 
            this.killyourself.Location = new System.Drawing.Point(12, 121);
            this.killyourself.Name = "killyourself";
            this.killyourself.Size = new System.Drawing.Size(115, 25);
            this.killyourself.TabIndex = 91;
            this.killyourself.Text = "Die";
            this.killyourself.UseVisualStyleBackColor = true;
            this.killyourself.Click += new System.EventHandler(this.killyourself_Click);
            // 
            // loadpos
            // 
            this.loadpos.Location = new System.Drawing.Point(12, 64);
            this.loadpos.Name = "loadpos";
            this.loadpos.Size = new System.Drawing.Size(115, 25);
            this.loadpos.TabIndex = 90;
            this.loadpos.Text = "Load Position";
            this.loadpos.UseVisualStyleBackColor = true;
            this.loadpos.Click += new System.EventHandler(this.loadpos_Click);
            // 
            // savepos
            // 
            this.savepos.Location = new System.Drawing.Point(12, 34);
            this.savepos.Name = "savepos";
            this.savepos.Size = new System.Drawing.Size(115, 25);
            this.savepos.TabIndex = 89;
            this.savepos.Text = "Save Position";
            this.savepos.UseVisualStyleBackColor = true;
            this.savepos.Click += new System.EventHandler(this.savepos_Click);
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
            this.positions_comboBox.Location = new System.Drawing.Point(133, 36);
            this.positions_comboBox.Name = "positions_comboBox";
            this.positions_comboBox.Size = new System.Drawing.Size(95, 21);
            this.positions_comboBox.TabIndex = 92;
            this.positions_comboBox.SelectedIndexChanged += new System.EventHandler(this.positions_comboBox_SelectedIndexChanged);
            // 
            // loadPlanetButton
            // 
            this.loadPlanetButton.Location = new System.Drawing.Point(133, 225);
            this.loadPlanetButton.Name = "loadPlanetButton";
            this.loadPlanetButton.Size = new System.Drawing.Size(95, 23);
            this.loadPlanetButton.TabIndex = 94;
            this.loadPlanetButton.Text = "Load";
            this.loadPlanetButton.UseVisualStyleBackColor = true;
            this.loadPlanetButton.Click += new System.EventHandler(this.loadPlanetButton_Click);
            // 
            // planets_comboBox
            // 
            this.planets_comboBox.FormattingEnabled = true;
            this.planets_comboBox.Items.AddRange(new object[] {
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
            this.planets_comboBox.Location = new System.Drawing.Point(12, 225);
            this.planets_comboBox.Name = "planets_comboBox";
            this.planets_comboBox.Size = new System.Drawing.Size(115, 21);
            this.planets_comboBox.TabIndex = 95;
            this.planets_comboBox.SelectedIndexChanged += new System.EventHandler(this.planets_comboBox_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 411);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 25);
            this.button2.TabIndex = 97;
            this.button2.Text = "Input Display";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(359, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 99;
            this.label8.Text = "Bolt Count:";
            // 
            // bolts_textBox
            // 
            this.bolts_textBox.Location = new System.Drawing.Point(361, 58);
            this.bolts_textBox.Name = "bolts_textBox";
            this.bolts_textBox.Size = new System.Drawing.Size(105, 20);
            this.bolts_textBox.TabIndex = 98;
            this.bolts_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bolts_textBox_KeyDown);
            // 
            // ghostCheckbox
            // 
            this.ghostCheckbox.AutoSize = true;
            this.ghostCheckbox.Location = new System.Drawing.Point(344, 225);
            this.ghostCheckbox.Name = "ghostCheckbox";
            this.ghostCheckbox.Size = new System.Drawing.Size(95, 17);
            this.ghostCheckbox.TabIndex = 100;
            this.ghostCheckbox.Text = "Ghost Ratchet";
            this.ghostCheckbox.UseVisualStyleBackColor = true;
            this.ghostCheckbox.CheckedChanged += new System.EventHandler(this.ghostCheckbox_CheckedChanged_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menusToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(478, 24);
            this.menuStrip1.TabIndex = 101;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menusToolStripMenuItem
            // 
            this.menusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureButtonCombosToolStripMenuItem,
            this.switchGameToolStripMenuItem,
            this.toolStripSeparator1,
            this.levelFlagViewerToolStripMenuItem,
            this.patchLoaderToolStripMenuItem,
            this.memoryUtilitiesToolStripMenuItem,
            this.tasToolStripMenuItem});
            this.menusToolStripMenuItem.Name = "menusToolStripMenuItem";
            this.menusToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menusToolStripMenuItem.Text = "Menu";
            // 
            // configureButtonCombosToolStripMenuItem
            // 
            this.configureButtonCombosToolStripMenuItem.Name = "configureButtonCombosToolStripMenuItem";
            this.configureButtonCombosToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.configureButtonCombosToolStripMenuItem.Text = "Configure button combos";
            this.configureButtonCombosToolStripMenuItem.Click += new System.EventHandler(this.configureButtonCombosToolStripMenuItem_Click);
            // 
            // switchGameToolStripMenuItem
            // 
            this.switchGameToolStripMenuItem.Name = "switchGameToolStripMenuItem";
            this.switchGameToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.switchGameToolStripMenuItem.Text = "Switch game";
            this.switchGameToolStripMenuItem.Click += new System.EventHandler(this.switchGameToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(209, 6);
            // 
            // levelFlagViewerToolStripMenuItem
            // 
            this.levelFlagViewerToolStripMenuItem.Name = "levelFlagViewerToolStripMenuItem";
            this.levelFlagViewerToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.levelFlagViewerToolStripMenuItem.Text = "Level flag viewer";
            this.levelFlagViewerToolStripMenuItem.Click += new System.EventHandler(this.levelFlagViewerToolStripMenuItem_Click);
            // 
            // patchLoaderToolStripMenuItem
            // 
            this.patchLoaderToolStripMenuItem.Name = "patchLoaderToolStripMenuItem";
            this.patchLoaderToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.patchLoaderToolStripMenuItem.Text = "Patch loader...";
            this.patchLoaderToolStripMenuItem.Click += new System.EventHandler(this.patchLoaderToolStripMenuItem_Click);
            // 
            // memoryUtilitiesToolStripMenuItem
            // 
            this.memoryUtilitiesToolStripMenuItem.Name = "memoryUtilitiesToolStripMenuItem";
            this.memoryUtilitiesToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.memoryUtilitiesToolStripMenuItem.Text = "Memory utilities";
            this.memoryUtilitiesToolStripMenuItem.Click += new System.EventHandler(this.memoryUtilitiesToolStripMenuItem_Click);
            // 
            // tasToolStripMenuItem
            // 
            this.tasToolStripMenuItem.Name = "tasToolStripMenuItem";
            this.tasToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.tasToolStripMenuItem.Text = "TAS tools";
            this.tasToolStripMenuItem.Click += new System.EventHandler(this.tasToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugFeaturesToolStripMenuItem,
            this.activateQEToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.DropDownOpening += new System.EventHandler(this.debugToolStripMenuItem_DropDownOpening);
            // 
            // debugFeaturesToolStripMenuItem
            // 
            this.debugFeaturesToolStripMenuItem.Name = "debugFeaturesToolStripMenuItem";
            this.debugFeaturesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.debugFeaturesToolStripMenuItem.Text = "Enable debug mode";
            this.debugFeaturesToolStripMenuItem.Click += new System.EventHandler(this.debugFeaturesToolStripMenuItem_Click);
            this.debugFeaturesToolStripMenuItem.MouseHover += new System.EventHandler(this.debugFeaturesToolStripMenuItem_MouseHover);
            // 
            // activateQEToolStripMenuItem
            // 
            this.activateQEToolStripMenuItem.Name = "activateQEToolStripMenuItem";
            this.activateQEToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.activateQEToolStripMenuItem.Text = "Activate QE...";
            this.activateQEToolStripMenuItem.Click += new System.EventHandler(this.activateQEToolStripMenuItem_Click);
            // 
            // AutosplitterCheckbox
            // 
            this.AutosplitterCheckbox.AutoSize = true;
            this.AutosplitterCheckbox.Location = new System.Drawing.Point(344, 248);
            this.AutosplitterCheckbox.Name = "AutosplitterCheckbox";
            this.AutosplitterCheckbox.Size = new System.Drawing.Size(78, 17);
            this.AutosplitterCheckbox.TabIndex = 102;
            this.AutosplitterCheckbox.Text = "Autosplitter";
            this.AutosplitterCheckbox.UseVisualStyleBackColor = true;
            this.AutosplitterCheckbox.CheckedChanged += new System.EventHandler(this.AutosplitterCheckbox_CheckedChanged);
            // 
            // freezeAmmoCheckbox
            // 
            this.freezeAmmoCheckbox.AutoSize = true;
            this.freezeAmmoCheckbox.Location = new System.Drawing.Point(249, 225);
            this.freezeAmmoCheckbox.Name = "freezeAmmoCheckbox";
            this.freezeAmmoCheckbox.Size = new System.Drawing.Size(89, 17);
            this.freezeAmmoCheckbox.TabIndex = 103;
            this.freezeAmmoCheckbox.Text = "Infinite Ammo";
            this.freezeAmmoCheckbox.UseVisualStyleBackColor = true;
            this.freezeAmmoCheckbox.CheckedChanged += new System.EventHandler(this.freezeAmmoCheckbox_CheckedChanged);
            // 
            // freezeHealthCheckbox
            // 
            this.freezeHealthCheckbox.AutoSize = true;
            this.freezeHealthCheckbox.Location = new System.Drawing.Point(249, 248);
            this.freezeHealthCheckbox.Name = "freezeHealthCheckbox";
            this.freezeHealthCheckbox.Size = new System.Drawing.Size(92, 17);
            this.freezeHealthCheckbox.TabIndex = 104;
            this.freezeHealthCheckbox.Text = "Freeze Health";
            this.freezeHealthCheckbox.UseVisualStyleBackColor = true;
            this.freezeHealthCheckbox.CheckedChanged += new System.EventHandler(this.freezeHealthCheckbox_CheckedChanged);
            // 
            // raritaniumTextBox
            // 
            this.raritaniumTextBox.Location = new System.Drawing.Point(249, 97);
            this.raritaniumTextBox.Name = "raritaniumTextBox";
            this.raritaniumTextBox.Size = new System.Drawing.Size(105, 20);
            this.raritaniumTextBox.TabIndex = 105;
            this.raritaniumTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.raritaniumTextBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(246, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 106;
            this.label1.Text = "Raritanium:";
            // 
            // challengeTextBox
            // 
            this.challengeTextBox.Location = new System.Drawing.Point(250, 58);
            this.challengeTextBox.Name = "challengeTextBox";
            this.challengeTextBox.Size = new System.Drawing.Size(105, 20);
            this.challengeTextBox.TabIndex = 107;
            this.challengeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 108;
            this.label2.Text = "Challenge Mode:";
            // 
            // resetFileManipButton
            // 
            this.resetFileManipButton.Location = new System.Drawing.Point(360, 391);
            this.resetFileManipButton.Name = "resetFileManipButton";
            this.resetFileManipButton.Size = new System.Drawing.Size(105, 44);
            this.resetFileManipButton.TabIndex = 109;
            this.resetFileManipButton.Text = "Reset Any% manips";
            this.toolTip1.SetToolTip(this.resetFileManipButton, "Clears bolt manip, resets hoverbike menu, makes game\r\npyramid drop 1,024 bolts, a" +
        "nd resets Endako boss cutscene.\r\nDoes not affect slot machine RNG or act tuning." +
        "");
            this.resetFileManipButton.UseVisualStyleBackColor = true;
            this.resetFileManipButton.Click += new System.EventHandler(this.resetFileManipButton_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // buttonSwingshot
            // 
            this.buttonSwingshot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSwingshot.Location = new System.Drawing.Point(149, 304);
            this.buttonSwingshot.Name = "buttonSwingshot";
            this.buttonSwingshot.Size = new System.Drawing.Size(66, 53);
            this.buttonSwingshot.TabIndex = 113;
            this.buttonSwingshot.Text = "Store Swingshot";
            this.toolTip1.SetToolTip(this.buttonSwingshot, "Sets your previous weapon to the Swingshot. For file setup.");
            this.buttonSwingshot.UseVisualStyleBackColor = true;
            this.buttonSwingshot.Click += new System.EventHandler(this.button4_Click);
            // 
            // checkBoxExp
            // 
            this.checkBoxExp.AutoSize = true;
            this.checkBoxExp.Location = new System.Drawing.Point(12, 363);
            this.checkBoxExp.Name = "checkBoxExp";
            this.checkBoxExp.Size = new System.Drawing.Size(172, 17);
            this.checkBoxExp.TabIndex = 114;
            this.checkBoxExp.Text = "Enable weapon insta-upgrades";
            this.toolTip1.SetToolTip(this.checkBoxExp, "Boosts experience values so that killing an enemy instantly upgrades the weapon u" +
        "sed. Useful for file setup.");
            this.checkBoxExp.UseVisualStyleBackColor = true;
            this.checkBoxExp.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // buttonRaceStorage
            // 
            this.buttonRaceStorage.Location = new System.Drawing.Point(249, 391);
            this.buttonRaceStorage.Name = "buttonRaceStorage";
            this.buttonRaceStorage.Size = new System.Drawing.Size(105, 44);
            this.buttonRaceStorage.TabIndex = 115;
            this.buttonRaceStorage.Text = "Reset NG+ manips/menus";
            this.toolTip1.SetToolTip(this.buttonRaceStorage, "Resets the position of the cursor on the race selector.");
            this.buttonRaceStorage.UseVisualStyleBackColor = true;
            this.buttonRaceStorage.Click += new System.EventHandler(this.buttonRaceStorage_Click);
            // 
            // SetFastLoadCheckbox
            // 
            this.SetFastLoadCheckbox.AutoSize = true;
            this.SetFastLoadCheckbox.Location = new System.Drawing.Point(12, 252);
            this.SetFastLoadCheckbox.Name = "SetFastLoadCheckbox";
            this.SetFastLoadCheckbox.Size = new System.Drawing.Size(74, 17);
            this.SetFastLoadCheckbox.TabIndex = 116;
            this.SetFastLoadCheckbox.Text = "Fast loads";
            this.toolTip1.SetToolTip(this.SetFastLoadCheckbox, "Kinda make fast load?");
            this.SetFastLoadCheckbox.UseVisualStyleBackColor = true;
            this.SetFastLoadCheckbox.CheckedChanged += new System.EventHandler(this.SetFastLoadCheckbox_CheckedChanged);
            // 
            // buttonNGPlusMenu
            // 
            this.buttonNGPlusMenu.Location = new System.Drawing.Point(360, 304);
            this.buttonNGPlusMenu.Name = "buttonNGPlusMenu";
            this.buttonNGPlusMenu.Size = new System.Drawing.Size(105, 25);
            this.buttonNGPlusMenu.TabIndex = 118;
            this.buttonNGPlusMenu.Text = "NG+";
            this.toolTip1.SetToolTip(this.buttonNGPlusMenu, "Unlocks the insomniac museum shortcut and puts the cursor over it\r\nin the shortcu" +
        "ts menu, manipulates the gorn cutscenes and act tunes\r\nthe protopet and the sniv" +
        "elak bosses.");
            this.buttonNGPlusMenu.UseVisualStyleBackColor = true;
            this.buttonNGPlusMenu.Click += new System.EventHandler(this.buttonNGPlusMenu_Click);
            // 
            // buttonNoIMGMenu
            // 
            this.buttonNoIMGMenu.Location = new System.Drawing.Point(249, 304);
            this.buttonNoIMGMenu.Name = "buttonNoIMGMenu";
            this.buttonNoIMGMenu.Size = new System.Drawing.Size(105, 25);
            this.buttonNoIMGMenu.TabIndex = 124;
            this.buttonNoIMGMenu.Text = "NG+ No IMG";
            this.toolTip1.SetToolTip(this.buttonNoIMGMenu, "Setup barlow cursor in shortcuts menu, manipulates the gorn cutscenes and act tun" +
        "es\r\nthe protopet and the snivelak bosses.");
            this.buttonNoIMGMenu.UseVisualStyleBackColor = true;
            this.buttonNoIMGMenu.Click += new System.EventHandler(this.buttonNoIMGMenu_Click);
            // 
            // checkBoxResetFlags
            // 
            this.checkBoxResetFlags.AutoSize = true;
            this.checkBoxResetFlags.Location = new System.Drawing.Point(92, 252);
            this.checkBoxResetFlags.Name = "checkBoxResetFlags";
            this.checkBoxResetFlags.Size = new System.Drawing.Size(79, 17);
            this.checkBoxResetFlags.TabIndex = 131;
            this.checkBoxResetFlags.Text = "Reset flags";
            this.toolTip1.SetToolTip(this.checkBoxResetFlags, "Reset flags when loading");
            this.checkBoxResetFlags.UseVisualStyleBackColor = true;
            this.checkBoxResetFlags.CheckedChanged += new System.EventHandler(this.checkBoxResetFlags_CheckedChanged);
            // 
            // buttonSetupAllMissions
            // 
            this.buttonSetupAllMissions.Location = new System.Drawing.Point(249, 335);
            this.buttonSetupAllMissions.Name = "buttonSetupAllMissions";
            this.buttonSetupAllMissions.Size = new System.Drawing.Size(105, 25);
            this.buttonSetupAllMissions.TabIndex = 132;
            this.buttonSetupAllMissions.Text = "NG+ All Missions";
            this.toolTip1.SetToolTip(this.buttonSetupAllMissions, "you get the idea");
            this.buttonSetupAllMissions.UseVisualStyleBackColor = true;
            this.buttonSetupAllMissions.Click += new System.EventHandler(this.buttonSetupAllMissions_Click);
            // 
            // labelLap
            // 
            this.labelLap.AutoSize = true;
            this.labelLap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLap.Location = new System.Drawing.Point(12, 512);
            this.labelLap.Name = "labelLap";
            this.labelLap.Size = new System.Drawing.Size(136, 20);
            this.labelLap.TabIndex = 110;
            this.labelLap.Text = "Lap is: <disabled>";
            this.labelLap.Visible = false;
            // 
            // loadFileButton
            // 
            this.loadFileButton.Enabled = false;
            this.loadFileButton.Location = new System.Drawing.Point(133, 92);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(95, 25);
            this.loadFileButton.TabIndex = 111;
            this.loadFileButton.Text = "Load File";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // setAsideFileButton
            // 
            this.setAsideFileButton.Enabled = false;
            this.setAsideFileButton.Location = new System.Drawing.Point(133, 64);
            this.setAsideFileButton.Name = "setAsideFileButton";
            this.setAsideFileButton.Size = new System.Drawing.Size(95, 25);
            this.setAsideFileButton.TabIndex = 112;
            this.setAsideFileButton.Text = "Set Aside File";
            this.setAsideFileButton.UseVisualStyleBackColor = true;
            this.setAsideFileButton.Click += new System.EventHandler(this.setAsideFileButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 304);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 53);
            this.button1.TabIndex = 117;
            this.button1.Text = "Unlocks";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // checkBoxAutoReset
            // 
            this.checkBoxAutoReset.AutoSize = true;
            this.checkBoxAutoReset.Location = new System.Drawing.Point(249, 441);
            this.checkBoxAutoReset.Name = "checkBoxAutoReset";
            this.checkBoxAutoReset.Size = new System.Drawing.Size(105, 17);
            this.checkBoxAutoReset.TabIndex = 121;
            this.checkBoxAutoReset.Text = "Auto-reset (NG+)";
            this.checkBoxAutoReset.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(357, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 123;
            this.label3.Text = "Health XP:";
            // 
            // textBoxHealthXP
            // 
            this.textBoxHealthXP.Location = new System.Drawing.Point(360, 97);
            this.textBoxHealthXP.Name = "textBoxHealthXP";
            this.textBoxHealthXP.Size = new System.Drawing.Size(105, 20);
            this.textBoxHealthXP.TabIndex = 122;
            this.textBoxHealthXP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHealthXP_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(215, 314);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 127;
            // 
            // buttonRespawn
            // 
            this.buttonRespawn.Location = new System.Drawing.Point(12, 92);
            this.buttonRespawn.Name = "buttonRespawn";
            this.buttonRespawn.Size = new System.Drawing.Size(115, 25);
            this.buttonRespawn.TabIndex = 128;
            this.buttonRespawn.Text = "Set Respawn";
            this.buttonRespawn.UseVisualStyleBackColor = true;
            this.buttonRespawn.Click += new System.EventHandler(this.buttonRespawn_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(360, 335);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 25);
            this.button3.TabIndex = 133;
            this.button3.Text = "Maktar slots";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonCosmetics
            // 
            this.buttonCosmetics.Location = new System.Drawing.Point(148, 411);
            this.buttonCosmetics.Name = "buttonCosmetics";
            this.buttonCosmetics.Size = new System.Drawing.Size(67, 24);
            this.buttonCosmetics.TabIndex = 134;
            this.buttonCosmetics.Text = "Cosmetics";
            this.buttonCosmetics.UseVisualStyleBackColor = true;
            this.buttonCosmetics.Click += new System.EventHandler(this.buttonCosmetics_Click);
            // 
            // checkBox_autoResetAnyPercent
            // 
            this.checkBox_autoResetAnyPercent.AutoSize = true;
            this.checkBox_autoResetAnyPercent.Location = new System.Drawing.Point(360, 441);
            this.checkBox_autoResetAnyPercent.Name = "checkBox_autoResetAnyPercent";
            this.checkBox_autoResetAnyPercent.Size = new System.Drawing.Size(109, 17);
            this.checkBox_autoResetAnyPercent.TabIndex = 135;
            this.checkBox_autoResetAnyPercent.Text = "Auto-reset (Any%)";
            this.checkBox_autoResetAnyPercent.UseVisualStyleBackColor = true;
            // 
            // resetBossesComboBox
            // 
            this.resetBossesComboBox.AutoSize = true;
            this.resetBossesComboBox.Location = new System.Drawing.Point(12, 175);
            this.resetBossesComboBox.Name = "resetBossesComboBox";
            this.resetBossesComboBox.Size = new System.Drawing.Size(149, 17);
            this.resetBossesComboBox.TabIndex = 140;
            this.resetBossesComboBox.Text = "Reset bossfights on death";
            this.resetBossesComboBox.UseVisualStyleBackColor = true;
            this.resetBossesComboBox.CheckedChanged += new System.EventHandler(this.resetBossesComboBox_CheckedChanged);
            // 
            // platBoltsCheckBox
            // 
            this.platBoltsCheckBox.AutoSize = true;
            this.platBoltsCheckBox.Location = new System.Drawing.Point(12, 152);
            this.platBoltsCheckBox.Name = "platBoltsCheckBox";
            this.platBoltsCheckBox.Size = new System.Drawing.Size(166, 17);
            this.platBoltsCheckBox.TabIndex = 147;
            this.platBoltsCheckBox.Text = "Reset platinum bolts on death";
            this.platBoltsCheckBox.UseVisualStyleBackColor = true;
            this.platBoltsCheckBox.CheckedChanged += new System.EventHandler(this.platBoltsCheckBox_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 148;
            this.label4.Text = "Load Planet:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 288);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 149;
            this.label5.Text = "File Setup";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(249, 126);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(216, 66);
            this.button4.TabIndex = 150;
            this.button4.Text = "Manage Collectables";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(249, 288);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 151;
            this.label6.Text = "Category Setup";
            // 
            // RAC2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 492);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SetFastLoadCheckbox);
            this.Controls.Add(this.planets_comboBox);
            this.Controls.Add(this.checkBoxExp);
            this.Controls.Add(this.platBoltsCheckBox);
            this.Controls.Add(this.resetBossesComboBox);
            this.Controls.Add(this.checkBox_autoResetAnyPercent);
            this.Controls.Add(this.buttonCosmetics);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonSetupAllMissions);
            this.Controls.Add(this.checkBoxResetFlags);
            this.Controls.Add(this.buttonRespawn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonNoIMGMenu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxHealthXP);
            this.Controls.Add(this.checkBoxAutoReset);
            this.Controls.Add(this.coordsLabel);
            this.Controls.Add(this.buttonNGPlusMenu);
            this.Controls.Add(this.buttonRaceStorage);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.setAsideFileButton);
            this.Controls.Add(this.loadFileButton);
            this.Controls.Add(this.buttonSwingshot);
            this.Controls.Add(this.labelLap);
            this.Controls.Add(this.resetFileManipButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.challengeTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.raritaniumTextBox);
            this.Controls.Add(this.freezeHealthCheckbox);
            this.Controls.Add(this.freezeAmmoCheckbox);
            this.Controls.Add(this.AutosplitterCheckbox);
            this.Controls.Add(this.ghostCheckbox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.bolts_textBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.loadPlanetButton);
            this.Controls.Add(this.CComboCheckBox);
            this.Controls.Add(this.killyourself);
            this.Controls.Add(this.loadpos);
            this.Controls.Add(this.savepos);
            this.Controls.Add(this.positions_comboBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.coordsComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RAC2Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ratchet & Clank 2 - NPEA00386 (PAL)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RAC2Form_FormClosing);
            this.Load += new System.EventHandler(this.RAC2Form_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label coordsLabel;
        private System.Windows.Forms.CheckBox CComboCheckBox;
        private System.Windows.Forms.CheckBox SetFastLoadCheckbox;
        private System.Windows.Forms.Button killyourself;
        private System.Windows.Forms.Button loadpos;
        private System.Windows.Forms.Button savepos;
        private System.Windows.Forms.ComboBox positions_comboBox;
        private System.Windows.Forms.Button loadPlanetButton;
        private System.Windows.Forms.ComboBox planets_comboBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox bolts_textBox;
        private System.Windows.Forms.CheckBox ghostCheckbox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureButtonCombosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchGameToolStripMenuItem;
        private System.Windows.Forms.CheckBox AutosplitterCheckbox;
        private System.Windows.Forms.CheckBox freezeAmmoCheckbox;
        private System.Windows.Forms.CheckBox freezeHealthCheckbox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem memoryUtilitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patchLoaderToolStripMenuItem;
        private System.Windows.Forms.TextBox raritaniumTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox challengeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button resetFileManipButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelLap;
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.Button setAsideFileButton;
        private System.Windows.Forms.Button buttonSwingshot;
        private System.Windows.Forms.CheckBox checkBoxExp;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonNGPlusMenu;
        private System.Windows.Forms.Button buttonRaceStorage;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugFeaturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activateQEToolStripMenuItem;
        private System.Windows.Forms.CheckBox coordsComboBox;
        private System.Windows.Forms.CheckBox checkBoxAutoReset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxHealthXP;
        private System.Windows.Forms.Button buttonNoIMGMenu;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonRespawn;
        private System.Windows.Forms.ToolStripMenuItem levelFlagViewerToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxResetFlags;
        private System.Windows.Forms.Button buttonSetupAllMissions;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonCosmetics;
        private System.Windows.Forms.ToolStripMenuItem tasToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox_autoResetAnyPercent;
        private System.Windows.Forms.CheckBox resetBossesComboBox;
        private System.Windows.Forms.CheckBox platBoltsCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label6;
    }
}