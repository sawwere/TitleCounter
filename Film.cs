using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hltb
{
    public class Film : Content
    {
        private string rusName;
        private List<string> genres;

        new public long? Time
        {
            get { return base.Time; }
        }

        public string Rus_Name
        {
            get { return rusName; }
            private set { rusName = value; }
        }

        public List<string> Genres 
        {
            get { return genres; }
            private set { genres = value; } 
        }

        /*public virtual void print()
        {
            Console.WriteLine(Name + ' ' + Rus_Name + ' ' + Image_Url + ' ' + Link + ' ' + Time
                + ' ' + Score + ' ' + Year + ' ' + Status);
            foreach (var g in genres)
                Console.WriteLine(g);
            return;
        }*/

        [JsonConstructor]
        public Film(string rus_name, List<string> genres, string name, string image_url, string list, double time, 
            int score, int year, string status)
            : base(name, image_url, list, time, score, year, new Status())
        {
            Rus_Name = rus_name;
            Genres = genres;
        }
    }
}
