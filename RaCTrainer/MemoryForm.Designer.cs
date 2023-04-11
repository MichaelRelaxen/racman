namespace racman
{
    partial class MemoryForm
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
            this.registerAddressTextBox = new System.Windows.Forms.TextBox();
            this.registerAddressTypeCombo = new System.Windows.Forms.ComboBox();
            this.addMemoryWatchButton = new System.Windows.Forms.Button();
            this.watchedMemoryAddressesListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // registerAddressTextBox
            // 
            this.registerAddressTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerAddressTextBox.Location = new System.Drawing.Point(12, 418);
            this.registerAddressTextBox.Name = "registerAddressTextBox";
            this.registerAddressTextBox.Size = new System.Drawing.Size(100, 20);
            this.registerAddressTextBox.TabIndex = 0;
            // 
            // registerAddressTypeCombo
            // 
            this.registerAddressTypeCombo.FormattingEnabled = true;
            this.registerAddressTypeCombo.Items.AddRange(new object[] {
            "Int32",
            "Int64",
            "Int16",
            "Byte",
            "Float",
            "Pointer"});
            this.registerAddressTypeCombo.Location = new System.Drawing.Point(118, 418);
            this.registerAddressTypeCombo.Name = "registerAddressTypeCombo";
            this.registerAddressTypeCombo.Size = new System.Drawing.Size(121, 21);
            this.registerAddressTypeCombo.TabIndex = 1;
            this.registerAddressTypeCombo.Text = "Int32";
            // 
            // addMemoryWatchButton
            // 
            this.addMemoryWatchButton.Location = new System.Drawing.Point(249, 418);
            this.addMemoryWatchButton.Name = "addMemoryWatchButton";
            this.addMemoryWatchButton.Size = new System.Drawing.Size(75, 23);
            this.addMemoryWatchButton.TabIndex = 2;
            this.addMemoryWatchButton.Text = "Add";
            this.addMemoryWatchButton.UseVisualStyleBackColor = true;
            this.addMemoryWatchButton.Click += new System.EventHandler(this.addMemoryWatchButton_Click);
            // 
            // watchedMemoryAddressesListView
            // 
            this.watchedMemoryAddressesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.AddressColumnHeader,
            this.ValueColumnHeader});
            this.watchedMemoryAddressesListView.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.watchedMemoryAddressesListView.GridLines = true;
            this.watchedMemoryAddressesListView.HideSelection = false;
            this.watchedMemoryAddressesListView.LabelEdit = true;
            this.watchedMemoryAddressesListView.Location = new System.Drawing.Point(12, 12);
            this.watchedMemoryAddressesListView.Name = "watchedMemoryAddressesListView";
            this.watchedMemoryAddressesListView.Size = new System.Drawing.Size(312, 400);
            this.watchedMemoryAddressesListView.TabIndex = 3;
            this.watchedMemoryAddressesListView.UseCompatibleStateImageBehavior = false;
            this.watchedMemoryAddressesListView.View = System.Windows.Forms.View.Details;
            this.watchedMemoryAddressesListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.watchedMemoryAddressesListView_MouseClick);
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 105;
            // 
            // AddressColumnHeader
            // 
            this.AddressColumnHeader.Text = "Address";
            this.AddressColumnHeader.Width = 107;
            // 
            // ValueColumnHeader
            // 
            this.ValueColumnHeader.Text = "Value";
            this.ValueColumnHeader.Width = 99;
            // 
            // MemoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 451);
            this.Controls.Add(this.watchedMemoryAddressesListView);
            this.Controls.Add(this.addMemoryWatchButton);
            this.Controls.Add(this.registerAddressTypeCombo);
            this.Controls.Add(this.registerAddressTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MemoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Memory utilities";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MemoryForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox registerAddressTextBox;
        private System.Windows.Forms.ComboBox registerAddressTypeCombo;
        private System.Windows.Forms.Button addMemoryWatchButton;
        private System.Windows.Forms.ListView watchedMemoryAddressesListView;
        private System.Windows.Forms.ColumnHeader AddressColumnHeader;
        private System.Windows.Forms.ColumnHeader ValueColumnHeader;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
    }
}