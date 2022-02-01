using System;
using Newtonsoft.Json;

namespace hltb
{
    public class Game : Title
    {
        private double similarity;

        public double Similarity 
        {
            get { return similarity; }
            private set { similarity = value; }
        }

        public void print()
        {
            Console.WriteLine(Name + ' ' + Image_Url + ' ' + Link + ' ' + Time
                + ' ' + Similarity + ' ' + Score + ' ' + Year + ' ' + Status);
            return;
        }

        [JsonConstructor]
        public Game(double similarity, string name, string image_url, string list, double time, int score, int year, string status)
            :base(name, image_url, list, time, score, year, status)
        {
            Similarity = similarity;
        }
    }
}
