using System;
using Newtonsoft.Json;

namespace hltb.Models.Outdated
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
        private bool has_image;
        private string image_name;

        public string Name
        {
            get { return name; }
            private set { name = value; }
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
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public bool HasImage
        {
            get { return has_image; }
            set { has_image = value; }
        }

        public string ImageName
        {
            get { return image_name; }
            private set { image_name = value; }
        }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public Title()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
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
            Status = status;
            //System.Enum.TryParse(status.ToUpper(), out this.status);
        }
    }

    public class GameJson : Title
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
        public GameJson(double similarity, string name, string image_url, string link, double time, int score, int year, string status)
            : base(name, image_url, link, time, score, year, status)
        {
            Similarity = similarity;
        }
    }
}