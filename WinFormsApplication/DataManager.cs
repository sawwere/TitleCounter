using hltb.Models;
using hltb.Models.Outdated;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Diagnostics;

namespace hltb
{
    public static class DataManager
    {
        public static readonly string PATH = Directory.GetCurrentDirectory();
        
        //public static List<Content> GetContent(mode md)
        //{
        //    string json_string = "";
        //    json_string = File.ReadAllText($"{PATH}\\data\\" + md.ToString().ToLower() + "_sheet.json");

        //    var titles = new List<Content>();
        //    switch (md)
        //    {
        //        case mode.GAMES:
        //            {

        //                var games = JsonConvert.DeserializeObject<List<GameJson>>(json_string);
        //                using (TitleCounterContext db = new TitleCounterContext())
        //                {
        //                    foreach (var game in games)
        //                    {
        //                        var ls = db.Statuses.ToList();
        //                        foreach (var ss in ls)
        //                        {
        //                            Console.WriteLine(ss);

        //                        }
        //                        hltb.Models.Status stat = db.Statuses.ToList().First(x => x.Name == game.Status);
        //                        Game Game1 = new Game
        //                        {
        //                            Title = game.Name,
        //                            FixedTitle = GetSafeName(game.Name),
        //                            ImageUrl = game.Image_Url,
        //                            LinkUrl = game.Link,
        //                            Time = (int)game.Time * 60,
        //                            Score = game.Score,
        //                        };
        //                        stat.Games.Add(Game1);
        //                        db.Games.Add(Game1);

        //                        db.SaveChanges();
        //                    }
        //                }
        //                return titles;
        //            }
        //        case mode.FILMS:
        //            {
        //                var films = JsonConvert.DeserializeObject<List<FilmJson>>(json_string);
        //                using (TitleCounterContext db = new TitleCounterContext())
        //                {
        //                    foreach (var film in films)
        //                    {
        //                        var ls = db.Statuses.ToList();
        //                        foreach (var ss in ls)
        //                        {
        //                            Console.WriteLine(ss);

        //                        }
        //                        hltb.Models.Status stat = db.Statuses.ToList().First(x => x.Name == film.Status);
        //                        Film _film = new Film
        //                        {
        //                            Title = film.Name,
        //                            FixedTitle = GetSafeName(film.Name),
        //                            ImageUrl = film.Image_Url,
        //                            LinkUrl = film.Link,
        //                            Time = (int)film.Time * 60,
        //                            Score = film.Score,
        //                        };
        //                        stat.Films.Add(_film);
        //                        db.Films.Add(_film);

        //                        db.SaveChanges();
        //                    }
        //                }
        //                return titles;
        //            }
        //        case mode.TVSERIES:
        //            {
        //                return titles;
        //            }
        //    }
        //    return titles;
        //}

        public static void SaveImage(ModeState _mode, Content content, byte[] decodedImage)
        {
            File.WriteAllBytes($"{PATH}\\data\\images\\{_mode.ToString()}\\{content.Id} {content.Title}.jpg", decodedImage);
        }

        public static string GetSafeName(string name)
        {
            char[] proh = { '<', '>', ':', '"', '"', '/', '\\', '|', '?', '*' };
            return new string(name.Where(x => !proh.Contains(x)).ToArray());
        }

        public static void CheckDataFiles()
        {
            Directory.CreateDirectory(PATH + "\\data\\images");
            foreach (mode m in Enum.GetValues(typeof(mode)))
            {
                Directory.CreateDirectory(PATH + "\\data\\images\\" + m.ToString().ToLower());
                using (var fs = new FileStream(PATH + "\\data\\" + m.ToString().ToLower() + "_sheet.json", FileMode.OpenOrCreate));
            }
        }
    }
}
