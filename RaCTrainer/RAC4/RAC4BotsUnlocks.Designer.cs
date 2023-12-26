namespace racman
{
    partial class RAC4BotsUnlocks
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
            this.buttonRemoveAll = new System.Windows.Forms.Button();
            this.buttonUnlockAll = new System.Windows.Forms.Button();
            this.lbUnlockAll = new System.Windows.Forms.Label();
            this.botsUnlocksCheckList = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // buttonRemoveAll
            // 
            this.buttonRemoveAll.Location = new System.Drawing.Point(12, 22);
            this.buttonRemoveAll.Name = "buttonRemoveAll";
            this.buttonRemoveAll.Size = new System.Drawing.Size(178, 24);
            this.buttonRemoveAll.TabIndex = 54;
            this.buttonRemoveAll.Text = "Remove All";
            this.buttonRemoveAll.UseVisualStyleBackColor = true;
            this.buttonRemoveAll.Click += new System.EventHandler(this.buttonRemoveAll_Click);
            // 
            // buttonUnlockAll
            // 
            this.buttonUnlockAll.Location = new System.Drawing.Point(211, 22);
            this.buttonUnlockAll.Name = "buttonUnlockAll";
            this.buttonUnlockAll.Size = new System.Drawing.Size(178, 24);
            this.buttonUnlockAll.TabIndex = 53;
            this.buttonUnlockAll.Text = "Unlock All";
            this.buttonUnlockAll.UseVisualStyleBackColor = true;
            this.buttonUnlockAll.Click += new System.EventHandler(this.buttonUnlockAll_Click);
            // 
            // lbUnlockAll
            // 
            this.lbUnlockAll.AutoSize = true;
            this.lbUnlockAll.Location = new System.Drawing.Point(9, 6);
            this.lbUnlockAll.Name = "lbUnlockAll";
            this.lbUnlockAll.Size = new System.Drawing.Size(46, 13);
            this.lbUnlockAll.TabIndex = 52;
            this.lbUnlockAll.Text = "Unlocks";
            // 
            // botsUnlocksCheckList
            // 
            this.botsUnlocksCheckList.FormattingEnabled = true;
            this.botsUnlocksCheckList.Location = new System.Drawing.Point(12, 67);
            this.botsUnlocksCheckList.Name = "botsUnlocksCheckList";
            this.botsUnlocksCheckList.Size = new System.Drawing.Size(377, 574);
            this.botsUnlocksCheckList.TabIndex = 51;
            // 
            // RAC4BotsUnlocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 670);
            this.Controls.Add(this.buttonRemoveAll);
            this.Controls.Add(this.buttonUnlockAll);
            this.Controls.Add(this.lbUnlockAll);
            this.Controls.Add(this.botsUnlocksCheckList);
            this.Name = "RAC4BotsUnlocks";
            this.Text = "RAC4BotsUnlocks";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonRemoveAll;
        private System.Windows.Forms.Button buttonUnlockAll;
        private System.Windows.Forms.Label lbUnlockAll;
        private System.Windows.Forms.CheckedListBox botsUnlocksCheckList;
    }
}