
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
            this.wrcheckbox = new System.Windows.Forms.CheckBox();
            this.levelinfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // writetext
            // 
            this.writetext.AutoSize = true;
            this.writetext.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writetext.Location = new System.Drawing.Point(12, 12);
            this.writetext.Name = "writetext";
            this.writetext.Size = new System.Drawing.Size(151, 35);
            this.writetext.TabIndex = 0;
            this.writetext.Text = "Level IGT";
            this.writetext.UseVisualStyleBackColor = true;
            this.writetext.CheckedChanged += new System.EventHandler(this.writetext_CheckedChanged);
            // 
            // wrcheckbox
            // 
            this.wrcheckbox.AutoSize = true;
            this.wrcheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wrcheckbox.Location = new System.Drawing.Point(12, 53);
            this.wrcheckbox.Name = "wrcheckbox";
            this.wrcheckbox.Size = new System.Drawing.Size(113, 28);
            this.wrcheckbox.TabIndex = 2;
            this.wrcheckbox.Text = "Show WR";
            this.wrcheckbox.UseVisualStyleBackColor = true;
            // 
            // levelinfo
            // 
            this.levelinfo.AutoSize = true;
            this.levelinfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelinfo.Location = new System.Drawing.Point(169, 20);
            this.levelinfo.Name = "levelinfo";
            this.levelinfo.Size = new System.Drawing.Size(15, 24);
            this.levelinfo.TabIndex = 1;
            this.levelinfo.Text = " ";
            // 
            // RAC4Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 136);
            this.Controls.Add(this.wrcheckbox);
            this.Controls.Add(this.levelinfo);
            this.Controls.Add(this.writetext);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RAC4Form";
            this.Text = "Ratchet: Deadlocked (PAL) - NPEA00423";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RAC4Form_FormClosing);
            this.Load += new System.EventHandler(this.RAC4Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox writetext;
        private System.Windows.Forms.CheckBox wrcheckbox;
        private System.Windows.Forms.Label levelinfo;
    }
}