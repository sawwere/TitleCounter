using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb
{
    public class Film : Title
    {
        private string rusName;
        private List<string> genres;

        public string Rus_Name
        {
            get { return rusName; }
            set { rusName = value; }
        }

        public List<string> Genres 
        {
            get { return genres; }
            set { genres = value; } 
        }

        public virtual void print()
        {
            Console.WriteLine(Name + ' ' + Rus_Name + ' ' + Image_Url + ' ' + Link + ' ' + Time
                + ' ' + Score + ' ' + Year + ' ' + Status);
            foreach (var g in genres)
                Console.WriteLine(g);
            return;
        }
    }
}
