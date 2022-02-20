using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using static hltb.DataFiles;

namespace hltb
{
    public enum mode { GAMES, FILMS, TVSERIES };
    public partial class Mainform : Form
    {
        Panel list_panel = new Panel();

        Dictionary<mode, List<Title>> titles;
        Title cur_title;

        mode currentMode = mode.GAMES;


        private void OnApplicationExit(object sender, EventArgs e)
        {
            SaveGames(titles[mode.GAMES]);
            SaveFilms(titles[mode.FILMS]);
            SaveTVSeries(titles[mode.TVSERIES]);
        }

        void UpdateStatisticsLabel()
        {
            double total = 1, cmpltd = 1, tmcmpltd = 1, tmtotal = 1;
            total = titles[currentMode].Count() + 0.0;
            cmpltd = titles[currentMode].Where(x => (x.Status == TitleStatus.COMPLETED) || (x.Status == TitleStatus.RETIRED)).Count();
            tmcmpltd = titles[currentMode].Where(x => (x.Status == TitleStatus.COMPLETED) || (x.Status == TitleStatus.RETIRED)).Select(x => x.Time).Sum();
            tmtotal = titles[currentMode].Select(x => x.Time).Sum();

            statisticsLabel.Text = $"Completed: {cmpltd} / {total}  ({(cmpltd / total * 100):F2}%)" + '\n'
                + $"Time : {tmcmpltd} / {tmtotal} ({(tmcmpltd / tmtotal * 100):F2}%)";
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
            CheckDataFiles();

            titles = new Dictionary<mode, List<Title>>(3);

            titles[mode.GAMES] = GetGames();
            titles[mode.FILMS] = GetFilms();
            titles[mode.TVSERIES] = GetTVSeries();

            ResetYears();
            UpdateStatisticsLabel();

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
                string title = namebox.Text + "#" + statusbox.Text + "#" + scorebox.SelectedItem + '#' + currentMode.ToString();
                string pathToFind = path;
                for ( int i = 0; i < 2; i++)
                {
                    int pos = pathToFind.LastIndexOf("\\");
                    pathToFind = pathToFind.Remove(pos, pathToFind.Length - pos);
                }
                Process p = Process.Start(new ProcessStartInfo
                {
                    FileName = "F:/programs/Python/python.exe",
                    Arguments = pathToFind + "/python_part/find.py \"" + title + "\"",
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
                    switch (currentMode)
                    {
                        case mode.GAMES:
                            titles[mode.GAMES] = GetGames();
                            break;
                        case mode.FILMS:
                            titles[mode.FILMS] = GetFilms();
                            break;
                        case mode.TVSERIES:
                            titles[mode.TVSERIES] = GetTVSeries();
                            break;
                    }
                }
            }
            operationLabel.Text = status.ToString();
            UpdateStatisticsLabel();
        }
        
