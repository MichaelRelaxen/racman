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
            this.checkListWeapons = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonUnlock = new System.Windows.Forms.Button();
            this.checkListGadgets = new System.Windows.Forms.CheckedListBox();
            this.checkListItems = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelGadgets
            // 
            this.labelGadgets.AutoSize = true;
            this.labelGadgets.Location = new System.Drawing.Point(301, 253);
            this.labelGadgets.Name = "labelGadgets";
            this.labelGadgets.Size = new System.Drawing.Size(79, 13);
            this.labelGadgets.TabIndex = 0;
            this.labelGadgets.Text = "Important Items";
            // 
            // checkListWeapons
            // 
            this.checkListWeapons.FormattingEnabled = true;
            this.checkListWeapons.Location = new System.Drawing.Point(12, 29);
            this.checkListWeapons.Name = "checkListWeapons";
            this.checkListWeapons.Size = new System.Drawing.Size(120, 169);
            this.checkListWeapons.TabIndex = 1;
            this.checkListWeapons.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkListWeapons_ItemCheck);
            this.checkListWeapons.SelectedIndexChanged += new System.EventHandler(this.checkListWeapons_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(301, 269);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "(unlock)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(138, 227);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(79, 65);
            this.buttonRemove.TabIndex = 3;
            this.buttonRemove.Text = "Remove All";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonUnlock
            // 
            this.buttonUnlock.Location = new System.Drawing.Point(12, 227);
            this.buttonUnlock.Name = "buttonUnlock";
            this.buttonUnlock.Size = new System.Drawing.Size(120, 65);
            this.buttonUnlock.TabIndex = 4;
            this.buttonUnlock.Text = "Unlock All Weapons, Gadgets and Items";
            this.buttonUnlock.UseVisualStyleBackColor = true;
            this.buttonUnlock.Click += new System.EventHandler(this.buttonUnlock_Click);
            // 
            // checkListGadgets
            // 
            this.checkListGadgets.FormattingEnabled = true;
            this.checkListGadgets.Location = new System.Drawing.Point(138, 29);
            this.checkListGadgets.Name = "checkListGadgets";
            this.checkListGadgets.Size = new System.Drawing.Size(120, 169);
            this.checkListGadgets.TabIndex = 5;
            this.checkListGadgets.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkListGadgets_ItemCheck);
            this.checkListGadgets.SelectedIndexChanged += new System.EventHandler(this.checkListGadgets_SelectedIndexChanged);
            // 
            // checkListItems
            // 
            this.checkListItems.FormattingEnabled = true;
            this.checkListItems.Location = new System.Drawing.Point(264, 29);
            this.checkListItems.Name = "checkListItems";
            this.checkListItems.Size = new System.Drawing.Size(120, 169);
            this.checkListItems.TabIndex = 6;
            this.checkListItems.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkListItems_ItemCheck);
            this.checkListItems.SelectedIndexChanged += new System.EventHandler(this.checkListItems_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Weapons";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Gadgets";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(261, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Items";
            // 
            // RC2Unlocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 304);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkListItems);
            this.Controls.Add(this.checkListGadgets);
            this.Controls.Add(this.buttonUnlock);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkListWeapons);
            this.Controls.Add(this.labelGadgets);
            this.Name = "RC2Unlocks";
            this.Text = "Unlocks";
            this.Load += new System.EventHandler(this.RC2Unlocks_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelGadgets;
        private System.Windows.Forms.CheckedListBox checkListWeapons;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonUnlock;
        private System.Windows.Forms.CheckedListBox checkListGadgets;
        private System.Windows.Forms.CheckedListBox checkListItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}