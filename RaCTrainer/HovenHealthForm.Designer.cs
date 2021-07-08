
namespace racman
{
    partial class HovenHealthForm
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
            System.Windows.Forms.Button refreshBtn;
            this.hpLabel = new System.Windows.Forms.Label();
            refreshBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // refreshBtn
            // 
            refreshBtn.Location = new System.Drawing.Point(12, 12);
            refreshBtn.Name = "refreshBtn";
            refreshBtn.Size = new System.Drawing.Size(103, 23);
            refreshBtn.TabIndex = 0;
            refreshBtn.Text = "Refresh ship";
            refreshBtn.UseVisualStyleBackColor = true;
            refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // hpLabel
            // 
            this.hpLabel.AutoSize = true;
            this.hpLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hpLabel.Location = new System.Drawing.Point(12, 53);
            this.hpLabel.Name = "hpLabel";
            this.hpLabel.Size = new System.Drawing.Size(89, 20);
            this.hpLabel.TabIndex = 1;
            this.hpLabel.Text = "Ship health";
            // 
            // HovenHealthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 153);
            this.Controls.Add(this.hpLabel);
            this.Controls.Add(refreshBtn);
            this.Name = "HovenHealthForm";
            this.Text = "HovenHealthForm";
            this.Load += new System.EventHandler(this.HovenHealthForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label hpLabel;
    }
}