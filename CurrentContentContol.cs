using hltb.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace hltb
{
    public partial class CurrentContentContol : UserControl
    {
        private Content content;
        private mode currentMode;
        private EFGenericRepository<Status> statusRepository;
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

        public CurrentContentContol(mode cm, Content content)
        {
            InitializeComponent();
            statusRepository = new EFGenericRepository<Status>(new TitleCounterContext());

            this.content = content;

            currentMode = cm;
            titlePicture.Image = new Bitmap(DataManager.PATH + "\\data\\images\\"
                + currentMode.ToString().ToLower() + "\\"
                + content.Id + " " + content.FixedTitle + ".jpg");
            nameLabel.Text = GetFullName();

            timeLabel.Location = new Point(nameLabel.Location.X, nameLabel.Bottom + 5);

            timeHour.Text = (content.Time / 60).ToString();
            timeHourLabel.Top = timeHour.Top = timeMinute.Top = timeMinuteLabel.Top = timeLabel.Top;
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

            score_c.Width = 75;
            score_c.Items.AddRange(new object[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            score_c.SelectedItem = ((int)content.Score);
            score_c.Location = new Point(scoreLabel.Left + 80, scoreLabel.Top);

            statusLabel.Text = "Status:";
            statusLabel.Location = new Point(nameLabel.Left, scoreLabel.Bottom + 5); ;
            statusLabel.Width = 75;

            status_c.Width = 192;
            status_c.Items.AddRange(statusRepository.Get().OrderBy(x => x.Id).Select(x=>x.Name).ToArray() );
            status_c.SelectedIndex = (int)content.StatusId - 1;
            status_c.Location = new Point(statusLabel.Left + 80, statusLabel.Top);

            completitionLabel.Location = new Point(statusLabel.Left, statusLabel.Bottom + 5);

            competitionDay.Text = content.DateCompleted.Day.ToString();
            competitionDay.Location = new Point(completitionLabel.Right + 10, completitionLabel.Top);
            competitionMonth.Text = content.DateCompleted.Month.ToString();
            competitionMonth.Location = new Point(competitionDay.Right + 10, completitionLabel.Top);
            competitionYear.Text = content.DateCompleted.Year.ToString();
            competitionYear.Location = new Point(competitionMonth.Right + 10, completitionLabel.Top);

            noteTextBox.Top = completitionLabel.Bottom + 15;
            if (content.Note is not null)
                noteTextBox.Text = content.Note;
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
                //}
            }
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
            if (!int.TryParse(textbox.Text + symb.ToString(), out int hours)
                && (e.KeyChar != 8)
                && (currentMode == mode.GAMES))
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


        private void deleteButton_Click(object sender, EventArgs eventArgs)
        {
            Controls.Clear();
            
            ((Mainform)this.Parent.Parent).RemoveContent(currentMode, content.Id);
            // TODO Image delete
            Image img = titlePicture.Image;
            titlePicture = null;
            img.Dispose();
            //DataManager.DeleteImage(currentMode, content);

            content = new Content();
        }

        private void nameLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = content.LinkUrl,
                UseShellExecute = true
            });
        }

        private void UpdateContent()
        {
            switch (currentMode)
            {
                case mode.GAMES: { gameRepository.Update(content as Game); break; }
                case mode.FILMS: { filmRepository.Update(content as Film); break; }
            }
            ((Mainform)this.TopLevelControl).RefreshTitles(currentMode);
        }

        /// <summary>
        /// Save all changes of the current content to the database
        /// </summary>
        private void saveButton_Click(object sender, EventArgs e)
        {
            // hours, minutes
            int hours = int.Parse(timeHour.Text);
            int minutes = int.Parse(timeMinute.Text);
            content.Time = hours * 60 + minutes;
            // score
            content.Score = (int)score_c.SelectedItem;
            // status
            var status = status_c.SelectedItem.ToString();
            content.StatusId = statusRepository.Get(x=>x.Name==status).First().Id;
            // competition date
            int day = competitionDay.SelectedIndex + 1;
            int month = competitionMonth.SelectedIndex + 1;
            int year = int.Parse(competitionYear.Text);
            content.DateCompleted = new DateOnly(year, month, day);
            // note
            content.Note = noteTextBox.Text;

            UpdateContent();
        }
    }
}
