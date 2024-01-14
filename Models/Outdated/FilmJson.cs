using System;
using System.Collections.Generic;
using hltb.Models;
using Newtonsoft.Json;

namespace hltb.Models.Outdated
{
    public class FilmJson : Title
    {
        private string rusName;
        private List<string> genres;

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
        public FilmJson(string rus_name, List<string> genres, string name, string image_url, string list, double time,
            int score, int year, string status)
            : base(name, image_url, list, time, score, year, status)
        {
            Rus_Name = rus_name;
            Genres = genres;
        }
    }
}
