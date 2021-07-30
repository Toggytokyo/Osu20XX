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

        //MapPanel storage
        readonly int MAPS_PER_PAGE = 6; 
        private int currentPage = 0;
        private List<MapPanel> displayedMaps = new List<MapPanel>();
        private List<MapInfo> allMaps = new List<MapInfo>();
        string[] searchTerms = { "" };
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

        private void rightPageButton_Click(object sender, EventArgs e)
        {
            if (currentPage < (int)Math.Ceiling((float)allMaps.Count / MAPS_PER_PAGE) - 1)
            {
                currentPage++;
                UpdateMapList();
            }
        }

        private void leftPageButton_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                UpdateMapList();
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            allMaps.Clear();
            UpdateMapList();
        }

        private void pageLabel_TextChanged(object sender, EventArgs e)
        {
            if (pageLabel.Enabled == false)
                return;

            int input;
            if (int.TryParse(pageLabel.Text, out input))
            {
                if (input > 0 && input <= (int)Math.Ceiling((float)allMaps.Count / MAPS_PER_PAGE))
                {
                    currentPage = input - 1;
                    UpdateMapList();
                    return;
                }
            }

            currentPage = 0;
            UpdateMapList();
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            searchTerms = searchBox.Text.Split(' ');
            UpdateMapList();
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
            allMaps.Remove(map.associatedMapInfo);
            UpdateMapList();
        }

        public void EvaluateMap(MapInfo mapInfo)
        {
            var predictedYear = ConsumeModel.Predict(mapInfo.ConvertToModelInput());
            YearLabel.Text = predictedYear.Prediction;
        }

        private bool FindMaps(MapInfo map)
        {
            foreach(string term in searchTerms)
            {
                if (!(map.MapName.ToLower().Contains(term.ToLower())
                    || map.DiffName.ToLower().Contains(term.ToLower())
                    || map.ArtistName.ToLower().Contains(term.ToLower())
                    || map.CreatorName.ToLower().Contains(term.ToLower())))
                    return false;
            }
            return true;
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
                                    ReadOsuFile(new StreamReader(entry.Open()));
                                }
                            }
                        }
                    }

                    //If the given file is just a .osu file, we can just read it in
                    else if (path.EndsWith(".osu"))
                    {
                        ReadOsuFile(File.OpenText(path));
                    }
                    count++;
                    worker.ReportProgress((int)(((float)count /(float)filePaths.Length) * 100));
                }
            }
        }

        //Returns the smaller of the two arguments
        private float Minimum(float a, float b)
        {
            if (a < b)
                return a;
            return b;
        }

        //Reads a .osu file stream and adds its representation to the MapPanel list
        private void ReadOsuFile(StreamReader sr)
        {
            //Initializing a new MapInfo container and loading in the metadata from the .osu file
            //Its faster to read the entire map into a string and run regex checks
            MapInfo newMap = new MapInfo();
            string fileText = "";
            List<float> deltaTimes = new List<float>();
            List<float> diffs = new List<float>();
  
            //Read metadata into fileText string
            string line = null;
            while (!(line = sr.ReadLine()).Contains("[HitObjects]"))
            {
                fileText += line + "\n";
            }

            //Read the hitobjects and generate related data points
            float last_x = float.MinValue;
            float last_y = float.MinValue;
            float last_time = float.MinValue;

            Match hitRxMatch;
            while ((line = sr.ReadLine()) != null)
            {
                hitRxMatch = Regex.Match(line, @"(-?\d+\.?\d*),(-?\d+\.?\d*),(-?\d+\.?\d*)");
                if (hitRxMatch.Success)
                {
                    float x = float.Parse(hitRxMatch.Groups[1].Value);
                    float y = float.Parse(hitRxMatch.Groups[2].Value);
                    float time = float.Parse(hitRxMatch.Groups[3].Value);
                    if (last_x != float.MinValue && last_y != float.MinValue && last_time != float.MinValue)
                    {
                        float distance = (float)Math.Sqrt(Math.Pow(x - last_x, 2) + Math.Pow(y - last_y, 2));
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
            
            newMap.AvgDeltaTime = Enumerable.Average(deltaTimes);
            newMap.StddevDeltaTime = (float)Math.Sqrt(deltaTimes.Average(v => Math.Pow(v - newMap.AvgDeltaTime, 2)));
            float diffAverage = Enumerable.Average(diffs);
            newMap.DiffVariance = (float)Math.Sqrt(diffs.Average(v => Math.Pow(v - diffAverage, 2))) / diffAverage;
            Match rxMatch;

            //Perform regex matches on fileText

            rxMatch = Regex.Match(fileText, @"(Title:)(.+)");
            if (rxMatch.Success)
            {
                newMap.MapName = rxMatch.Groups[2].Value;
            }

            rxMatch = Regex.Match(fileText, @"(Version:)(.+)");
            if (rxMatch.Success)
            {
                newMap.DiffName = rxMatch.Groups[2].Value;
            }

            rxMatch = Regex.Match(fileText, @"(Artist:)(.+)");
            if (rxMatch.Success)
            {
                newMap.ArtistName = rxMatch.Groups[2].Value;
            }

            rxMatch = Regex.Match(fileText, @"(Creator:)(.+)");
            if (rxMatch.Success)
            {
                newMap.CreatorName = rxMatch.Groups[2].Value;
            }

            rxMatch = Regex.Match(fileText, @"(HPDrainRate:)(\d*.?\d*)");
            if (rxMatch.Success)
            {
                newMap.Hp = float.Parse(rxMatch.Groups[2].Value);
            }

            rxMatch = Regex.Match(fileText, @"(CircleSize:)(\d*.?\d*)");
            if (rxMatch.Success)
            {
                newMap.Cs = float.Parse(rxMatch.Groups[2].Value);
            }

            rxMatch = Regex.Match(fileText, @"(OverallDifficulty:)(\d*.?\d*)");
            if (rxMatch.Success)
            {
                newMap.Od = float.Parse(rxMatch.Groups[2].Value);
            }

            rxMatch = Regex.Match(fileText, @"(ApproachRate:)(\d*.?\d*)");
            if (rxMatch.Success)
            {
                newMap.Ar = float.Parse(rxMatch.Groups[2].Value);
            }
            else
            {
                newMap.Ar = newMap.Od;
            }

            //Create a new MapPanel and add it to our list of MapPanels
            allMaps.Add(newMap);
        }

        //Updates the maps being displayed
        public void UpdateMapList()
        {
            List<MapInfo> filteredMaps = new List<MapInfo>();
            filteredMaps = allMaps.FindAll(FindMaps);

            if (currentPage * MAPS_PER_PAGE >= filteredMaps.Count && currentPage > 0)
                currentPage--;

            //Check if we need to display the label saying that maps will appear here 
            if (allMaps.Count > 0) {
                pageLabel.Text = (currentPage + 1).ToString();
                MapsLabel.Visible = false;
                if (filteredMaps.Count > 0)
                {
                    pageLabel.Enabled = true;
                    pageLabel.Text = (currentPage + 1).ToString();
                    noResultsLabel.Visible = false;
                }
                else
                {
                    pageLabel.Enabled = false;
                    pageLabel.Text = "0";
                    noResultsLabel.Visible = true;
                }
            }               
            else
            {
                pageLabel.Enabled = false;
                noResultsLabel.Visible = false;
                MapsLabel.Visible = true;
                pageLabel.Text = "0";
            }

            
            //Sort the maps into alphabetical order
            filteredMaps.Sort((m1, m2) => m1.MapName.CompareTo(m2.MapName));


            foreach(MapPanel map in displayedMaps)
            {
                map.Dispose();
            }
            displayedMaps.Clear();

            
            if (allMaps.Count > 0)
            {
                int count = 0;
                foreach (MapInfo map in filteredMaps.GetRange(currentPage * MAPS_PER_PAGE, (int)Minimum((filteredMaps.Count - currentPage * MAPS_PER_PAGE), MAPS_PER_PAGE)))
                {
                    MapPanel newDisplayedMap = new MapPanel(map, count, this);
                    displayedMaps.Add(newDisplayedMap);
                    MapPanel.Controls.Add(newDisplayedMap);
                    count++;
                }
            }

        }

        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
