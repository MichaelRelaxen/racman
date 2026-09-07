namespace racman.TOD
{
    partial class CategoryChoiceForm
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
            this.buttonWith = new System.Windows.Forms.Button();
            this.buttonWithout = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonWith
            // 
            this.buttonWith.Location = new System.Drawing.Point(12, 93);
            this.buttonWith.Name = "buttonWith";
            this.buttonWith.Size = new System.Drawing.Size(312, 75);
            this.buttonWith.TabIndex = 0;
            this.buttonWith.Text = "With bolt smugging\r\n(AGB, 10GB)\r\n";
            this.buttonWith.UseVisualStyleBackColor = true;
            this.buttonWith.Click += new System.EventHandler(this.buttonAGB_Click);
            // 
            // buttonWithout
            // 
            this.buttonWithout.Location = new System.Drawing.Point(12, 12);
            this.buttonWithout.Name = "buttonWithout";
            this.buttonWithout.Size = new System.Drawing.Size(312, 75);
            this.buttonWithout.TabIndex = 1;
            this.buttonWithout.Text = "Without bolt smuggling\r\n(NG+, Any%, 100%)";
            this.buttonWithout.UseVisualStyleBackColor = true;
            this.buttonWithout.Click += new System.EventHandler(this.buttonNormal_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.Location = new System.Drawing.Point(241, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 75);
            this.button1.TabIndex = 2;
            this.button1.Text = "With old ASS (NG+, Any%)";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.buttonEvil_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 174);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(223, 75);
            this.button2.TabIndex = 3;
            this.button2.Text = "Wihout autoscroller skips";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonNone_Click);
            // 
            // CategoryChoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 259);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonWithout);
            this.Controls.Add(this.buttonWith);
            this.Name = "CategoryChoiceForm";
            this.Text = "Select category type...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonWith;
        private System.Windows.Forms.Button buttonWithout;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}