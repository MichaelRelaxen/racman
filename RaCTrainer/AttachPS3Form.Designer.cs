
namespace racman
{
    partial class AttachPS3Form
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
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.attachButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(15, 25);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(100, 20);
            this.IPTextBox.TabIndex = 0;
            // 
            // attachButton
            // 
            this.attachButton.Location = new System.Drawing.Point(121, 23);
            this.attachButton.Name = "attachButton";
            this.attachButton.Size = new System.Drawing.Size(75, 23);
            this.attachButton.TabIndex = 1;
            this.attachButton.Text = "Attach";
            this.attachButton.UseVisualStyleBackColor = true;
            this.attachButton.Click += new System.EventHandler(this.attachButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP Address:";
            // 
            // AttachPS3Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 67);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.attachButton);
            this.Controls.Add(this.IPTextBox);
            this.Name = "AttachPS3Form";
            this.Text = "Attach to Webman";
            this.Load += new System.EventHandler(this.AttachPS3Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.Button attachButton;
        private System.Windows.Forms.Label label1;
    }
}