namespace racman.TOD
{
    partial class FormLevelFlags
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
            this.flagPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            //
            // flagPanel
            //
            this.flagPanel.AutoScroll = true;
            this.flagPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagPanel.Location = new System.Drawing.Point(0, 0);
            this.flagPanel.Name = "flagPanel";
            this.flagPanel.Size = new System.Drawing.Size(334, 461);
            this.flagPanel.TabIndex = 0;
            //
            // FormLevelFlags
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 461);
            this.Controls.Add(this.flagPanel);
            this.Name = "FormLevelFlags";
            this.Text = "Level Flags";
            this.Load += new System.EventHandler(this.FormLevelFlags_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLevelFlags_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel flagPanel;
    }
}
