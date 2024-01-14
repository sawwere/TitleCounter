using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Win32;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Diagnostics;
using hltb.Models;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections;
using hltb.Models.Outdated;

namespace hltb
{
    public static class DataManager
    {
        public static readonly string PATH = Directory.GetCurrentDirectory();
        
        public static List<Content> GetContent(mode md, bool tmp = false)
        {
            string json_string = "";
            if (tmp)
                json_string = File.ReadAllText(PATH + "\\data\\temp_sheet.json");
            else
                json_string = File.ReadAllText("C:\\Users\\sawer\\Desktop\\folder\\list\\net6.0-windows\\data\\" + md.ToString().ToLower() + "_sheet.json");

            var titles = new List<Content>();
            switch (md)
            {
                case mode.GAMES:
                    {

                        var games = JsonConvert.DeserializeObject<List<GameJson>>(json_string);
                        using (TitleCounterContext db = new TitleCounterContext())
                        {
                            foreach (var game in games)
                            {
                                Console.WriteLine(777777777777);
                                var ls = db.Statuses.ToList();
                                foreach (var ss in ls)
                                {
                                    Console.WriteLine(ss);

                                }
                                hltb.Models.Status stat = db.Statuses.ToList().First(x => x.Name == game.Status);
                                // создаем два объекта Game
                                Game Game1 = new Game
                                {
                                    Title = game.Name,
                                    FixedTitle = game.Name,
                                    ImageUrl = game.Image_Url,
                                    LinkUrl = game.Link,
                                    Time = ((int)game.Time),
                                    Score = game.Score,
                                };
                                stat.Games.Add(Game1);
                                // добавляем их в бд
                                db.Games.Add(Game1);

                                db.SaveChanges();
                            }
                        }

                        //return titles;// games.Select(x => x as Content).ToList();
                        //// TODO
                        using (TitleCounterContext db = new TitleCounterContext())
                        {
                            var Games = db.Games.Include(x => x.Status).ToList();
                            return Games.Select(x => x as Content).ToList();
                        }
                    }
                case mode.FILMS:
                    {
                        var films = JsonConvert.DeserializeObject<List<FilmJson>>(json_string);
                        using (TitleCounterContext db = new TitleCounterContext())
                        {
                            foreach (var film in films)
                            {
                                Console.WriteLine(777777777777);
                                var ls = db.Statuses.ToList();
                                foreach (var ss in ls)
                                {
                                    Console.WriteLine(ss);

                                }
                                hltb.Models.Status stat = db.Statuses.ToList().First(x => x.Name == film.Status);
                                Film _film = new Film
                                {
                                    Title = film.Name,
                                    FixedTitle = film.Name,
                                    ImageUrl = film.Image_Url,
                                    LinkUrl = film.Link,
                                    Time = ((int)film.Time),
                                    Score = film.Score,
                                };
                                stat.Films.Add(_film);
                                // добавляем их в бд
                                db.Films.Add(_film);

                                db.SaveChanges();
                            }
                        }
                        return titles;
                    }
                case mode.TVSERIES:
                    {
                        return titles;
                    }
            }
            return titles;
        }

        public static void SaveImage(mode _mode, Content content, byte[] decodedImage)
        {
            File.WriteAllBytes($"{PATH}\\data\\images\\{_mode.ToString().ToLower()}\\{content.Id} {content.FixedTitle}.jpg", decodedImage);
        }

        public static void DeleteImage(mode _mode, Content content)
        {
            File.Delete($"{PATH}\\data\\images\\{_mode.ToString().ToLower()}\\{content.Id} {content.FixedTitle}.jpg");
        }

        public static string SearchNewContent(mode _mode, string title, long statusId, int score = 0)
        {
            string request = $"{_mode.ToString().ToLower()};;{title};;{statusId};;{score}";
            string pathToFind = PATH;
            for (int i = 0; i < 3; i++)
            {
                int pos = pathToFind.LastIndexOf("\\");
                pathToFind = pathToFind.Remove(pos, pathToFind.Length - pos);
            }

            Process p = Process.Start(new ProcessStartInfo
            {
                FileName = @"T:\\Programs\\Python39\\python.exe",// GetPythonPath(),
                Arguments = pathToFind + "/python_part/find.py \"" + request + "\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden
            }) ;
            return p.StandardOutput.ReadToEnd();
        }