        private void ScoreCSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            var combobox = (ComboBox)sender;
            var s = (int)combobox.SelectedItem;
            cur_title.Score = s;
        }
        private void StatusCSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            var combobox = (ComboBox)sender;
            TitleStatus s; System.Enum.TryParse(combobox.SelectedItem.ToString().ToLower(), out s);
            cur_title.Status = s;
        }
        
        private void SeasonsCSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            var combobox = (ComboBox)sender;
            var s = combobox.SelectedItem.ToString();
            currentTitlePanel.Controls.RemoveByKey("episodesLabel");
            Label episodesLabel = new Label();
            episodesLabel.Name = "episodesLabel";
            episodesLabel.Text = $"Episodes count: {(cur_title as TVSeries).Seasons[int.Parse(combobox.SelectedItem.ToString())]}";
            episodesLabel.Width = 125;
            episodesLabel.Location = new Point(combobox.Left + combobox.Width, combobox.Top);
            currentTitlePanel.Controls.Add(episodesLabel);
        }
        
        //TODO Erase deleted title's image
        private void deleteButtonClick(object sender, EventArgs eventArgs)
        {
            currentTitlePanel.Controls.Clear();
            titles[currentMode].Remove(cur_title);
            cur_title = new Title();
            switch (currentMode)
            {
                case mode.GAMES:

                    SaveGames(titles[mode.GAMES]);
                    break;
                case mode.FILMS:
                    SaveFilms(titles[mode.FILMS]);
                    break;
                case mode.TVSERIES:
                    SaveTVSeries(titles[mode.TVSERIES]);
                    break;
            }
            UpdateStatisticsLabel();
        }
        //TODO Fix 
        public string BuildStingGenres<T>(T m) where T : Film
        {
            StringBuilder str = new StringBuilder();
            str.Append("Genres:");
            int len = str.Length;
            foreach (var gen in m.Genres)
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

        public string GetTime<T>(T t) where T : Title
        {
            string res = "";
            switch (currentMode)
            {
                case mode.GAMES:
                    res = t.Time.ToString();
                    break;
                case mode.FILMS:
                    var h = (int)t.Time / 60;
                    var m = (int)t.Time % 60;
                    res = $"                {h}h {m}m";
                    break;
                case mode.TVSERIES:
                    h = (int)t.Time / 60;
                    m = (int)t.Time % 60;
                    res = $"                {h}h {m}m";
                    break;
            }
            return res;
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            currentTitlePanel.Controls.Clear();

            

            var button = (Button)sender;

            var g = button.Text.Where(x => (x != ':') & (x != '/')).ToArray();
            StringBuilder s = new StringBuilder();
            foreach (var ss in g)
            {
                s.Append(ss.ToString());
            }

            cur_title = titles[currentMode].Find(x => x.Name == button.Text);
            currentTitlePanel.Controls.Add(new CurrentTitleContol(cur_title, currentMode));

            //Label yearLabel = new Label();
            //yearLabel.Text = $"Year:                 {cur_title.Year}";
            //yearLabel.Location = new Point(nameLabel.Left, timeLabel.Top + 25);
            //yearLabel.Width = 200;
            //currentTitlePanel.Controls.Add(yearLabel);

            //Label scoreLabel = new Label();
            //scoreLabel.Text = "Score:";
            //scoreLabel.Location = new Point(nameLabel.Left, yearLabel.Top + 25);
            //scoreLabel.Width = 75;
            //currentTitlePanel.Controls.Add(scoreLabel);

            //Label statusLabel = new Label();
            //statusLabel.Text = "Status:";
            //statusLabel.Location = new Point(nameLabel.Left, scoreLabel.Top + 25); ;
            //statusLabel.Width = 75;
            //currentTitlePanel.Controls.Add(statusLabel);

            //ComboBox score_c = new ComboBox();
            //score_c.Text = cur_title.Score.ToString();
            //score_c.Width = 75;
            //score_c.Items.AddRange(new object[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            //score_c.SelectedIndexChanged += ScoreCSelectedIndexChanged;
            //score_c.Location = new Point(scoreLabel.Left + 80, scoreLabel.Top);
            //currentTitlePanel.Controls.Add(score_c);

            //ComboBox status_c = new ComboBox();
            //status_c.Text = cur_title.Status.ToString().ToLower();
            //status_c.Width = 75;
            //status_c.Items.AddRange(new object[] {
            //"completed",
            //"backlog",
            //"retired"});
            //status_c.SelectedIndexChanged += StatusCSelectedIndexChanged;
            //status_c.Location = new Point(statusLabel.Left + 80, statusLabel.Top);
            //currentTitlePanel.Controls.Add(status_c);

            //Button deleteButton = new Button();
            //deleteButton.Text = "Delete this title";
            //deleteButton.Width = 125;
            //deleteButton.Location = new Point(statusLabel.Left, statusLabel.Top + 120);
            //deleteButton.Click += deleteButtonClick;
            //currentTitlePanel.Controls.Add(deleteButton);
            //if (currentMode != mode.GAMES)
            //{
            //    Label genresLabel = new Label();
            //    genresLabel.Location = new Point(nameLabel.Left, status_c.Top + 25);
            //    string str = BuildStingGenres(cur_title as Film);
            //    genresLabel.Width = 225;
            //    genresLabel.Height += 9 * (str.Length / 40);
            //    genresLabel.Text = str.ToString();
            //    currentTitlePanel.Controls.Add(genresLabel);

            //    if (cur_title is TVSeries tVSeries)
            //    {
            //        Label seasonsLabel = new Label();
            //        seasonsLabel.Text = "Select Season:";
            //        seasonsLabel.Width = 80;
            //        seasonsLabel.Location = new Point(nameLabel.Left, genresLabel.Bottom + 3);
            //        currentTitlePanel.Controls.Add(seasonsLabel);

            //        ComboBox seasons_c = new ComboBox();
            //        seasons_c.Location = new Point(seasonsLabel.Left + seasonsLabel.Width, seasonsLabel.Top);
            //        seasons_c.Width = status_c.Width;
            //        int i = 0;
            //        var a = new object[tVSeries.Seasons.Count];
            //        foreach (var season in tVSeries.Seasons)
            //        {
            //                a[i] = season.Key;
            //                i++;
            //        }
            //        seasons_c.Items.AddRange(a);
            //        seasons_c.SelectedIndexChanged += SeasonsCSelectedIndexChanged;
            //        currentTitlePanel.Controls.Add(seasons_c);
            //    }
            //}
            this.Controls.Add(currentTitlePanel);
        }
        public void AddButtons(List<Title> titles, int y = 0)
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
                button.Text = g.Name;
                button.BackColor = GetColor(g.Score);
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
            foreach (var g in titles[currentMode])
                set.Add(g.Year);
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
            //DANGER
            foreach (Film f in titles[currentMode])
            {
                foreach (var g in f.Genres)
                    set.Add(g);
            }
            object[] a = new object[set.Count];
            int i = 0;
            foreach (var s in set)
            {
                a[i++] = s;
            }
            GenreSortBox.Items.AddRange(a);
            GenreSortBox.SelectedIndex = 0;
        }
        public void ResetStatus()
        {
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
            currentTitlePanel.Controls.Clear();
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
            currentTitlePanel.Controls.Clear();
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
            currentTitlePanel.Controls.Clear();
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
            currentTitlePanel.Controls.Clear();
            StatusSortBox_SelectedValueChanged(sender, e);
        }
        
        private void YearSortBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string cur_year = YearSortBox.SelectedItem.ToString();
            AddButtons(titles[currentMode].Where(x => x.Year.ToString() == cur_year).ToList());
        }
        private void ScoreSortBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string cur_score = ScoreSortBox.SelectedItem.ToString();
            AddButtons(titles[currentMode].Where(x => x.Score.ToString() == cur_score).ToList());
        }
        private void GenreSortBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string cur_genre = GenreSortBox.SelectedItem.ToString();
            AddButtons(titles[currentMode].Where(x => (x as Film).Genres.Any(y => y == cur_genre)).ToList());
        }
        private void StatusSortBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string cur_status = StatusSortBox.SelectedItem.ToString().ToUpper();
            AddButtons(titles[currentMode].Where(x => x.Status.ToString() == cur_status).ToList());
        }

        private void ModeBox_SelectedValueChanged(object sender, EventArgs e)
        {

            string nmode = ModeBox.SelectedItem.ToString().ToLower();
            if (nmode == currentMode.ToString())
                return;
            Console.WriteLine(nmode);
            list_panel.Controls.Clear();
            currentTitlePanel.Controls.Clear();
            switch (nmode)
            {
                case "games":
                    ByYearButton.Visible = true;
                    ByScoreButton.Visible = true;
                    ByGenreButton.Visible = false;
                    ByStatusButton.Visible = true;
                    currentMode = mode.GAMES;
                    break;
                case "films":
                    ByYearButton.Visible = true;
                    ByScoreButton.Visible = true;
                    ByGenreButton.Visible = true;
                    ByStatusButton.Visible = true;
                    currentMode = mode.FILMS;
                    break;
                case "tvseries":
                    ByYearButton.Visible = true;
                    ByScoreButton.Visible = true;
                    ByGenreButton.Visible = true;
                    ByStatusButton.Visible = true;
                    currentMode = mode.TVSERIES;
                    break;
            }
            
            ByYearButton_Click(sender, e);

            UpdateStatisticsLabel();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}