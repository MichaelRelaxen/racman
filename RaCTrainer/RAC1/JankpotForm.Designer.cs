
namespace racman
{
    partial class JankpotForm
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
            this.lblState = new System.Windows.Forms.Label();
            this.cbJankpotEnabled = new System.Windows.Forms.CheckBox();
            this.btnInfJank = new System.Windows.Forms.Button();
            this.lblMultiplier = new System.Windows.Forms.Label();
            this.lblJankBoltsTitle = new System.Windows.Forms.Label();
            this.lblHelp = new System.Windows.Forms.Label();
            this.lblJankBolts = new System.Windows.Forms.Label();
            this.txtJankBolts = new System.Windows.Forms.TextBox();
            this.lblTimerTitle = new System.Windows.Forms.Label();
            this.lblJankTimer = new System.Windows.Forms.Label();
            this.txtJankTimer = new System.Windows.Forms.TextBox();
            this.lblPlayerBoltsTitle = new System.Windows.Forms.Label();
            this.lblPlayerBolts = new System.Windows.Forms.Label();
            this.txtPlayerBolts = new System.Windows.Forms.TextBox();
            this.lblNGPlus = new System.Windows.Forms.Label();
            this.pbGraph = new System.Windows.Forms.PictureBox();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.ForeColor = System.Drawing.Color.Gray;
            this.lblState.Location = new System.Drawing.Point(12, 9);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(115, 65);
            this.lblState.TabIndex = 0;
            this.lblState.Text = "OFF";
            // 
            // cbJankpotEnabled
            // 
            this.cbJankpotEnabled.AutoSize = true;
            this.cbJankpotEnabled.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbJankpotEnabled.Location = new System.Drawing.Point(400, 35);
            this.cbJankpotEnabled.Name = "cbJankpotEnabled";
            this.cbJankpotEnabled.Size = new System.Drawing.Size(95, 17);
            this.cbJankpotEnabled.TabIndex = 1;
            this.cbJankpotEnabled.Text = "Jankpot Active";
            this.cbJankpotEnabled.UseVisualStyleBackColor = true;
            this.cbJankpotEnabled.CheckedChanged += new System.EventHandler(this.cbJankpotEnabled_CheckedChanged);
            // 
            // btnInfJank
            // 
            this.btnInfJank.Location = new System.Drawing.Point(400, 55);
            this.btnInfJank.Name = "btnInfJank";
            this.btnInfJank.Size = new System.Drawing.Size(75, 23);
            this.btnInfJank.TabIndex = 2;
            this.btnInfJank.Text = "InfJank";
            this.btnInfJank.UseVisualStyleBackColor = true;
            this.btnInfJank.Click += new System.EventHandler(this.btnInfJank_Click);
            // 
            // lblMultiplier
            // 
            this.lblMultiplier.AutoSize = true;
            this.lblMultiplier.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMultiplier.Location = new System.Drawing.Point(12, 80);
            this.lblMultiplier.Name = "lblMultiplier";
            this.lblMultiplier.Size = new System.Drawing.Size(199, 86);
            this.lblMultiplier.TabIndex = 2;
            this.lblMultiplier.Text = "1.00x";
            // 
            // lblJankBoltsTitle
            // 
            this.lblJankBoltsTitle.AutoSize = true;
            this.lblJankBoltsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJankBoltsTitle.Location = new System.Drawing.Point(20, 180);
            this.lblJankBoltsTitle.Name = "lblJankBoltsTitle";
            this.lblJankBoltsTitle.Size = new System.Drawing.Size(99, 21);
            this.lblJankBoltsTitle.TabIndex = 3;
            this.lblJankBoltsTitle.Text = "Jankpot Bolts";
            // 
            // lblJankBolts
            // 
            this.lblJankBolts.AutoSize = true;
            this.lblJankBolts.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJankBolts.Location = new System.Drawing.Point(20, 201);
            this.lblJankBolts.Name = "lblJankBolts";
            this.lblJankBolts.Size = new System.Drawing.Size(27, 32);
            this.lblJankBolts.TabIndex = 4;
            this.lblJankBolts.Text = "0";
            // 
            // txtJankBolts
            // 
            this.txtJankBolts.Location = new System.Drawing.Point(150, 210);
            this.txtJankBolts.Name = "txtJankBolts";
            this.txtJankBolts.Size = new System.Drawing.Size(75, 20);
            this.txtJankBolts.TabIndex = 5;
            this.txtJankBolts.Text = "0";
            this.txtJankBolts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtJankBolts_KeyDown);
            // 
            // lblTimerTitle
            // 
            this.lblTimerTitle.AutoSize = true;
            this.lblTimerTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimerTitle.Location = new System.Drawing.Point(250, 180);
            this.lblTimerTitle.Name = "lblTimerTitle";
            this.lblTimerTitle.Size = new System.Drawing.Size(50, 21);
            this.lblTimerTitle.TabIndex = 6;
            this.lblTimerTitle.Text = "Timer";
            // 
            // lblJankTimer
            // 
            this.lblJankTimer.AutoSize = true;
            this.lblJankTimer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJankTimer.Location = new System.Drawing.Point(250, 201);
            this.lblJankTimer.Name = "lblJankTimer";
            this.lblJankTimer.Size = new System.Drawing.Size(57, 32);
            this.lblJankTimer.TabIndex = 7;
            this.lblJankTimer.Text = "0.0s";
            // 
            // txtJankTimer
            // 
            this.txtJankTimer.Location = new System.Drawing.Point(380, 210);
            this.txtJankTimer.Name = "txtJankTimer";
            this.txtJankTimer.Size = new System.Drawing.Size(75, 20);
            this.txtJankTimer.TabIndex = 8;
            this.txtJankTimer.Text = "0";
            this.txtJankTimer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtJankTimer_KeyDown);
            // 
            // lblPlayerBoltsTitle
            // 
            this.lblPlayerBoltsTitle.AutoSize = true;
            this.lblPlayerBoltsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerBoltsTitle.Location = new System.Drawing.Point(20, 260);
            this.lblPlayerBoltsTitle.Name = "lblPlayerBoltsTitle";
            this.lblPlayerBoltsTitle.Size = new System.Drawing.Size(91, 21);
            this.lblPlayerBoltsTitle.TabIndex = 9;
            this.lblPlayerBoltsTitle.Text = "Player Bolts";
            // 
            // lblPlayerBolts
            // 
            this.lblPlayerBolts.AutoSize = true;
            this.lblPlayerBolts.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerBolts.Location = new System.Drawing.Point(20, 281);
            this.lblPlayerBolts.Name = "lblPlayerBolts";
            this.lblPlayerBolts.Size = new System.Drawing.Size(27, 32);
            this.lblPlayerBolts.TabIndex = 10;
            this.lblPlayerBolts.Text = "0";
            // 
            // txtPlayerBolts
            // 
            this.txtPlayerBolts.Location = new System.Drawing.Point(150, 290);
            this.txtPlayerBolts.Name = "txtPlayerBolts";
            this.txtPlayerBolts.Size = new System.Drawing.Size(75, 20);
            this.txtPlayerBolts.TabIndex = 11;
            this.txtPlayerBolts.Text = "0";
            this.txtPlayerBolts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPlayerBolts_KeyDown);
            // 
            // lblNGPlus
            // 
            this.lblNGPlus.AutoSize = true;
            this.lblNGPlus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNGPlus.Location = new System.Drawing.Point(400, 330);
            this.lblNGPlus.Name = "lblNGPlus";
            this.lblNGPlus.Size = new System.Drawing.Size(46, 15);
            this.lblNGPlus.TabIndex = 12;
            this.lblNGPlus.Text = "NG+: -";
            // 
            // lblHelp
            // 
            this.lblHelp.AutoSize = true;
            this.lblHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHelp.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHelp.Location = new System.Drawing.Point(470, 9);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(18, 21);
            this.lblHelp.TabIndex = 14;
            this.lblHelp.Text = "?";
            this.lblHelp.Click += new System.EventHandler(this.lblHelp_Click);
            // 
            // pbGraph
            // 
            this.pbGraph.BackColor = System.Drawing.Color.White;
            this.pbGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbGraph.Location = new System.Drawing.Point(12, 355);
            this.pbGraph.Name = "pbGraph";
            this.pbGraph.Size = new System.Drawing.Size(476, 150);
            this.pbGraph.TabIndex = 13;
            this.pbGraph.TabStop = false;
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // JankpotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 520);
            this.Controls.Add(this.pbGraph);
            this.Controls.Add(this.lblNGPlus);
            this.Controls.Add(this.txtPlayerBolts);
            this.Controls.Add(this.lblPlayerBolts);
            this.Controls.Add(this.lblPlayerBoltsTitle);
            this.Controls.Add(this.lblHelp);
            this.Controls.Add(this.txtJankTimer);
            this.Controls.Add(this.lblJankTimer);
            this.Controls.Add(this.lblTimerTitle);
            this.Controls.Add(this.txtJankBolts);
            this.Controls.Add(this.lblJankBolts);
            this.Controls.Add(this.lblJankBoltsTitle);
            this.Controls.Add(this.lblMultiplier);
            this.Controls.Add(this.btnInfJank);
            this.Controls.Add(this.cbJankpotEnabled);
            this.Controls.Add(this.lblState);
            this.Name = "JankpotForm";
            this.Text = "Jankpot Monitor";
            this.Load += new System.EventHandler(this.JankpotForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.CheckBox cbJankpotEnabled;
        private System.Windows.Forms.Button btnInfJank;
        private System.Windows.Forms.Label lblMultiplier;
        private System.Windows.Forms.Label lblJankBoltsTitle;
        private System.Windows.Forms.Label lblJankBolts;
        private System.Windows.Forms.TextBox txtJankBolts;
        private System.Windows.Forms.Label lblTimerTitle;
        private System.Windows.Forms.Label lblJankTimer;
        private System.Windows.Forms.TextBox txtJankTimer;
        private System.Windows.Forms.Label lblPlayerBoltsTitle;
        private System.Windows.Forms.Label lblPlayerBolts;
        private System.Windows.Forms.TextBox txtPlayerBolts;
        private System.Windows.Forms.Label lblNGPlus;
        private System.Windows.Forms.PictureBox pbGraph;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.Label lblHelp;
    }
}
