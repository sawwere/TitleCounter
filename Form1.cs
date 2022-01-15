using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace hltb
{
    public partial class Mainform : Form
    {
        Panel list_panel = new Panel();
        Panel content_panel = new Panel();
        List<Game> games = GetGames();
        List<Film> films = GetFilms();
        List<TVSeries> tvseries = GetTVSeries();
        Game cur_game;
        Film cur_film;
        TVSeries cur_tvseries;

        string mode = "games";
        //"F:/my_programs/python/HowLongToBeat" +
        //Directory.GetCurrentDirectory()
        //
        static string path = "F:/my_programs/python/HowLongToBeat";

        private void OnApplicationExit(object sender, EventArgs e)
        {
            SaveGames();
            SaveFilms();
            SaveTVSeries();
        }
        static List<Game> GetGames()
        {
            string json_string = File.ReadAllText(path + "/game_sheet.json");
            var games = JsonConvert.DeserializeObject<List<Game>>(json_string);
            return games;
        }

        static List<Film> GetFilms()
        {
            string json_string = File.ReadAllText(path + "/film_sheet.json");
            var films = JsonConvert.DeserializeObject<List<Film>>(json_string);
            return films;
        }
        static List<TVSeries> GetTVSeries()
        {
            string json_string = File.ReadAllText(path + "/tvseries_sheet.json");
            var tvseries = JsonConvert.DeserializeObject<List<TVSeries>>(json_string);
            return tvseries;
        }
        void UpdateLabel2()
        {
            double total = 1, cmpltd = 1, tmcmpltd = 1, tmtotal = 1;
            switch (mode)
            {
                case "games":
                    total = games.Count() + 0.0;
                    cmpltd = games.Where(x => (x.status == "completed") || (x.status == "retired")).Count();
                    tmcmpltd = games.Where(x => (x.status == "completed") || (x.status == "retired")).Select(x => x.time).Sum();
                    tmtotal = games.Select(x => x.time).Sum();
                    break;
                case "films":
                    total = films.Count() + 0.0;
                    cmpltd = films.Where(x => (x.status == "completed") || (x.status == "retired")).Count();
                    tmcmpltd = films.Where(x => (x.status == "completed") || (x.status == "retired")).Select(x => x.time).Sum();
                    tmtotal = films.Select(x => x.time).Sum();
                    break;
                case "tvseries":
                    total = tvseries.Count() + 0.0;
                    cmpltd = tvseries.Where(x => (x.status == "completed") || (x.status == "retired")).Count();
                    break;
            }

            label2.Text = $"Completed: {cmpltd} / {total}  ({(cmpltd / total * 100):F2}%)" + '\n'
                + $"Time : {tmcmpltd} / {tmtotal} ({(tmcmpltd / tmtotal * 100):F2}%)";
        }
        void SaveGames()
        {
            string file_name = path + "/game_sheet.json";
            string jstring = JsonConvert.SerializeObject(games, Formatting.Indented);
            File.WriteAllText(file_name, jstring);
        }

        void SaveFilms()
        {
            string file_name = path + "/film_sheet.json";
            string jstring = JsonConvert.SerializeObject(films, Formatting.Indented);
            File.WriteAllText(file_name, jstring);
        }
        void SaveTVSeries()
        {
            string file_name = path + "/tvseries_sheet.json";
            string jstring = JsonConvert.SerializeObject(tvseries, Formatting.Indented);
            File.WriteAllText(file_name, jstring);
        }
        Color GetColor(int score)
        {
            Color result = Color.FromArgb(0, 0, 0, 0);
            switch (score)
            {
                case 1:
                    result = Color.FromArgb(255, 255, 100, 0);
                    break;
                case 2:
                    result = Color.FromArgb(255, 255, 160, 0);
                    break;
                case 3:
                    result = Color.FromArgb(255, 255, 200, 0);
                    break;
                case 4:
                    result = Color.FromArgb(255, 255, 240, 0);
                    break;
                case 5:
                    result = Color.FromArgb(255, 200, 240, 110);
                    break;
                case 6:
                    result = Color.FromArgb(255, 180, 255, 0);
                    break;
                case 7:
                    result = Color.FromArgb(255, 70, 200, 0);
                    break;
                case 8:
                    result = Color.FromArgb(255, 60, 170, 0);
                    break;
                case 9:
                    result = Color.FromArgb(255, 50, 140, 0);
                    break;
                case 10:
                    result = Color.FromArgb(255, 40, 110, 36);
                    break;
            }
            return result;
        }
        public Mainform()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            ResetYears();
            UpdateLabel2();

            ModeBox.SelectedItem = ModeBox.Items[0];

            statusbox.SelectedIndex = 1;
            scorebox.SelectedIndex = 0;

            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

        }
        private void statusbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void scorebox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void addtitle_MouseDown(object sender, MouseEventArgs e)
        {
            StringBuilder status = new StringBuilder();
            if (namebox.Text == "")
            {
                status.Append("Error: empty name field");
            }
            else
            {
                string title = namebox.Text + "#" + statusbox.Text + "#" + scorebox.SelectedItem + '#' + mode;
                Process p = Process.Start(new ProcessStartInfo
                {
                    //"F:/programs/Python" 
                    //"F:/my_programs/python/HowLongToBeat"
                    FileName = path + "/python.exe",
                    Arguments = path + "/find.py \"" + title + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                });
                var t = p.StandardOutput.ReadToEnd();
                var r = t.Split(' ');
                Console.WriteLine(r[0]);
                if (r[0] == "ERROR")
                {
                    status.Append("ERROR");
                    switch (r[1])
                    {
                        case "a":
                            status.Append(": title is already in list");
                            break;
                        case "f":
                            status.Append(": title has not found");
                            break;
                        case "t":
                            status.Append(": incorrect type. Choose correct mode");
                            break;
                    }
                }
                else
                {
                    status.Append("Completed succesfuly");
                    namebox.Text = "";
                    statusbox.SelectedIndex = 1;
                    scorebox.SelectedIndex = 0;
                    switch (mode)
                    {
                        case "games":
                            games = GetGames();
                            break;
                        case "films":
                            films = GetFilms();
                            break;
                        case "tvseries":
                            tvseries = GetTVSeries();
                            break;
                    }
                }
            }
            operationLabel.Text = status.ToString();
            UpdateLabel2();
        }
        private void ScoreCSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            var combobox = (ComboBox)sender;
            var s = (int)combobox.SelectedItem;
            switch (mode)
            {
                case "games":
                    cur_game.UpdateScore(s);
                    break;
                case "films":
                    cur_film.UpdateScore(s);
                    break;
                case "tvseries":
                    cur_tvseries.UpdateScore(s);
                    break;
            }
        }
        private void StatusCSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            var combobox = (ComboBox)sender;
            var s = combobox.SelectedItem.ToString();
            switch (mode)
            {
                case "games":
                    cur_game.UpdateStatus(s);
                    break;
                case "films":
                    cur_film.UpdateStatus(s);
                    break;
                case "tvseries":
                    cur_tvseries.UpdateStatus(s);
                    break;
            }
        }
        private void SeasonsCSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            var combobox = (ComboBox)sender;
            var s = combobox.SelectedItem.ToString();
            content_panel.Controls.RemoveByKey("episodesLabel");
            Label episodesLabel = new Label();
            episodesLabel.Name = "episodesLabel";
            episodesLabel.Text = $"Episodes count: {cur_tvseries.seasons[int.Parse(combobox.SelectedItem.ToString())]}";
            episodesLabel.Width = 125;
            episodesLabel.Location = new Point(combobox.Left + combobox.Width, combobox.Top);
            content_panel.Controls.Add(episodesLabel);
        }
        private void TimeCKeyPress(object sender, KeyPressEventArgs e)
        {
            var textbox = (TextBox)sender;
            char symb = e.KeyChar;
            if (!double.TryParse(textbox.Text + symb.ToString(), out double a) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        private void TimeCTextChanged(object sender, EventArgs eventArgs)
        {
            var textbox = (TextBox)sender;
            if (!double.TryParse(textbox.Text, out double a))
            {
                textbox.Text = "0,0";
            }
            var s = double.Parse(textbox.Text);
            cur_game.UpdateTime(s);
        }

        private void CopyButtonOnClick(object sender, EventArgs eventArgs)
        {
            switch (mode)
            {
                case "games":
                    Clipboard.SetText(cur_game.name);
                    break;
                case "films":
                    Clipboard.SetText(cur_film.name);
                    break;
                case "tvseries":
                    Clipboard.SetText(cur_tvseries.name);
                    break;
            }
        }
        private void deleteButtonClick(object sender, EventArgs eventArgs)
        {
            content_panel.Controls.Clear();
            switch (mode)
            {
                case "games":

                    games.Remove(cur_game);
                    cur_game = new Game();
                    SaveGames();
                    break;
                case "films":
                    films.Remove(cur_film);
                    cur_film = new Film();
                    SaveFilms();
                    break;
                case "tvseries":
                    tvseries.Remove(cur_tvseries);
                    cur_tvseries = new TVSeries();
                    SaveTVSeries();
                    break;
            }
            UpdateLabel2();
        }
        public string BuildStingGenres<T>(T m) where T : Film
        {
            StringBuilder str = new StringBuilder();
            str.Append("Genres:");
            int len = str.Length;
            foreach (var gen in m.genres)
            {
                if ((len + gen.Length + 2) / 40 < 1)
                {
                    str.Append(" " + gen + ";");
                    len = str.Length;
                }
                else
                {
                    str.Append("\n              " + gen + ";");
                    len = str.Length - len;
                }
            }
            return str.ToString();
        }
        public string GetOrigTitle<T>(T m) where T : Film
        {
            return m.rus_name;
        }
        public string GetTime<T>(T t) where T : Title
        {
            string res = "";
            switch (mode)
            {
                case "games":
                    res = t.time.ToString();
                    break;
                case "films":
                    var h = (int)t.time / 60;
                    var m = (int)t.time % 60;
                    res = $"                {h}h {m}m";
                    break;
                case "tvseries":
                    h = (int)t.time / 60;
                    m = (int)t.time % 60;
                    res = $"                {h}h {m}m";
                    break;
            }
            return res;
        }
        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            content_panel.Controls.Clear();

            var button = (Button)sender;

            content_panel.Location = new Point(1000, 25);
            content_panel.Width = 300;
            content_panel.Height = 600;

            PictureBox pb = new PictureBox();
            pb.Location = new Point(0, 0);
            pb.Size = new System.Drawing.Size(200, 250);

            var g = button.Text.Where(x => (x != ':') & (x != '/')).ToArray();
            StringBuilder s = new StringBuilder();
            foreach (var ss in g)
            {
                s.Append(ss.ToString());
            }
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            content_panel.Controls.Add(pb);

            Title cur_title = new Title();
            switch (mode)
            {
                case "games":
                    pb.Image = new Bitmap(path + "//images//game//" + s + ".jpg");
                    cur_game = games.Find(x => x.name == button.Text);
                    cur_title = cur_game as Title;
                    break;
                case "films":
                    pb.Image = new Bitmap(path + "//images//film//" + s + ".jpg");
                    cur_film = films.Find(x => x.name == button.Text);
                    cur_title = cur_film as Title;
                    break;
                case "tvseries":
                    pb.Image = new Bitmap(path + "//images//tvseries//" + s + ".jpg");
                    cur_tvseries = tvseries.Find(x => x.name == button.Text);
                    cur_title = cur_tvseries as Title;
                    break;
            }


            Label nameLabel = new Label();
            nameLabel.Text = "Title name: " + cur_title.name;
            nameLabel.Location = new Point(0, 275);
            nameLabel.Width = 200;
            content_panel.Controls.Add(nameLabel);
            // Get rus name for Films, TvSeries
            switch (mode)
            {
                case "films":
                    {
                        if (nameLabel.Text.Length < 20)
                            nameLabel.Text += "  ||  " + GetOrigTitle(cur_film);
                        else
                        {
                            nameLabel.Text += "\n  ||  " + GetOrigTitle(cur_film);
                            nameLabel.Height += 9;
                        }
                        break;
                    }
                case "tvseries":
                    if (nameLabel.Text.Length < 20)
                        nameLabel.Text += "  ||  " + GetOrigTitle(cur_tvseries);
                    else
                    {
                        nameLabel.Text += "\n  ||  " + GetOrigTitle(cur_tvseries);
                        nameLabel.Height += 9;
                    }
                    break;
            }

            Button copyButton = new Button();
            copyButton.Text = "Copy";
            copyButton.Location = new Point(nameLabel.Right + 25, nameLabel.Top);
            copyButton.Click += CopyButtonOnClick;
            content_panel.Controls.Add(copyButton);

            Label timeLabel = new Label();
            timeLabel.Text = "Time: ";
            timeLabel.Location = new Point(nameLabel.Left, nameLabel.Bottom + 5);
            timeLabel.Width = 75;
            content_panel.Controls.Add(timeLabel);

            switch (mode)
            {
                case "games":
                    TextBox time_c = new TextBox();
                    time_c.Text = GetTime(cur_game);
                    time_c.Width = 75;
                    time_c.Location = new Point(timeLabel.Left + 80, timeLabel.Top);
                    time_c.KeyPress += TimeCKeyPress;
                    time_c.TextChanged += TimeCTextChanged;
                    content_panel.Controls.Add(time_c);
                    break;
                case "films":
                    timeLabel.Width = 200;
                    timeLabel.Text += GetTime(cur_film);
                    break;
                case "tvseries":
                    timeLabel.Width = 200;
                    timeLabel.Text += GetTime(cur_tvseries);
                    break;
            }

            Label yearLabel = new Label();
            yearLabel.Text = $"Year:                 {cur_title.year}";
            yearLabel.Location = new Point(nameLabel.Left, timeLabel.Top + 25);
            yearLabel.Width = 200;
            content_panel.Controls.Add(yearLabel);

            Label scoreLabel = new Label();
            scoreLabel.Text = "Score:";
            scoreLabel.Location = new Point(nameLabel.Left, yearLabel.Top + 25);
            scoreLabel.Width = 75;
            content_panel.Controls.Add(scoreLabel);

            Label statusLabel = new Label();
            statusLabel.Text = "Status:";
            statusLabel.Location = new Point(nameLabel.Left, scoreLabel.Top + 25); ;
            statusLabel.Width = 75;
            content_panel.Controls.Add(statusLabel);

            ComboBox score_c = new ComboBox();
            score_c.Text = cur_title.score.ToString();
            score_c.Width = 75;
            score_c.Items.AddRange(new object[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            score_c.SelectedIndexChanged += ScoreCSelectedIndexChanged;
            score_c.Location = new Point(scoreLabel.Left + 80, scoreLabel.Top);
            content_panel.Controls.Add(score_c);

            ComboBox status_c = new ComboBox();
            status_c.Text = cur_title.status.ToString();
            status_c.Width = 75;
            status_c.Items.AddRange(new object[] {
            "completed",
            "backlog",
            "retired"});
            status_c.SelectedIndexChanged += StatusCSelectedIndexChanged;
            status_c.Location = new Point(statusLabel.Left + 80, statusLabel.Top);
            content_panel.Controls.Add(status_c);

            Button deleteButton = new Button();
            deleteButton.Text = "Delete this title";
            deleteButton.Width = 125;
            deleteButton.Location = new Point(statusLabel.Left, statusLabel.Top + 120);
            deleteButton.Click += deleteButtonClick;
            content_panel.Controls.Add(deleteButton);
            if (mode != "games")
            {
                Label genresLabel = new Label();
                genresLabel.Location = new Point(nameLabel.Left, status_c.Top + 25);
                string str = "";
                switch (mode)
                {
                    case "films":
                        str = BuildStingGenres(cur_film);
                        break;
                    case "tvseries":
                        str = BuildStingGenres(cur_tvseries);
                        break;
                }
                genresLabel.Width = 225;
                genresLabel.Height += 9 * (str.Length / 40);
                genresLabel.Text = str.ToString();
                content_panel.Controls.Add(genresLabel);

                switch (mode)
                {
                    case "tvseries":
                        Label seasonsLabel = new Label();
                        seasonsLabel.Text = "Select Season:";
                        seasonsLabel.Width = 80;
                        seasonsLabel.Location = new Point(nameLabel.Left, genresLabel.Bottom + 3);
                        content_panel.Controls.Add(seasonsLabel);

                        ComboBox seasons_c = new ComboBox();
                        seasons_c.Location = new Point(seasonsLabel.Left + seasonsLabel.Width, seasonsLabel.Top);
                        seasons_c.Width = status_c.Width;
                        int i = 0;
                        var a = new object[cur_tvseries.seasons.Count];
                        foreach (var season in cur_tvseries.seasons)
                        {
                            a[i] = season.Key;
                            i++;
                        }
                        seasons_c.Items.AddRange(a);
                        seasons_c.SelectedIndexChanged += SeasonsCSelectedIndexChanged;
                        content_panel.Controls.Add(seasons_c);
                        break;
                }
            }
            this.Controls.Add(content_panel);
        }
        public void AddButtons<T>(List<T> titles, int y = 0) where T : Title
        {
            list_panel.Controls.Clear();
            list_panel.Location = new Point(ByYearButton.Left, YearSortBox.Bottom + 25);
            list_panel.Width = 800;
            list_panel.AutoScroll = true;
            list_panel.Height = 351;
            int i = 1;
            foreach (var g in titles)
            {
                Button button = new Button();
                button.Left = 250 * ((i - 1) % 3);
                button.Top = y;
                button.Width = 240;
                button.Height = 25;
                button.Name = "btn" + i;
                button.Text = g.name;
                button.BackColor = GetColor(g.score);
                button.Click += ButtonOnClick;
                button.ForeColor = Color.Black;
                button.Font = new Font("Arial", 8, FontStyle.Bold);
                list_panel.Controls.Add(button);
                if (i % 3 == 0)
                    y += button.Height + 2;
                i++;
            }

            this.Controls.Add(list_panel);
        }
        public void ResetYears()
        {
            YearSortBox.Items.Clear();
            var set = new SortedSet<int>();
            switch (mode)
            {
                case "games":
                    foreach (var g in games)
                        set.Add(g.year);
                    break;
                case "films":
                    foreach (var f in films)
                        set.Add(f.year);
                    break;
                case "tvseries":
                    foreach (var t in tvseries)
                        set.Add(t.year);
                    break;
            }
            object[] a = new object[set.Count];
            int i = 0;
            foreach (var s in set)
            {
                a[i] = s;
                i++;
            }
            YearSortBox.Items.AddRange(a);
            YearSortBox.SelectedIndex = 0;
        }
        public void ResetGenres()
        {
            GenreSortBox.Items.Clear();
            var set = new SortedSet<string>();
            switch (mode)
            {
                case "films":
                    foreach (var f in films)
                        foreach (var g in f.genres)
                            set.Add(g);
                    break;
                case "tvseries":
                    foreach (var t in tvseries)
                        foreach (var g in t.genres)
                            set.Add(g);
                    break;
            }
            object[] a = new object[set.Count];
            int i = 0;
            foreach (var s in set)
            {
                a[i] = s;
                i++;
            }
            GenreSortBox.Items.AddRange(a);
            GenreSortBox.SelectedIndex = 0;
        }
        public void ResetStatus()
        {
            //GenreSortBox.Items.Clear();
            //var set = new SortedSet<string>();
            StatusSortBox.SelectedIndex = 0;
        }
        private void ByYearButton_Click(object sender, EventArgs e)
        {
            ResetYears();

            YearSortBox.Visible = true;
            ScoreSortBox.Visible = false;
            GenreSortBox.Visible = false;
            StatusSortBox.Visible = false;

            YearSortBox.SelectedIndex = 0;

            list_panel.Controls.Clear();
            content_panel.Controls.Clear();
            YearSortBox_SelectedValueChanged(sender, e);
        }

        private void ByScoreButton_Click(object sender, EventArgs e)
        {
            YearSortBox.Visible = false;
            ScoreSortBox.Visible = true;
            GenreSortBox.Visible = false;
            StatusSortBox.Visible = false;

            ScoreSortBox.SelectedIndex = 0;

            list_panel.Controls.Clear();
            content_panel.Controls.Clear();
            ScoreSortBox_SelectedValueChanged(sender, e);
        }
        private void ByGenreButton_Click(object sender, EventArgs e)
        {
            ResetGenres();

            YearSortBox.Visible = false;
            ScoreSortBox.Visible = false;
            GenreSortBox.Visible = true;
            StatusSortBox.Visible = false;

            GenreSortBox.SelectedIndex = 0;

            list_panel.Controls.Clear();
            content_panel.Controls.Clear();
            GenreSortBox_SelectedValueChanged(sender, e);
        }
        private void ByStatusButton_Click(object sender, EventArgs e)
        {
            ResetStatus();

            YearSortBox.Visible = false;
            ScoreSortBox.Visible = false;
            GenreSortBox.Visible = false;
            StatusSortBox.Visible = true;

            StatusSortBox.SelectedIndex = 0;

            list_panel.Controls.Clear();
            content_panel.Controls.Clear();
            StatusSortBox_SelectedValueChanged(sender, e);
        }

        private void YearSortBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string cur_year = YearSortBox.SelectedItem.ToString();
            switch (mode)
            {
                case "games":
                    AddButtons(games.Where(x => x.year.ToString() == cur_year).ToList());
                    break;
                case "films":
                    AddButtons(films.Where(x => x.year.ToString() == cur_year).ToList());
                    break;
                case "tvseries":
                    AddButtons(tvseries.Where(x => x.year.ToString() == cur_year).ToList());
                    break;
            }
        }

        private void ScoreSortBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string cur_score = ScoreSortBox.SelectedItem.ToString();// System.DateTime.Now.Year.ToString();
            switch (mode)
            {
                case "games":
                    AddButtons(games.Where(x => x.score.ToString() == cur_score).ToList());
                    break;
                case "films":
                    AddButtons(films.Where(x => x.score.ToString() == cur_score).ToList());
                    break;
                case "tvseries":
                    AddButtons(tvseries.Where(x => x.score.ToString() == cur_score).ToList());
                    break;
            }
        }
        private void GenreSortBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string cur_genre = GenreSortBox.SelectedItem.ToString();// System.DateTime.Now.Year.ToString();
            switch (mode)
            {
                case "films":
                    AddButtons(films.Where(x => x.genres.Any(y => y == cur_genre)).ToList());
                    break;
                case "tvseries":
                    AddButtons(tvseries.Where(x => x.genres.Any(y => y == cur_genre)).ToList());
                    break;
            }
        }
        private void StatusSortBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string cur_status = StatusSortBox.SelectedItem.ToString().ToLower();
            switch (mode)
            {
                case "games":
                    AddButtons(games.Where(x => x.status == cur_status).ToList());
                    break;
                case "films":
                    AddButtons(films.Where(x => x.status == cur_status).ToList());
                    break;
                case "tvseries":
                    AddButtons(tvseries.Where(x => x.status == cur_status).ToList());
                    break;
            }
        }
        private void ModeBox_SelectedValueChanged(object sender, EventArgs e)
        {

            string nmode = ModeBox.SelectedItem.ToString().ToLower();
            if (nmode == mode)
                return;
            Console.WriteLine(nmode);
            list_panel.Controls.Clear();
            content_panel.Controls.Clear();
            switch (nmode)
            {
                case "games":
                    ByYearButton.Visible = true;
                    ByScoreButton.Visible = true;
                    ByGenreButton.Visible = false;
                    ByStatusButton.Visible = true;
                    break;
                case "films":
                    ByYearButton.Visible = true;
                    ByScoreButton.Visible = true;
                    ByGenreButton.Visible = true;
                    ByStatusButton.Visible = true;
                    break;
                case "tvseries":
                    ByYearButton.Visible = true;
                    ByScoreButton.Visible = true;
                    ByGenreButton.Visible = true;
                    ByStatusButton.Visible = true;
                    break;
            }
            mode = nmode;
            ByYearButton_Click(sender, e);

            UpdateLabel2();
        }

    }
}