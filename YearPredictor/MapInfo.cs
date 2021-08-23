using System;
using System.Collections.Generic;
using System.Text;
using Osu20XXML.Model;

namespace Osu20XXML.WindowsForm

{
    /// <summary>
    //  Container class for information on a beatmap.
    /// </summary>
    public class MapInfo
    {
        //Properties
        private string mapName;
        private string diffName;
        private string artistName;
        private string creatorName;
        private float hp;
        private float cs;
        private float od;
        private float ar;
        private float avgDeltaTime;
        private float stddevDeltaTime;
        private float diffVariance;

        //Default constructor
        public MapInfo()
        {
            MapName = "N/A";
            DiffName = "N/A";
            ArtistName = "N/A";
            CreatorName = "N/A";
            Hp = 0;
            Cs = 0;
            Od = 0;
            Ar = 0;
            AvgDeltaTime = 0;
            StddevDeltaTime = 0;
            DiffVariance = 0;
        }

        //Specific constructor
        public MapInfo(string mapName, string diffName, string artistName, string creatorName, float hp, float cs, float od, float ar, float avgDeltaTime, float stddevDeltaTime, float diffVariance)
        {
            MapName = mapName;
            DiffName = diffName;
            ArtistName = artistName;
            CreatorName = creatorName;
            Hp = hp;
            Cs = cs;
            Od = od;
            Ar = ar;
            AvgDeltaTime = avgDeltaTime;
            StddevDeltaTime = stddevDeltaTime;
            DiffVariance = diffVariance;
        }

        //Getters/setters
        public string MapName { get => mapName; set => mapName = value; }
        public string DiffName { get => diffName; set => diffName = value; }
        public float Hp { get => hp; set => hp = value; }
        public float Cs { get => cs; set => cs = value; }
        public float Od { get => od; set => od = value; }
        public float Ar { get => ar; set => ar = value; }
        public float AvgDeltaTime { get => avgDeltaTime; set => avgDeltaTime = value; }
        public float StddevDeltaTime { get => stddevDeltaTime; set => stddevDeltaTime = value; }
        public float DiffVariance { get => diffVariance; set => diffVariance = value; }
        public string ArtistName { get => artistName; set => artistName = value; }
        public string CreatorName { get => creatorName; set => creatorName = value; }

        //Microsoft ML conversion function
        public ModelInput ConvertToModelInput()
        {
            ModelInput newModel = new ModelInput()
            {
                Hp = this.Hp,
                Cs = this.Cs,
                Od = this.Od,
                Ar = this.Ar,
                AvgDeltaTime = this.AvgDeltaTime,
                StddevDeltaTime = this.StddevDeltaTime,
                DiffVariance = this.DiffVariance,
            };

            return newModel;
        }
    }
}
