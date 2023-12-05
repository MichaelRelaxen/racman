namespace racman
{
    partial class RC2Unlocks
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
            this.labelGadgets = new System.Windows.Forms.Label();
            this.checklistItems = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonUnlock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelGadgets
            // 
            this.labelGadgets.AutoSize = true;
            this.labelGadgets.Location = new System.Drawing.Point(138, 12);
            this.labelGadgets.Name = "labelGadgets";
            this.labelGadgets.Size = new System.Drawing.Size(79, 13);
            this.labelGadgets.TabIndex = 0;
            this.labelGadgets.Text = "Important Items";
            // 
            // checklistItems
            // 
            this.checklistItems.FormattingEnabled = true;
            this.checklistItems.Location = new System.Drawing.Point(12, 12);
            this.checklistItems.Name = "checklistItems";
            this.checklistItems.Size = new System.Drawing.Size(120, 169);
            this.checklistItems.TabIndex = 1;
            this.checklistItems.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checklistItems_ItemCheck);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(138, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "(unlock)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(138, 187);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(79, 65);
            this.buttonRemove.TabIndex = 3;
            this.buttonRemove.Text = "Remove All";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonUnlock
            // 
            this.buttonUnlock.Location = new System.Drawing.Point(12, 187);
            this.buttonUnlock.Name = "buttonUnlock";
            this.buttonUnlock.Size = new System.Drawing.Size(120, 65);
            this.buttonUnlock.TabIndex = 4;
            this.buttonUnlock.Text = "Unlock All Weapons, Gadgets and Items";
            this.buttonUnlock.UseVisualStyleBackColor = true;
            this.buttonUnlock.Click += new System.EventHandler(this.buttonUnlock_Click);
            // 
            // RC2Unlocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 264);
            this.Controls.Add(this.buttonUnlock);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checklistItems);
            this.Controls.Add(this.labelGadgets);
            this.Name = "RC2Unlocks";
            this.Text = "Unlocks";
            this.Load += new System.EventHandler(this.RC2Unlocks_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelGadgets;
        private System.Windows.Forms.CheckedListBox checklistItems;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonUnlock;
    }
}