
namespace racman
{
    partial class HotkeysMenu
    {
        /// <summary>loadPositionHotkeyTextBox
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
            this.loadPositionHotkeyTextBox = new System.Windows.Forms.TextBox();
            this.savePositionLabelText = new System.Windows.Forms.Label();
            this.loadPositionLabelText = new System.Windows.Forms.Label();
            this.savePositionHotkeyTextBox = new System.Windows.Forms.TextBox();
            this.killYourselfLabelText = new System.Windows.Forms.Label();
            this.killYourselfHotkeyTextBox = new System.Windows.Forms.TextBox();
            this.coords1LabelText = new System.Windows.Forms.Label();
            this.coords1HotkeyTextBox = new System.Windows.Forms.TextBox();
            this.coords2LabelText = new System.Windows.Forms.Label();
            this.coords2HotkeyTextBox = new System.Windows.Forms.TextBox();
            this.saveHotkeysButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loadPositionHotkeyTextBox
            // 
            this.loadPositionHotkeyTextBox.Location = new System.Drawing.Point(104, 50);
            this.loadPositionHotkeyTextBox.Name = "loadPositionHotkeyTextBox";
            this.loadPositionHotkeyTextBox.ReadOnly = true;
            this.loadPositionHotkeyTextBox.Size = new System.Drawing.Size(100, 20);
            this.loadPositionHotkeyTextBox.TabIndex = 7;
            // 
            // savePositionLabelText
            // 
            this.savePositionLabelText.AutoSize = true;
            this.savePositionLabelText.Location = new System.Drawing.Point(13, 25);
            this.savePositionLabelText.Name = "savePositionLabelText";
            this.savePositionLabelText.Size = new System.Drawing.Size(75, 13);
            this.savePositionLabelText.TabIndex = 0;
            this.savePositionLabelText.Text = "Save Position:";
            // 
            // loadPositionLabelText
            // 
            this.loadPositionLabelText.AutoSize = true;
            this.loadPositionLabelText.Location = new System.Drawing.Point(13, 55);
            this.loadPositionLabelText.Name = "loadPositionLabelText";
            this.loadPositionLabelText.Size = new System.Drawing.Size(74, 13);
            this.loadPositionLabelText.TabIndex = 1;
            this.loadPositionLabelText.Text = "Load Position:";
            // 
            // savePositionHotkeyTextBox
            // 
            this.savePositionHotkeyTextBox.Location = new System.Drawing.Point(104, 20);
            this.savePositionHotkeyTextBox.Name = "savePositionHotkeyTextBox";
            this.savePositionHotkeyTextBox.ReadOnly = true;
            this.savePositionHotkeyTextBox.Size = new System.Drawing.Size(100, 20);
            this.savePositionHotkeyTextBox.TabIndex = 6;
            // 
            // killYourselfLabelText
            // 
            this.killYourselfLabelText.AutoSize = true;
            this.killYourselfLabelText.Location = new System.Drawing.Point(13, 85);
            this.killYourselfLabelText.Name = "killYourselfLabelText";
            this.killYourselfLabelText.Size = new System.Drawing.Size(64, 13);
            this.killYourselfLabelText.TabIndex = 2;
            this.killYourselfLabelText.Text = "Kill Yourself:";
            // 
            // killYourselfHotkeyTextBox
            // 
            this.killYourselfHotkeyTextBox.Location = new System.Drawing.Point(104, 82);
            this.killYourselfHotkeyTextBox.Name = "killYourselfHotkeyTextBox";
            this.killYourselfHotkeyTextBox.ReadOnly = true;
            this.killYourselfHotkeyTextBox.Size = new System.Drawing.Size(100, 20);
            this.killYourselfHotkeyTextBox.TabIndex = 8;
            // 
            // coords1LabelText
            // 
            this.coords1LabelText.AutoSize = true;
            this.coords1LabelText.Location = new System.Drawing.Point(13, 115);
            this.coords1LabelText.Name = "coords1LabelText";
            this.coords1LabelText.Size = new System.Drawing.Size(74, 13);
            this.coords1LabelText.TabIndex = 3;
            this.coords1LabelText.Text = "Pos Scroll Up:";
            // 
            // coords1HotkeyTextBox
            // 
            this.coords1HotkeyTextBox.Location = new System.Drawing.Point(104, 112);
            this.coords1HotkeyTextBox.Name = "coords1HotkeyTextBox";
            this.coords1HotkeyTextBox.ReadOnly = true;
            this.coords1HotkeyTextBox.Size = new System.Drawing.Size(100, 20);
            this.coords1HotkeyTextBox.TabIndex = 9;
            // 
            // coords2LabelText
            // 
            this.coords2LabelText.AutoSize = true;
            this.coords2LabelText.Location = new System.Drawing.Point(13, 146);
            this.coords2LabelText.Name = "coords2LabelText";
            this.coords2LabelText.Size = new System.Drawing.Size(88, 13);
            this.coords2LabelText.TabIndex = 4;
            this.coords2LabelText.Text = "Pos Scroll Down:";
            // 
            // coords2HotkeyTextBox
            // 
            this.coords2HotkeyTextBox.Location = new System.Drawing.Point(104, 143);
            this.coords2HotkeyTextBox.Name = "coords2HotkeyTextBox";
            this.coords2HotkeyTextBox.ReadOnly = true;
            this.coords2HotkeyTextBox.Size = new System.Drawing.Size(100, 20);
            this.coords2HotkeyTextBox.TabIndex = 10;
            // 
            // saveHotkeysButton
            // 
            this.saveHotkeysButton.Location = new System.Drawing.Point(104, 169);
            this.saveHotkeysButton.Name = "saveHotkeysButton";
            this.saveHotkeysButton.Size = new System.Drawing.Size(100, 23);
            this.saveHotkeysButton.TabIndex = 12;
            this.saveHotkeysButton.Text = "Save";
            this.saveHotkeysButton.UseVisualStyleBackColor = true;
            this.saveHotkeysButton.Click += new System.EventHandler(this.SaveHotkeysButton_Click);
            // 
            // HotkeysMenu
            // 
            this.ClientSize = new System.Drawing.Size(225, 208);
            this.Controls.Add(this.saveHotkeysButton);
            this.Controls.Add(this.coords2LabelText);
            this.Controls.Add(this.coords2HotkeyTextBox);
            this.Controls.Add(this.coords1LabelText);
            this.Controls.Add(this.coords1HotkeyTextBox);
            this.Controls.Add(this.killYourselfLabelText);
            this.Controls.Add(this.killYourselfHotkeyTextBox);
            this.Controls.Add(this.loadPositionLabelText);
            this.Controls.Add(this.savePositionHotkeyTextBox);
            this.Controls.Add(this.savePositionLabelText);
            this.Controls.Add(this.loadPositionHotkeyTextBox);
            this.Name = "HotkeysMenu";
            this.Text = "RaC 1 Hotkeys";
            this.Load += new System.EventHandler(this.HotkeysMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.TextBox loadPositionHotkeyTextBox;
        private System.Windows.Forms.Label savePositionLabelText;
        private System.Windows.Forms.Label loadPositionLabelText;
        private System.Windows.Forms.TextBox savePositionHotkeyTextBox;
        private System.Windows.Forms.Label killYourselfLabelText;
        private System.Windows.Forms.TextBox killYourselfHotkeyTextBox;
        private System.Windows.Forms.Label coords1LabelText;
        private System.Windows.Forms.TextBox coords1HotkeyTextBox;
        private System.Windows.Forms.Label coords2LabelText;
        private System.Windows.Forms.TextBox coords2HotkeyTextBox;
        private System.Windows.Forms.Button saveHotkeysButton;
    }
}

