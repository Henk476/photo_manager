
namespace CanutePhotoOrg
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            label1 = new System.Windows.Forms.Label();
            txtSource = new System.Windows.Forms.TextBox();
            btnLoadSource = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            txtOutputPath = new System.Windows.Forms.TextBox();
            btnOutput = new System.Windows.Forms.Button();
            btnGo = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            txtProject = new System.Windows.Forms.TextBox();
            btnSettings = new System.Windows.Forms.Button();
            btnClear = new System.Windows.Forms.Button();
            lblCount = new System.Windows.Forms.Label();
            progressCopy = new System.Windows.Forms.ProgressBar();
            lblProgress = new System.Windows.Forms.Label();
            lblCurrentFile = new System.Windows.Forms.Label();
            lblTiming = new System.Windows.Forms.Label();
            toolTipMain = new System.Windows.Forms.ToolTip(components);
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 55);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(46, 15);
            label1.TabIndex = 0;
            label1.Text = "Source:";
            // 
            // txtSource
            // 
            txtSource.Location = new System.Drawing.Point(64, 52);
            txtSource.Multiline = true;
            txtSource.Name = "txtSource";
            txtSource.ReadOnly = true;
            txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtSource.Size = new System.Drawing.Size(213, 227);
            txtSource.TabIndex = 2;
            // 
            // btnLoadSource
            // 
            btnLoadSource.Location = new System.Drawing.Point(283, 52);
            btnLoadSource.Name = "btnLoadSource";
            btnLoadSource.Size = new System.Drawing.Size(29, 23);
            btnLoadSource.TabIndex = 3;
            btnLoadSource.Text = "...";
            toolTipMain.SetToolTip(btnLoadSource, "Select source files (RAW/video). Multi-select supported; opens your preferred card/folder automatically.");
            btnLoadSource.UseVisualStyleBackColor = true;
            btnLoadSource.Click += btnLoadSource_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(10, 299);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(48, 15);
            label2.TabIndex = 3;
            label2.Text = "Output:";
            // 
            // txtOutputPath
            // 
            txtOutputPath.Location = new System.Drawing.Point(64, 296);
            txtOutputPath.Name = "txtOutputPath";
            txtOutputPath.Size = new System.Drawing.Size(213, 23);
            txtOutputPath.TabIndex = 4;
            // 
            // btnOutput
            // 
            btnOutput.Location = new System.Drawing.Point(283, 296);
            btnOutput.Name = "btnOutput";
            btnOutput.Size = new System.Drawing.Size(29, 23);
            btnOutput.TabIndex = 5;
            btnOutput.Text = "...";
            btnOutput.UseVisualStyleBackColor = true;
            btnOutput.Click += btnOutput_Click;
            // 
            // btnGo
            // 
            btnGo.Location = new System.Drawing.Point(237, 334);
            btnGo.Name = "btnGo";
            btnGo.Size = new System.Drawing.Size(75, 23);
            btnGo.TabIndex = 6;
            btnGo.Text = "Generate";
            btnGo.UseVisualStyleBackColor = true;
            btnGo.Click += btnGo_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(11, 19);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(47, 15);
            label3.TabIndex = 7;
            label3.Text = "Project:";
            // 
            // txtProject
            // 
            txtProject.Location = new System.Drawing.Point(64, 16);
            txtProject.Name = "txtProject";
            txtProject.Size = new System.Drawing.Size(175, 23);
            txtProject.TabIndex = 1;
            txtProject.Leave += txtProject_Leave;
            // 
            // btnSettings
            // 
            btnSettings.Location = new System.Drawing.Point(245, 16);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new System.Drawing.Size(67, 23);
            btnSettings.TabIndex = 8;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new System.Drawing.Point(156, 334);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(75, 23);
            btnClear.TabIndex = 9;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // lblCount
            // 
            lblCount.AutoSize = true;
            lblCount.Location = new System.Drawing.Point(10, 330);
            lblCount.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            lblCount.Name = "lblCount";
            lblCount.Size = new System.Drawing.Size(42, 15);
            lblCount.TabIndex = 10;
            lblCount.Text = "Files: 0";
            // 
            // progressCopy
            // 
            progressCopy.Location = new System.Drawing.Point(12, 376);
            progressCopy.Margin = new System.Windows.Forms.Padding(1);
            progressCopy.Name = "progressCopy";
            progressCopy.Size = new System.Drawing.Size(300, 11);
            progressCopy.TabIndex = 11;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new System.Drawing.Point(12, 412);
            lblProgress.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new System.Drawing.Size(276, 15);
            lblProgress.TabIndex = 12;
            lblProgress.Text = "Progress: 0/0 (0%) | Copied: 0 | Skipped: 0 | Failed: 0";
            // 
            // lblCurrentFile
            // 
            lblCurrentFile.AutoEllipsis = true;
            lblCurrentFile.Location = new System.Drawing.Point(10, 360);
            lblCurrentFile.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            lblCurrentFile.Name = "lblCurrentFile";
            lblCurrentFile.Size = new System.Drawing.Size(302, 15);
            lblCurrentFile.TabIndex = 13;
            lblCurrentFile.Text = "Current: Idle";
            // 
            // lblTiming
            // 
            lblTiming.AutoSize = true;
            lblTiming.Location = new System.Drawing.Point(12, 388);
            lblTiming.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            lblTiming.Name = "lblTiming";
            lblTiming.Size = new System.Drawing.Size(166, 15);
            lblTiming.TabIndex = 14;
            lblTiming.Text = "Elapsed: 00:00:00 | ETA: --:--:--";
            // 
            // frmMain
            // 
            AcceptButton = btnGo;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            ClientSize = new System.Drawing.Size(323, 438);
            Controls.Add(lblTiming);
            Controls.Add(lblCurrentFile);
            Controls.Add(lblProgress);
            Controls.Add(progressCopy);
            Controls.Add(lblCount);
            Controls.Add(btnClear);
            Controls.Add(btnSettings);
            Controls.Add(txtProject);
            Controls.Add(label3);
            Controls.Add(btnGo);
            Controls.Add(btnOutput);
            Controls.Add(txtOutputPath);
            Controls.Add(label2);
            Controls.Add(btnLoadSource);
            Controls.Add(txtSource);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "frmMain";
            Text = "Canute Photo Organizer";
            Load += frmMain_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Button btnLoadSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProject;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.ProgressBar progressCopy;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblCurrentFile;
        private System.Windows.Forms.Label lblTiming;
        private System.Windows.Forms.ToolTip toolTipMain;
    }
}

