namespace racman.RAC4
{
    partial class FormAutosplitter
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.checkBoxSoftlocks = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(23, 49);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(110, 26);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // checkBoxSoftlocks
            // 
            this.checkBoxSoftlocks.AutoSize = true;
            this.checkBoxSoftlocks.Checked = true;
            this.checkBoxSoftlocks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSoftlocks.Location = new System.Drawing.Point(23, 26);
            this.checkBoxSoftlocks.Name = "checkBoxSoftlocks";
            this.checkBoxSoftlocks.Size = new System.Drawing.Size(110, 17);
            this.checkBoxSoftlocks.TabIndex = 113;
            this.checkBoxSoftlocks.Text = "Fix reset softlocks";
            this.checkBoxSoftlocks.UseVisualStyleBackColor = true;
            this.checkBoxSoftlocks.CheckedChanged += new System.EventHandler(this.checkBoxSoftlocks_CheckedChanged);
            // 
            // FormAutosplitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 112);
            this.Controls.Add(this.checkBoxSoftlocks);
            this.Controls.Add(this.buttonClose);
            this.Name = "FormAutosplitter";
            this.Text = "Autosplitter running...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAutosplitter_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.CheckBox checkBoxSoftlocks;
    }
}