using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace hltb
{
    public static class DataFiles
    {
        public static readonly string path = Directory.GetCurrentDirectory();
        
        public static List<Title> GetTitles(mode md, bool tmp = false)
        {
            string json_string = "";
            if (tmp)
                json_string = File.ReadAllText(path + "\\data\\temp_sheet.json");
            else
                json_string = File.ReadAllText(path + "\\data\\" + md.ToString().ToLower() + "_sheet.json");

            var titles = new List<Title>();
            switch (md)
            {
                case mode.GAMES:
                    {
                        var games = JsonConvert.DeserializeObject<List<Game>>(json_string);
                        return games.Select(x => x as Title).ToList();
                    }
                case mode.FILMS:
                    {
                        var films = JsonConvert.DeserializeObject<List<Film>>(json_string);
                        return films.Select(x => x as Title).ToList();
                    }
                case mode.TVSERIES:
                    {
                        var tvseries = JsonConvert.DeserializeObject<List<TVSeries>>(json_string);
                        return tvseries.Select(x => x as Title).ToList();
                    }
            }
            return titles;
        }

        public static void SaveTitles(List<Title> titles, mode md)
        {
            string file_name = path + "\\data\\" + md.ToString().ToLower() + "_sheet.json";
            string jstring = JsonConvert.SerializeObject(titles, Formatting.Indented);
            File.WriteAllText(file_name, jstring);
        }


        public static void CheckDataFiles()
        {
            Directory.CreateDirectory(path + "\\data\\images");
            foreach (mode m in Enum.GetValues(typeof(mode)))
            {
                Directory.CreateDirectory(path + "\\data\\images\\" + m.ToString().ToLower());
                using (var fs = new FileStream(path + "\\data\\" + m.ToString().ToLower() + "_sheet.json", FileMode.OpenOrCreate)) ;
            }
        }
    }
}
