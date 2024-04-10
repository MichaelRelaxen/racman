
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
            this.CComboCheckBox = new System.Windows.Forms.CheckBox();
            this.killyourself = new System.Windows.Forms.Button();
            this.loadpos = new System.Windows.Forms.Button();
            this.savepos = new System.Windows.Forms.Button();
            this.positions_comboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
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
            this.patchLoaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryUtilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.SetFastLoadCheckbox = new System.Windows.Forms.CheckBox();
            this.buttonNGPlusMenu = new System.Windows.Forms.Button();
            this.buttonRaceStorage = new System.Windows.Forms.Button();
            this.labelLap = new System.Windows.Forms.Label();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.setAsideFileButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CComboCheckBox
            // 
            this.CComboCheckBox.AutoSize = true;
            this.CComboCheckBox.Location = new System.Drawing.Point(12, 129);
            this.CComboCheckBox.Name = "CComboCheckBox";
            this.CComboCheckBox.Size = new System.Drawing.Size(147, 17);
            this.CComboCheckBox.TabIndex = 93;
            this.CComboCheckBox.Text = "Enable Controller Combos";
            this.CComboCheckBox.UseVisualStyleBackColor = true;
            this.CComboCheckBox.CheckedChanged += new System.EventHandler(this.CComboCheckBox_CheckedChanged);
            // 
            // killyourself
            // 
            this.killyourself.Location = new System.Drawing.Point(12, 94);
            this.killyourself.Name = "killyourself";
            this.killyourself.Size = new System.Drawing.Size(115, 23);
            this.killyourself.TabIndex = 91;
            this.killyourself.Text = "Die";
            this.killyourself.UseVisualStyleBackColor = true;
            this.killyourself.Click += new System.EventHandler(this.killyourself_Click);
            // 
            // loadpos
            // 
            this.loadpos.Location = new System.Drawing.Point(12, 65);
            this.loadpos.Name = "loadpos";
            this.loadpos.Size = new System.Drawing.Size(115, 23);
            this.loadpos.TabIndex = 90;
            this.loadpos.Text = "Load Position";
            this.loadpos.UseVisualStyleBackColor = true;
            this.loadpos.Click += new System.EventHandler(this.loadpos_Click);
            // 
            // savepos
            // 
            this.savepos.Location = new System.Drawing.Point(12, 36);
            this.savepos.Name = "savepos";
            this.savepos.Size = new System.Drawing.Size(115, 23);
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
            this.positions_comboBox.Size = new System.Drawing.Size(96, 21);
            this.positions_comboBox.TabIndex = 92;
            this.positions_comboBox.SelectedIndexChanged += new System.EventHandler(this.positions_comboBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 96;
            this.label6.Text = "Load Planet:";
            // 
            // loadPlanetButton
            // 
            this.loadPlanetButton.Location = new System.Drawing.Point(133, 174);
            this.loadPlanetButton.Name = "loadPlanetButton";
            this.loadPlanetButton.Size = new System.Drawing.Size(75, 23);
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
            this.planets_comboBox.Location = new System.Drawing.Point(12, 175);
            this.planets_comboBox.Name = "planets_comboBox";
            this.planets_comboBox.Size = new System.Drawing.Size(115, 21);
            this.planets_comboBox.TabIndex = 95;
            this.planets_comboBox.SelectedIndexChanged += new System.EventHandler(this.planets_comboBox_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 206);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 23);
            this.button2.TabIndex = 97;
            this.button2.Text = "Input Display";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(244, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 99;
            this.label8.Text = "Bolt Count:";
            // 
            // bolts_textBox
            // 
            this.bolts_textBox.Location = new System.Drawing.Point(247, 127);
            this.bolts_textBox.Name = "bolts_textBox";
            this.bolts_textBox.Size = new System.Drawing.Size(105, 20);
            this.bolts_textBox.TabIndex = 98;
            this.bolts_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bolts_textBox_KeyDown);
            // 
            // ghostCheckbox
            // 
            this.ghostCheckbox.AutoSize = true;
            this.ghostCheckbox.Location = new System.Drawing.Point(247, 199);
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
            this.menuStrip1.Size = new System.Drawing.Size(363, 24);
            this.menuStrip1.TabIndex = 101;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menusToolStripMenuItem
            // 
            this.menusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureButtonCombosToolStripMenuItem,
            this.switchGameToolStripMenuItem,
            this.toolStripSeparator1,
            this.patchLoaderToolStripMenuItem,
            this.memoryUtilitiesToolStripMenuItem});
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
            this.AutosplitterCheckbox.Location = new System.Drawing.Point(247, 222);
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
            this.freezeAmmoCheckbox.Location = new System.Drawing.Point(247, 153);
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
            this.freezeHealthCheckbox.Location = new System.Drawing.Point(247, 176);
            this.freezeHealthCheckbox.Name = "freezeHealthCheckbox";
            this.freezeHealthCheckbox.Size = new System.Drawing.Size(92, 17);
            this.freezeHealthCheckbox.TabIndex = 104;
            this.freezeHealthCheckbox.Text = "Freeze Health";
            this.freezeHealthCheckbox.UseVisualStyleBackColor = true;
            this.freezeHealthCheckbox.CheckedChanged += new System.EventHandler(this.freezeHealthCheckbox_CheckedChanged);
            // 
            // raritaniumTextBox
            // 
            this.raritaniumTextBox.Location = new System.Drawing.Point(246, 90);
            this.raritaniumTextBox.Name = "raritaniumTextBox";
            this.raritaniumTextBox.Size = new System.Drawing.Size(106, 20);
            this.raritaniumTextBox.TabIndex = 105;
            this.raritaniumTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.raritaniumTextBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 106;
            this.label1.Text = "Raritanium:";
            // 
            // challengeTextBox
            // 
            this.challengeTextBox.Location = new System.Drawing.Point(246, 52);
            this.challengeTextBox.Name = "challengeTextBox";
            this.challengeTextBox.Size = new System.Drawing.Size(105, 20);
            this.challengeTextBox.TabIndex = 107;
            this.challengeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 108;
            this.label2.Text = "Challenge Mode:";
            // 
            // resetFileManipButton
            // 
            this.resetFileManipButton.Location = new System.Drawing.Point(247, 254);
            this.resetFileManipButton.Name = "resetFileManipButton";
            this.resetFileManipButton.Size = new System.Drawing.Size(104, 26);
            this.resetFileManipButton.TabIndex = 109;
            this.resetFileManipButton.Text = "Setup Any%";
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
            // labelLap
            // 
            this.labelLap.AutoSize = true;
            this.labelLap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLap.Location = new System.Drawing.Point(12, 350);
            this.labelLap.Name = "labelLap";
            this.labelLap.Size = new System.Drawing.Size(136, 20);
            this.labelLap.TabIndex = 110;
            this.labelLap.Text = "Lap is: <disabled>";
            this.labelLap.Visible = false;
            this.labelLap.Click += new System.EventHandler(this.labelLap_Click);
            // 
            // loadFileButton
            // 
            this.loadFileButton.Enabled = false;
            this.loadFileButton.Location = new System.Drawing.Point(132, 94);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(96, 23);
            this.loadFileButton.TabIndex = 111;
            this.loadFileButton.Text = "Load File";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // setAsideFileButton
            // 
            this.setAsideFileButton.Enabled = false;
            this.setAsideFileButton.Location = new System.Drawing.Point(133, 65);
            this.setAsideFileButton.Name = "setAsideFileButton";
            this.setAsideFileButton.Size = new System.Drawing.Size(95, 23);
            this.setAsideFileButton.TabIndex = 112;
            this.setAsideFileButton.Text = "Set Aside File";
            this.setAsideFileButton.UseVisualStyleBackColor = true;
            this.setAsideFileButton.Click += new System.EventHandler(this.setAsideFileButton_Click);
            // 
            // buttonSwingshot
            // 
            this.buttonSwingshot.Location = new System.Drawing.Point(140, 254);
            this.buttonSwingshot.Name = "buttonSwingshot";
            this.buttonSwingshot.Size = new System.Drawing.Size(68, 52);
            this.buttonSwingshot.TabIndex = 113;
            this.buttonSwingshot.Text = "Store\r\nSwingshot\r\n";
            this.toolTip1.SetToolTip(this.buttonSwingshot, "Sets your previous weapon to the Swingshot. For file setup.");
            this.buttonSwingshot.UseVisualStyleBackColor = true;
            this.buttonSwingshot.Click += new System.EventHandler(this.button4_Click);
            // 
            // checkBoxExp
            // 
            this.checkBoxExp.AutoSize = true;
            this.checkBoxExp.Location = new System.Drawing.Point(12, 322);
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
            this.buttonRaceStorage.Location = new System.Drawing.Point(247, 322);
            this.buttonRaceStorage.Name = "buttonRaceStorage";
            this.buttonRaceStorage.Size = new System.Drawing.Size(104, 36);
            this.buttonRaceStorage.TabIndex = 115;
            this.buttonRaceStorage.Text = "Reset race\r\nstorage\r\n";
            this.toolTip1.SetToolTip(this.buttonRaceStorage, "Resets the position of the cursor on the race selector.");
            this.buttonRaceStorage.UseVisualStyleBackColor = true;
            this.buttonRaceStorage.Click += new System.EventHandler(this.buttonRaceStorage_Click);
            // 
            // SetFastLoadCheckbox
            // 
            this.SetFastLoadCheckbox.AutoSize = true;
            this.SetFastLoadCheckbox.Location = new System.Drawing.Point(132, 210);
            this.SetFastLoadCheckbox.Name = "SetFastLoadCheckbox";
            this.SetFastLoadCheckbox.Size = new System.Drawing.Size(107, 17);
            this.SetFastLoadCheckbox.TabIndex = 116;
            this.SetFastLoadCheckbox.Text = "Enable fast loads";
            this.toolTip1.SetToolTip(this.SetFastLoadCheckbox, "Kinda make fast load?");
            this.SetFastLoadCheckbox.UseVisualStyleBackColor = true;
            this.SetFastLoadCheckbox.CheckedChanged += new System.EventHandler(this.SetFastLoadCheckbox_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 254);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 52);
            this.button1.TabIndex = 117;
            this.button1.Text = "Unlocks";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // buttonNGPlusMenu
            // 
            this.buttonNGPlusMenu.Location = new System.Drawing.Point(247, 280);
            this.buttonNGPlusMenu.Name = "buttonNGPlusMenu";
            this.buttonNGPlusMenu.Size = new System.Drawing.Size(104, 26);
            this.buttonNGPlusMenu.TabIndex = 118;
            this.buttonNGPlusMenu.Text = "Setup NG+";
            this.toolTip1.SetToolTip(this.buttonNGPlusMenu, "Unlocks the insomniac museum shortcut and puts the cursor over it\r\nin the shortcu" +
            "ts menu, manipulates the gorn cutscenes and act tunes\r\nthe protopet and the sniv" +
            "elak bosses.");
            this.buttonNGPlusMenu.UseVisualStyleBackColor = true;
            this.buttonNGPlusMenu.Click += new System.EventHandler(this.buttonNGPlusMenu_Click);
            // 
            // RAC2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 379);
            this.Controls.Add(this.SetFastLoadCheckbox);
            this.Controls.Add(this.buttonNGPlusMenu);
            this.Controls.Add(this.buttonRaceStorage);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.setAsideFileButton);
            this.Controls.Add(this.loadFileButton);
            this.Controls.Add(this.checkBoxExp);
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
            this.Controls.Add(this.label6);
            this.Controls.Add(this.loadPlanetButton);
            this.Controls.Add(this.planets_comboBox);
            this.Controls.Add(this.CComboCheckBox);
            this.Controls.Add(this.killyourself);
            this.Controls.Add(this.loadpos);
            this.Controls.Add(this.savepos);
            this.Controls.Add(this.positions_comboBox);
            this.Controls.Add(this.menuStrip1);
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

        private System.Windows.Forms.CheckBox CComboCheckBox;
        private System.Windows.Forms.CheckBox SetFastLoadCheckbox;
        private System.Windows.Forms.Button killyourself;
        private System.Windows.Forms.Button loadpos;
        private System.Windows.Forms.Button savepos;
        private System.Windows.Forms.ComboBox positions_comboBox;
        private System.Windows.Forms.Label label6;
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
    }
}
