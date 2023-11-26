
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
            this.AutosplitterCheckbox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.bolts_textBox = new System.Windows.Forms.TextBox();
            this.killyourself = new System.Windows.Forms.Button();
            this.botsUnlocksWindowButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // writetext
            // 
            this.writetext.AutoSize = true;
            this.writetext.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writetext.Location = new System.Drawing.Point(12, 35);
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
            this.levelinfo.Location = new System.Drawing.Point(35, 73);
            this.levelinfo.Name = "levelinfo";
            this.levelinfo.Size = new System.Drawing.Size(138, 20);
            this.levelinfo.TabIndex = 1;
            this.levelinfo.Text = "Challenge - Planet";
            // 
            // wrtext
            // 
            this.wrtext.AutoSize = true;
            this.wrtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wrtext.Location = new System.Drawing.Point(35, 100);
            this.wrtext.Name = "wrtext";
            this.wrtext.Size = new System.Drawing.Size(44, 20);
            this.wrtext.TabIndex = 2;
            this.wrtext.Text = "WR: ";
            // 
            // ghostcheck
            // 
            this.ghostcheck.AutoSize = true;
            this.ghostcheck.Location = new System.Drawing.Point(522, 105);
            this.ghostcheck.Name = "ghostcheck";
            this.ghostcheck.Size = new System.Drawing.Size(95, 17);
            this.ghostcheck.TabIndex = 3;
            this.ghostcheck.Text = "Ghost Ratchet";
            this.ghostcheck.UseVisualStyleBackColor = true;
            this.ghostcheck.CheckedChanged += new System.EventHandler(this.ghostcheck_CheckedChanged);
            // 
            // inputdisplaybutton
            // 
            this.inputdisplaybutton.Location = new System.Drawing.Point(542, 61);
            this.inputdisplaybutton.Name = "inputdisplaybutton";
            this.inputdisplaybutton.Size = new System.Drawing.Size(75, 23);
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
            this.menuStrip1.Size = new System.Drawing.Size(629, 24);
            this.menuStrip1.TabIndex = 77;
            this.menuStrip1.Text = "menuStrip1";
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
            // AutosplitterCheckbox
            // 
            this.AutosplitterCheckbox.AutoSize = true;
            this.AutosplitterCheckbox.Location = new System.Drawing.Point(542, 35);
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
            this.label8.Location = new System.Drawing.Point(519, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 107;
            this.label8.Text = "Bolt Count:";
            // 
            // bolts_textBox
            // 
            this.bolts_textBox.Location = new System.Drawing.Point(522, 153);
            this.bolts_textBox.Name = "bolts_textBox";
            this.bolts_textBox.Size = new System.Drawing.Size(105, 20);
            this.bolts_textBox.TabIndex = 106;
            // 
            // killyourself
            // 
            this.killyourself.Location = new System.Drawing.Point(502, 190);
            this.killyourself.Name = "killyourself";
            this.killyourself.Size = new System.Drawing.Size(115, 23);
            this.killyourself.TabIndex = 108;
            this.killyourself.Text = "Die";
            this.killyourself.UseVisualStyleBackColor = true;
            this.killyourself.Click += new System.EventHandler(this.killyourself_Click);
            // 
            // botsUnlocksWindowButton
            // 
            this.botsUnlocksWindowButton.Location = new System.Drawing.Point(12, 190);
            this.botsUnlocksWindowButton.Name = "botsUnlocksWindowButton";
            this.botsUnlocksWindowButton.Size = new System.Drawing.Size(106, 23);
            this.botsUnlocksWindowButton.TabIndex = 109;
            this.botsUnlocksWindowButton.Text = "Bots Unlocks";
            this.botsUnlocksWindowButton.UseVisualStyleBackColor = true;
            this.botsUnlocksWindowButton.Click += new System.EventHandler(this.botsUnlocksWindowButton_Click);
            // 
            // RAC4Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 225);
            this.Controls.Add(this.botsUnlocksWindowButton);
            this.Controls.Add(this.killyourself);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.bolts_textBox);
            this.Controls.Add(this.AutosplitterCheckbox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.inputdisplaybutton);
            this.Controls.Add(this.ghostcheck);
            this.Controls.Add(this.wrtext);
            this.Controls.Add(this.levelinfo);
            this.Controls.Add(this.writetext);
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
        private System.Windows.Forms.CheckBox AutosplitterCheckbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox bolts_textBox;
        private System.Windows.Forms.Button killyourself;
        private System.Windows.Forms.Button botsUnlocksWindowButton;
    }
}
