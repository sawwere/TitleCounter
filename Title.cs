using Newtonsoft.Json;


namespace hltb
{
    public enum TitleStatus { COMPLETED, BACKLOG, RETIRED};

    public class Title
    {
        private string name;
        private string image_url;
        private string link;
        private double time;
        private int score;
        private int year;
        private TitleStatus status;

        public string Name
        {
            get { return name; }
            private set { name = value;  }
        }
        public string Image_Url
        {
            get { return image_url; }
            private set { image_url = value; }
        }
        public string Link
        {
            get { return link; }
            private set { link = value; }
        }
        public int Year
        {
            get { return year; }
            private set { year = value; }
        }

        public virtual double Time
        {
            get { return time; }
            set { time = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        } 
        public TitleStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public Title()
        {

        }

        [JsonConstructor]
        public Title(string name, string image_url, string link, double time, int score, int year, string status)
        {
            Name = name;
            Image_Url = image_url;
            Link = link;
            Time = time;
            Score = score;
            Year = year;
            System.Enum.TryParse(status.ToUpper(), out this.status);
        }
    }
}
