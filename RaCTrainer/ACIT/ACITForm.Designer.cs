
namespace racman
{
    partial class ACITForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ACITForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchLoaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryUtilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputdisplay = new System.Windows.Forms.Button();
            this.AutosplitterCheckbox = new System.Windows.Forms.CheckBox();
            this.bolts_textBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.killyourself = new System.Windows.Forms.Button();
            this.unlocksWindowButton = new System.Windows.Forms.Button();
            this.disableCutscenesCheckBox = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(291, 24);
            this.menuStrip1.TabIndex = 76;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patchLoaderToolStripMenuItem,
            this.memoryUtilitiesToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            this.menuToolStripMenuItem.Click += new System.EventHandler(this.menuToolStripMenuItem_Click);
            // 
            // patchLoaderToolStripMenuItem
            // 
            this.patchLoaderToolStripMenuItem.Name = "patchLoaderToolStripMenuItem";
            this.patchLoaderToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.patchLoaderToolStripMenuItem.Text = "Patch loader...";
            this.patchLoaderToolStripMenuItem.Click += new System.EventHandler(this.patchLoaderToolStripMenuItem_Click);
            // 
            // memoryUtilitiesToolStripMenuItem
            // 
            this.memoryUtilitiesToolStripMenuItem.Name = "memoryUtilitiesToolStripMenuItem";
            this.memoryUtilitiesToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.memoryUtilitiesToolStripMenuItem.Text = "Memory utilities";
            this.memoryUtilitiesToolStripMenuItem.Click += new System.EventHandler(this.memoryUtilitiesToolStripMenuItem_Click);
            // 
            // inputdisplay
            // 
            this.inputdisplay.Location = new System.Drawing.Point(11, 26);
            this.inputdisplay.Margin = new System.Windows.Forms.Padding(2);
            this.inputdisplay.Name = "inputdisplay";
            this.inputdisplay.Size = new System.Drawing.Size(106, 22);
            this.inputdisplay.TabIndex = 83;
            this.inputdisplay.Text = "Input Display";
            this.inputdisplay.UseVisualStyleBackColor = true;
            this.inputdisplay.Click += new System.EventHandler(this.inputdisplay_Click);
            // 
            // AutosplitterCheckbox
            // 
            this.AutosplitterCheckbox.AutoSize = true;
            this.AutosplitterCheckbox.Location = new System.Drawing.Point(201, 81);
            this.AutosplitterCheckbox.Name = "AutosplitterCheckbox";
            this.AutosplitterCheckbox.Size = new System.Drawing.Size(78, 17);
            this.AutosplitterCheckbox.TabIndex = 103;
            this.AutosplitterCheckbox.Text = "Autosplitter";
            this.AutosplitterCheckbox.UseVisualStyleBackColor = true;
            this.AutosplitterCheckbox.CheckedChanged += new System.EventHandler(this.AutosplitterCheckbox_CheckedChanged);
            // 
            // bolts_textBox
            // 
            this.bolts_textBox.Location = new System.Drawing.Point(174, 45);
            this.bolts_textBox.Name = "bolts_textBox";
            this.bolts_textBox.Size = new System.Drawing.Size(105, 20);
            this.bolts_textBox.TabIndex = 104;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(171, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 105;
            this.label8.Text = "Bolt Count:";
            // 
            // killyourself
            // 
            this.killyourself.Location = new System.Drawing.Point(164, 114);
            this.killyourself.Name = "killyourself";
            this.killyourself.Size = new System.Drawing.Size(115, 23);
            this.killyourself.TabIndex = 106;
            this.killyourself.Text = "Die";
            this.killyourself.UseVisualStyleBackColor = true;
            this.killyourself.Click += new System.EventHandler(this.killyourself_Click);
            // 
            // unlocksWindowButton
            // 
            this.unlocksWindowButton.Location = new System.Drawing.Point(12, 53);
            this.unlocksWindowButton.Name = "unlocksWindowButton";
            this.unlocksWindowButton.Size = new System.Drawing.Size(106, 23);
            this.unlocksWindowButton.TabIndex = 107;
            this.unlocksWindowButton.Text = "Unlocks";
            this.unlocksWindowButton.UseVisualStyleBackColor = true;
            this.unlocksWindowButton.Click += new System.EventHandler(this.unlocksWindowButton_Click);
            // 
            // disableCutscenesCheckBox
            // 
            this.disableCutscenesCheckBox.AutoSize = true;
            this.disableCutscenesCheckBox.Location = new System.Drawing.Point(12, 81);
            this.disableCutscenesCheckBox.Name = "disableCutscenesCheckBox";
            this.disableCutscenesCheckBox.Size = new System.Drawing.Size(118, 17);
            this.disableCutscenesCheckBox.TabIndex = 108;
            this.disableCutscenesCheckBox.Text = "Disable cutscenes";
            this.disableCutscenesCheckBox.UseVisualStyleBackColor = true;
            this.disableCutscenesCheckBox.CheckedChanged += new System.EventHandler(this.disableCutscenesCheckBox_CheckedChanged);
            // 
            // ACITForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 149);
            this.Controls.Add(this.disableCutscenesCheckBox);
            this.Controls.Add(this.unlocksWindowButton);
            this.Controls.Add(this.killyourself);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.bolts_textBox);
            this.Controls.Add(this.AutosplitterCheckbox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.inputdisplay);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ACITForm";
            this.Text = "ACITForm";
            this.Load += new System.EventHandler(this.ACITForm_Load);
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
        private System.Windows.Forms.Button inputdisplay;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox AutosplitterCheckbox;
        private System.Windows.Forms.TextBox bolts_textBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button killyourself;
        private System.Windows.Forms.Button unlocksWindowButton;
        private System.Windows.Forms.CheckBox disableCutscenesCheckBox;
    }
}
