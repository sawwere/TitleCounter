using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace hltb
{
    public class Title
    {
        private string name;
        private string image_url;
        private string link;
        private double time;
        private int score;
        private int year;
        private string status;

        public virtual string Name
        {
            get { return name; }
            set { name = value;  }
        }
        public virtual string Image_Url
        {
            get { return image_url; }
            set { image_url = value; }
        }
        public virtual string Link
        {
            get { return link; }
            set { link = value; }
        }
        public virtual double Time
        {
            get { return time; }
            set { time = value; }
        }
        public virtual int Score
        {
            get { return score; }
            set { score = value; }
        }

        public virtual int Year
        {
            get { return year; }
            set { year = value; }
        }
        public virtual string Status
        {
            get { return status; }
            set { status = value; }
        }        
    }
}
