namespace racman
{
    partial class RAC2JPForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RAC2JPForm));
            this.textBoxRari = new System.Windows.Forms.TextBox();
            this.labelRari = new System.Windows.Forms.Label();
            this.labelBolts = new System.Windows.Forms.Label();
            this.textBoxBolts = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureButtonCombosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.patchLoaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryUtilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSlots = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.AutosplitterCheckbox = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxRari
            // 
            this.textBoxRari.Location = new System.Drawing.Point(12, 51);
            this.textBoxRari.Name = "textBoxRari";
            this.textBoxRari.Size = new System.Drawing.Size(100, 20);
            this.textBoxRari.TabIndex = 0;
            this.textBoxRari.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRari_KeyDown);
            // 
            // labelRari
            // 
            this.labelRari.AutoSize = true;
            this.labelRari.Location = new System.Drawing.Point(12, 35);
            this.labelRari.Name = "labelRari";
            this.labelRari.Size = new System.Drawing.Size(60, 13);
            this.labelRari.TabIndex = 1;
            this.labelRari.Text = "Raritanium:";
            // 
            // labelBolts
            // 
            this.labelBolts.AutoSize = true;
            this.labelBolts.Location = new System.Drawing.Point(12, 74);
            this.labelBolts.Name = "labelBolts";
            this.labelBolts.Size = new System.Drawing.Size(33, 13);
            this.labelBolts.TabIndex = 3;
            this.labelBolts.Text = "Bolts:";
            // 
            // textBoxBolts
            // 
            this.textBoxBolts.Location = new System.Drawing.Point(12, 90);
            this.textBoxBolts.Name = "textBoxBolts";
            this.textBoxBolts.Size = new System.Drawing.Size(100, 20);
            this.textBoxBolts.TabIndex = 2;
            this.textBoxBolts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxBolts_KeyDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menusToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(341, 24);
            this.menuStrip1.TabIndex = 102;
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
            this.menusToolStripMenuItem.Click += new System.EventHandler(this.menusToolStripMenuItem_Click);
            // 
            // configureButtonCombosToolStripMenuItem
            // 
            this.configureButtonCombosToolStripMenuItem.Enabled = false;
            this.configureButtonCombosToolStripMenuItem.Name = "configureButtonCombosToolStripMenuItem";
            this.configureButtonCombosToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.configureButtonCombosToolStripMenuItem.Text = "Configure button combos";
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
            // buttonSlots
            // 
            this.buttonSlots.Location = new System.Drawing.Point(118, 35);
            this.buttonSlots.Name = "buttonSlots";
            this.buttonSlots.Size = new System.Drawing.Size(211, 75);
            this.buttonSlots.TabIndex = 103;
            this.buttonSlots.Text = "SETUP SLOTS (for real)";
            this.buttonSlots.UseVisualStyleBackColor = true;
            this.buttonSlots.Click += new System.EventHandler(this.buttonSlots_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.Location = new System.Drawing.Point(249, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 104;
            this.label1.Text = "more here soon";
            // 
            // AutosplitterCheckbox
            // 
            this.AutosplitterCheckbox.AutoSize = true;
            this.AutosplitterCheckbox.Enabled = false;
            this.AutosplitterCheckbox.Location = new System.Drawing.Point(12, 125);
            this.AutosplitterCheckbox.Name = "AutosplitterCheckbox";
            this.AutosplitterCheckbox.Size = new System.Drawing.Size(78, 17);
            this.AutosplitterCheckbox.TabIndex = 105;
            this.AutosplitterCheckbox.Text = "Autosplitter";
            this.AutosplitterCheckbox.UseVisualStyleBackColor = true;
            this.AutosplitterCheckbox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // RAC2JPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 154);
            this.Controls.Add(this.AutosplitterCheckbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSlots);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.labelBolts);
            this.Controls.Add(this.textBoxBolts);
            this.Controls.Add(this.labelRari);
            this.Controls.Add(this.textBoxRari);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RAC2JPForm";
            this.Text = " Ratchet & Clank 2 - NPJA40002 (JP)";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxRari;
        private System.Windows.Forms.Label labelRari;
        private System.Windows.Forms.Label labelBolts;
        private System.Windows.Forms.TextBox textBoxBolts;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureButtonCombosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem patchLoaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memoryUtilitiesToolStripMenuItem;
        private System.Windows.Forms.Button buttonSlots;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox AutosplitterCheckbox;
    }
}