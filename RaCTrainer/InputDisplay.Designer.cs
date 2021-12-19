
namespace racman
{
    partial class InputDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputDisplay));
            this.skinComboBox = new System.Windows.Forms.ComboBox();
            this.skinLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // skinComboBox
            // 
            this.skinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.skinComboBox.FormattingEnabled = true;
            this.skinComboBox.Location = new System.Drawing.Point(15, 585);
            this.skinComboBox.Name = "skinComboBox";
            this.skinComboBox.Size = new System.Drawing.Size(121, 21);
            this.skinComboBox.TabIndex = 0;
            this.skinComboBox.SelectedIndexChanged += new System.EventHandler(this.skinComboBox_SelectedIndexChanged);
            // 
            // skinLabel
            // 
            this.skinLabel.AutoSize = true;
            this.skinLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skinLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.skinLabel.Location = new System.Drawing.Point(12, 569);
            this.skinLabel.Name = "skinLabel";
            this.skinLabel.Size = new System.Drawing.Size(36, 13);
            this.skinLabel.TabIndex = 1;
            this.skinLabel.Text = "Skin:";
            // 
            // InputDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(787, 620);
            this.Controls.Add(this.skinLabel);
            this.Controls.Add(this.skinComboBox);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InputDisplay";
            this.Text = "Input Display";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InputDisplay_FormClosing);
            this.Load += new System.EventHandler(this.InputDisplay_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.InputDisplay_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox skinComboBox;
        private System.Windows.Forms.Label skinLabel;
    }
}