using System;
using System.Collections.Generic;
using System.Text;

namespace YearPredictor
{
    public class MapInfo
    {
        private string mapName;
        private string diffName;
        private float hp;
        private float cs;
        private float od;
        private float ar;
        private float avgDeltaTime;
        private float stddevDeltaTime;
        private float diffVariance;

        public MapInfo()
        {
            MapName = "N/A";
            DiffName = "N/A";
            Hp = 0;
            Cs = 0;
            Od = 0;
            Ar = 0;
            AvgDeltaTime = 0;
            StddevDeltaTime = 0;
            DiffVariance = 0;
        }

        public MapInfo(string mapName, string diffName, float hp, float cs, float od, float ar, float avgDeltaTime, float stddevDeltaTime, float diffVariance)
        {
            MapName = mapName;
            DiffName = diffName;
            Hp = hp;
            Cs = cs;
            Od = od;
            Ar = ar;
            AvgDeltaTime = avgDeltaTime;
            StddevDeltaTime = stddevDeltaTime;
            DiffVariance = diffVariance;
        }

        public string MapName { get => mapName; set => mapName = value; }
        public string DiffName { get => diffName; set => diffName = value; }
        public float Hp { get => hp; set => hp = value; }
        public float Cs { get => cs; set => cs = value; }
        public float Od { get => od; set => od = value; }
        public float Ar { get => ar; set => ar = value; }
        public float AvgDeltaTime { get => avgDeltaTime; set => avgDeltaTime = value; }
        public float StddevDeltaTime { get => stddevDeltaTime; set => stddevDeltaTime = value; }
        public float DiffVariance { get => diffVariance; set => diffVariance = value; }
    }
}
