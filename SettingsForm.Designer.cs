namespace CanutePhotoOrg
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtDefaultInputDrive = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDefaultOutputRoot = new System.Windows.Forms.TextBox();
            this.btnBrowseOutputRoot = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFolderTemplate = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Drive:";
            // 
            // txtDefaultInputDrive
            // 
            this.txtDefaultInputDrive.Location = new System.Drawing.Point(215, 26);
            this.txtDefaultInputDrive.Name = "txtDefaultInputDrive";
            this.txtDefaultInputDrive.Size = new System.Drawing.Size(361, 39);
            this.txtDefaultInputDrive.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Output Root:";
            // 
            // txtDefaultOutputRoot
            // 
            this.txtDefaultOutputRoot.Location = new System.Drawing.Point(215, 89);
            this.txtDefaultOutputRoot.Name = "txtDefaultOutputRoot";
            this.txtDefaultOutputRoot.Size = new System.Drawing.Size(361, 39);
            this.txtDefaultOutputRoot.TabIndex = 3;
            // 
            // btnBrowseOutputRoot
            // 
            this.btnBrowseOutputRoot.Location = new System.Drawing.Point(595, 86);
            this.btnBrowseOutputRoot.Name = "btnBrowseOutputRoot";
            this.btnBrowseOutputRoot.Size = new System.Drawing.Size(58, 45);
            this.btnBrowseOutputRoot.TabIndex = 4;
            this.btnBrowseOutputRoot.Text = "...";
            this.btnBrowseOutputRoot.UseVisualStyleBackColor = true;
            this.btnBrowseOutputRoot.Click += new System.EventHandler(this.btnBrowseOutputRoot_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 32);
            this.label3.TabIndex = 5;
            this.label3.Text = "Folder Template:";
            // 
            // txtFolderTemplate
            // 
            this.txtFolderTemplate.Location = new System.Drawing.Point(215, 152);
            this.txtFolderTemplate.Name = "txtFolderTemplate";
            this.txtFolderTemplate.Size = new System.Drawing.Size(438, 39);
            this.txtFolderTemplate.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(447, 220);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 45);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(553, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 45);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 288);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtFolderTemplate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnBrowseOutputRoot);
            this.Controls.Add(this.txtDefaultOutputRoot);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDefaultInputDrive);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDefaultInputDrive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDefaultOutputRoot;
        private System.Windows.Forms.Button btnBrowseOutputRoot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFolderTemplate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
