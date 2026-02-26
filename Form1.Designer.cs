
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.btnLoadSource = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.btnOutput = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProject = new System.Windows.Forms.TextBox();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.progressCopy = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblCurrentFile = new System.Windows.Forms.Label();
            this.lblTiming = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 176);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source:";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(183, 166);
            this.txtSource.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.ReadOnly = true;
            this.txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSource.Size = new System.Drawing.Size(601, 718);
            this.txtSource.TabIndex = 2;
            // 
            // btnLoadSource
            // 
            this.btnLoadSource.Location = new System.Drawing.Point(809, 166);
            this.btnLoadSource.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.btnLoadSource.Name = "btnLoadSource";
            this.btnLoadSource.Size = new System.Drawing.Size(83, 74);
            this.btnLoadSource.TabIndex = 3;
            this.btnLoadSource.Text = "...";
            this.btnLoadSource.UseVisualStyleBackColor = true;
            this.btnLoadSource.Click += new System.EventHandler(this.btnLoadSource_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 957);
            this.label2.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 48);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output:";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(183, 947);
            this.txtOutputPath.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(601, 55);
            this.txtOutputPath.TabIndex = 4;
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(809, 947);
            this.btnOutput.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(83, 74);
            this.btnOutput.TabIndex = 5;
            this.btnOutput.Text = "...";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(677, 1069);
            this.btnGo.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(214, 74);
            this.btnGo.TabIndex = 6;
            this.btnGo.Text = "Generate";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 61);
            this.label3.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 48);
            this.label3.TabIndex = 7;
            this.label3.Text = "Project:";
            // 
            // txtProject
            // 
            this.txtProject.Location = new System.Drawing.Point(183, 51);
            this.txtProject.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.txtProject.Name = "txtProject";
            this.txtProject.Size = new System.Drawing.Size(601, 55);
            this.txtProject.TabIndex = 1;
            this.txtProject.Leave += new System.EventHandler(this.txtProject_Leave);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(677, 51);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(114, 74);
            this.btnSettings.TabIndex = 8;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.Location = new System.Drawing.Point(809, 51);
            this.btnExit.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(83, 74);
            this.btnExit.TabIndex = 9;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(446, 1069);
            this.btnClear.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(214, 74);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(29, 1056);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(127, 48);
            this.lblCount.TabIndex = 10;
            this.lblCount.Text = "Files: 0";
            // 
            // progressCopy
            // 
            this.progressCopy.Location = new System.Drawing.Point(183, 892);
            this.progressCopy.Name = "progressCopy";
            this.progressCopy.Size = new System.Drawing.Size(601, 36);
            this.progressCopy.TabIndex = 11;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(29, 888);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(386, 48);
            this.lblProgress.TabIndex = 12;
            this.lblProgress.Text = "Progress: 0/0 (0%) | Copied: 0 | Skipped: 0 | Failed: 0";
            // 
            // lblCurrentFile
            // 
            this.lblCurrentFile.AutoEllipsis = true;
            this.lblCurrentFile.Location = new System.Drawing.Point(29, 832);
            this.lblCurrentFile.Name = "lblCurrentFile";
            this.lblCurrentFile.Size = new System.Drawing.Size(863, 48);
            this.lblCurrentFile.TabIndex = 13;
            this.lblCurrentFile.Text = "Current: Idle";
            // 
            // lblTiming
            // 
            this.lblTiming.AutoSize = true;
            this.lblTiming.Location = new System.Drawing.Point(29, 1115);
            this.lblTiming.Name = "lblTiming";
            this.lblTiming.Size = new System.Drawing.Size(392, 48);
            this.lblTiming.TabIndex = 14;
            this.lblTiming.Text = "Elapsed: 00:00:00 | ETA: --:--:--";
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnGo;
            this.AutoScaleDimensions = new System.Drawing.SizeF(20F, 48F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(923, 1174);
            this.Controls.Add(this.lblTiming);
            this.Controls.Add(this.lblCurrentFile);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressCopy);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.txtProject);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.txtOutputPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLoadSource);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.Name = "frmMain";
            this.Text = "Canute Photo Organizer";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.ProgressBar progressCopy;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblCurrentFile;
        private System.Windows.Forms.Label lblTiming;
    }
}

