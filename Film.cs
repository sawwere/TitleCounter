using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb
{
    public class Film : Title
    {
        public string rus_name { get; set; }

        public List<string> genres { get; set; }

        public void print()
        {
            Console.WriteLine(name, rus_name, image_url,
                link, time, score, year, status);
            foreach (var g in genres)
                Console.WriteLine(g);
            return;
        }
    }
}
