using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        bool isCopyInProgress;
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
        private static readonly string[] RequiredOutputSubfolders = new[] { "RAW", "Edit", "LRC" };

        private sealed class CopyRequest
        {
            public CopyRequest(string outputPath, IReadOnlyList<string> sourceFiles)
            {
                OutputPath = outputPath;
                SourceFiles = sourceFiles;
            }

            public string OutputPath { get; }
            public IReadOnlyList<string> SourceFiles { get; }
        }

        private sealed class CopyProgressState
        {
            public int TotalFiles { get; set; }
            public int ProcessedFiles { get; set; }
            public int CopiedFiles { get; set; }
            public int SkippedFiles { get; set; }
            public int FailedFiles { get; set; }
            public string CurrentFileName { get; set; }
            public TimeSpan Elapsed { get; set; }
            public TimeSpan? EstimatedRemaining { get; set; }
        }

        private sealed class CopyResult
        {
            public string OutputPath { get; set; }
            public int TotalFiles { get; set; }
            public int CopiedCount { get; set; }
            public int SkippedCount { get; set; }
            public int FailedCount { get; set; }
            public List<string> FailedFiles { get; set; }
            public TimeSpan Elapsed { get; set; }
            public int MaxParallelWorkers { get; set; }
        }

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

        private string EnsureOutputFolderStructure(string targetOutputPath)
        {
            Directory.CreateDirectory(targetOutputPath);

            foreach (string subfolder in RequiredOutputSubfolders)
            {
                Directory.CreateDirectory(Path.Combine(targetOutputPath, subfolder));
            }

            foreach (string subfolder in RequiredOutputSubfolders)
            {
                string folder = Path.Combine(targetOutputPath, subfolder);
                if (!Directory.Exists(folder))
                {
                    throw new IOException("Failed to prepare required output subfolder: " + folder);
                }
            }

            return Path.Combine(targetOutputPath, "RAW");
        }

        private static string FormatDuration(TimeSpan value)
        {
            return value.ToString(@"hh\:mm\:ss");
        }

        private static string BuildProgressText(CopyProgressState state)
        {
            int percent = state.TotalFiles <= 0
                ? 0
                : (int)Math.Round((double)state.ProcessedFiles * 100 / state.TotalFiles);
            return "Progress: " + state.ProcessedFiles + "/" + state.TotalFiles + " (" + percent + "%) | Copied: " + state.CopiedFiles + " | Skipped: " + state.SkippedFiles + " | Failed: " + state.FailedFiles;
        }

        private static string BuildTimingText(CopyProgressState state)
        {
            string etaText = state.EstimatedRemaining.HasValue ? FormatDuration(state.EstimatedRemaining.Value) : "--:--:--";
            return "Elapsed: " + FormatDuration(state.Elapsed) + " | ETA: " + etaText;
        }

        private static string BuildSummaryText(CopyResult result)
        {
            var summary = new StringBuilder();
            summary.AppendLine("Copy complete.");
            summary.AppendLine("Total: " + result.TotalFiles);
            summary.AppendLine("Copied: " + result.CopiedCount);
            summary.AppendLine("Skipped: " + result.SkippedCount);
            summary.AppendLine("Failed: " + result.FailedCount);
            summary.AppendLine("Workers: " + result.MaxParallelWorkers);
            summary.AppendLine("Elapsed: " + FormatDuration(result.Elapsed));

            if (result.Elapsed.TotalSeconds > 0)
            {
                double filesPerSecond = result.TotalFiles / result.Elapsed.TotalSeconds;
                summary.AppendLine("Throughput: " + filesPerSecond.ToString("0.00") + " files/sec");
            }

            if (result.FailedFiles.Count > 0)
            {
                summary.AppendLine();
                summary.AppendLine("Failed files:");
                foreach (string failedFile in result.FailedFiles)
                {
                    summary.AppendLine(failedFile);
                }
            }

            return summary.ToString();
        }

        private int GetMaxParallelCopyWorkers()
        {
            int configured = Settings.Default.MaxParallelCopyWorkers;
            if (configured < 1)
            {
                configured = 2;
            }

            return Math.Min(configured, 16);
        }

        private CopyResult CopyFiles(IReadOnlyCollection<string> sourceFiles, string targetOutputPath, Action<CopyProgressState> reportProgress)
        {
            string rawFolder = EnsureOutputFolderStructure(targetOutputPath);
            int maxWorkers = GetMaxParallelCopyWorkers();
            int copiedCount = 0;
            int skippedCount = 0;
            int failedCount = 0;
            int processedCount = 0;
            int totalFiles = sourceFiles.Count;
            List<string> failedFiles = new List<string>();
            object failedFilesLock = new object();
            var stopwatch = Stopwatch.StartNew();

            Debug.WriteLine("Copy preflight complete. Total files: " + totalFiles + ". Workers: " + maxWorkers);

            reportProgress?.Invoke(new CopyProgressState
            {
                TotalFiles = totalFiles,
                ProcessedFiles = 0,
                CopiedFiles = 0,
                SkippedFiles = 0,
                FailedFiles = 0,
                CurrentFileName = string.Empty,
                Elapsed = TimeSpan.Zero,
                EstimatedRemaining = null
            });

            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = maxWorkers
            };

            Parallel.ForEach(sourceFiles, parallelOptions, sourceFile =>
            {
                string fileName = Path.GetFileName(sourceFile);

                try
                {
                    string extension = Path.GetExtension(sourceFile);
                    if (string.IsNullOrWhiteSpace(extension))
                    {
                        Interlocked.Increment(ref skippedCount);
                        return;
                    }

                    string destinationFolder = null;
                    if (RawExtensions.Contains(extension))
                    {
                        destinationFolder = rawFolder;
                    }
                    else if (VideoExtensions.Contains(extension))
                    {
                        destinationFolder = targetOutputPath;
                    }
                    else
                    {
                        Interlocked.Increment(ref skippedCount);
                        return;
                    }

                    string destFile = Path.Combine(destinationFolder, fileName);
                    if (File.Exists(destFile))
                    {
                        Interlocked.Increment(ref skippedCount);
                        return;
                    }

                    try
                    {
                        File.Copy(sourceFile, destFile, false);
                        Interlocked.Increment(ref copiedCount);
                    }
                    catch (IOException ioex)
                    {
                        if (File.Exists(destFile))
                        {
                            Interlocked.Increment(ref skippedCount);
                        }
                        else
                        {
                            Interlocked.Increment(ref failedCount);
                            lock (failedFilesLock)
                            {
                                failedFiles.Add(fileName + " - " + ioex.Message);
                            }
                        }
                    }
                    catch (Exception ex) when (ex is UnauthorizedAccessException || ex is NotSupportedException)
                    {
                        Interlocked.Increment(ref failedCount);
                        lock (failedFilesLock)
                        {
                            failedFiles.Add(fileName + " - " + ex.Message);
                        }
                    }
                }
                finally
                {
                    Interlocked.Increment(ref processedCount);
                    ReportProgressSnapshot(fileName);
                }
            });

            stopwatch.Stop();
            Debug.WriteLine("Copy processing complete. Elapsed: " + stopwatch.Elapsed);
            return new CopyResult
            {
                OutputPath = targetOutputPath,
                TotalFiles = totalFiles,
                CopiedCount = copiedCount,
                SkippedCount = skippedCount,
                FailedCount = failedCount,
                FailedFiles = failedFiles,
                Elapsed = stopwatch.Elapsed,
                MaxParallelWorkers = maxWorkers
            };

            void ReportProgressSnapshot(string currentFileName)
            {
                int processedSnapshot = Volatile.Read(ref processedCount);
                int copiedSnapshot = Volatile.Read(ref copiedCount);
                int skippedSnapshot = Volatile.Read(ref skippedCount);
                int failedSnapshot = Volatile.Read(ref failedCount);

                double avgTicksPerFile = processedSnapshot > 0 ? (double)stopwatch.ElapsedTicks / processedSnapshot : 0;
                int remainingFiles = Math.Max(0, totalFiles - processedSnapshot);
                TimeSpan? eta = processedSnapshot >= 3
                    ? TimeSpan.FromTicks((long)(avgTicksPerFile * remainingFiles))
                    : (TimeSpan?)null;

                reportProgress?.Invoke(new CopyProgressState
                {
                    TotalFiles = totalFiles,
                    ProcessedFiles = processedSnapshot,
                    CopiedFiles = copiedSnapshot,
                    SkippedFiles = skippedSnapshot,
                    FailedFiles = failedSnapshot,
                    CurrentFileName = currentFileName,
                    Elapsed = stopwatch.Elapsed,
                    EstimatedRemaining = eta
                });
            }
        }


        public frmMain()
        {
            InitializeComponent();
            txtOutputPath.TextChanged += txtOutputPath_TextChanged;
            ThemeManager.ApplyTheme(this);

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
            if (isCopyInProgress)
            {
                return;
            }

            if (!TryValidateOutputPath(out string validatedPath))
            {
                return;
            }

            if (source == null || source.Count == 0)
            {
                MessageBox.Show("Please select at least one source file before generating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            outputPath = validatedPath;
            var request = new CopyRequest(validatedPath, source.ToList());

            isCopyInProgress = true;
            btnGo.Enabled = false;
            progressCopy.Value = 0;
            progressCopy.Maximum = request.SourceFiles.Count <= 0 ? 1 : request.SourceFiles.Count;
            lblCurrentFile.Text = "Current: Starting...";
            lblProgress.Text = "Progress: 0/" + request.SourceFiles.Count + " (0%) | Copied: 0 | Skipped: 0 | Failed: 0";
            lblTiming.Text = "Elapsed: 00:00:00 | ETA: --:--:--";
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(request);            
            
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var request = e.Argument as CopyRequest;
            if (request != null && !string.IsNullOrWhiteSpace(request.OutputPath))
            {
                var backgroundWorker = sender as BackgroundWorker;
                var result = CopyFiles(request.SourceFiles, request.OutputPath, progressState =>
                {
                    if (backgroundWorker != null)
                    {
                        backgroundWorker.ReportProgress(0, progressState);
                    }
                });
                e.Result = result;
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            isCopyInProgress = false;
            btnGo.Enabled = true;
            lblCurrentFile.Text = "Current: Idle";

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = e.Result as CopyResult;
            if (result == null)
            {
                return;
            }

            lblTiming.Text = "Elapsed: " + FormatDuration(result.Elapsed) + " | ETA: 00:00:00";

            try
            {
                Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", result.OutputPath);
            }
            catch (Win32Exception win32Exception)
            {
                MessageBox.Show(win32Exception.Message, "Error", MessageBoxButtons.OK);
            }

            MessageBox.Show(
                BuildSummaryText(result),
                result.FailedCount > 0 ? "Copy Completed with Errors" : "Copy Completed",
                MessageBoxButtons.OK,
                result.FailedCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var state = e.UserState as CopyProgressState;
            if (state == null)
            {
                return;
            }

            int safeTotal = state.TotalFiles <= 0 ? 1 : state.TotalFiles;
            progressCopy.Maximum = safeTotal;
            progressCopy.Value = Math.Max(0, Math.Min(state.ProcessedFiles, safeTotal));
            lblProgress.Text = BuildProgressText(state);
            lblTiming.Text = BuildTimingText(state);
            lblCurrentFile.Text = string.IsNullOrWhiteSpace(state.CurrentFileName)
                ? "Current: Waiting..."
                : "Current: " + state.CurrentFileName;
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

                    ThemeManager.ApplyTheme(this);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            source = new HashSet<string>();
            txtSource.Text = String.Empty;
            lblCount.Text = "Files: 0";
            progressCopy.Value = 0;
            progressCopy.Maximum = 1;
            lblProgress.Text = "Progress: 0/0 (0%) | Copied: 0 | Skipped: 0 | Failed: 0";
            lblTiming.Text = "Elapsed: 00:00:00 | ETA: --:--:--";
            lblCurrentFile.Text = "Current: Idle";
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
