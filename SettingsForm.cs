using System;
using System.IO;
using System.Windows.Forms;
using CanutePhotoOrg.Properties;

namespace CanutePhotoOrg
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            txtDefaultInputDrive.Text = Settings.Default.DefaultInputDrive;
            txtDefaultOutputRoot.Text = Settings.Default.DefaultOutputRoot;
            txtFolderTemplate.Text = Settings.Default.FolderTemplate;
        }

        private void btnBrowseOutputRoot_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtDefaultOutputRoot.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string inputDrive = txtDefaultInputDrive.Text.Trim();
            string outputRoot = txtDefaultOutputRoot.Text.Trim();
            string template = txtFolderTemplate.Text.Trim();

            if (string.IsNullOrWhiteSpace(outputRoot))
            {
                MessageBox.Show("Default output root cannot be empty.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(outputRoot))
            {
                DialogResult confirm = MessageBox.Show(
                    "The output root does not exist. Save anyway?",
                    "Settings",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                {
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(template))
            {
                MessageBox.Show("Folder template cannot be empty.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Settings.Default.DefaultInputDrive = inputDrive;
            Settings.Default.DefaultOutputRoot = outputRoot;
            Settings.Default.FolderTemplate = template;
            Settings.Default.Save();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
