using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

namespace hltb
{
    public partial class CurrentTitleContol : UserControl
    {
        private Content content;
        private mode currentMode;
        private EFGenericRepository<Game> gameRepository = new EFGenericRepository<Game>(new TitleCounterContext());
        private EFGenericRepository<Film> filmRepository = new EFGenericRepository<Film>(new TitleCounterContext());

        /// <summary>
        /// Divide title into rows and concat with RusName for films
        /// </summary>
        /// <returns>String splitted into rows</returns>
        private string GetFullName()
        {
            StringBuilder res = new StringBuilder("");
            var ss = content.Title.Split(' ');
            foreach (var part in ss)
            {
                if ((res.Length % 30 + part.Length + 1) >= 30)
                    res.Append('\n');
                res.Append(part + ' ');
            }

            if (content is Film f && f.RusTitle != null)
            {
                res.Append(" || ");
                foreach (var part in f.RusTitle.Split(' '))
                {
                    if ((res.Length % 30 + part.Length + 1) >= 30)
                        res.Append('\n');
                    res.Append(part + ' ');
                }
            }
            return res.ToString();
        }

        private void SeasonsCSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            var e = GetFullName();
            var combobox = (ComboBox)sender;
            var s = combobox.SelectedItem.ToString();
            Controls.RemoveByKey("episodesLabel");
            Label episodesLabel = new Label();
            episodesLabel.Name = "episodesLabel";
            //episodesLabel.Text = $"Episodes count: {(content as TVSeries).Seasons[int.Parse(combobox.SelectedItem.ToString())]}";
            episodesLabel.Width = 125;
            episodesLabel.Location = new Point(combobox.Left + combobox.Width, combobox.Top);
            Controls.Add(episodesLabel);
        }

