using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CanutePhotoOrg.Properties;

namespace CanutePhotoOrg
{
    public partial class frmMain : Form
    {
        HashSet<string> source;
        string outputPath;
        string year;
        string month;
        string day;
        string project;
        BackgroundWorker worker;
        bool isOutputPathManuallyEdited;
        bool suppressOutputPathTextChanged;
        private const string IngestFilter = "Ingest Files (*.nef;*.cr2;*.cr3;*.arw;*.dng;*.raf;*.orf;*.rw2;*.pef;*.srw;*.mp4;*.mov;*.avi;*.mxf)|*.nef;*.cr2;*.cr3;*.arw;*.dng;*.raf;*.orf;*.rw2;*.pef;*.srw;*.mp4;*.mov;*.avi;*.mxf";
        private static readonly HashSet<string> RawExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".nef", ".cr2", ".cr3", ".arw", ".dng", ".raf", ".orf", ".rw2", ".pef", ".srw"
        };
        private static readonly HashSet<string> VideoExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".mp4", ".mov", ".avi", ".mxf"
        };
        private static readonly HashSet<string> ImageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".tif", ".tiff"
        };

        private string GetDefaultOutputRoot()
        {
            string root = Settings.Default.DefaultOutputRoot;
            if (string.IsNullOrWhiteSpace(root))
            {
                root = "F:\\";
            }
            return root.TrimEnd('\\');
        }

        private string GetFolderTemplate()
        {
            string template = Settings.Default.FolderTemplate;
            if (string.IsNullOrWhiteSpace(template))
            {
                template = "{YYYY}-{MM}-{DD} - {Project}";
            }
            return template;
        }

        private string BuildOutputPath(string projectName)
        {
            DateTime now = DateTime.Now;
            string template = GetFolderTemplate();

            string folderName = template
                .Replace("{YYYY}", now.ToString("yyyy"))
                .Replace("{MM}", now.ToString("MM"))
                .Replace("{DD}", now.ToString("dd"))
                .Replace("{MMMM}", now.ToString("MMMM"))
                .Replace("{Project}", projectName ?? string.Empty)
                .TrimEnd();

            if (string.IsNullOrWhiteSpace(projectName) && folderName.EndsWith("-"))
            {
                folderName = folderName.TrimEnd(' ', '-');
            }

            return Path.Combine(GetDefaultOutputRoot(), folderName);
        }

        public void createOutputPath()
        {
            outputPath = BuildOutputPath(project);
            suppressOutputPathTextChanged = true;
            txtOutputPath.Text = outputPath;
            suppressOutputPathTextChanged = false;
            isOutputPathManuallyEdited = false;
        }

        private void SetManualOutputPath(string path)
        {
            outputPath = path;
            suppressOutputPathTextChanged = true;
            txtOutputPath.Text = path;
            suppressOutputPathTextChanged = false;
            isOutputPathManuallyEdited = true;
        }

        private bool TryValidateOutputPath(out string validatedPath)
        {
            validatedPath = string.Empty;
            string candidate = txtOutputPath.Text.Trim();
            if (string.IsNullOrWhiteSpace(candidate))
            {
                MessageBox.Show("Output path cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                validatedPath = Path.GetFullPath(candidate);
            }
            catch
            {
                MessageBox.Show("Output path is invalid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string root = Path.GetPathRoot(validatedPath);
            if (string.IsNullOrWhiteSpace(root) || !Directory.Exists(root))
            {
                MessageBox.Show("Output drive or root folder is unavailable.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private string GetPreferredRemovableDrive()
        {
            DriveInfo[] drives;
            try
            {
                drives = DriveInfo.GetDrives();
            }
            catch
            {
                return string.Empty;
            }

            var removableRoots = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (DriveInfo drive in drives)
            {
                if (drive.DriveType == DriveType.Removable && drive.IsReady)
                {
                    removableRoots.Add(drive.RootDirectory.FullName);
                }
            }

            if (removableRoots.Count == 0)
            {
                return string.Empty;
            }

            string lastUsedRemovable = Settings.Default.LastUsedRemovableDrive;
            if (!string.IsNullOrWhiteSpace(lastUsedRemovable))
            {
                string normalizedLastUsed = Path.GetPathRoot(lastUsedRemovable) ?? lastUsedRemovable;
                if (removableRoots.Contains(normalizedLastUsed))
                {
                    return normalizedLastUsed;
                }
            }

            string preferred = string.Empty;
            foreach (string root in removableRoots)
            {
                if (string.IsNullOrEmpty(preferred) || string.Compare(root, preferred, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    preferred = root;
                }
            }

            return preferred;
        }

        private string ResolveInitialSourceDirectory()
        {
            string preferredRemovable = GetPreferredRemovableDrive();
            if (!string.IsNullOrWhiteSpace(preferredRemovable) && Directory.Exists(preferredRemovable))
            {
                return preferredRemovable;
            }

            string defaultInput = Settings.Default.DefaultInputDrive;
            if (!string.IsNullOrWhiteSpace(defaultInput) && Directory.Exists(defaultInput))
            {
                return defaultInput;
            }

            string lastUsedFolder = Settings.Default.LastUsedSourceFolder;
            if (!string.IsNullOrWhiteSpace(lastUsedFolder) && Directory.Exists(lastUsedFolder))
            {
                return lastUsedFolder;
            }

            return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        }

        private void SaveLastUsedSourceContext(string selectedFilePath)
        {
            if (string.IsNullOrWhiteSpace(selectedFilePath))
            {
                return;
            }

            string selectedFolder = Path.GetDirectoryName(selectedFilePath);
            if (string.IsNullOrWhiteSpace(selectedFolder))
            {
                return;
            }

            Settings.Default.LastUsedSourceFolder = selectedFolder;

            string root = Path.GetPathRoot(selectedFolder);
            if (!string.IsNullOrWhiteSpace(root))
            {
                try
                {
                    DriveInfo drive = new DriveInfo(root);
                    if (drive.DriveType == DriveType.Removable && drive.IsReady)
                    {
                        Settings.Default.LastUsedRemovableDrive = drive.RootDirectory.FullName;
                    }
                }
                catch
                {
                    // Ignore invalid drive metadata for source persistence.
                }
            }

            Settings.Default.Save();
        }

        public void CopyFiles()
        {
            try
            {
                Directory.CreateDirectory(outputPath);
                string rawFolder = Path.Combine(outputPath, "RAW");
                Directory.CreateDirectory(rawFolder);
                Directory.CreateDirectory(Path.Combine(outputPath, "Edit"));
                Directory.CreateDirectory(Path.Combine(outputPath, "Select"));

                foreach (string sourceFile in source)
                {
                    string extension = Path.GetExtension(sourceFile);
                    if (string.IsNullOrWhiteSpace(extension))
                    {
                        continue;
                    }

                    string destinationFolder = null;
                    if (RawExtensions.Contains(extension))
                    {
                        destinationFolder = rawFolder;
                    }
                    else if (VideoExtensions.Contains(extension))
                    {
                        destinationFolder = outputPath;
                    }
                    else if (ImageExtensions.Contains(extension))
                    {
                        continue;
                    }
                    else
                    {
                        continue;
                    }

                    string fileName = Path.GetFileName(sourceFile);
                    string destFile = Path.Combine(destinationFolder, fileName);
                    if (!File.Exists(destFile))
                    {
                        File.Copy(sourceFile, destFile, false);
                    }
                }

                
                try
                {
                    //Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", pathToFolder
                    Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", outputPath);
                }
                catch (Win32Exception win32Exception)
                {
                    //The system cannot find the file specified...
                    MessageBox.Show(win32Exception.Message, "Error", MessageBoxButtons.OK);
                }

            }
            catch (IOException ioex)
            {
                MessageBox.Show(ioex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
            
        }


        public frmMain()
        {
            InitializeComponent();
            txtOutputPath.TextChanged += txtOutputPath_TextChanged;

            year = DateTime.Today.Year.ToString();
            DateTime m = DateTime.Now;
            month = m.ToString("MM") + " - " + m.ToString("MMMM");
            day = m.ToString("dd");
            createOutputPath();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            year = DateTime.Today.Year.ToString();
            DateTime m = DateTime.Now;
            month = m.ToString("MM") + " - " + m.ToString("MMMM");

            createOutputPath();

            source = new HashSet<string>();
        }

        private void btnLoadSource_Click(object sender, EventArgs e)
        {
            
            System.IO.Stream myStream;
            OpenFileDialog thisDialog = new OpenFileDialog();

            thisDialog.InitialDirectory = ResolveInitialSourceDirectory();
            thisDialog.Filter = IngestFilter;
            thisDialog.FilterIndex = 1;
            thisDialog.RestoreDirectory = true;
            thisDialog.Multiselect = true;
            thisDialog.Title = "Select Project Files";
            
            
            StringBuilder sb = new StringBuilder();

            if (thisDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in thisDialog.FileNames)
                {
                    try
                    {
                        if ((myStream = thisDialog.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                source.Add(file);
                                sb.AppendLine(file.Substring(file.LastIndexOf("\\")+1));
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }

                txtSource.Text = sb.ToString();
                lblCount.Text = "Files: " + source.Count.ToString();
                SaveLastUsedSourceContext(thisDialog.FileName);
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    SetManualOutputPath(fbd.SelectedPath);
                }
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!TryValidateOutputPath(out string validatedPath))
            {
                return;
            }

            outputPath = validatedPath;
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();            
            
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            try
            {
                if (outputPath.Length > 0)
                {
                    CopyFiles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
            finally
            {
                
            }
        }

        private void txtProject_Leave(object sender, EventArgs e)
        {
            project = txtProject.Text;
            if (!isOutputPathManuallyEdited)
            {
                createOutputPath();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var settingsForm = new SettingsForm())
            {
                if (settingsForm.ShowDialog(this) == DialogResult.OK)
                {
                    if (!isOutputPathManuallyEdited)
                    {
                        createOutputPath();
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            source = new HashSet<string>();
            txtSource.Text = String.Empty;
        }

        private void txtOutputPath_TextChanged(object sender, EventArgs e)
        {
            if (!suppressOutputPathTextChanged)
            {
                outputPath = txtOutputPath.Text;
                isOutputPathManuallyEdited = true;
            }
        }
    }
}
