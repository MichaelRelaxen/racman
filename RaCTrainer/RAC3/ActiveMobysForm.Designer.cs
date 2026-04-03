namespace racman.RAC3
{
    partial class ActiveMobysForm
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
            this.table = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // table
            // 
            this.table.AllowColumnReorder = true;
            this.table.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.table.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.AddressColumnHeader,
            this.ValueColumnHeader});
            this.table.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.table.GridLines = true;
            this.table.HideSelection = false;
            this.table.LabelEdit = true;
            this.table.Location = new System.Drawing.Point(12, 12);
            this.table.MaximumSize = new System.Drawing.Size(900, 900);
            this.table.MinimumSize = new System.Drawing.Size(308, 448);
            this.table.Name = "table";
            this.table.Size = new System.Drawing.Size(511, 799);
            this.table.TabIndex = 4;
            this.table.UseCompatibleStateImageBehavior = false;
            this.table.View = System.Windows.Forms.View.Details;
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Active";
            this.nameColumnHeader.Width = 180;
            // 
            // AddressColumnHeader
            // 
            this.AddressColumnHeader.Text = "Backup";
            this.AddressColumnHeader.Width = 180;
            // 
            // ValueColumnHeader
            // 
            this.ValueColumnHeader.Text = "Tag";
            this.ValueColumnHeader.Width = 40;
            // 
            // ActiveMobysForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 854);
            this.Controls.Add(this.table);
            this.Name = "ActiveMobysForm";
            this.Text = "ActiveMobysForm";
            this.Load += new System.EventHandler(this.ActiveMobysForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView table;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader AddressColumnHeader;
        private System.Windows.Forms.ColumnHeader ValueColumnHeader;
    }
}