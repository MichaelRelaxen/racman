namespace racman.RAC1
{
    partial class NewUnlocks
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
            this.weaponsListBox = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gadgetsItemsListBox = new System.Windows.Forms.CheckedListBox();
            this.ammoTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.goldStatusCheckBox = new System.Windows.Forms.CheckBox();
            this.ammoForAllWeaponsButton = new System.Windows.Forms.Button();
            this.unlockAllButton = new System.Windows.Forms.Button();
            this.removeAllButton = new System.Windows.Forms.Button();
            this.allGoldWeaponsButton = new System.Windows.Forms.Button();
            this.itemsListBox = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // weaponsListBox
            // 
            this.weaponsListBox.FormattingEnabled = true;
            this.weaponsListBox.Location = new System.Drawing.Point(15, 23);
            this.weaponsListBox.Name = "weaponsListBox";
            this.weaponsListBox.Size = new System.Drawing.Size(143, 244);
            this.weaponsListBox.TabIndex = 0;
            this.weaponsListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listBoxItemCheck);
            this.weaponsListBox.SelectedIndexChanged += new System.EventHandler(this.weaponsListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Weapons";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Gadgets";
            // 
            // gadgetsItemsListBox
            // 
            this.gadgetsItemsListBox.FormattingEnabled = true;
            this.gadgetsItemsListBox.Location = new System.Drawing.Point(164, 24);
            this.gadgetsItemsListBox.Name = "gadgetsItemsListBox";
            this.gadgetsItemsListBox.Size = new System.Drawing.Size(135, 244);
            this.gadgetsItemsListBox.TabIndex = 2;
            this.gadgetsItemsListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listBoxItemCheck);
            // 
            // ammoTextBox
            // 
            this.ammoTextBox.Location = new System.Drawing.Point(15, 286);
            this.ammoTextBox.Name = "ammoTextBox";
            this.ammoTextBox.Size = new System.Drawing.Size(143, 20);
            this.ammoTextBox.TabIndex = 5;
            this.ammoTextBox.TextChanged += new System.EventHandler(this.ammoTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ammo";
            // 
            // goldStatusCheckBox
            // 
            this.goldStatusCheckBox.AutoSize = true;
            this.goldStatusCheckBox.Location = new System.Drawing.Point(15, 312);
            this.goldStatusCheckBox.Name = "goldStatusCheckBox";
            this.goldStatusCheckBox.Size = new System.Drawing.Size(81, 17);
            this.goldStatusCheckBox.TabIndex = 7;
            this.goldStatusCheckBox.Text = "Gold Status";
            this.goldStatusCheckBox.UseVisualStyleBackColor = true;
            this.goldStatusCheckBox.CheckedChanged += new System.EventHandler(this.goldStatusCheckBox_CheckedChanged);
            // 
            // ammoForAllWeaponsButton
            // 
            this.ammoForAllWeaponsButton.Location = new System.Drawing.Point(164, 342);
            this.ammoForAllWeaponsButton.Name = "ammoForAllWeaponsButton";
            this.ammoForAllWeaponsButton.Size = new System.Drawing.Size(276, 24);
            this.ammoForAllWeaponsButton.TabIndex = 10;
            this.ammoForAllWeaponsButton.Text = "Ammo for all weapons";
            this.ammoForAllWeaponsButton.UseVisualStyleBackColor = true;
            this.ammoForAllWeaponsButton.Click += new System.EventHandler(this.AmmoAllClick);
            // 
            // unlockAllButton
            // 
            this.unlockAllButton.Location = new System.Drawing.Point(164, 286);
            this.unlockAllButton.Name = "unlockAllButton";
            this.unlockAllButton.Size = new System.Drawing.Size(135, 24);
            this.unlockAllButton.TabIndex = 11;
            this.unlockAllButton.Text = "Unlock All";
            this.unlockAllButton.UseVisualStyleBackColor = true;
            this.unlockAllButton.Click += new System.EventHandler(this.LockUnlockAllClick);
            // 
            // removeAllButton
            // 
            this.removeAllButton.Location = new System.Drawing.Point(164, 312);
            this.removeAllButton.Name = "removeAllButton";
            this.removeAllButton.Size = new System.Drawing.Size(135, 24);
            this.removeAllButton.TabIndex = 12;
            this.removeAllButton.Text = "Remove All";
            this.removeAllButton.UseVisualStyleBackColor = true;
            this.removeAllButton.Click += new System.EventHandler(this.LockUnlockAllClick);
            // 
            // allGoldWeaponsButton
            // 
            this.allGoldWeaponsButton.Location = new System.Drawing.Point(305, 286);
            this.allGoldWeaponsButton.Name = "allGoldWeaponsButton";
            this.allGoldWeaponsButton.Size = new System.Drawing.Size(135, 24);
            this.allGoldWeaponsButton.TabIndex = 13;
            this.allGoldWeaponsButton.Text = "All Gold Weapons";
            this.allGoldWeaponsButton.UseVisualStyleBackColor = true;
            this.allGoldWeaponsButton.Click += new System.EventHandler(this.GoldAllClick);
            // 
            // itemsListBox
            // 
            this.itemsListBox.FormattingEnabled = true;
            this.itemsListBox.Location = new System.Drawing.Point(305, 24);
            this.itemsListBox.Name = "itemsListBox";
            this.itemsListBox.Size = new System.Drawing.Size(135, 244);
            this.itemsListBox.TabIndex = 14;
            this.itemsListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listBoxItemCheck);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(302, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Items";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(305, 312);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 24);
            this.button1.TabIndex = 16;
            this.button1.Text = "No Gold Weapons";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.GoldAllClick);
            // 
            // NewUnlocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 376);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.itemsListBox);
            this.Controls.Add(this.allGoldWeaponsButton);
            this.Controls.Add(this.removeAllButton);
            this.Controls.Add(this.unlockAllButton);
            this.Controls.Add(this.ammoForAllWeaponsButton);
            this.Controls.Add(this.goldStatusCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ammoTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gadgetsItemsListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.weaponsListBox);
            this.Name = "NewUnlocks";
            this.ShowIcon = false;
            this.Text = "NewUnlocks";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox weaponsListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox gadgetsItemsListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox goldStatusCheckBox;
        private System.Windows.Forms.Button ammoForAllWeaponsButton;
        private System.Windows.Forms.Button unlockAllButton;
        private System.Windows.Forms.Button removeAllButton;
        private System.Windows.Forms.TextBox ammoTextBox;
        private System.Windows.Forms.Button allGoldWeaponsButton;
        private System.Windows.Forms.CheckedListBox itemsListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}