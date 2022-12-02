
namespace racman
{
    partial class ModLoaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModLoaderForm));
            this.modsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.addZipButton = new System.Windows.Forms.Button();
            this.descriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.openConsoleButton = new System.Windows.Forms.Button();
            this.modNameLabel = new System.Windows.Forms.Label();
            this.authorNameLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.buttonScripting = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // modsCheckedListBox
            // 
            this.modsCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modsCheckedListBox.FormattingEnabled = true;
            this.modsCheckedListBox.Location = new System.Drawing.Point(12, 12);
            this.modsCheckedListBox.Name = "modsCheckedListBox";
            this.modsCheckedListBox.Size = new System.Drawing.Size(603, 259);
            this.modsCheckedListBox.TabIndex = 0;
            this.modsCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.modsCheckedListBox_ItemCheck);
            this.modsCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.modsCheckedListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 309);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Author: ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 345);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Link: ";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 327);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Version: ";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 291);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Name: ";
            // 
            // addZipButton
            // 
            this.addZipButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addZipButton.Location = new System.Drawing.Point(539, 341);
            this.addZipButton.Name = "addZipButton";
            this.addZipButton.Size = new System.Drawing.Size(75, 23);
            this.addZipButton.TabIndex = 5;
            this.addZipButton.Text = "Add ZIP...";
            this.addZipButton.UseVisualStyleBackColor = true;
            this.addZipButton.Click += new System.EventHandler(this.addZipButton_Click);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.BackColor = System.Drawing.SystemColors.Menu;
            this.descriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptionTextBox.Location = new System.Drawing.Point(200, 286);
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ReadOnly = true;
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(333, 78);
            this.descriptionTextBox.TabIndex = 6;
            this.descriptionTextBox.Text = "";
            // 
            // openConsoleButton
            // 
            this.openConsoleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openConsoleButton.Location = new System.Drawing.Point(539, 312);
            this.openConsoleButton.Name = "openConsoleButton";
            this.openConsoleButton.Size = new System.Drawing.Size(75, 23);
            this.openConsoleButton.TabIndex = 7;
            this.openConsoleButton.Text = "Console";
            this.openConsoleButton.UseVisualStyleBackColor = true;
            this.openConsoleButton.Click += new System.EventHandler(this.openConsoleButton_Click);
            // 
            // modNameLabel
            // 
            this.modNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.modNameLabel.AutoSize = true;
            this.modNameLabel.Location = new System.Drawing.Point(59, 291);
            this.modNameLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.modNameLabel.Name = "modNameLabel";
            this.modNameLabel.Size = new System.Drawing.Size(27, 13);
            this.modNameLabel.TabIndex = 8;
            this.modNameLabel.Text = "N/A";
            // 
            // authorNameLabel
            // 
            this.authorNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.authorNameLabel.AutoSize = true;
            this.authorNameLabel.Location = new System.Drawing.Point(59, 309);
            this.authorNameLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.authorNameLabel.Name = "authorNameLabel";
            this.authorNameLabel.Size = new System.Drawing.Size(27, 13);
            this.authorNameLabel.TabIndex = 9;
            this.authorNameLabel.Text = "N/A";
            // 
            // versionLabel
            // 
            this.versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(59, 327);
            this.versionLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(27, 13);
            this.versionLabel.TabIndex = 10;
            this.versionLabel.Text = "N/A";
            // 
            // linkLabel
            // 
            this.linkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel.AutoSize = true;
            this.linkLabel.Location = new System.Drawing.Point(59, 345);
            this.linkLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(27, 13);
            this.linkLabel.TabIndex = 11;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "N/A";
            // 
            // buttonScripting
            // 
            this.buttonScripting.Location = new System.Drawing.Point(539, 283);
            this.buttonScripting.Name = "buttonScripting";
            this.buttonScripting.Size = new System.Drawing.Size(75, 23);
            this.buttonScripting.TabIndex = 12;
            this.buttonScripting.Text = "Scripting";
            this.buttonScripting.UseVisualStyleBackColor = true;
            this.buttonScripting.Click += new System.EventHandler(this.buttonScripting_Click);
            // 
            // ModLoaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 376);
            this.Controls.Add(this.buttonScripting);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.authorNameLabel);
            this.Controls.Add(this.modNameLabel);
            this.Controls.Add(this.openConsoleButton);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.addZipButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.modsCheckedListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(643, 323);
            this.Name = "ModLoaderForm";
            this.Text = "Mod Loader";
            this.Activated += new System.EventHandler(this.ModLoaderForm_Activated);
            this.Load += new System.EventHandler(this.ModLoaderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox modsCheckedListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button addZipButton;
        private System.Windows.Forms.RichTextBox descriptionTextBox;
        private System.Windows.Forms.Button openConsoleButton;
        private System.Windows.Forms.Label modNameLabel;
        private System.Windows.Forms.Label authorNameLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.Button buttonScripting;
    }
}