        public CurrentTitleContol(Content content, mode cm)
        {
            InitializeComponent();

            switch (cm)
            {
                case mode.GAMES: { this.content = gameRepository.FindById(content.Id); break; }
                case mode.FILMS: { this.content = filmRepository.FindById(content.Id); break; }
            }

            currentMode = cm;
            titlePicture.Image = new Bitmap(DataFiles.PATH + "\\data\\images\\" 
                + currentMode.ToString().ToLower() + "\\" 
                + content.FixedTitle + ".jpg");
            nameLabel.Text = GetFullName();

            timeLabel.Location = new Point(nameLabel.Location.X, nameLabel.Bottom + 5);

            timeHour.Text = (content.Time / 60).ToString();
            timeMinute.Text = (content.Time % 60).ToString();
            switch (currentMode)
            {
                case mode.GAMES:
                    break;
                // TODO Restrict updating time
                case mode.FILMS:
                    break;
                case mode.TVSERIES:
                    break;
            }
            releaseLabel.Text = $"Release:  {content.DateRelease.Day}.{content.DateRelease.Month}.{content.DateRelease.Year}";
            releaseLabel.Location = new Point(nameLabel.Left, timeLabel.Bottom + 5);
            releaseLabel.Width = 200;

            scoreLabel.Location = new Point(nameLabel.Left, releaseLabel.Bottom + 5);
            scoreLabel.Width = 75;

            score_c.Text = content.Score.ToString();
            score_c.Width = 75;
            score_c.Items.AddRange(new object[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            score_c.Location = new Point(scoreLabel.Left + 80, scoreLabel.Top);

            statusLabel.Text = "Status:";
            statusLabel.Location = new Point(nameLabel.Left, scoreLabel.Bottom + 5); ;
            statusLabel.Width = 75;

            status_c.Text = content.Status.Name.ToLower();
            status_c.Width = 192;
            status_c.Items.AddRange(new object[] {
            "Completed",
            "Backlog",
            "Retired",
            "In progress"});
            status_c.Location = new Point(statusLabel.Left + 80, statusLabel.Top);

            completitionLabel.Location = new Point(statusLabel.Left, statusLabel.Bottom + 5);

            competitionDay.Text = content.DateCompleted.Day.ToString();
            competitionDay.Location = new Point(completitionLabel.Right + 10, completitionLabel.Top);
            competitionMonth.Text = content.DateCompleted.Month.ToString();
            competitionMonth.Location = new Point(competitionDay.Right + 10, completitionLabel.Top);
            competitionYear.Text = content.DateCompleted.Year.ToString();
            competitionYear.Location = new Point(competitionMonth.Right + 10, completitionLabel.Top);
            {
                //if (currentMode != mode.GAMES)
                //{
                //    Label genresLabel = new Label();
                //    genresLabel.Location = new Point(nameLabel.Left, statusLabel.Bottom + 5);
                //    string str = BuildStingGenres(content as Film);
                //    genresLabel.Width = 22500;
                //    genresLabel.Text = str.ToString();
                //    genresLabel.Font = new Font("Microsoft Tai Le", 16, FontStyle.Bold);
                //    Controls.Add(genresLabel);

                //    if (content is TVSeries tVSeries)
                //    {
                //        Label seasonsLabel = new Label();
                //        seasonsLabel.Text = "Select Season:";
                //        seasonsLabel.Font = new Font("Microsoft Tai Le", 16, FontStyle.Bold);
                //        Graphics g = CreateGraphics();
                //        seasonsLabel.Width = (int)Math.Ceiling(g.MeasureString(seasonsLabel.Text + " ", seasonsLabel.Font).Width) + 10;
                //        Console.WriteLine(seasonsLabel.Width);
                //        seasonsLabel.Location = new Point(nameLabel.Left, genresLabel.Bottom + 5);
                //        Controls.Add(seasonsLabel);

                //        ComboBox seasons_c = new ComboBox();
                //        seasons_c.Location = new Point(seasonsLabel.Right + 5, seasonsLabel.Top);
                //        seasons_c.Width = status_c.Width;
                //        seasons_c.Font = new Font("Microsoft Tai Le", 14, FontStyle.Bold);
                //        int i = 0;
                //        var a = new object[tVSeries.Seasons.Count];
                //        foreach (var season in tVSeries.Seasons)
                //        {
                //            a[i] = season.Key;
                //            i++;
                //        }
                //        seasons_c.Items.AddRange(a);
                //        seasons_c.SelectedIndexChanged += SeasonsCSelectedIndexChanged;
                //        Controls.Add(seasons_c);
                //    }
                //}
            }
            deleteButton.Text = "Delete this title";
            //deleteButton.Location = new Point(statusLabel.Left, 600);
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(content.Title);
        }

        private void TimeHourCKeyPress(object sender, KeyPressEventArgs e)
        {
            var textbox = (TextBox)sender;
            char symb = e.KeyChar;
            if (!int.TryParse(textbox.Text + symb.ToString(), out int hours) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        private void TimeHourCTextChanged(object sender, EventArgs eventArgs)
        {
            var textbox = (TextBox)sender;
            if (!int.TryParse(textbox.Text, out int a))
            {
                textbox.Text = "0";
            }
        }
        // TODO
        //public string BuildStingGenres<T>(T m) where T : Film
        //{
        //    StringBuilder str = new StringBuilder();
        //    str.Append("Genres:");
        //    int len = str.Length;
        //    foreach (var gen in m.Genres)
        //    {
        //        if ((len + gen.Length + 2) / 40 < 1)
        //        {
        //            str.Append(" " + gen + ";");
        //            len = str.Length;
        //        }
        //        else
        //        {
        //            str.Append("\n              " + gen + ";");
        //            len = str.Length - len;
        //        }
        //    }
        //    return str.ToString();
        //}

        //TODO Erase deleted title's image
        private void deleteButton_Click(object sender, EventArgs eventArgs)
        {
            Controls.Clear();
            (this.Parent.Parent as Mainform).RemoveContent(content, currentMode);
            content = new Content();
        }

        private void nameLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(content.LinkUrl);
        }

        private void score_c_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combobox = (ComboBox)sender;
            var s = (int)combobox.SelectedItem;
            content.Score = s;
            UpdateContent();
        }

        private void status_c_SelectedIndexChanged(object sender, EventArgs e)
        {
            var status = status_c.SelectedItem.ToString();
            TitleStatus ts = TitleStatus.BACKLOG;
            System.Enum.TryParse(status.ToUpper(), out ts);
            content.StatusId = (int)ts;
            UpdateContent();
        }

        private void timeMinute_Leave(object sender, EventArgs e)
        {
            int minutes = int.Parse(timeMinute.Text);
            content.Time = (content.Time / 60) * 60 + minutes;
            UpdateContent();
        }

        private void timeHour_Leave(object sender, EventArgs e)
        {
            int hours = int.Parse(timeHour.Text);
            content.Time = content.Time % 60 + hours * 60;
            UpdateContent();
        }

        private void competitionDateChanged(object sender, EventArgs e)
        {
            int day = competitionDay.SelectedIndex + 1;
            int month = competitionMonth.SelectedIndex + 1;
            int year = int.Parse(competitionYear.Text);
            content.DateCompleted = new DateOnly(year, month, day);
            UpdateContent();
        }

        private void UpdateContent()
        {
            switch (currentMode)
            {
                case mode.GAMES: { gameRepository.Update(content as Game); break; }
                case mode.FILMS: { filmRepository.Update(content as Film); break; }
            }
            (this.TopLevelControl as Mainform).RefreshTitles(currentMode);
        }
    }
}
