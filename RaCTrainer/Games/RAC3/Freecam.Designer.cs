
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
            this.lockbutton = new System.Windows.Forms.Button();
            this.saveLerpButton = new System.Windows.Forms.Button();
            this.lerpButton = new System.Windows.Forms.Button();
            this.resetLerpButton = new System.Windows.Forms.Button();
            this.cycleLookAtButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lerpTracker = new System.Windows.Forms.TrackBar();
            this.lerpSpeedLabel = new System.Windows.Forms.Label();
            this.savedPoints = new System.Windows.Forms.Label();
            this.upLerpConst = new System.Windows.Forms.Button();
            this.downLerpConst = new System.Windows.Forms.Button();
            this.defaultLerpConst = new System.Windows.Forms.Button();
            this.fovSlider = new System.Windows.Forms.TrackBar();
            this.fovLabel = new System.Windows.Forms.Label();
            this.toggleUiButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mftracker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mstracker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tstracker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tftracker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lerpTracker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fovSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // mftracker
            // 
            this.mftracker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mftracker.Location = new System.Drawing.Point(448, 103);
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
            this.mstracker.Location = new System.Drawing.Point(558, 103);
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
            this.mflabel.Location = new System.Drawing.Point(456, 87);
            this.mflabel.Name = "mflabel";
            this.mflabel.Size = new System.Drawing.Size(68, 13);
            this.mflabel.TabIndex = 2;
            this.mflabel.Text = "Move friction";
            // 
            // mslabel
            // 
            this.mslabel.AutoSize = true;
            this.mslabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.mslabel.Location = new System.Drawing.Point(568, 87);
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
            this.listbox.Size = new System.Drawing.Size(214, 171);
            this.listbox.TabIndex = 5;
            this.listbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listbox_MouseDown);
            // 
            // loadbutton
            // 
            this.loadbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(52)))), ((int)(((byte)(10)))));
            this.loadbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadbutton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.loadbutton.Location = new System.Drawing.Point(119, 231);
            this.loadbutton.Name = "loadbutton";
            this.loadbutton.Size = new System.Drawing.Size(107, 66);
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
            this.savebutton.Location = new System.Drawing.Point(12, 231);
            this.savebutton.Name = "savebutton";
            this.savebutton.Size = new System.Drawing.Size(101, 66);
            this.savebutton.TabIndex = 7;
            this.savebutton.Text = "Save";
            this.savebutton.UseVisualStyleBackColor = false;
            this.savebutton.Click += new System.EventHandler(this.savebutton_Click);
            // 
            // lookingat
            // 
            this.lookingat.AutoSize = true;
            this.lookingat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.lookingat.Location = new System.Drawing.Point(236, 39);
            this.lookingat.Name = "lookingat";
            this.lookingat.Size = new System.Drawing.Size(61, 13);
            this.lookingat.TabIndex = 8;
            this.lookingat.Text = "Looking At:";
            // 
            // controlling
            // 
            this.controlling.AutoSize = true;
            this.controlling.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.controlling.Location = new System.Drawing.Point(236, 12);
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
            this.savebox.Location = new System.Drawing.Point(12, 198);
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
            this.enablebutton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enablebutton.ForeColor = System.Drawing.Color.Black;
            this.enablebutton.Location = new System.Drawing.Point(459, 230);
            this.enablebutton.Name = "enablebutton";
            this.enablebutton.Size = new System.Drawing.Size(203, 67);
            this.enablebutton.TabIndex = 11;
            this.enablebutton.Text = "TOGGLE FREECAM!!";
            this.enablebutton.UseVisualStyleBackColor = false;
            this.enablebutton.Click += new System.EventHandler(this.enablebutton_Click);
            // 
            // tslabel
            // 
            this.tslabel.AutoSize = true;
            this.tslabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.tslabel.Location = new System.Drawing.Point(568, 12);
            this.tslabel.Name = "tslabel";
            this.tslabel.Size = new System.Drawing.Size(61, 13);
            this.tslabel.TabIndex = 15;
            this.tslabel.Text = "Turn speed";
            // 
            // tflabel
            // 
            this.tflabel.AutoSize = true;
            this.tflabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.tflabel.Location = new System.Drawing.Point(456, 12);
            this.tflabel.Name = "tflabel";
            this.tflabel.Size = new System.Drawing.Size(63, 13);
            this.tflabel.TabIndex = 14;
            this.tflabel.Text = "Turn friction";
            // 
            // tstracker
            // 
            this.tstracker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tstracker.Location = new System.Drawing.Point(558, 28);
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
            this.tftracker.Location = new System.Drawing.Point(448, 28);
            this.tftracker.Maximum = 0;
            this.tftracker.Minimum = -20;
            this.tftracker.Name = "tftracker";
            this.tftracker.Size = new System.Drawing.Size(104, 45);
            this.tftracker.TabIndex = 12;
            this.tftracker.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tftracker.ValueChanged += new System.EventHandler(this.tftracker_ValueChanged);
            // 
            // lockbutton
            // 
            this.lockbutton.BackColor = System.Drawing.Color.Maroon;
            this.lockbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lockbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lockbutton.ForeColor = System.Drawing.Color.Black;
            this.lockbutton.Location = new System.Drawing.Point(237, 230);
            this.lockbutton.Name = "lockbutton";
            this.lockbutton.Size = new System.Drawing.Size(210, 30);
            this.lockbutton.TabIndex = 16;
            this.lockbutton.Text = "Lock Cam";
            this.lockbutton.UseVisualStyleBackColor = false;
            this.lockbutton.Click += new System.EventHandler(this.lockbutton_Click);
            // 
            // saveLerpButton
            // 
            this.saveLerpButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(52)))), ((int)(((byte)(10)))));
            this.saveLerpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveLerpButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.saveLerpButton.Location = new System.Drawing.Point(239, 81);
            this.saveLerpButton.Name = "saveLerpButton";
            this.saveLerpButton.Size = new System.Drawing.Size(61, 30);
            this.saveLerpButton.TabIndex = 19;
            this.saveLerpButton.Text = "Save";
            this.saveLerpButton.UseVisualStyleBackColor = false;
            this.saveLerpButton.Click += new System.EventHandler(this.saveLerpButton_Click);
            // 
            // lerpButton
            // 
            this.lerpButton.BackColor = System.Drawing.Color.Maroon;
            this.lerpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lerpButton.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lerpButton.ForeColor = System.Drawing.Color.Black;
            this.lerpButton.Location = new System.Drawing.Point(374, 81);
            this.lerpButton.Name = "lerpButton";
            this.lerpButton.Size = new System.Drawing.Size(73, 30);
            this.lerpButton.TabIndex = 20;
            this.lerpButton.Text = "Toggle";
            this.lerpButton.UseVisualStyleBackColor = false;
            this.lerpButton.Click += new System.EventHandler(this.lerpButton_Click);
            // 
            // resetLerpButton
            // 
            this.resetLerpButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(52)))), ((int)(((byte)(10)))));
            this.resetLerpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetLerpButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.resetLerpButton.Location = new System.Drawing.Point(306, 81);
            this.resetLerpButton.Name = "resetLerpButton";
            this.resetLerpButton.Size = new System.Drawing.Size(62, 30);
            this.resetLerpButton.TabIndex = 21;
            this.resetLerpButton.Text = "Reset";
            this.resetLerpButton.UseVisualStyleBackColor = false;
            this.resetLerpButton.Click += new System.EventHandler(this.resetLerpButton_Click);
            // 
            // cycleLookAtButton
            // 
            this.cycleLookAtButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(52)))), ((int)(((byte)(10)))));
            this.cycleLookAtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cycleLookAtButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.cycleLookAtButton.Location = new System.Drawing.Point(237, 268);
            this.cycleLookAtButton.Name = "cycleLookAtButton";
            this.cycleLookAtButton.Size = new System.Drawing.Size(210, 29);
            this.cycleLookAtButton.TabIndex = 22;
            this.cycleLookAtButton.Text = "Cycle lookat";
            this.cycleLookAtButton.UseVisualStyleBackColor = false;
            this.cycleLookAtButton.Click += new System.EventHandler(this.cycleLookAtButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.label1.Location = new System.Drawing.Point(236, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Lerp settings";
            // 
            // lerpTracker
            // 
            this.lerpTracker.Location = new System.Drawing.Point(237, 153);
            this.lerpTracker.Maximum = 100;
            this.lerpTracker.Minimum = -100;
            this.lerpTracker.Name = "lerpTracker";
            this.lerpTracker.Size = new System.Drawing.Size(210, 45);
            this.lerpTracker.TabIndex = 24;
            this.lerpTracker.Value = 1;
            this.lerpTracker.ValueChanged += new System.EventHandler(this.lerpTracker_ValueChanged);
            // 
            // lerpSpeedLabel
            // 
            this.lerpSpeedLabel.AutoSize = true;
            this.lerpSpeedLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.lerpSpeedLabel.Location = new System.Drawing.Point(237, 138);
            this.lerpSpeedLabel.Name = "lerpSpeedLabel";
            this.lerpSpeedLabel.Size = new System.Drawing.Size(63, 13);
            this.lerpSpeedLabel.TabIndex = 25;
            this.lerpSpeedLabel.Text = "Lerp speed:";
            this.lerpSpeedLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // savedPoints
            // 
            this.savedPoints.AutoSize = true;
            this.savedPoints.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.savedPoints.Location = new System.Drawing.Point(237, 119);
            this.savedPoints.Name = "savedPoints";
            this.savedPoints.Size = new System.Drawing.Size(72, 13);
            this.savedPoints.TabIndex = 26;
            this.savedPoints.Text = "Saved points:";
            // 
            // upLerpConst
            // 
            this.upLerpConst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(52)))), ((int)(((byte)(10)))));
            this.upLerpConst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.upLerpConst.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.upLerpConst.Location = new System.Drawing.Point(374, 184);
            this.upLerpConst.Name = "upLerpConst";
            this.upLerpConst.Size = new System.Drawing.Size(62, 30);
            this.upLerpConst.TabIndex = 27;
            this.upLerpConst.Text = "+";
            this.upLerpConst.UseVisualStyleBackColor = false;
            this.upLerpConst.Click += new System.EventHandler(this.upLerpConst_Click);
            // 
            // downLerpConst
            // 
            this.downLerpConst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(52)))), ((int)(((byte)(10)))));
            this.downLerpConst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downLerpConst.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.downLerpConst.Location = new System.Drawing.Point(249, 184);
            this.downLerpConst.Name = "downLerpConst";
            this.downLerpConst.Size = new System.Drawing.Size(62, 30);
            this.downLerpConst.TabIndex = 28;
            this.downLerpConst.Text = "-";
            this.downLerpConst.UseVisualStyleBackColor = false;
            this.downLerpConst.Click += new System.EventHandler(this.downLerpConst_Click);
            // 
            // defaultLerpConst
            // 
            this.defaultLerpConst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(52)))), ((int)(((byte)(10)))));
            this.defaultLerpConst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.defaultLerpConst.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.defaultLerpConst.Location = new System.Drawing.Point(317, 184);
            this.defaultLerpConst.Name = "defaultLerpConst";
            this.defaultLerpConst.Size = new System.Drawing.Size(51, 30);
            this.defaultLerpConst.TabIndex = 29;
            this.defaultLerpConst.Text = "def";
            this.defaultLerpConst.UseVisualStyleBackColor = false;
            this.defaultLerpConst.Click += new System.EventHandler(this.defaultLerpConst_Click);
            // 
            // fovSlider
            // 
            this.fovSlider.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fovSlider.Location = new System.Drawing.Point(448, 179);
            this.fovSlider.Maximum = 100;
            this.fovSlider.Minimum = 10;
            this.fovSlider.Name = "fovSlider";
            this.fovSlider.Size = new System.Drawing.Size(147, 45);
            this.fovSlider.TabIndex = 30;
            this.fovSlider.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.fovSlider.Value = 100;
            this.fovSlider.ValueChanged += new System.EventHandler(this.fovSlider_ValueChanged);
            // 
            // fovLabel
            // 
            this.fovLabel.AutoSize = true;
            this.fovLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(166)))), ((int)(((byte)(77)))));
            this.fovLabel.Location = new System.Drawing.Point(456, 163);
            this.fovLabel.Name = "fovLabel";
            this.fovLabel.Size = new System.Drawing.Size(58, 13);
            this.fovLabel.TabIndex = 31;
            this.fovLabel.Text = "FOV slider:";
            // 
            // toggleUiButton
            // 
            this.toggleUiButton.BackColor = System.Drawing.Color.Maroon;
            this.toggleUiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toggleUiButton.Font = new System.Drawing.Font("Segoe Print", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleUiButton.ForeColor = System.Drawing.Color.Black;
            this.toggleUiButton.Location = new System.Drawing.Point(601, 163);
            this.toggleUiButton.Name = "toggleUiButton";
            this.toggleUiButton.Size = new System.Drawing.Size(61, 56);
            this.toggleUiButton.TabIndex = 32;
            this.toggleUiButton.Text = "Toggle UI";
            this.toggleUiButton.UseVisualStyleBackColor = false;
            this.toggleUiButton.Click += new System.EventHandler(this.toggleUiButton_Click);
            // 
            // Freecam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(21)))), ((int)(((byte)(4)))));
            this.ClientSize = new System.Drawing.Size(674, 310);
            this.Controls.Add(this.toggleUiButton);
            this.Controls.Add(this.fovLabel);
            this.Controls.Add(this.fovSlider);
            this.Controls.Add(this.defaultLerpConst);
            this.Controls.Add(this.downLerpConst);
            this.Controls.Add(this.upLerpConst);
            this.Controls.Add(this.savedPoints);
            this.Controls.Add(this.lerpSpeedLabel);
            this.Controls.Add(this.lerpTracker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cycleLookAtButton);
            this.Controls.Add(this.resetLerpButton);
            this.Controls.Add(this.lerpButton);
            this.Controls.Add(this.saveLerpButton);
            this.Controls.Add(this.lockbutton);
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
            this.Load += new System.EventHandler(this.Freecam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mftracker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mstracker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tstracker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tftracker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lerpTracker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fovSlider)).EndInit();
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
        private System.Windows.Forms.Button lockbutton;
        private System.Windows.Forms.Button saveLerpButton;
        private System.Windows.Forms.Button lerpButton;
        private System.Windows.Forms.Button resetLerpButton;
        private System.Windows.Forms.Button cycleLookAtButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar lerpTracker;
        private System.Windows.Forms.Label lerpSpeedLabel;
        private System.Windows.Forms.Label savedPoints;
        private System.Windows.Forms.Button upLerpConst;
        private System.Windows.Forms.Button downLerpConst;
        private System.Windows.Forms.Button defaultLerpConst;
        private System.Windows.Forms.TrackBar fovSlider;
        private System.Windows.Forms.Label fovLabel;
        private System.Windows.Forms.Button toggleUiButton;
    }
}