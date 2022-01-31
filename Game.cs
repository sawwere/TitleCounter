using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hltb
{
    public class Game : Title
    {
        private double similarity;

        public double Similarity 
        {
            get { return similarity; }
            set { similarity = value; }
        }

        public void print()
        {
            Console.WriteLine(Name + ' ' + Image_Url + ' ' + Link + ' ' + Time
                + ' ' + Similarity + ' ' + Score + ' ' + Year + ' ' + Status);
            return;
        }

        //[JsonConstructor]
        //public Game(double sim, string n, string iu, string l, double t, int sc, int y, string st)
        //{
        //    similarity = sim;
        //    Name = n;
        //    //ImageUrl = iu;
        //    ImageUrl = iu;
        //    Link = l;
        //    Time = t;
        //    Score = sc;
        //    Year = y;
        //    Status = st;
        //}
    }
}
