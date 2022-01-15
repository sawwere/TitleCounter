using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb
{
    public class Title
    {
        public string name { get; set; }
        public string image_url { get; set; }
        public string link { get; set; }
        public double time { get; set; }
        public int score { get; set; }
        public int year { get; set; }
        public string status { get; set; }

        public void UpdateYear(int year)
        {
            this.year = year;
        }

        public void UpdateScore(int score)
        {
            this.score = score;
        }

        public void UpdateStatus(string status)
        {
            this.status = status;
        }

        public void UpdateTime(double time)
        {
            this.time = time;
        }
    }
}
