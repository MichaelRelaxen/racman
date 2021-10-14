
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
            this.modsCheckedListBox.Size = new System.Drawing.Size(776, 424);
            this.modsCheckedListBox.TabIndex = 0;
            this.modsCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.modsCheckedListBox_ItemCheck);
            // 
            // ModLoaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.modsCheckedListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModLoaderForm";
            this.Text = "Mod Loader";
            this.Load += new System.EventHandler(this.ModLoaderForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox modsCheckedListBox;
    }
}