        public static Content? GetFromJson(string json_string, mode currentMode)
        {
            switch (currentMode)
            {
                case mode.GAMES:
                    {
                        return JsonConvert.DeserializeObject<Game>(json_string);
                    }
                case mode.FILMS:
                    {
                        return JsonConvert.DeserializeObject<Film>(json_string);
                    }
                case mode.TVSERIES:
                    {
                        return null;
                    }
            }
            return null;
        }

        public static void SaveContent(List<Content> titles, mode md)
        {
            using (TitleCounterContext db = new TitleCounterContext())
            {
                db.SaveChanges();
            }
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


        public static string GetPythonPath(string requiredVersion = "", string maxVersion = "")
        {
            string[] possiblePythonLocations = new string[3] {
                @"HKLM\SOFTWARE\Python\PythonCore\",
                @"HKCU\SOFTWARE\Python\PythonCore\",
                @"HKLM\SOFTWARE\Wow6432Node\Python\PythonCore\"
            };

            //Version number, install path
            Dictionary<string, string> pythonLocations = new Dictionary<string, string>();

            foreach (string possibleLocation in possiblePythonLocations)
            {
                string regKey = possibleLocation.Substring(0, 4), actualPath = possibleLocation.Substring(5);
                RegistryKey theKey = (regKey == "HKEY_LOCAL_MACHINE" ? Registry.LocalMachine : Registry.CurrentUser);
                RegistryKey theValue = theKey.OpenSubKey(actualPath);
                if (theValue is null)
                    continue;

                foreach (var v in theValue.GetSubKeyNames())
                {
                    RegistryKey productKey = theValue.OpenSubKey(v);
                    if (productKey != null)
                    {
                        try
                        {
                            string? pythonExePath = productKey.OpenSubKey("InstallPath").GetValue("ExecutablePath")?.ToString();

                            if (pythonExePath != null && pythonExePath != "")
                            {
                                Console.WriteLine("Got python version; " + v + " at path; " + pythonExePath);
                                pythonLocations.Add(v.ToString(), pythonExePath);
                            }
                        }
                        catch (System.NullReferenceException ex) 
                        {
                            Console.WriteLine(ex.Message);
                            //Install path doesn't exist
                        }
                    }
                }
            }

            if (pythonLocations.Count > 0)
            {
                System.Version desiredVersion = new System.Version(requiredVersion == "" ? "0.0.1" : requiredVersion),
                    maxPVersion = new System.Version(maxVersion == "" ? "999.999.999" : maxVersion);

                string highestVersion = "", highestVersionPath = "";

                foreach (KeyValuePair<string, string> pVersion in pythonLocations)
                {
                    //TODO; if on 64-bit machine, prefer the 64 bit version over 32 and vice versa
                    int index = pVersion.Key.IndexOf("-"); //For x-32 and x-64 in version numbers
                    string formattedVersion = index > 0 ? pVersion.Key.Substring(0, index) : pVersion.Key;

                    System.Version thisVersion = new System.Version(formattedVersion);
                    int comparison = desiredVersion.CompareTo(thisVersion),
                        maxComparison = maxPVersion.CompareTo(thisVersion);

                    if (comparison <= 0)
                    {
                        //Version is greater or equal
                        if (maxComparison >= 0)
                        {
                            desiredVersion = thisVersion;

                            highestVersion = pVersion.Key;
                            highestVersionPath = pVersion.Value;
                        }
                        else
                        {
                            //Console.WriteLine("Version is too high; " + maxComparison.ToString());
                        }
                    }
                    else
                    {
                        //Console.WriteLine("Version (" + pVersion.Key + ") is not within the spectrum.");
                    }
                }
                return highestVersionPath;
            }
            return "";
        }
    }
}
