
namespace racman.RAC3
{
    partial class Freecam
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
            this.mftracker = new System.Windows.Forms.TrackBar();
            this.mstracker = new System.Windows.Forms.TrackBar();
            this.mflabel = new System.Windows.Forms.Label();
            this.mslabel = new System.Windows.Forms.Label();
            this.listbox = new System.Windows.Forms.ListBox();
            this.loadbutton = new System.Windows.Forms.Button();
            this.savebutton = new System.Windows.Forms.Button();
            this.lookingat = new System.Windows.Forms.Label();
            this.controlling = new System.Windows.Forms.Label();
            this.savebox = new System.Windows.Forms.TextBox();
            this.enablebutton = new System.Windows.Forms.Button();
            this.tslabel = new System.Windows.Forms.Label();
            this.tflabel = new System.Windows.Forms.Label();
            this.tstracker = new System.Windows.Forms.TrackBar();
            this.tftracker = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.mftracker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mstracker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tstracker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tftracker)).BeginInit();
            this.SuspendLayout();
            // 
            // mftracker
            // 
            this.mftracker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mftracker.Location = new System.Drawing.Point(228, 147);
            this.mftracker.Maximum = 0;
            this.mftracker.Minimum = -20;
            this.mftracker.Name = "mftracker";
            this.mftracker.Size = new System.Drawing.Size(104, 45);
            this.mftracker.TabIndex = 0;
            this.mftracker.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.mftracker.ValueChanged += new System.EventHandler(this.mftracker_ValueChanged);
            // 
            // mstracker
            // 
            this.mstracker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mstracker.Location = new System.Drawing.Point(338, 147);
            this.mstracker.Maximum = 40;
            this.mstracker.Minimum = 1;
            this.mstracker.Name = "mstracker";
            this.mstracker.Size = new System.Drawing.Size(104, 45);
            this.mstracker.TabIndex = 1;
            this.mstracker.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.mstracker.Value = 1;
            this.mstracker.ValueChanged += new System.EventHandler(this.mstracker_ValueChanged);
            // 
            // mflabel
            // 
            this.mflabel.AutoSize = true;
            this.mflabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.mflabel.Location = new System.Drawing.Point(236, 131);
            this.mflabel.Name = "mflabel";
            this.mflabel.Size = new System.Drawing.Size(68, 13);
            this.mflabel.TabIndex = 2;
            this.mflabel.Text = "Move friction";
            // 
            // mslabel
            // 
            this.mslabel.AutoSize = true;
            this.mslabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.mslabel.Location = new System.Drawing.Point(348, 131);
            this.mslabel.Name = "mslabel";
            this.mslabel.Size = new System.Drawing.Size(66, 13);
            this.mslabel.TabIndex = 3;
            this.mslabel.Text = "Move speed";
            // 
            // listbox
            // 
            this.listbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(52)))), ((int)(((byte)(10)))));
            this.listbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.listbox.FormattingEnabled = true;
            this.listbox.Location = new System.Drawing.Point(12, 12);
            this.listbox.Name = "listbox";
            this.listbox.Size = new System.Drawing.Size(214, 314);
            this.listbox.TabIndex = 5;
            this.listbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listbox_MouseDown);
            // 
            // loadbutton
            // 
            this.loadbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(52)))), ((int)(((byte)(10)))));
            this.loadbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadbutton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.loadbutton.Location = new System.Drawing.Point(235, 302);
            this.loadbutton.Name = "loadbutton";
            this.loadbutton.Size = new System.Drawing.Size(97, 56);
            this.loadbutton.TabIndex = 6;
            this.loadbutton.Text = "Load";
            this.loadbutton.UseVisualStyleBackColor = false;
            this.loadbutton.Click += new System.EventHandler(this.loadbutton_Click);
            // 
            // savebutton
            // 
            this.savebutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(52)))), ((int)(((byte)(10)))));
            this.savebutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.savebutton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.savebutton.Location = new System.Drawing.Point(338, 302);
            this.savebutton.Name = "savebutton";
            this.savebutton.Size = new System.Drawing.Size(97, 56);
            this.savebutton.TabIndex = 7;
            this.savebutton.Text = "Save";
            this.savebutton.UseVisualStyleBackColor = false;
            this.savebutton.Click += new System.EventHandler(this.savebutton_Click);
            // 
            // lookingat
            // 
            this.lookingat.AutoSize = true;
            this.lookingat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.lookingat.Location = new System.Drawing.Point(241, 37);
            this.lookingat.Name = "lookingat";
            this.lookingat.Size = new System.Drawing.Size(61, 13);
            this.lookingat.TabIndex = 8;
            this.lookingat.Text = "Looking At:";
            // 
            // controlling
            // 
            this.controlling.AutoSize = true;
            this.controlling.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.controlling.Location = new System.Drawing.Point(241, 10);
            this.controlling.Name = "controlling";
            this.controlling.Size = new System.Drawing.Size(59, 13);
            this.controlling.TabIndex = 9;
            this.controlling.Text = "Controlling:";
            // 
            // savebox
            // 
            this.savebox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(52)))), ((int)(((byte)(10)))));
            this.savebox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.savebox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.savebox.Location = new System.Drawing.Point(12, 340);
            this.savebox.Name = "savebox";
            this.savebox.Size = new System.Drawing.Size(214, 20);
            this.savebox.TabIndex = 10;
            this.savebox.Text = "Enter position name here¯\\_( ͡° ͜ʖ ͡°)_/¯";
            this.savebox.Click += new System.EventHandler(this.savebox_Click);
            // 
            // enablebutton
            // 
            this.enablebutton.BackColor = System.Drawing.Color.Red;
            this.enablebutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.enablebutton.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enablebutton.ForeColor = System.Drawing.Color.Black;
            this.enablebutton.Location = new System.Drawing.Point(235, 198);
            this.enablebutton.Name = "enablebutton";
            this.enablebutton.Size = new System.Drawing.Size(200, 98);
            this.enablebutton.TabIndex = 11;
            this.enablebutton.Text = "Disable/Enable";
            this.enablebutton.UseVisualStyleBackColor = false;
            this.enablebutton.Click += new System.EventHandler(this.enablebutton_Click);
            // 
            // tslabel
            // 
            this.tslabel.AutoSize = true;
            this.tslabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.tslabel.Location = new System.Drawing.Point(348, 67);
            this.tslabel.Name = "tslabel";
            this.tslabel.Size = new System.Drawing.Size(61, 13);
            this.tslabel.TabIndex = 15;
            this.tslabel.Text = "Turn speed";
            // 
            // tflabel
            // 
            this.tflabel.AutoSize = true;
            this.tflabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.tflabel.Location = new System.Drawing.Point(236, 67);
            this.tflabel.Name = "tflabel";
            this.tflabel.Size = new System.Drawing.Size(63, 13);
            this.tflabel.TabIndex = 14;
            this.tflabel.Text = "Turn friction";
            // 
            // tstracker
            // 
            this.tstracker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tstracker.Location = new System.Drawing.Point(338, 83);
            this.tstracker.Maximum = 40;
            this.tstracker.Minimum = 1;
            this.tstracker.Name = "tstracker";
            this.tstracker.Size = new System.Drawing.Size(104, 45);
            this.tstracker.TabIndex = 13;
            this.tstracker.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tstracker.Value = 1;
            this.tstracker.ValueChanged += new System.EventHandler(this.tstracker_ValueChanged);
            // 
            // tftracker
            // 
            this.tftracker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tftracker.Location = new System.Drawing.Point(228, 83);
            this.tftracker.Maximum = 0;
            this.tftracker.Minimum = -20;
            this.tftracker.Name = "tftracker";
            this.tftracker.Size = new System.Drawing.Size(104, 45);
            this.tftracker.TabIndex = 12;
            this.tftracker.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tftracker.ValueChanged += new System.EventHandler(this.tftracker_ValueChanged);
            // 
            // Freecam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(21)))), ((int)(((byte)(4)))));
            this.ClientSize = new System.Drawing.Size(449, 372);
            this.Controls.Add(this.tslabel);
            this.Controls.Add(this.tflabel);
            this.Controls.Add(this.tstracker);
            this.Controls.Add(this.tftracker);
            this.Controls.Add(this.enablebutton);
            this.Controls.Add(this.savebox);
            this.Controls.Add(this.controlling);
            this.Controls.Add(this.lookingat);
            this.Controls.Add(this.savebutton);
            this.Controls.Add(this.loadbutton);
            this.Controls.Add(this.listbox);
            this.Controls.Add(this.mslabel);
            this.Controls.Add(this.mflabel);
            this.Controls.Add(this.mstracker);
            this.Controls.Add(this.mftracker);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Freecam";
            this.Text = "Freecam";
            ((System.ComponentModel.ISupportInitialize)(this.mftracker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mstracker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tstracker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tftracker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar mftracker;
        private System.Windows.Forms.TrackBar mstracker;
        private System.Windows.Forms.Label mflabel;
        private System.Windows.Forms.Label mslabel;
        private System.Windows.Forms.ListBox listbox;
        private System.Windows.Forms.Button loadbutton;
        private System.Windows.Forms.Button savebutton;
        private System.Windows.Forms.Label lookingat;
        private System.Windows.Forms.Label controlling;
        private System.Windows.Forms.TextBox savebox;
        private System.Windows.Forms.Button enablebutton;
        private System.Windows.Forms.Label tslabel;
        private System.Windows.Forms.Label tflabel;
        private System.Windows.Forms.TrackBar tstracker;
        private System.Windows.Forms.TrackBar tftracker;
    }
}