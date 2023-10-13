namespace racman
{
    partial class ACITUnlocks
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
            this.components = new System.ComponentModel.Container();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonDowngrade = new System.Windows.Forms.Button();
            this.buttonUpgrade = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonRemoveAll = new System.Windows.Forms.Button();
            this.buttonUnlockAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.weaponsCheckList = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // buttonDowngrade
            // 
            this.buttonDowngrade.Location = new System.Drawing.Point(171, 52);
            this.buttonDowngrade.Name = "buttonDowngrade";
            this.buttonDowngrade.Size = new System.Drawing.Size(168, 24);
            this.buttonDowngrade.TabIndex = 57;
            this.buttonDowngrade.Text = "v1 All";
            this.buttonDowngrade.UseVisualStyleBackColor = true;
            // 
            // buttonUpgrade
            // 
            this.buttonUpgrade.Location = new System.Drawing.Point(171, 22);
            this.buttonUpgrade.Name = "buttonUpgrade";
            this.buttonUpgrade.Size = new System.Drawing.Size(168, 24);
            this.buttonUpgrade.TabIndex = 56;
            this.buttonUpgrade.Text = "v8 All*";
            this.buttonUpgrade.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "Levelling";
            // 
            // buttonRemoveAll
            // 
            this.buttonRemoveAll.Location = new System.Drawing.Point(12, 52);
            this.buttonRemoveAll.Name = "buttonRemoveAll";
            this.buttonRemoveAll.Size = new System.Drawing.Size(139, 24);
            this.buttonRemoveAll.TabIndex = 54;
            this.buttonRemoveAll.Text = "Remove All";
            this.buttonRemoveAll.UseVisualStyleBackColor = true;
            this.buttonRemoveAll.Click += new System.EventHandler(this.buttonRemoveAll_Click);
            // 
            // buttonUnlockAll
            // 
            this.buttonUnlockAll.Location = new System.Drawing.Point(12, 22);
            this.buttonUnlockAll.Name = "buttonUnlockAll";
            this.buttonUnlockAll.Size = new System.Drawing.Size(139, 24);
            this.buttonUnlockAll.TabIndex = 53;
            this.buttonUnlockAll.Text = "Unlock All";
            this.buttonUnlockAll.UseVisualStyleBackColor = true;
            this.buttonUnlockAll.Click += new System.EventHandler(this.buttonUnlockAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Unlocks";
            // 
            // weaponsCheckList
            // 
            this.weaponsCheckList.FormattingEnabled = true;
            this.weaponsCheckList.Location = new System.Drawing.Point(12, 82);
            this.weaponsCheckList.Name = "weaponsCheckList";
            this.weaponsCheckList.Size = new System.Drawing.Size(327, 559);
            this.weaponsCheckList.TabIndex = 51;
            // 
            // ACITUnlocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 670);
            this.Controls.Add(this.buttonDowngrade);
            this.Controls.Add(this.buttonUpgrade);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonRemoveAll);
            this.Controls.Add(this.buttonUnlockAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.weaponsCheckList);
            this.Name = "ACITUnlocks";
            this.Text = "ACITUnlocks";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonDowngrade;
        private System.Windows.Forms.Button buttonUpgrade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonRemoveAll;
        private System.Windows.Forms.Button buttonUnlockAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox weaponsCheckList;
    }
}