using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb
{
    public class Game : Title
    {
        public double similarity { get; set; }
        public void print()
        {
            Console.WriteLine(name + ' ' + image_url + ' ' + link + ' ' + time
                + ' ' + similarity + ' ' + score + ' ' + year + ' ' + status);
            return;
        }

        private void Update(Game other)
        {
            name = other.name;
            image_url = other.image_url;
            link = other.link;
            time = other.time;
            similarity = other.similarity;
            score = other.score;
            year = other.year;
            status = other.status;
        }

        
    }
}
