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
            ThemeManager.ApplyTheme(this);
        }

        private void LoadSettings()
        {
            cmbThemePreference.Items.Clear();
            cmbThemePreference.Items.Add("System");
            cmbThemePreference.Items.Add("Light");
            cmbThemePreference.Items.Add("Dark");

            txtDefaultInputDrive.Text = Settings.Default.DefaultInputDrive;
            txtDefaultOutputRoot.Text = Settings.Default.DefaultOutputRoot;
            txtFolderTemplate.Text = Settings.Default.FolderTemplate;
            txtMaxParallelWorkers.Text = Settings.Default.MaxParallelCopyWorkers.ToString();
            cmbThemePreference.SelectedItem = ThemeManager.NormalizeThemePreference(Settings.Default.ThemePreference);
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
            string maxWorkersText = txtMaxParallelWorkers.Text.Trim();
            string themePreference = cmbThemePreference.SelectedItem?.ToString() ?? "System";

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

            if (!int.TryParse(maxWorkersText, out int maxWorkers) || maxWorkers < 1 || maxWorkers > 16)
            {
                MessageBox.Show("Max parallel workers must be a whole number between 1 and 16.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Settings.Default.DefaultInputDrive = inputDrive;
            Settings.Default.DefaultOutputRoot = outputRoot;
            Settings.Default.FolderTemplate = template;
            Settings.Default.MaxParallelCopyWorkers = maxWorkers;
            Settings.Default.ThemePreference = ThemeManager.NormalizeThemePreference(themePreference);
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
