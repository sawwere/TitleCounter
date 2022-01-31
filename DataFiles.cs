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
        
        public static List<Title> GetGames()
        {
            string json_string = File.ReadAllText(path + "\\data\\games_sheet.json");
            var games = JsonConvert.DeserializeObject<List<Game>>(json_string);
            List<Title> res = games.Select(x => x as Title).ToList();

            return res;
        }
        public static List<Title> GetFilms()
        {
            string json_string = File.ReadAllText(path + "\\data\\films_sheet.json");
            var films = JsonConvert.DeserializeObject<List<Film>>(json_string);
            List<Title> res = films.Select(x => x as Title).ToList();
            return res;
        }
        public static List<Title> GetTVSeries()
        {
            string json_string = File.ReadAllText(path + "\\data\\tvseries_sheet.json");
            var tvseries = JsonConvert.DeserializeObject<List<TVSeries>>(json_string);
            List<Title> res = tvseries.Select(x => x as Title).ToList();
            return res;
        }


        public static void SaveGames(List<Title> games)
        {
            string file_name = path + "\\data\\games_sheet.json";
            string jstring = JsonConvert.SerializeObject(games, Formatting.Indented);
            File.WriteAllText(file_name, jstring);
        }
        public static void SaveFilms(List<Title> films)
        {
            string file_name = path + "\\data\\films_sheet.json";
            string jstring = JsonConvert.SerializeObject(films, Formatting.Indented);
            File.WriteAllText(file_name, jstring);
        }
        public static void SaveTVSeries(List<Title> tvseries)
        {
            string file_name = path + "\\data\\tvseries_sheet.json";
            string jstring = JsonConvert.SerializeObject(tvseries, Formatting.Indented);
            File.WriteAllText(file_name, jstring);
        }

        public static void CheckDataFiles()
        {
            Directory.CreateDirectory(path + "\\data\\images");
            foreach (mode m in Enum.GetValues(typeof(mode)))
            {
                Directory.CreateDirectory(path + "\\data//images\\" + m.ToString().ToLower());
                using (var fs = new FileStream(path + "\\data\\" + m.ToString().ToLower() + "_sheet.json", FileMode.OpenOrCreate)) ;
            }
        }
    }
}
