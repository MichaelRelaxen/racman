
namespace racman
{
    partial class ConfigureCombos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureCombos));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.savePositionTextBox = new System.Windows.Forms.TextBox();
            this.loadPositionTextBox = new System.Windows.Forms.TextBox();
            this.switchPositionTextBox = new System.Windows.Forms.TextBox();
            this.dieTextBox = new System.Windows.Forms.TextBox();
            this.loadPlanetTextBox = new System.Windows.Forms.TextBox();
            this.infoText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Save Position:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Load Position:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Die:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Switch Position:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Load Planet:";
            // 
            // savePositionTextBox
            // 
            this.savePositionTextBox.Location = new System.Drawing.Point(12, 29);
            this.savePositionTextBox.Name = "savePositionTextBox";
            this.savePositionTextBox.Size = new System.Drawing.Size(126, 20);
            this.savePositionTextBox.TabIndex = 5;
            this.savePositionTextBox.Click += new System.EventHandler(this.savePositionTextBox_Click);
            // 
            // loadPositionTextBox
            // 
            this.loadPositionTextBox.Location = new System.Drawing.Point(12, 73);
            this.loadPositionTextBox.Name = "loadPositionTextBox";
            this.loadPositionTextBox.Size = new System.Drawing.Size(126, 20);
            this.loadPositionTextBox.TabIndex = 6;
            this.loadPositionTextBox.Click += new System.EventHandler(this.loadPositionTextBox_Click);
            // 
            // switchPositionTextBox
            // 
            this.switchPositionTextBox.Location = new System.Drawing.Point(12, 112);
            this.switchPositionTextBox.Name = "switchPositionTextBox";
            this.switchPositionTextBox.Size = new System.Drawing.Size(126, 20);
            this.switchPositionTextBox.TabIndex = 7;
            this.switchPositionTextBox.Click += new System.EventHandler(this.switchPositionTextBox_Click);
            // 
            // dieTextBox
            // 
            this.dieTextBox.Location = new System.Drawing.Point(12, 152);
            this.dieTextBox.Name = "dieTextBox";
            this.dieTextBox.Size = new System.Drawing.Size(126, 20);
            this.dieTextBox.TabIndex = 8;
            this.dieTextBox.Click += new System.EventHandler(this.dieTextBox_Click);
            // 
            // loadPlanetTextBox
            // 
            this.loadPlanetTextBox.Location = new System.Drawing.Point(12, 194);
            this.loadPlanetTextBox.Name = "loadPlanetTextBox";
            this.loadPlanetTextBox.Size = new System.Drawing.Size(126, 20);
            this.loadPlanetTextBox.TabIndex = 9;
            this.loadPlanetTextBox.Click += new System.EventHandler(this.loadPlanetTextBox_Click);
            // 
            // infoText
            // 
            this.infoText.AutoSize = true;
            this.infoText.Location = new System.Drawing.Point(12, 221);
            this.infoText.Name = "infoText";
            this.infoText.Size = new System.Drawing.Size(27, 13);
            this.infoText.TabIndex = 10;
            this.infoText.Text = "stuff";
            // 
            // ConfigureCombos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 270);
            this.Controls.Add(this.infoText);
            this.Controls.Add(this.loadPlanetTextBox);
            this.Controls.Add(this.dieTextBox);
            this.Controls.Add(this.switchPositionTextBox);
            this.Controls.Add(this.loadPositionTextBox);
            this.Controls.Add(this.savePositionTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigureCombos";
            this.Text = "Configure Combos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigureCombos_FormClosing);
            this.Load += new System.EventHandler(this.ConfigureCombos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox savePositionTextBox;
        private System.Windows.Forms.TextBox loadPositionTextBox;
        private System.Windows.Forms.TextBox switchPositionTextBox;
        private System.Windows.Forms.TextBox dieTextBox;
        private System.Windows.Forms.TextBox loadPlanetTextBox;
        private System.Windows.Forms.Label infoText;
    }
}