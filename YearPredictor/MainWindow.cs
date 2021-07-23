using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace YearPredictor
{
    public partial class MainWindow : Form
    {
        //Window dragging stuff
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        //MapInfo storage
        private List<MapPanel> maps= new List<MapPanel>();

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ControlBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void DropPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (CheckFileTypes(e.Data.GetData(DataFormats.FileDrop) as string[]))
                {
                    e.Effect = DragDropEffects.Copy;
                    DropPanel.BackColor = Color.PaleGreen;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                    DropPanel.BackColor = Color.PaleVioletRed;
                }
            }
            else
            {
                DropPanel.BackColor = Color.PaleVioletRed;
                e.Effect = DragDropEffects.None;
            }
        }

        private bool CheckFileTypes(string[] filePaths)
        {
            foreach (string path in filePaths)
                if (!(path.EndsWith(".osz") || path.EndsWith(".osu")))
                    return false;
            return true;
        }

        private void DropPanel_DragLeave(object sender, EventArgs e)
        {
            DropPanel.BackColor = Color.LightSlateGray;
        }

        private void DropPanel_Click(object sender, EventArgs e)
        {
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                string[] filePaths = fileDialog.FileNames;
                fileLoadingProgressBar.Visible = true;
                fileLoadingProgressBar.Value = 0;
                fileLoader.RunWorkerAsync(filePaths);
            }
        }

        private void DropLabel_Click(object sender, EventArgs e)
        {
            DropPanel_Click(sender, e);
        }

        private void LoadFiles(string[] filePaths, BackgroundWorker worker, DoWorkEventArgs e)
        {
            if (CheckFileTypes(filePaths))
            {
                int count = 0;
                worker.ReportProgress(0);
                foreach (string path in filePaths)
                {
                    if (path.EndsWith(".osz"))
                    {
                        using (ZipArchive archive = ZipFile.OpenRead(path))
                        {
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                if (entry.FullName.EndsWith(".osu", StringComparison.OrdinalIgnoreCase))
                                {
                                    MapInfo newMap = new MapInfo();
                                    using (StreamReader sr = new StreamReader(entry.Open()))
                                    {
                                        string line;
                                        // Read and display lines from the file until the end of
                                        // the file is reached.
                                        
                                        while ((line = sr.ReadLine()) != null)
                                        {
                                            Regex titleRx = new Regex(@"(Title:)(.+)", RegexOptions.Compiled);
                                            Match titleMatch = titleRx.Match(line);
                                            if(titleMatch.Success)
                                            {
                                                newMap.MapName = titleMatch.Groups[2].Value;
                                            }
                                            Regex diffRx = new Regex(@"(Version:)(.+)", RegexOptions.Compiled);
                                            Match diffMatch = diffRx.Match(line);
                                            if (diffMatch.Success)
                                            {
                                                newMap.DiffName = diffMatch.Groups[2].Value;
                                            }
                                        }
                                    }
                                    maps.Add(new MapPanel(newMap, 0, this));
                                }
                            }
                        }
                    }
                    else if (path.EndsWith(".osu"))
                    {
                        MapInfo newMap = new MapInfo();
                        using (StreamReader sr = new StreamReader(File.Open(path, FileMode.Open)))
                        {
                            string line;
                            // Read and display lines from the file until the end of
                            // the file is reached.

                            while ((line = sr.ReadLine()) != null)
                            {
                                Regex titleRx = new Regex(@"(Title:)(.+)", RegexOptions.Compiled);
                                Match titleMatch = titleRx.Match(line);
                                if (titleMatch.Success)
                                {
                                    newMap.MapName = titleMatch.Groups[2].Value;
                                }
                                Regex diffRx = new Regex(@"(Version:)(.+)", RegexOptions.Compiled);
                                Match diffMatch = diffRx.Match(line);
                                if (diffMatch.Success)
                                {
                                    newMap.DiffName = diffMatch.Groups[2].Value;
                                }
                            }
                        }
                        maps.Add(new MapPanel(newMap, 0, this));
                    }
                    count++;
                    worker.ReportProgress((int)(((float)count / (float)filePaths.Length) * 100));
                }
            }
        }

        private void fileLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            LoadFiles((string[])e.Argument, worker, e);
        }

        private void fileLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateMapList();
            fileLoadingProgressBar.Visible = false;
        }

        public void UpdateMapList()
        {
            MapPanel.VerticalScroll.Value = 0;
            MapPanel.VerticalScroll.Maximum = maps.Count * 50;

            if (maps.Count > 0)
                MapsLabel.Visible = false;
            else
                MapsLabel.Visible = true;

            maps.Sort((m1, m2) => m1.associatedMapInfo.MapName.CompareTo(m2.associatedMapInfo.MapName));
            int count = 0;
            foreach (MapPanel map in maps)
            {
                if(!MapPanel.Controls.Contains(map))
                    MapPanel.Controls.Add(map);
                map.move(count * 50, maps.Count*50 > MapPanel.Size.Height);
                count++;
            }
            
        }
       
        public void DeleteMap(MapPanel map)
        {
            maps.Remove(map);
            map.Dispose();
            UpdateMapList();
        }

        private void fileLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            fileLoadingProgressBar.Value = e.ProgressPercentage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateMapList();
        }
    }
}
