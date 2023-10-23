
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
            this.currentVerLabel = new System.Windows.Forms.Label();
            this.AttachRPCS3Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(57, 58);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(100, 20);
            this.IPTextBox.TabIndex = 0;
            this.IPTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IPTextBox_KeyDown);
            // 
            // attachButton
            // 
            this.attachButton.Location = new System.Drawing.Point(163, 56);
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
            this.label1.Location = new System.Drawing.Point(54, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP Address:";
            // 
            // currentVerLabel
            // 
            this.currentVerLabel.AutoSize = true;
            this.currentVerLabel.Location = new System.Drawing.Point(12, 104);
            this.currentVerLabel.Name = "currentVerLabel";
            this.currentVerLabel.Size = new System.Drawing.Size(41, 13);
            this.currentVerLabel.TabIndex = 3;
            this.currentVerLabel.Text = "gaming";
            this.currentVerLabel.Click += new System.EventHandler(this.currentVerLabel_Click);
            // 
            // AttachRPCS3Button
            // 
            this.AttachRPCS3Button.Location = new System.Drawing.Point(216, 93);
            this.AttachRPCS3Button.Name = "AttachRPCS3Button";
            this.AttachRPCS3Button.Size = new System.Drawing.Size(75, 23);
            this.AttachRPCS3Button.TabIndex = 4;
            this.AttachRPCS3Button.Text = "RPCS3";
            this.AttachRPCS3Button.UseVisualStyleBackColor = true;
            this.AttachRPCS3Button.Click += new System.EventHandler(this.AttachRPCS3Button_Click);
            // 
            // AttachPS3Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 126);
            this.Controls.Add(this.AttachRPCS3Button);
            this.Controls.Add(this.currentVerLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.attachButton);
            this.Controls.Add(this.IPTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AttachPS3Form";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attach to Ratchet & Clank";
            this.Load += new System.EventHandler(this.AttachPS3Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.Button attachButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label currentVerLabel;
        private System.Windows.Forms.Button AttachRPCS3Button;
    }
}