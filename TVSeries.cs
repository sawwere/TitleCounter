using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb
{
    public class TVSeries : Film
    {
        private Dictionary<int, int> seasons;

        public override double Time
        {
            get { return Seasons.Select(p => p.Key * p.Value).Sum() * base.Time; }
            set { base.Time = value; }
        }

        public Dictionary<int, int> Seasons 
        {
            get { return seasons; }
            set { seasons = value; }
        }

        public override void print() 
        {
            base.print();
            foreach (var s in Seasons)
                Console.WriteLine(s.Key.ToString(), s.Value);
            return;
        }
    }
}
