namespace racman
{
    partial class UYAUnlocks
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
            this.checklistItems = new System.Windows.Forms.CheckedListBox();
            this.buttonUnlockAll = new System.Windows.Forms.Button();
            this.buttonRemoveAll = new System.Windows.Forms.Button();
            this.buttonUpgrade = new System.Windows.Forms.Button();
            this.buttonDowngrade = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.levelComboBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonBomb = new System.Windows.Forms.Button();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.gadgetsItems = new System.Windows.Forms.CheckedListBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.infiniteAmmoButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.xBox = new System.Windows.Forms.TextBox();
            this.ammoBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checklistItems
            // 
            this.checklistItems.FormattingEnabled = true;
            this.checklistItems.Location = new System.Drawing.Point(127, 21);
            this.checklistItems.Name = "checklistItems";
            this.checklistItems.Size = new System.Drawing.Size(117, 304);
            this.checklistItems.TabIndex = 0;
            this.checklistItems.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.checklistItems.SelectedIndexChanged += new System.EventHandler(this.checklistItems_SelectedIndexChanged);
            // 
            // buttonUnlockAll
            // 
            this.buttonUnlockAll.Location = new System.Drawing.Point(251, 130);
            this.buttonUnlockAll.Name = "buttonUnlockAll";
            this.buttonUnlockAll.Size = new System.Drawing.Size(100, 24);
            this.buttonUnlockAll.TabIndex = 2;
            this.buttonUnlockAll.Text = "Unlock All";
            this.buttonUnlockAll.UseVisualStyleBackColor = true;
            this.buttonUnlockAll.Click += new System.EventHandler(this.buttonUnlockAll_Click);
            // 
            // buttonRemoveAll
            // 
            this.buttonRemoveAll.Location = new System.Drawing.Point(251, 160);
            this.buttonRemoveAll.Name = "buttonRemoveAll";
            this.buttonRemoveAll.Size = new System.Drawing.Size(100, 24);
            this.buttonRemoveAll.TabIndex = 3;
            this.buttonRemoveAll.Text = "Remove All";
            this.buttonRemoveAll.UseVisualStyleBackColor = true;
            this.buttonRemoveAll.Click += new System.EventHandler(this.buttonRemoveAll_Click);
            // 
            // buttonUpgrade
            // 
            this.buttonUpgrade.Location = new System.Drawing.Point(301, 190);
            this.buttonUpgrade.Name = "buttonUpgrade";
            this.buttonUpgrade.Size = new System.Drawing.Size(50, 26);
            this.buttonUpgrade.TabIndex = 6;
            this.buttonUpgrade.Text = "V8 All*";
            this.buttonUpgrade.UseVisualStyleBackColor = true;
            this.buttonUpgrade.Click += new System.EventHandler(this.buttonUpgrade_Click);
            this.buttonUpgrade.MouseHover += new System.EventHandler(this.buttonUpgrade_MouseHover);
            // 
            // buttonDowngrade
            // 
            this.buttonDowngrade.Location = new System.Drawing.Point(251, 190);
            this.buttonDowngrade.Name = "buttonDowngrade";
            this.buttonDowngrade.Size = new System.Drawing.Size(50, 26);
            this.buttonDowngrade.TabIndex = 7;
            this.buttonDowngrade.Text = "V1 All";
            this.buttonDowngrade.UseVisualStyleBackColor = true;
            this.buttonDowngrade.Click += new System.EventHandler(this.buttonDowngrade_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(285, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Level";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // levelComboBox
            // 
            this.levelComboBox.FormattingEnabled = true;
            this.levelComboBox.Location = new System.Drawing.Point(251, 21);
            this.levelComboBox.Name = "levelComboBox";
            this.levelComboBox.Size = new System.Drawing.Size(100, 21);
            this.levelComboBox.TabIndex = 29;
            this.levelComboBox.SelectedIndexChanged += new System.EventHandler(this.levelCombo_selectedIndex_Changed);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(251, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 35);
            this.button1.TabIndex = 49;
            this.button1.Text = "Setup NG+ Weapons";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonBomb
            // 
            this.buttonBomb.Location = new System.Drawing.Point(326, 220);
            this.buttonBomb.Name = "buttonBomb";
            this.buttonBomb.Size = new System.Drawing.Size(25, 35);
            this.buttonBomb.TabIndex = 50;
            this.buttonBomb.Text = "💣";
            this.buttonBomb.UseVisualStyleBackColor = true;
            this.buttonBomb.Click += new System.EventHandler(this.buttonBomb_Click);
            this.buttonBomb.MouseHover += new System.EventHandler(this.buttonBomb_MouseHover);
            // 
            // gadgetsItems
            // 
            this.gadgetsItems.FormattingEnabled = true;
            this.gadgetsItems.Location = new System.Drawing.Point(4, 21);
            this.gadgetsItems.Name = "gadgetsItems";
            this.gadgetsItems.Size = new System.Drawing.Size(117, 304);
            this.gadgetsItems.TabIndex = 51;
            this.gadgetsItems.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(157, 5);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 13);
            this.label23.TabIndex = 52;
            this.label23.Text = "Weapons";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(25, 4);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(77, 13);
            this.label24.TabIndex = 53;
            this.label24.Text = "Gadgets/Items";
            // 
            // infiniteAmmoButton
            // 
            this.infiniteAmmoButton.Location = new System.Drawing.Point(251, 261);
            this.infiniteAmmoButton.Name = "infiniteAmmoButton";
            this.infiniteAmmoButton.Size = new System.Drawing.Size(99, 64);
            this.infiniteAmmoButton.TabIndex = 54;
            this.infiniteAmmoButton.Text = "GIGA AMMO!!!";
            this.infiniteAmmoButton.UseVisualStyleBackColor = true;
            this.infiniteAmmoButton.Click += new System.EventHandler(this.infiniteAmmoButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(290, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 55;
            this.label1.Text = "XP";
            // 
            // xBox
            // 
            this.xBox.Location = new System.Drawing.Point(250, 61);
            this.xBox.Name = "xBox";
            this.xBox.Size = new System.Drawing.Size(101, 20);
            this.xBox.TabIndex = 56;
            this.xBox.TextChanged += new System.EventHandler(this.xBox_TextChanged);
            // 
            // ammoBox
            // 
            this.ammoBox.Location = new System.Drawing.Point(250, 102);
            this.ammoBox.Name = "ammoBox";
            this.ammoBox.Size = new System.Drawing.Size(101, 20);
            this.ammoBox.TabIndex = 58;
            this.ammoBox.TextChanged += new System.EventHandler(this.ammoBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 57;
            this.label2.Text = "Ammo";
            // 
            // UYAUnlocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 339);
            this.Controls.Add(this.ammoBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.xBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.infiniteAmmoButton);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.gadgetsItems);
            this.Controls.Add(this.buttonBomb);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.levelComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonDowngrade);
            this.Controls.Add(this.buttonUpgrade);
            this.Controls.Add(this.buttonRemoveAll);
            this.Controls.Add(this.buttonUnlockAll);
            this.Controls.Add(this.checklistItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UYAUnlocks";
            this.Text = "Unlocks";
            this.Load += new System.EventHandler(this.UYAUnlocks_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checklistItems;
        private System.Windows.Forms.Button buttonUnlockAll;
        private System.Windows.Forms.Button buttonRemoveAll;
        private System.Windows.Forms.Button buttonUpgrade;
        private System.Windows.Forms.Button buttonDowngrade;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox levelComboBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonBomb;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.CheckedListBox gadgetsItems;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button infiniteAmmoButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox xBox;
        private System.Windows.Forms.TextBox ammoBox;
        private System.Windows.Forms.Label label2;
    }
}