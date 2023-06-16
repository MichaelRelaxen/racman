namespace racman
{
    partial class AutosplitterConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutosplitterConfigForm));
            this.routeSelectionListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grid = new System.Windows.Forms.DataGridView();
            this.SourceColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DestColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.applyChangesButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.openFromFileButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertAboveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertBelowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // routeSelectionListBox
            // 
            this.routeSelectionListBox.FormattingEnabled = true;
            this.routeSelectionListBox.Location = new System.Drawing.Point(12, 25);
            this.routeSelectionListBox.Name = "routeSelectionListBox";
            this.routeSelectionListBox.Size = new System.Drawing.Size(121, 160);
            this.routeSelectionListBox.TabIndex = 0;
            this.routeSelectionListBox.SelectedIndexChanged += new System.EventHandler(this.routeSelectionListBox_SelectedIndexChanged);
            this.routeSelectionListBox.DoubleClick += new System.EventHandler(this.routeSelectionListBox_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Split Route:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grid);
            this.groupBox1.Controls.Add(this.applyChangesButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(139, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 209);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // grid
            // 
            this.grid.AllowUserToResizeColumns = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SourceColumn,
            this.DestColumn});
            this.grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grid.Location = new System.Drawing.Point(7, 16);
            this.grid.Name = "grid";
            this.grid.RowHeadersVisible = false;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(276, 159);
            this.grid.TabIndex = 3;
            this.grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellClick);
            this.grid.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_CellMouseDown);
            // 
            // SourceColumn
            // 
            this.SourceColumn.HeaderText = "Source";
            this.SourceColumn.Items.AddRange(new object[] {
            "Veldin",
            "Florana",
            "Phoenix",
            "Marcadia",
            "Daxx",
            "Phoenix Rescue",
            "Annihilation Nation",
            "Aquatos",
            "Tyhrranosis",
            "Zeldrin",
            "Obani Gemini",
            "Blackwater",
            "Holostar (Ratchet)",
            "Koros",
            "Unknown 0xF",
            "Metropolis",
            "Crash Site",
            "Aridia",
            "Hideout",
            "Launch Site",
            "Obani Draco",
            "Command Center",
            "Holostar (Clank)",
            "Insomniac Museum",
            "Unknown 0x19",
            "Metropolis (Rangers)",
            "Aquatos (Clank)",
            "Aquatos (Sewers)",
            "Tyhrranosis (Rangers)",
            "Vid-Comic 6",
            "Vid-Comic 1",
            "Vid-Comic 4",
            "Vid-Comic 2",
            "Vid-Comic 3",
            "Vid-Comic 5",
            "Vid-Comic 1 Special Edition"});
            this.SourceColumn.Name = "SourceColumn";
            this.SourceColumn.Width = 128;
            // 
            // DestColumn
            // 
            this.DestColumn.HeaderText = "Destination";
            this.DestColumn.Items.AddRange(new object[] {
            "Veldin",
            "Florana",
            "Phoenix",
            "Marcadia",
            "Daxx",
            "Phoenix Rescue",
            "Annihilation Nation",
            "Aquatos",
            "Tyhrranosis",
            "Zeldrin",
            "Obani Gemini",
            "Blackwater",
            "Holostar (Ratchet)",
            "Koros",
            "Unknown 0xF",
            "Metropolis",
            "Crash Site",
            "Aridia",
            "Hideout",
            "Launch Site",
            "Obani Draco",
            "Command Center",
            "Holostar (Clank)",
            "Insomniac Museum",
            "Unknown 0x19",
            "Metropolis (Rangers)",
            "Aquatos (Clank)",
            "Aquatos (Sewers)",
            "Tyhrranosis (Rangers)",
            "Vid-Comic 6",
            "Vid-Comic 1",
            "Vid-Comic 4",
            "Vid-Comic 2",
            "Vid-Comic 3",
            "Vid-Comic 5",
            "Vid-Comic 1 Special Edition"});
            this.DestColumn.Name = "DestColumn";
            this.DestColumn.Width = 128;
            // 
            // applyChangesButton
            // 
            this.applyChangesButton.Location = new System.Drawing.Point(207, 182);
            this.applyChangesButton.Name = "applyChangesButton";
            this.applyChangesButton.Size = new System.Drawing.Size(77, 24);
            this.applyChangesButton.TabIndex = 2;
            this.applyChangesButton.Text = "Apply";
            this.applyChangesButton.UseVisualStyleBackColor = true;
            this.applyChangesButton.Click += new System.EventHandler(this.applyChangesButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(47, 183);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(154, 20);
            this.textBox1.TabIndex = 0;
            // 
            // addButton
            // 
            this.addButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.Location = new System.Drawing.Point(12, 188);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(30, 30);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "+";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeButton.Location = new System.Drawing.Point(48, 188);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(30, 30);
            this.removeButton.TabIndex = 4;
            this.removeButton.Text = "-";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // openFromFileButton
            // 
            this.openFromFileButton.Location = new System.Drawing.Point(84, 188);
            this.openFromFileButton.Name = "openFromFileButton";
            this.openFromFileButton.Size = new System.Drawing.Size(49, 30);
            this.openFromFileButton.TabIndex = 5;
            this.openFromFileButton.Text = "Open...";
            this.openFromFileButton.UseVisualStyleBackColor = true;
            this.openFromFileButton.Click += new System.EventHandler(this.openFromFileButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "UYA Split Routes|*.usr";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertAboveToolStripMenuItem,
            this.insertBelowToolStripMenuItem,
            this.deleteRowToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(141, 70);
            // 
            // insertAboveToolStripMenuItem
            // 
            this.insertAboveToolStripMenuItem.Name = "insertAboveToolStripMenuItem";
            this.insertAboveToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.insertAboveToolStripMenuItem.Text = "Insert Above";
            this.insertAboveToolStripMenuItem.Click += new System.EventHandler(this.insertAboveToolStripMenuItem_Click);
            // 
            // insertBelowToolStripMenuItem
            // 
            this.insertBelowToolStripMenuItem.Name = "insertBelowToolStripMenuItem";
            this.insertBelowToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.insertBelowToolStripMenuItem.Text = "Insert Below";
            this.insertBelowToolStripMenuItem.Click += new System.EventHandler(this.insertBelowToolStripMenuItem_Click);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.deleteRowToolStripMenuItem.Text = "Delete Row";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // AutosplitterConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(435, 221);
            this.Controls.Add(this.openFromFileButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.routeSelectionListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AutosplitterConfigForm";
            this.Text = "Split Route Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox routeSelectionListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button applyChangesButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button openFromFileButton;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewComboBoxColumn SourceColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn DestColumn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem insertAboveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertBelowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
    }
}