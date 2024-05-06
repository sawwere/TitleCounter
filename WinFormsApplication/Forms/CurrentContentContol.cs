using hltb.Models;
using hltb.Service;
using System;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace hltb
{
    public partial class CurrentContentContol : UserControl
    {
        private Content content;
        Mainform parent;

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
                if ((res.Length % 27 + part.Length + 3) >= 29)
                {
                    res.Append("...");
                    break;
                }
                res.Append(part + ' ');
            }

            //if (content is Film f && f.RusTitle != null)
            //{
            //    res.Append(" || ");
            //    foreach (var part in f.RusTitle.Split(' '))
            //    {
            //        if ((res.Length % 30 + part.Length + 1) >= 30)
            //            res.Append('\n');
            //        res.Append(part + ' ');
            //    }
            //}
            return res.ToString();
        }


        public CurrentContentContol(Mainform owner, Content content)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            parent = owner;

            this.content = content;
            var tmp = $@"http://localhost:8080/images/{owner.modeState.ToString()}/{content.Id}.jpg";

            titlePicture.Image = RestApiSerice.Instance.GetImage(tmp);
            nameLabel.Text = GetFullName();

            //timeLabel.Location = new Point(nameLabel.Location.X, nameLabel.Bottom + 5);

            timeHour.Text = (content.Time / 60).ToString();
            //timeHourLabel.Top = timeHour.Top = timeMinute.Top = timeMinuteLabel.Top = timeLabel.Top;
            timeMinute.Text = (content.Time % 60).ToString();

            releaseLabel.Text = $"Release:  {content.DateRelease.Day}.{content.DateRelease.Month}.{content.DateRelease.Year}";
            //releaseLabel.Location = new Point(nameLabel.Left, timeLabel.Bottom + 5);
            releaseLabel.Width = 200;

            //scoreLabel.Location = new Point(nameLabel.Left, releaseLabel.Bottom + 5);
            scoreLabel.Width = 75;

            score_c.Width = 75;
            score_c.Items.AddRange(new object[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            score_c.SelectedItem = ((int)content.Score);
            //score_c.Location = new Point(scoreLabel.Left + 80, scoreLabel.Top);

            statusLabel.Text = "Status:";
            //statusLabel.Location = new Point(nameLabel.Left, scoreLabel.Bottom + 5); ;
            statusLabel.Width = 75;

            status_c.Width = 192;
            status_c.Items.AddRange(new string[] { "completed", "backlog", "retired", "in progress"});
            status_c.SelectedIndex = status_c.Items.IndexOf(content.Status);
            //status_c.Location = new Point(statusLabel.Left + 80, statusLabel.Top);

            //completitionLabel.Location = new Point(statusLabel.Left, statusLabel.Bottom + 5);

            competitionDay.Text = content.DateCompleted.Day.ToString();
            //competitionDay.Location = new Point(completitionLabel.Right + 10, completitionLabel.Top);
            competitionMonth.Text = content.DateCompleted.Month.ToString();
            //competitionMonth.Location = new Point(competitionDay.Right + 10, completitionLabel.Top);
            competitionYear.Text = content.DateCompleted.Year.ToString();
            //competitionYear.Location = new Point(competitionMonth.Right + 10, completitionLabel.Top);
            //competiotionButtonToday.Location = new Point(competitionYear.Right + 10, competitionYear.Top);


            noteTextBox.Top = completitionLabel.Bottom + 15;
            if (content.Note is not null)
                noteTextBox.Text = content.Note;
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
                && (parent.modeState is GameService))
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

        private void deleteButton_Click(object sender, EventArgs eventArgs)
        {
            Controls.Clear();

            ((Mainform)this.Parent.Parent).RemoveContent(content.Id);
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
            parent.modeState.Update(content);
            ((Mainform)this.TopLevelControl).RefreshTitles();
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
            var status = status_c.SelectedItem is null ? status_c.Items[0].ToString() : status_c.SelectedItem.ToString();
            content.Status = status;
            // competition date
            int day = competitionDay.SelectedIndex + 1;
            int month = competitionMonth.SelectedIndex + 1;
            int year = int.Parse(competitionYear.Text);
            content.DateCompleted = new DateOnly(year, month, day);
            // note
            content.Note = noteTextBox.Text;

            UpdateContent();
        }

        private void competiotionButtonToday_Click(object sender, EventArgs e)
        {
            var today = DateTime.Today;
            competitionDay.SelectedIndex = today.Day - 1;
            competitionMonth.SelectedIndex = today.Month - 1;
            competitionYear.Text = today.Year.ToString();
        }
    }
}
