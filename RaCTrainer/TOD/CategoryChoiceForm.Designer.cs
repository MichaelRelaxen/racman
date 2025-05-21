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
            this.buttonWith.Click += new System.EventHandler(this.buttonWith_Click);
            // 
            // buttonWithout
            // 
            this.buttonWithout.Location = new System.Drawing.Point(12, 12);
            this.buttonWithout.Name = "buttonWithout";
            this.buttonWithout.Size = new System.Drawing.Size(312, 75);
            this.buttonWithout.TabIndex = 1;
            this.buttonWithout.Text = "Without bolt smuggling\r\n(NG+, Any%, 100%)";
            this.buttonWithout.UseVisualStyleBackColor = true;
            this.buttonWithout.Click += new System.EventHandler(this.buttonWithout_Click);
            // 
            // CategoryChoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 177);
            this.Controls.Add(this.buttonWithout);
            this.Controls.Add(this.buttonWith);
            this.Name = "CategoryChoiceForm";
            this.Text = "Select category type...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonWith;
        private System.Windows.Forms.Button buttonWithout;
    }
}