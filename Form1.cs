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
            txtOutputPath.Text = outputPath;
        }

        public void CopyFiles()
        {
            try
            {
                if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath + "\\RAW");
                    Directory.CreateDirectory(outputPath + "\\Edit");
                    Directory.CreateDirectory(outputPath + "\\Select");
                }

                foreach (string sourceFile in source)
                {
                    
                    if (Path.GetExtension(sourceFile).ToUpper() == ".NEF")
                    {
                        string fileName = Path.GetFileName(sourceFile);
                        string destFile = Path.Combine(outputPath + "\\RAW", fileName);
                        if (!File.Exists(destFile))
                        {
                            File.Copy(sourceFile, destFile, false);
                        }                        
                    }
                    else if (sourceFile.Substring(sourceFile.LastIndexOf(".") + 1).ToUpper() == "JPG")
                    {
                        string fileName = Path.GetFileName(sourceFile);
                        string destFile = Path.Combine(outputPath, fileName);
                        if (!File.Exists(destFile))
                        {
                            File.Copy(sourceFile, destFile, false);
                        }
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

            string defaultInput = Settings.Default.DefaultInputDrive;
            if (!string.IsNullOrWhiteSpace(defaultInput) && Directory.Exists(defaultInput))
            {
                thisDialog.InitialDirectory = defaultInput;
            }
            else
            {
                thisDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            }
            thisDialog.Filter = "Media Files (*.png;*.jpg;*.jpeg;*.tif;*.tiff;*.nef;*.cr2;*.cr3;*.arw;*.dng;*.raf;*.orf;*.rw2;*.pef;*.srw;*.mp4;*.mov;*.avi;*.mxf)|*.png;*.jpg;*.jpeg;*.tif;*.tiff;*.nef;*.cr2;*.cr3;*.arw;*.dng;*.raf;*.orf;*.rw2;*.pef;*.srw;*.mp4;*.mov;*.avi;*.mxf";
            thisDialog.FilterIndex = 2;
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
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    outputPath = fbd.SelectedPath;
                    outputPath = txtOutputPath.Text;
                }
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
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
            createOutputPath();
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
                    createOutputPath();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            source = new HashSet<string>();
            txtSource.Text = String.Empty;
        }
    }
}
