using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hltb
{
    public partial class CurrentTitleContol : UserControl
    {
        private Title title;
        private mode currentMode;

        private string GetFullName()
        {
            StringBuilder res = new StringBuilder( "");
            var ss = title.Name.Split(' ');//.Concat((title as Film).Rus_Name.Split(' '));
            foreach (var part in ss)
            {
                if ((res.Length % 30 + part.Length + 1) >= 30)
                    res.Append('\n');
                res.Append(part + ' ');
            }
            
            if (title is Film f)
            {
                res.Append(" || ");
                foreach (var part in f.Rus_Name.Split(' '))
                {
                    if ((res.Length % 30 + part.Length + 1) >= 30)
                        res.Append('\n');
                    res.Append(part + ' ');
                }
            }


            return res.ToString();
        }

        private string GetSafeName()
        {
            char[] proh = { '<', '>', ':', '"', '"', '/', '\\', '|', '?', '*' };
            return new string(title.Name.Where(x => !proh.Contains(x)).ToArray());

        }

        private void SeasonsCSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            var combobox = (ComboBox)sender;
            var s = combobox.SelectedItem.ToString();
            Controls.RemoveByKey("episodesLabel");
            Label episodesLabel = new Label();
            episodesLabel.Name = "episodesLabel";
            episodesLabel.Text = $"Episodes count: {(title as TVSeries).Seasons[int.Parse(combobox.SelectedItem.ToString())]}";
            episodesLabel.Width = 125;
            episodesLabel.Location = new Point(combobox.Left + combobox.Width, combobox.Top);
            Controls.Add(episodesLabel);
        }

        public CurrentTitleContol(Title title, mode cm)
        {
            InitializeComponent();
            this.title = title;
            currentMode = cm;
            string safeName = GetSafeName();
            titlePicture.Image = new Bitmap(DataFiles.path + "\\data\\images\\" + currentMode.ToString().ToLower() + "\\" + safeName + ".jpg");
            nameLabel.Text = "Title name: " + title.Name;
            //if (currentMode != mode.GAMES)
                nameLabel.Text = GetFullName();

            timeLabel.Location = new Point(nameLabel.Location.X, nameLabel.Bottom + 5);
            switch (currentMode)
            {
                case mode.GAMES:
                    TextBox time_c = new TextBox();
                    time_c.Text = GetTime(title);
                    time_c.Font = new Font("Microsoft Tai Le", 14, FontStyle.Bold);
                    time_c.Width = 75;
                    time_c.Location = new Point(timeLabel.Left + 80, timeLabel.Top);
                    time_c.KeyPress += TimeCKeyPress;
                    time_c.TextChanged += TimeCTextChanged;
                    Controls.Add(time_c);
                    break;
                case mode.FILMS:
                    timeLabel.Width = 200;
                    timeLabel.Text += GetTime(title);
                    break;
                case mode.TVSERIES:
                    timeLabel.Width = 200;
                    timeLabel.Text += GetTime(title);
                    break;
            }
            yearLabel.Text = $"Year:                 {title.Year}";
            yearLabel.Location = new Point(nameLabel.Left, timeLabel.Bottom + 5);
            yearLabel.Width = 200;

            scoreLabel.Location = new Point(nameLabel.Left, yearLabel.Bottom + 5);
            scoreLabel.Width = 75;

            score_c.Text = title.Score.ToString();
            score_c.Font = new Font("Microsoft Tai Le", 14, FontStyle.Bold);
            score_c.Width = 75;
            score_c.Items.AddRange(new object[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            score_c.Location = new Point(scoreLabel.Left + 80, scoreLabel.Top);

            statusLabel.Text = "Status:";
            statusLabel.Location = new Point(nameLabel.Left, scoreLabel.Bottom + 5); ;
            statusLabel.Width = 75;

            status_c.Text = title.Status.ToString().ToLower();
            status_c.Font = new Font("Microsoft Tai Le", 14, FontStyle.Bold);
            status_c.Width = 128;
            status_c.Items.AddRange(new object[] {
            "COMPLETED",
            "BACKLOG",
            "RETIRED"});
            status_c.Location = new Point(statusLabel.Left + 80, statusLabel.Top);

            if (currentMode != mode.GAMES)
            {
                Label genresLabel = new Label();
                genresLabel.Location = new Point(nameLabel.Left, statusLabel.Bottom + 5);
                string str = BuildStingGenres(title as Film);
                genresLabel.Width = 22500;
                genresLabel.Text = str.ToString();
                genresLabel.Font = new Font("Microsoft Tai Le", 16, FontStyle.Bold);
                Controls.Add(genresLabel);

                if (title is TVSeries tVSeries)
                {
                    Label seasonsLabel = new Label();
                    seasonsLabel.Text = "Select Season:";
                    seasonsLabel.Font = new Font("Microsoft Tai Le", 16, FontStyle.Bold);
                    Graphics g = CreateGraphics();
                    seasonsLabel.Width = (int)Math.Ceiling(g.MeasureString(seasonsLabel.Text + " ", seasonsLabel.Font).Width) + 10;
                    Console.WriteLine(seasonsLabel.Width);
                    seasonsLabel.Location = new Point(nameLabel.Left, genresLabel.Bottom + 5);
                    Controls.Add(seasonsLabel);

                    ComboBox seasons_c = new ComboBox();
                    seasons_c.Location = new Point(seasonsLabel.Right + 5, seasonsLabel.Top);
                    seasons_c.Width = status_c.Width;
                    seasons_c.Font = new Font("Microsoft Tai Le", 14, FontStyle.Bold);
                    int i = 0;
                    var a = new object[tVSeries.Seasons.Count];
                    foreach (var season in tVSeries.Seasons)
                    {
                        a[i] = season.Key;
                        i++;
                    }
                    seasons_c.Items.AddRange(a);
                    seasons_c.SelectedIndexChanged += SeasonsCSelectedIndexChanged;
                    Controls.Add(seasons_c);
                }
            }

            deleteButton.Text = "Delete this title";
            //deleteButton.Location = new Point(statusLabel.Left, 600);
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(title.Name);
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
            (title as Game).Time = s;
        }

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

        //TODO Erase deleted title's image
        private void deleteButton_Click(object sender, EventArgs eventArgs)
        {
            Controls.Clear();
            (this.Parent.Parent as Mainform).RemoveTitle(title, currentMode);
            title = new Title();
        }

        private void nameLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(title.Link);
        }

        private void score_c_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combobox = (ComboBox)sender;
            var s = (int)combobox.SelectedItem;
            title.Score = s;
        }

        private void status_c_SelectedIndexChanged(object sender, EventArgs e)
        {
            var status = status_c.SelectedItem.ToString();
            TitleStatus ts = TitleStatus.BACKLOG;
            System.Enum.TryParse(status.ToUpper(), out ts);
            title.Status = ts;
        }
    }
}
