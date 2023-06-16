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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemoryForm));
            this.registerAddressTextBox = new System.Windows.Forms.TextBox();
            this.registerAddressTypeCombo = new System.Windows.Forms.ComboBox();
            this.addMemoryWatchButton = new System.Windows.Forms.Button();
            this.watchedMemoryAddressesListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.selectedMobyComboBox = new System.Windows.Forms.ComboBox();
            this.refreshMobysButton = new System.Windows.Forms.Button();
            this.mobyInspectorListView = new System.Windows.Forms.ListView();
            this.mobyAddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mobyPropNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mobyValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // registerAddressTextBox
            // 
            this.registerAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.registerAddressTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerAddressTextBox.Location = new System.Drawing.Point(12, 467);
            this.registerAddressTextBox.MaximumSize = new System.Drawing.Size(106, 20);
            this.registerAddressTextBox.MinimumSize = new System.Drawing.Size(106, 20);
            this.registerAddressTextBox.Name = "registerAddressTextBox";
            this.registerAddressTextBox.Size = new System.Drawing.Size(106, 20);
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
            this.registerAddressTypeCombo.Location = new System.Drawing.Point(122, 467);
            this.registerAddressTypeCombo.MaximumSize = new System.Drawing.Size(121, 0);
            this.registerAddressTypeCombo.MinimumSize = new System.Drawing.Size(121, 0);
            this.registerAddressTypeCombo.Name = "registerAddressTypeCombo";
            this.registerAddressTypeCombo.Size = new System.Drawing.Size(121, 21);
            this.registerAddressTypeCombo.TabIndex = 1;
            this.registerAddressTypeCombo.Text = "Int32";
            // 
            // addMemoryWatchButton
            // 
            this.addMemoryWatchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addMemoryWatchButton.Location = new System.Drawing.Point(247, 466);
            this.addMemoryWatchButton.MaximumSize = new System.Drawing.Size(75, 23);
            this.addMemoryWatchButton.MinimumSize = new System.Drawing.Size(75, 23);
            this.addMemoryWatchButton.Name = "addMemoryWatchButton";
            this.addMemoryWatchButton.Size = new System.Drawing.Size(75, 23);
            this.addMemoryWatchButton.TabIndex = 2;
            this.addMemoryWatchButton.Text = "Add";
            this.addMemoryWatchButton.UseVisualStyleBackColor = true;
            this.addMemoryWatchButton.Click += new System.EventHandler(this.addMemoryWatchButton_Click);
            // 
            // watchedMemoryAddressesListView
            // 
            this.watchedMemoryAddressesListView.AllowColumnReorder = true;
            this.watchedMemoryAddressesListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.watchedMemoryAddressesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.AddressColumnHeader,
            this.ValueColumnHeader});
            this.watchedMemoryAddressesListView.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.watchedMemoryAddressesListView.GridLines = true;
            this.watchedMemoryAddressesListView.HideSelection = false;
            this.watchedMemoryAddressesListView.LabelEdit = true;
            this.watchedMemoryAddressesListView.Location = new System.Drawing.Point(12, 12);
            this.watchedMemoryAddressesListView.MaximumSize = new System.Drawing.Size(308, 448);
            this.watchedMemoryAddressesListView.MinimumSize = new System.Drawing.Size(308, 448);
            this.watchedMemoryAddressesListView.Name = "watchedMemoryAddressesListView";
            this.watchedMemoryAddressesListView.Size = new System.Drawing.Size(308, 448);
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
            // selectedMobyComboBox
            // 
            this.selectedMobyComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedMobyComboBox.FormattingEnabled = true;
            this.selectedMobyComboBox.Location = new System.Drawing.Point(330, 12);
            this.selectedMobyComboBox.Name = "selectedMobyComboBox";
            this.selectedMobyComboBox.Size = new System.Drawing.Size(285, 21);
            this.selectedMobyComboBox.TabIndex = 4;
            this.selectedMobyComboBox.SelectedIndexChanged += new System.EventHandler(this.selectedMobyComboBox_SelectedIndexChanged);
            // 
            // refreshMobysButton
            // 
            this.refreshMobysButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshMobysButton.Location = new System.Drawing.Point(621, 11);
            this.refreshMobysButton.Name = "refreshMobysButton";
            this.refreshMobysButton.Size = new System.Drawing.Size(125, 23);
            this.refreshMobysButton.TabIndex = 5;
            this.refreshMobysButton.Text = "Refresh (pauses game)";
            this.refreshMobysButton.UseVisualStyleBackColor = true;
            this.refreshMobysButton.Click += new System.EventHandler(this.refreshMobysButton_Click);
            // 
            // mobyInspectorListView
            // 
            this.mobyInspectorListView.AllowColumnReorder = true;
            this.mobyInspectorListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mobyInspectorListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.mobyAddressHeader,
            this.mobyPropNameHeader,
            this.mobyValueHeader});
            this.mobyInspectorListView.GridLines = true;
            this.mobyInspectorListView.HideSelection = false;
            this.mobyInspectorListView.Location = new System.Drawing.Point(330, 39);
            this.mobyInspectorListView.Name = "mobyInspectorListView";
            this.mobyInspectorListView.Size = new System.Drawing.Size(416, 448);
            this.mobyInspectorListView.TabIndex = 6;
            this.mobyInspectorListView.UseCompatibleStateImageBehavior = false;
            this.mobyInspectorListView.View = System.Windows.Forms.View.Details;
            // 
            // mobyAddressHeader
            // 
            this.mobyAddressHeader.Text = "Address";
            this.mobyAddressHeader.Width = 81;
            // 
            // mobyPropNameHeader
            // 
            this.mobyPropNameHeader.Text = "Property";
            this.mobyPropNameHeader.Width = 124;
            // 
            // mobyValueHeader
            // 
            this.mobyValueHeader.Text = "Value";
            this.mobyValueHeader.Width = 188;
            // 
            // MemoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 498);
            this.Controls.Add(this.mobyInspectorListView);
            this.Controls.Add(this.refreshMobysButton);
            this.Controls.Add(this.selectedMobyComboBox);
            this.Controls.Add(this.watchedMemoryAddressesListView);
            this.Controls.Add(this.addMemoryWatchButton);
            this.Controls.Add(this.registerAddressTypeCombo);
            this.Controls.Add(this.registerAddressTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
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
        private System.Windows.Forms.ComboBox selectedMobyComboBox;
        private System.Windows.Forms.Button refreshMobysButton;
        private System.Windows.Forms.ListView mobyInspectorListView;
        private System.Windows.Forms.ColumnHeader mobyAddressHeader;
        private System.Windows.Forms.ColumnHeader mobyPropNameHeader;
        private System.Windows.Forms.ColumnHeader mobyValueHeader;
    }
}