
namespace racman
{
    partial class SavefileLoader
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
            this.savebutton = new System.Windows.Forms.Button();
            this.loadbutton = new System.Windows.Forms.Button();
            this.nameinput = new System.Windows.Forms.TextBox();
            this.catdropdown = new System.Windows.Forms.ComboBox();
            this.savelist = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // savebutton
            // 
            this.savebutton.Location = new System.Drawing.Point(12, 12);
            this.savebutton.Name = "savebutton";
            this.savebutton.Size = new System.Drawing.Size(131, 45);
            this.savebutton.TabIndex = 0;
            this.savebutton.Text = "Save File";
            this.savebutton.UseVisualStyleBackColor = true;
            this.savebutton.Click += new System.EventHandler(this.savebutton_Click);
            // 
            // loadbutton
            // 
            this.loadbutton.Location = new System.Drawing.Point(149, 12);
            this.loadbutton.Name = "loadbutton";
            this.loadbutton.Size = new System.Drawing.Size(131, 45);
            this.loadbutton.TabIndex = 1;
            this.loadbutton.Text = "Set Aside File";
            this.loadbutton.UseVisualStyleBackColor = true;
            this.loadbutton.Click += new System.EventHandler(this.loadbutton_Click);
            // 
            // nameinput
            // 
            this.nameinput.Location = new System.Drawing.Point(12, 400);
            this.nameinput.Name = "nameinput";
            this.nameinput.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.nameinput.Size = new System.Drawing.Size(268, 20);
            this.nameinput.TabIndex = 2;
            this.nameinput.Text = "Enter savename here...";
            this.nameinput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nameinput.Click += new System.EventHandler(this.nameinput_Click);
            // 
            // catdropdown
            // 
            this.catdropdown.FormattingEnabled = true;
            this.catdropdown.Items.AddRange(new object[] {
            "NG+",
            "NG+ no QE",
            "Any% no FC",
            "Any%",
            "All Titanium Bolts",
            "All Collectables",
            "100%",
            "Misc/Other"});
            this.catdropdown.Location = new System.Drawing.Point(12, 63);
            this.catdropdown.Name = "catdropdown";
            this.catdropdown.Size = new System.Drawing.Size(268, 21);
            this.catdropdown.TabIndex = 3;
            this.catdropdown.Text = "Categories...";
            this.catdropdown.SelectedIndexChanged += new System.EventHandler(this.catdropdown_SelectedIndexChanged);
            // 
            // savelist
            // 
            this.savelist.FormattingEnabled = true;
            this.savelist.Location = new System.Drawing.Point(12, 90);
            this.savelist.Name = "savelist";
            this.savelist.Size = new System.Drawing.Size(268, 303);
            this.savelist.TabIndex = 4;
            // 
            // SavefileLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 430);
            this.Controls.Add(this.savelist);
            this.Controls.Add(this.catdropdown);
            this.Controls.Add(this.nameinput);
            this.Controls.Add(this.loadbutton);
            this.Controls.Add(this.savebutton);
            this.MaximizeBox = false;
            this.Name = "SavefileLoader";
            this.ShowIcon = false;
            this.Text = "Savefile Manager";
            this.Load += new System.EventHandler(this.SavefileLoader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button savebutton;
        private System.Windows.Forms.Button loadbutton;
        private System.Windows.Forms.TextBox nameinput;
        private System.Windows.Forms.ComboBox catdropdown;
        private System.Windows.Forms.ListBox savelist;
    }
}