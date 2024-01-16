using hltb.Models;
using System.Data;
using System.Text;

using static hltb.DataManager;

#pragma warning disable CS8601 // Возможно, назначение-ссылка, допускающее значение NULL.
namespace hltb
{
    public enum mode { GAMES, FILMS, TVSERIES };
    public enum filterCategory { YEAR, SCORE, STATUS, NAME, GENRE };
    public enum displayOption { BUTTONS, LINES, IMAGES };


    public partial class Mainform : Form
    {
        private Panel list_panel = new Panel();

        private EFGenericRepository<Status> statusRepository;
        private AddContent add_content = new AddContent();


        public ModeState modeState;
        private filterCategory filter = filterCategory.YEAR;
        private displayOption currentDisplayOption = displayOption.BUTTONS;
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("http://127.0.0.1:5000"),
        };


        private void OnApplicationExit(object sender, EventArgs e)
        {
            modeState.Save();
        }

        void UpdateStatisticsLabel()
        {
            double total = 1, cmpltd = 1, tmcmpltd = 1, tmtotal = 1;
            total = modeState.Contents.Count() + 0.0;
            cmpltd = modeState.Contents.Where(x => (x.StatusId == (int)TitleStatus.COMPLETED) || (x.StatusId == (int)TitleStatus.RETIRED)).Count();
            tmcmpltd = modeState.Contents.Where(x => (x.StatusId == (int)TitleStatus.COMPLETED) || (x.StatusId == (int)TitleStatus.RETIRED)).Select(x => (int)x.Time).Sum();
            tmtotal = modeState.Contents.Select(x => (int)x.Time).Sum();

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

        private void ChangeState(ModeState state)
        {
            this.modeState = state;
            modeState.Load();
            ResetYears();
            ResetStatus();
            ResetTitles();
            //RefreshTitles();
        }

        public void RefreshTitles()
        {
            //modeState.Load();
            switch (filter)
            {
                case filterCategory.YEAR:
                    {
                        string cur_year = YearSortBox.SelectedItem.ToString();
                        AddButtons(modeState.Contents.Where(x => x.DateRelease.Year.ToString() == cur_year).ToList());
                        break;
                    }

                case filterCategory.SCORE:
                    {
                        string cur_score = ScoreSortBox.SelectedItem.ToString();
                        AddButtons(modeState.Contents.Where(x => x.Score.ToString() == cur_score).ToList());
                        break;
                    }
                case filterCategory.STATUS:
                    {
                        string cur_status = StatusSortBox.SelectedItem.ToString().ToLower();
                        AddButtons(modeState.Contents.Where(x => x.Status.Name == cur_status).ToList());
                        break;
                    }
                case filterCategory.NAME:
                    {
                        char cur_letter = NameSortBox.SelectedItem.ToString().First();
                        AddButtons(modeState.Contents.Where(x => x.Title.ToString().First() == cur_letter).ToList());
                        break;
                    }
                case filterCategory.GENRE:
                    {
                        string cur_genre = GenreSortBox.SelectedItem.ToString();
                        //AddButtons(contents.Where(x => (x as TVSeries).Genres.Any(y => y == cur_genre)).ToList());
                        break;
                    }
            }
            UpdateStatisticsLabel();
        }

        public void RemoveContent(long id)
        {
            modeState.Remove(id);
            RefreshTitles();
        }

        public Mainform()
        {
            InitializeComponent();
            AddOwnedForm(add_content);
            statusRepository = new EFGenericRepository<Status>(new TitleCounterContext());
            ChangeState(new State<Game>(this));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckDataFiles();
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

        static async Task<byte[]> GetImageAsync(HttpClient httpClient, string imageUrl)
        {
            var response = await httpClient.GetStringAsync($"find/image?image_url={imageUrl}");
            return System.Convert.FromBase64String(response); ;
        }

        private async Task<Content> GetContentAsync(string title)
        {
            var response = await sharedClient.GetStringAsync($"find/{modeState.ToString()}?title={title}");
            var content = modeState.GetFromJson(response);
            if (content is null)
            {
                throw new ArgumentNullException("Invalid Json Deserialize");
            }
            content.StatusId = statusbox.SelectedIndex + 1;
            content.Score = scorebox.SelectedItem == null ? 0 : int.Parse(scorebox.SelectedItem.ToString());
            return content;
        }

        private async void addtitle_MouseDown(object sender, MouseEventArgs e)
        {
            StringBuilder status = new StringBuilder();
            if (namebox.Text == "")
            {
                status.Append("Error: empty name field");
            }
            else
            {
#pragma warning disable CS4014
                var content = await GetContentAsync(namebox.Text);
                var decodedImage = await Task.Run(() => GetImageAsync(sharedClient, content.ImageUrl));
#pragma warning restore CS4014
                char operationCode = '0';
                if (operationCode == '0')
                {
                    if (modeState.Contents.Where(x => x.Title == content.Title && x.DateRelease == content.DateRelease).Count() > 0)
                        operationCode = '2';
                }
                // send operation code to add_content Control, perform preparations
                add_content.SetStatus(operationCode);

                add_content.RecieveResponse(content.Title, decodedImage);
                if (add_content.ShowDialog() == DialogResult.OK)
                {
                    namebox.Text = "";
                    statusbox.SelectedIndex = 1;
                    scorebox.SelectedIndex = 0;
                    modeState.Create(content);
                    DataManager.SaveImage(modeState, content, decodedImage);
                }


            }
            //operationLabel.Text = status.ToString();
            UpdateStatisticsLabel();
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            currentTitlePanel.Controls.Clear();
            var button = (Button)sender;
            CurrentContentContol ccc = new CurrentContentContol(this, modeState.Contents.First(x => x.Id == (long)button.Tag));
            currentTitlePanel.Controls.Add(ccc);
            this.Controls.Add(currentTitlePanel);
        }
        private void AddButtons(List<Content> titles, int y = 5)
        {
            list_panel.Controls.Clear();
            list_panel.Location = new Point(ByYearButton.Left, displayButtonsButton.Bottom + 25);
            list_panel.Width = 800;
            list_panel.AutoScroll = true;
            list_panel.Height = 351;

            int buttonWidth = 1, buttonHeight = 1, colCount = 1;

            switch (currentDisplayOption)
            {
                case displayOption.BUTTONS:
                    {
                        buttonWidth = 250;
                        buttonHeight = 25;
                        colCount = 3;
                        break;
                    }
                case displayOption.LINES:
                    {
                        buttonWidth = 250;
                        buttonHeight = 25;
                        colCount = 1;
                        break;
                    }
                case displayOption.IMAGES:
                    {
                        buttonWidth = 120;
                        buttonHeight = 200;
                        colCount = 6;
                        break;
                    }
            }

            int i = 1;
            foreach (var g in titles)
            {
                Button button = new Button();
                button.Width = buttonWidth;
                button.Height = buttonHeight;
                button.Left = button.Width * ((i - 1) % colCount);
                button.Top = y;

                button.Name = "btn" + i;

                button.BackColor = GetColor((int)g.Score);
                button.ForeColor = Color.Black;
                button.Font = new Font("Arial", 8, FontStyle.Bold);
                button.Tag = g.Id;

                if (currentDisplayOption == displayOption.IMAGES)
                {
                    button.BackgroundImage = new Bitmap(DataManager.PATH + "\\data\\images\\"
                        + modeState.ToString() + "\\"
                        + g.Id + " " + g.FixedTitle + ".jpg");
                    button.BackgroundImageLayout = ImageLayout.Stretch;
                    button.ForeColor = Color.Transparent;
                }
                else
                {
                    button.Text = g.Title;
                }
                button.Click += ButtonOnClick;

                list_panel.Controls.Add(button);
                if (i % colCount == 0)
                    y += button.Height + 2;
                i++;
            }

            this.Controls.Add(list_panel);
        }

        private void ResetYears()
        {
            YearSortBox.Items.Clear();
            var set = new SortedSet<int>();
            foreach (var g in modeState.Contents)
                set.Add(g.DateRelease.Year);
            object[] a = new object[set.Count];
            int i = 0;
            foreach (var s in set)
            {
                a[i] = s;
                i++;
            }
            YearSortBox.Items.AddRange(a);
            if (YearSortBox.Items.Count > 0)
            {
                YearSortBox.SelectedIndex = 0;
            }
        }
        private void ResetGenres()
        {
            GenreSortBox.Items.Clear();
            var set = new SortedSet<string>();
            //DANGER TODO
            foreach (Film f in modeState.Contents)
            {
                //foreach (var g in f.Genres)
                //    set.Add(g);
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
        private void ResetStatus()
        {
            StatusSortBox.SelectedIndex = 0;
        }
        private void ResetTitles()
        {
            NameSortBox.Items.Clear();
            var set = new SortedSet<int>();
            foreach (var g in modeState.Contents)
                set.Add(g.Title.First());
            object[] a = new object[set.Count];
            int i = 0;
            foreach (var s in set)
            {
                a[i] = (char)s;
                i++;
            }
            NameSortBox.Items.AddRange(a);
            NameSortBox.SelectedIndex = 0;
        }
        private void ByYearButton_Click(object sender, EventArgs e)
        {
            filter = filterCategory.YEAR;

            YearSortBox.Visible = true;
            ScoreSortBox.Visible = false;
            NameSortBox.Visible = false;
            StatusSortBox.Visible = false;
            GenreSortBox.Visible = false;

            list_panel.Controls.Clear();
            currentTitlePanel.Controls.Clear();
        }

        private void ByScoreButton_Click(object sender, EventArgs e)
        {
            filter = filterCategory.SCORE;
            YearSortBox.Visible = false;
            ScoreSortBox.Visible = true;
            NameSortBox.Visible = false;
            StatusSortBox.Visible = false;
            GenreSortBox.Visible = false;

            ScoreSortBox.SelectedIndex = 0;

            list_panel.Controls.Clear();
            currentTitlePanel.Controls.Clear();
        }
        private void ByGenreButton_Click(object sender, EventArgs e)
        {
            filter = filterCategory.GENRE;
            YearSortBox.Visible = false;
            ScoreSortBox.Visible = false;
            NameSortBox.Visible = false;
            StatusSortBox.Visible = false;
            GenreSortBox.Visible = true;

            list_panel.Controls.Clear();
            currentTitlePanel.Controls.Clear();
        }
        private void ByStatusButton_Click(object sender, EventArgs e)
        {
            filter = filterCategory.STATUS;

            YearSortBox.Visible = false;
            ScoreSortBox.Visible = false;
            NameSortBox.Visible = false;
            StatusSortBox.Visible = true;
            GenreSortBox.Visible = false;

            StatusSortBox.SelectedIndex = 0;

            list_panel.Controls.Clear();
            currentTitlePanel.Controls.Clear();
        }
        private void ByNameButton_Click(object sender, EventArgs e)
        {
            filter = filterCategory.NAME;

            YearSortBox.Visible = false;
            ScoreSortBox.Visible = false;
            NameSortBox.Visible = true;
            StatusSortBox.Visible = false;
            GenreSortBox.Visible = false;

            list_panel.Controls.Clear();
            currentTitlePanel.Controls.Clear();
        }

        private void ModeBox_SelectedValueChanged(object sender, EventArgs e)
        {

            string nmode = ModeBox.SelectedItem.ToString().ToLower();
            if (nmode == modeState.ToString())
                return;
            list_panel.Controls.Clear();
            currentTitlePanel.Controls.Clear();
            switch (nmode)
            {
                case "games":
                    ByGenreButton.Visible = false;
                    ChangeState(new State<Game>(this));
                    break;
                // TODO
                case "films":
                    ByGenreButton.Visible = true;
                    ChangeState(new State<Film>(this));
                    break;
                case "tvseries":
                    ByGenreButton.Visible = true;
                    break;
            }
            ByYearButton_Click(sender, e);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshTitles();
        }

        private void displayImagesButton_Click(object sender, EventArgs e)
        {
            currentDisplayOption = displayOption.IMAGES;
            RefreshTitles();
        }

        private void displayLinesButton_Click(object sender, EventArgs e)
        {
            currentDisplayOption = displayOption.LINES;
            RefreshTitles();
        }

        private void displayButtonsButton_Click(object sender, EventArgs e)
        {
            currentDisplayOption = displayOption.BUTTONS;
            RefreshTitles();
        }

        private void YearSortBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshTitles();
        }
    }
}
#pragma warning restore CS8601 // Возможно, назначение-ссылка, допускающее значение NULL.