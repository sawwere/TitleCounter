using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace hltb
{
    public class TVSeries : Film
    {
        private Dictionary<int, int> seasons;

        public Dictionary<int, int> Seasons 
        {
            get { return seasons; }
            private set { seasons = value; }
        }

        public override void print() 
        {
            base.print();
            foreach (var s in Seasons)
                Console.WriteLine(s.Key.ToString(), s.Value);
            return;
        }

        [JsonConstructor]
        public TVSeries(Dictionary<int, int> seasons, string rus_name, List<string> genres, 
            string name, string image_url, string list, double time, int score, int year, string status)
            : base(rus_name, genres, name, image_url, list, time, score, year, status)
        {
            Seasons = seasons;
        }
    }
}
