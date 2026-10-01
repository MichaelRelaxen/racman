namespace racman
{
    partial class GhostManagerForm
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
            this.stateLabel = new System.Windows.Forms.Label();
            this.fileList = new System.Windows.Forms.ListView();
            this.ghostColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lengthColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filesPanel = new System.Windows.Forms.GroupBox();
            this.filesFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.refreshButton = new System.Windows.Forms.Button();
            this.downloadButton = new System.Windows.Forms.Button();
            this.uploadButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.libraryButton = new System.Windows.Forms.Button();
            this.ghostPanel = new System.Windows.Forms.GroupBox();
            this.ghostFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.playFileButton = new System.Windows.Forms.Button();
            this.raceRunButton = new System.Windows.Forms.Button();
            this.practiceModeButton = new System.Windows.Forms.Button();
            this.ghostOffButton = new System.Windows.Forms.Button();
            this.restartCheckBox = new System.Windows.Forms.CheckBox();
            this.gamePanel = new System.Windows.Forms.GroupBox();
            this.gameFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.armRunButton = new System.Windows.Forms.Button();
            this.stopRunButton = new System.Windows.Forms.Button();
            this.restartButton = new System.Windows.Forms.Button();
            this.savePracticeButton = new System.Windows.Forms.Button();
            this.savePreviousButton = new System.Windows.Forms.Button();
            this.newAttemptButton = new System.Windows.Forms.Button();
            this.settingsPanel = new System.Windows.Forms.GroupBox();
            this.settingsFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.combosCheckBox = new System.Windows.Forms.CheckBox();
            this.noSplitButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.filesPanel.SuspendLayout();
            this.filesFlow.SuspendLayout();
            this.ghostPanel.SuspendLayout();
            this.ghostFlow.SuspendLayout();
            this.gamePanel.SuspendLayout();
            this.settingsPanel.SuspendLayout();
            this.settingsFlow.SuspendLayout();
            this.gameFlow.SuspendLayout();
            this.SuspendLayout();
            //
            // stateLabel
            //
            this.stateLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.stateLabel.Location = new System.Drawing.Point(0, 0);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Padding = new System.Windows.Forms.Padding(6, 6, 6, 0);
            this.stateLabel.Size = new System.Drawing.Size(544, 44);
            this.stateLabel.TabIndex = 0;
            this.stateLabel.Text = "Reading the ghost mod...";
            //
            // fileList
            //
            this.fileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ghostColumn,
            this.lengthColumn,
            this.fileColumn});
            this.fileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileList.FullRowSelect = true;
            this.fileList.HideSelection = false;
            this.fileList.Location = new System.Drawing.Point(0, 44);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(544, 270);
            this.fileList.TabIndex = 1;
            this.fileList.UseCompatibleStateImageBehavior = false;
            this.fileList.View = System.Windows.Forms.View.Details;
            //
            // ghostColumn
            //
            this.ghostColumn.Text = "Ghost";
            this.ghostColumn.Width = 170;
            //
            // lengthColumn
            //
            this.lengthColumn.Text = "Length";
            this.lengthColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lengthColumn.Width = 80;
            //
            // fileColumn
            //
            this.fileColumn.Text = "File";
            this.fileColumn.Width = 260;
            //
            // filesPanel
            //
            this.filesPanel.Controls.Add(this.filesFlow);
            this.filesPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.filesPanel.Location = new System.Drawing.Point(0, 314);
            this.filesPanel.Name = "filesPanel";
            this.filesPanel.Size = new System.Drawing.Size(544, 56);
            this.filesPanel.TabIndex = 2;
            this.filesPanel.TabStop = false;
            this.filesPanel.Text = "Files on the PS3";
            //
            // filesFlow
            //
            this.filesFlow.Controls.Add(this.refreshButton);
            this.filesFlow.Controls.Add(this.downloadButton);
            this.filesFlow.Controls.Add(this.uploadButton);
            this.filesFlow.Controls.Add(this.deleteButton);
            this.filesFlow.Controls.Add(this.libraryButton);
            this.filesFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filesFlow.Location = new System.Drawing.Point(3, 16);
            this.filesFlow.Name = "filesFlow";
            this.filesFlow.Size = new System.Drawing.Size(538, 37);
            this.filesFlow.TabIndex = 0;
            //
            // refreshButton
            //
            this.refreshButton.AutoSize = true;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.TabIndex = 0;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            //
            // downloadButton
            //
            this.downloadButton.AutoSize = true;
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.TabIndex = 1;
            this.downloadButton.Text = "Download (selected or all)";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            //
            // uploadButton
            //
            this.uploadButton.AutoSize = true;
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.TabIndex = 2;
            this.uploadButton.Text = "Upload...";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            //
            // deleteButton
            //
            this.deleteButton.AutoSize = true;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            //
            // libraryButton
            //
            this.libraryButton.AutoSize = true;
            this.libraryButton.Name = "libraryButton";
            this.libraryButton.TabIndex = 4;
            this.libraryButton.Text = "Open library folder";
            this.libraryButton.UseVisualStyleBackColor = true;
            this.libraryButton.Click += new System.EventHandler(this.libraryButton_Click);
            //
            // ghostPanel
            //
            this.ghostPanel.Controls.Add(this.ghostFlow);
            this.ghostPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ghostPanel.Location = new System.Drawing.Point(0, 370);
            this.ghostPanel.Name = "ghostPanel";
            this.ghostPanel.Size = new System.Drawing.Size(544, 56);
            this.ghostPanel.TabIndex = 3;
            this.ghostPanel.TabStop = false;
            this.ghostPanel.Text = "Ghost to play";
            //
            // ghostFlow
            //
            this.ghostFlow.Controls.Add(this.playFileButton);
            this.ghostFlow.Controls.Add(this.raceRunButton);
            this.ghostFlow.Controls.Add(this.practiceModeButton);
            this.ghostFlow.Controls.Add(this.ghostOffButton);
            this.ghostFlow.Controls.Add(this.restartCheckBox);
            this.ghostFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ghostFlow.Location = new System.Drawing.Point(3, 16);
            this.ghostFlow.Name = "ghostFlow";
            this.ghostFlow.Size = new System.Drawing.Size(538, 37);
            this.ghostFlow.TabIndex = 0;
            //
            // playFileButton
            //
            this.playFileButton.AutoSize = true;
            this.playFileButton.Name = "playFileButton";
            this.playFileButton.TabIndex = 0;
            this.playFileButton.Text = "Play against file";
            this.playFileButton.UseVisualStyleBackColor = true;
            this.playFileButton.Click += new System.EventHandler(this.playFileButton_Click);
            //
            // raceRunButton
            //
            this.raceRunButton.AutoSize = true;
            this.raceRunButton.Name = "raceRunButton";
            this.raceRunButton.TabIndex = 1;
            this.raceRunButton.Text = "Race whole run";
            this.raceRunButton.UseVisualStyleBackColor = true;
            this.raceRunButton.Click += new System.EventHandler(this.raceRunButton_Click);
            //
            // practiceModeButton
            //
            this.practiceModeButton.AutoSize = true;
            this.practiceModeButton.Name = "practiceModeButton";
            this.practiceModeButton.TabIndex = 2;
            this.practiceModeButton.Text = "Practice ghosts";
            this.practiceModeButton.UseVisualStyleBackColor = true;
            this.practiceModeButton.Click += new System.EventHandler(this.practiceModeButton_Click);
            //
            // ghostOffButton
            //
            this.ghostOffButton.AutoSize = true;
            this.ghostOffButton.Name = "ghostOffButton";
            this.ghostOffButton.TabIndex = 3;
            this.ghostOffButton.Text = "Off";
            this.ghostOffButton.UseVisualStyleBackColor = true;
            this.ghostOffButton.Click += new System.EventHandler(this.ghostOffButton_Click);
            //
            // restartCheckBox
            //
            this.restartCheckBox.AutoSize = true;
            this.restartCheckBox.Checked = true;
            this.restartCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.restartCheckBox.Margin = new System.Windows.Forms.Padding(6, 7, 3, 3);
            this.restartCheckBox.Name = "restartCheckBox";
            this.restartCheckBox.TabIndex = 4;
            this.restartCheckBox.Text = "Restart level to play now";
            this.restartCheckBox.UseVisualStyleBackColor = true;
            //
            // gamePanel
            //
            this.gamePanel.Controls.Add(this.gameFlow);
            this.gamePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gamePanel.Location = new System.Drawing.Point(0, 397);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(544, 85);
            this.gamePanel.TabIndex = 4;
            this.gamePanel.TabStop = false;
            this.gamePanel.Text = "In game";
            //
            // gameFlow
            //
            this.gameFlow.Controls.Add(this.armRunButton);
            this.gameFlow.Controls.Add(this.stopRunButton);
            this.gameFlow.Controls.Add(this.restartButton);
            this.gameFlow.Controls.Add(this.savePracticeButton);
            this.gameFlow.Controls.Add(this.savePreviousButton);
            this.gameFlow.Controls.Add(this.newAttemptButton);
            this.gameFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameFlow.Location = new System.Drawing.Point(3, 16);
            this.gameFlow.Name = "gameFlow";
            this.gameFlow.Size = new System.Drawing.Size(538, 66);
            this.gameFlow.TabIndex = 0;
            //
            // armRunButton
            //
            this.armRunButton.AutoSize = true;
            this.armRunButton.Name = "armRunButton";
            this.armRunButton.TabIndex = 0;
            this.armRunButton.Text = "Arm run";
            this.armRunButton.UseVisualStyleBackColor = true;
            this.armRunButton.Click += new System.EventHandler(this.armRunButton_Click);
            //
            // stopRunButton
            //
            this.stopRunButton.AutoSize = true;
            this.stopRunButton.Name = "stopRunButton";
            this.stopRunButton.TabIndex = 1;
            this.stopRunButton.Text = "Stop / cancel run";
            this.stopRunButton.UseVisualStyleBackColor = true;
            this.stopRunButton.Click += new System.EventHandler(this.stopRunButton_Click);
            //
            // restartButton
            //
            this.restartButton.AutoSize = true;
            this.restartButton.Name = "restartButton";
            this.restartButton.TabIndex = 2;
            this.restartButton.Text = "Restart level";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            //
            // savePracticeButton
            //
            this.savePracticeButton.AutoSize = true;
            this.savePracticeButton.Name = "savePracticeButton";
            this.savePracticeButton.TabIndex = 3;
            this.savePracticeButton.Text = "Save attempt as practice ghost";
            this.savePracticeButton.UseVisualStyleBackColor = true;
            this.savePracticeButton.Click += new System.EventHandler(this.savePracticeButton_Click);
            //
            // savePreviousButton
            //
            this.savePreviousButton.AutoSize = true;
            this.savePreviousButton.Name = "savePreviousButton";
            this.savePreviousButton.TabIndex = 4;
            this.savePreviousButton.Text = "Save previous attempt";
            this.savePreviousButton.UseVisualStyleBackColor = true;
            this.savePreviousButton.Click += new System.EventHandler(this.savePreviousButton_Click);
            //
            // newAttemptButton
            //
            this.newAttemptButton.AutoSize = true;
            this.newAttemptButton.Name = "newAttemptButton";
            this.newAttemptButton.TabIndex = 5;
            this.newAttemptButton.Text = "New attempt on next death/reload";
            this.newAttemptButton.UseVisualStyleBackColor = true;
            this.newAttemptButton.Click += new System.EventHandler(this.newAttemptButton_Click);
            //
            // settingsPanel
            //
            this.settingsPanel.Controls.Add(this.settingsFlow);
            this.settingsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.settingsPanel.Location = new System.Drawing.Point(0, 482);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(544, 56);
            this.settingsPanel.TabIndex = 6;
            this.settingsPanel.TabStop = false;
            this.settingsPanel.Text = "Settings (refresh to send to PS3)";
            //
            // settingsFlow
            //
            this.settingsFlow.Controls.Add(this.combosCheckBox);
            this.settingsFlow.Controls.Add(this.noSplitButton);
            this.settingsFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsFlow.Location = new System.Drawing.Point(3, 16);
            this.settingsFlow.Name = "settingsFlow";
            this.settingsFlow.Size = new System.Drawing.Size(538, 37);
            this.settingsFlow.TabIndex = 0;
            //
            // combosCheckBox
            //
            this.combosCheckBox.AutoSize = true;
            this.combosCheckBox.Checked = true;
            this.combosCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.combosCheckBox.Margin = new System.Windows.Forms.Padding(6, 7, 3, 3);
            this.combosCheckBox.Name = "combosCheckBox";
            this.combosCheckBox.TabIndex = 0;
            this.combosCheckBox.Text = "Controller combos (L3+R3 ...)";
            this.combosCheckBox.UseVisualStyleBackColor = true;
            //
            // noSplitButton
            //
            this.noSplitButton.AutoSize = true;
            this.noSplitButton.Name = "noSplitButton";
            this.noSplitButton.TabIndex = 1;
            this.noSplitButton.Text = "Keep recording through deaths / reloads...";
            this.noSplitButton.UseVisualStyleBackColor = true;
            this.noSplitButton.Click += new System.EventHandler(this.noSplitButton_Click);
            //
            // statusLabel
            //
            this.statusLabel.AutoEllipsis = true;
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusLabel.Location = new System.Drawing.Point(0, 538);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.statusLabel.Size = new System.Drawing.Size(544, 22);
            this.statusLabel.TabIndex = 5;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // GhostManagerForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 560);
            this.Controls.Add(this.fileList);
            this.Controls.Add(this.filesPanel);
            this.Controls.Add(this.ghostPanel);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.settingsPanel);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.statusLabel);
            this.MinimumSize = new System.Drawing.Size(636, 400);
            this.Name = "GhostManagerForm";
            this.Text = "Ghosts";
            this.Load += new System.EventHandler(this.GhostManagerForm_Load);
            this.filesPanel.ResumeLayout(false);
            this.filesFlow.ResumeLayout(false);
            this.filesFlow.PerformLayout();
            this.ghostPanel.ResumeLayout(false);
            this.ghostFlow.ResumeLayout(false);
            this.ghostFlow.PerformLayout();
            this.gamePanel.ResumeLayout(false);
            this.gameFlow.ResumeLayout(false);
            this.gameFlow.PerformLayout();
            this.settingsPanel.ResumeLayout(false);
            this.settingsFlow.ResumeLayout(false);
            this.settingsFlow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.ListView fileList;
        private System.Windows.Forms.ColumnHeader ghostColumn;
        private System.Windows.Forms.ColumnHeader lengthColumn;
        private System.Windows.Forms.ColumnHeader fileColumn;
        private System.Windows.Forms.GroupBox filesPanel;
        private System.Windows.Forms.FlowLayoutPanel filesFlow;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button libraryButton;
        private System.Windows.Forms.GroupBox ghostPanel;
        private System.Windows.Forms.FlowLayoutPanel ghostFlow;
        private System.Windows.Forms.Button playFileButton;
        private System.Windows.Forms.Button raceRunButton;
        private System.Windows.Forms.Button practiceModeButton;
        private System.Windows.Forms.Button ghostOffButton;
        private System.Windows.Forms.CheckBox restartCheckBox;
        private System.Windows.Forms.GroupBox gamePanel;
        private System.Windows.Forms.FlowLayoutPanel gameFlow;
        private System.Windows.Forms.Button armRunButton;
        private System.Windows.Forms.Button stopRunButton;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.Button savePracticeButton;
        private System.Windows.Forms.Button savePreviousButton;
        private System.Windows.Forms.Button newAttemptButton;
        private System.Windows.Forms.GroupBox settingsPanel;
        private System.Windows.Forms.FlowLayoutPanel settingsFlow;
        private System.Windows.Forms.CheckBox combosCheckBox;
        private System.Windows.Forms.Button noSplitButton;
        private System.Windows.Forms.Label statusLabel;
    }
}
