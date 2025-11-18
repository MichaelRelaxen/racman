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
            this.labelSplitterRoute = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(437, 24);
            this.menuStrip1.TabIndex = 76;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patchLoaderToolStripMenuItem,
            this.memoryUtilitiesToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // patchLoaderToolStripMenuItem
            // 
            this.patchLoaderToolStripMenuItem.Name = "patchLoaderToolStripMenuItem";
            this.patchLoaderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.patchLoaderToolStripMenuItem.Text = "Mods and patches...";
            this.patchLoaderToolStripMenuItem.Click += new System.EventHandler(this.patchLoaderToolStripMenuItem_Click);
            // 
            // memoryUtilitiesToolStripMenuItem
            // 
            this.memoryUtilitiesToolStripMenuItem.Name = "memoryUtilitiesToolStripMenuItem";
            this.memoryUtilitiesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.memoryUtilitiesToolStripMenuItem.Text = "Memory utilities";
            this.memoryUtilitiesToolStripMenuItem.Click += new System.EventHandler(this.memoryUtilitiesToolStripMenuItem_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
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
            this.planets_comboBox.Location = new System.Drawing.Point(28, 64);
            this.planets_comboBox.Name = "planets_comboBox";
            this.planets_comboBox.Size = new System.Drawing.Size(115, 21);
            this.planets_comboBox.TabIndex = 98;
            this.planets_comboBox.SelectedIndexChanged += new System.EventHandler(this.planets_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(25, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "To change planets, save and load your file.\r\n";
            // 
            // buttonStartAutosplitter
            // 
            this.buttonStartAutosplitter.Location = new System.Drawing.Point(28, 120);
            this.buttonStartAutosplitter.Name = "buttonStartAutosplitter";
            this.buttonStartAutosplitter.Size = new System.Drawing.Size(115, 23);
            this.buttonStartAutosplitter.TabIndex = 104;
            this.buttonStartAutosplitter.Text = "Start Autosplitter";
            this.buttonStartAutosplitter.UseVisualStyleBackColor = true;
            this.buttonStartAutosplitter.Click += new System.EventHandler(this.buttonStartAutosplitter_Click);
            // 
            // labelAutosplitterStatus
            // 
            this.labelAutosplitterStatus.AutoSize = true;
            this.labelAutosplitterStatus.ForeColor = System.Drawing.Color.Red;
            this.labelAutosplitterStatus.Location = new System.Drawing.Point(149, 125);
            this.labelAutosplitterStatus.Name = "labelAutosplitterStatus";
            this.labelAutosplitterStatus.Size = new System.Drawing.Size(95, 13);
            this.labelAutosplitterStatus.TabIndex = 105;
            this.labelAutosplitterStatus.Text = "Autosplitter disbled";
            // 
            // labelSplitterRoute
            // 
            this.labelSplitterRoute.AutoSize = true;
            this.labelSplitterRoute.ForeColor = System.Drawing.Color.Gray;
            this.labelSplitterRoute.Location = new System.Drawing.Point(258, 125);
            this.labelSplitterRoute.Name = "labelSplitterRoute";
            this.labelSplitterRoute.Size = new System.Drawing.Size(101, 13);
            this.labelSplitterRoute.TabIndex = 106;
            this.labelSplitterRoute.Text = "Autosplitter disabled";
            this.labelSplitterRoute.Visible = false;
            // 
            // TODForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 170);
            this.Controls.Add(this.labelSplitterRoute);
            this.Controls.Add(this.labelAutosplitterStatus);
            this.Controls.Add(this.buttonStartAutosplitter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.planets_comboBox);
            this.Controls.Add(this.menuStrip1);
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
        private System.Windows.Forms.Label labelSplitterRoute;
    }
}