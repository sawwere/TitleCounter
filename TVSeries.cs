using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb
{
    public class TVSeries : Film
    {
        public Dictionary<int, int> seasons { get; set; }
        new public void print()
        {
            Console.WriteLine(name, rus_name, image_url,
                link, time, score, year, status);
            foreach (var g in genres)
                Console.WriteLine(g);
            foreach (var s in seasons)
                Console.WriteLine(s.Key.ToString(), s.Value);
            return;
        }
    }
}
