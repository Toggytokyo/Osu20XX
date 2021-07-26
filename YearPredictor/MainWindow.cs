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
using Osu20XXML.Model;

namespace Osu20XXML.WindowsForm
{
    public partial class MainWindow : Form
    {
        #region Variables

        //Window dragging stuff
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        //MapPanel storage
        private List<MapPanel> maps = new List<MapPanel>();

        #endregion


        #region Windows Form Functions


        #region Misc
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

        private void fileLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            //Call LoadFiles func and wait :)
            BackgroundWorker worker = sender as BackgroundWorker;

            LoadFiles((string[])e.Argument, worker, e);
        }

        private void fileLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Update UI
            UpdateMapList();
            fileLoadingProgressBar.Visible = false;
        }

        private void fileLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            fileLoadingProgressBar.Value = e.ProgressPercentage;
        }

        #endregion


        #region Drop Panel

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

        private void DropPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (CheckFileTypes(e.Data.GetData(DataFormats.FileDrop) as string[]))
            {
                fileLoadingProgressBar.Visible = true;
                fileLoadingProgressBar.Value = 0;
                DropPanel.BackColor = Color.LightSlateGray;
                fileLoader.RunWorkerAsync(e.Data.GetData(DataFormats.FileDrop) as string[]);
            }
        }

        #endregion


        #endregion


        #region Utility Functions

        //Returns whether or not every file path given is a .osu or .osz
        private bool CheckFileTypes(string[] filePaths)
        {
            foreach (string path in filePaths)
                if (!(path.EndsWith(".osz") || path.EndsWith(".osu")))
                    return false;
            return true;
        }

        //Deletes the given map from the maps list and updates the UI
        public void DeleteMap(MapPanel map)
        {
            maps.Remove(map);
            map.Dispose();
            UpdateMapList();
        }

        public void EvaluateMap(MapInfo mapInfo)
        {
            var predictedYear = ConsumeModel.Predict(mapInfo.ConvertToModelInput());
            YearLabel.Text = predictedYear.Prediction;
        }

        //Function to load files from the given  filepaths with a BackgroundWorker
        private void LoadFiles(string[] filePaths, BackgroundWorker worker, DoWorkEventArgs e)
        {
            //Check to make sure that the given file paths are .osu  and .osz
            if (CheckFileTypes(filePaths))
            {
                //Starting load loop
                int count = 0;
                worker.ReportProgress(0);

                foreach (string path in filePaths)
                {
                    //If the file is .osz, we treat it like a .zip
                    if (path.EndsWith(".osz"))
                    {
                        //Unzip...
                        using (ZipArchive archive = ZipFile.OpenRead(path))
                        {
                            //Go through each file in the archive
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                //Find the .osu files we need
                                if (entry.FullName.EndsWith(".osu", StringComparison.OrdinalIgnoreCase))
                                {
                                    //Initializing a new MapInfo container and loading in the metadata from the .osu file
                                    //Its faster to read the entire map into a string and run regex checks
                                    MapInfo newMap = new MapInfo();
                                    string fileText = File.ReadAllText(path);

                                    Regex titleRx = new Regex(@"(Title:)(.+)", RegexOptions.Compiled);
                                    Match titleMatch = titleRx.Match(fileText);
                                    if (titleMatch.Success)
                                    {
                                        newMap.MapName = titleMatch.Groups[2].Value;
                                    }

                                    Regex diffRx = new Regex(@"(Version:)(.+)", RegexOptions.Compiled);
                                    Match diffMatch = diffRx.Match(fileText);
                                    if (diffMatch.Success)
                                    {
                                        if (diffMatch.Groups[2].Value == "")
                                        {
                                            newMap.DiffName = "N/A";
                                        }
                                        else
                                        {
                                            Console.WriteLine("Diff: '" + diffMatch.Groups[2].Value + "'");
                                            newMap.DiffName = diffMatch.Groups[2].Value;
                                        }
                                    }

                                    //Create a new MapPanel and add it to our list of MapPanels
                                    maps.Add(new MapPanel(newMap, 0, this));
                                }
                            }
                        }
                    }

                    //If the given file is just a .osu file, we can just read it in
                    else if (path.EndsWith(".osu"))
                    {
                        //Initializing a new MapInfo container and loading in the metadata from the .osu file
                        //Its faster to read the entire map into a string and run regex checks
                        MapInfo newMap = new MapInfo();
                        string fileText = "";
                        List<float> deltaTimes = new List<float>();
                        List<float> diffs = new List<float>();
                        using (StreamReader sr = File.OpenText(path))
                        {
                            string line = null;
                            while (!(line = sr.ReadLine()).Contains("[HitObjects]"))
                            {
                                fileText += line + "\n";
                            }

                            float last_x = float.MinValue;
                            float last_y = float.MinValue;
                            float last_time = float.MinValue;
                            Regex hitRx = new Regex(@"(-?\d+\.?\d*),(-?\d+\.?\d*),(-?\d+\.?\d*)", RegexOptions.Compiled); ;
                            Match hitRxMatch;
                            while ((line = sr.ReadLine()) != null) {
                                hitRxMatch = hitRx.Match(line);
                                if(hitRxMatch.Success)
                                {
                                    float x = float.Parse(hitRxMatch.Groups[1].Value);
                                    float y = float.Parse(hitRxMatch.Groups[2].Value);
                                    float time = float.Parse(hitRxMatch.Groups[3].Value);
                                    if(last_x != float.MinValue && last_y != float.MinValue && last_time != float.MinValue)
                                    {
                                        float distance = (float)Math.Sqrt(Math.Pow(x - last_x, 2) + Math.Pow(y-last_y, 2));
                                        float deltaTime = time - last_time;
                                        if (deltaTime != 0)
                                            diffs.Add(distance / deltaTime);
                                        deltaTimes.Add(deltaTime);
                                    }
                                    last_x = x;
                                    last_y = y;
                                    last_time = time;
                                }
                            }
                        }
                        newMap.AvgDeltaTime = Enumerable.Average(deltaTimes);
                        newMap.StddevDeltaTime = (float)Math.Sqrt(deltaTimes.Average(v => Math.Pow(v - newMap.AvgDeltaTime, 2)));
                        float diffAverage = Enumerable.Average(diffs);
                        newMap.DiffVariance = (float)Math.Sqrt(diffs.Average(v => Math.Pow(v - diffAverage, 2))) / diffAverage;
                        Console.WriteLine(Enumerable.Average(deltaTimes).ToString() + " " + ((float)Math.Sqrt(deltaTimes.Average(v => Math.Pow(v - newMap.AvgDeltaTime, 2)))).ToString() + " " + ((float)Math.Sqrt(diffs.Average(v => Math.Pow(v - diffAverage, 2))) / diffAverage).ToString());
                        Regex rx;
                        Match rxMatch;

                        rx = new Regex(@"(Title:)(\w+)", RegexOptions.Compiled);
                        rxMatch = rx.Match(fileText);
                        if (rxMatch.Success)
                        {
                            newMap.MapName = rxMatch.Groups[2].Value;
                        }

                        rx = new Regex(@"(Version:)(\w+)", RegexOptions.Compiled);
                        rxMatch = rx.Match(fileText);
                        if (rxMatch.Success)
                        {
                              newMap.DiffName = rxMatch.Groups[2].Value;
                        }
                        else
                        {
                            newMap.DiffName = "N/A";
                        }

                        rx = new Regex(@"(HPDrainRate:)(\d*.?\d*)", RegexOptions.Compiled);
                        rxMatch = rx.Match(fileText);
                        if (rxMatch.Success)
                        {
                            newMap.Hp = float.Parse(rxMatch.Groups[2].Value);
                        }

                        rx = new Regex(@"(CircleSize:)(\d*.?\d*)", RegexOptions.Compiled);
                        rxMatch = rx.Match(fileText);
                        if (rxMatch.Success)
                        {
                            newMap.Cs = float.Parse(rxMatch.Groups[2].Value);
                        }

                        rx = new Regex(@"(OverallDifficulty:)(\d*.?\d*)", RegexOptions.Compiled);
                        rxMatch = rx.Match(fileText);
                        if (rxMatch.Success)
                        {
                            newMap.Od = float.Parse(rxMatch.Groups[2].Value);
                        }

                        rx = new Regex(@"(ApproachRate:)(\d*.?\d*)", RegexOptions.Compiled);
                        rxMatch = rx.Match(fileText);
                        if (rxMatch.Success)
                        {
                            newMap.Ar = float.Parse(rxMatch.Groups[2].Value);
                        } 
                        else
                        {
                            newMap.Ar = newMap.Od;
                        }

                        //Create a new MapPanel and add it to our list of MapPanels
                        maps.Add(new MapPanel(newMap, 0, this));
                    }
                    count++;
                    worker.ReportProgress((int)(((float)count / minimum((float)filePaths.Length, 100f)) * 100));
                    if (count == 100)
                    {
                        break;
                    }
                }
            }
        }

        //Returns the smaller of the two arguments
        private float minimum(float a, float b)
        {
            if (a < b)
                return a;
            return b;
        }

        //Updates the maps being displayed
        public void UpdateMapList()
        {
            //Kicks you to the top of the list
            //Not doing this results in weird behavior when deleting maps
            MapPanel.VerticalScroll.Value = 0;
            MapPanel.VerticalScroll.Maximum = maps.Count * 50;

            //Check if we need to display the label saying that maps will appear here 
            if (maps.Count > 0)
                MapsLabel.Visible = false;
            else
                MapsLabel.Visible = true;

            //Sort the maps into alphabetical order
            maps.Sort((m1, m2) => m1.associatedMapInfo.MapName.CompareTo(m2.associatedMapInfo.MapName));

            //Update the maps
            int count = 0;
            foreach (MapPanel map in maps)
            {
                if (!MapPanel.Controls.Contains(map))
                    MapPanel.Controls.Add(map);
                map.move(count * 50, maps.Count * 50 > MapPanel.Size.Height);
                count++;
            }

        }


        #endregion

    }
}
