using hltb.Dto;
using hltb.Forms.ContentListBuilder;
using hltb.Models;
using hltb.Models.Outdated;
using hltb.Service;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace hltb
{
    public enum mode { GAMES, FILMS, TVSERIES };

    public partial class Mainform : Form
    {
        private Panel list_panel = new Panel();
        private ContentListBuilder list_builder = new ButtonsContentList();
        private AddContent add_content = new AddContent();

        public ModeState modeState;
        private Dictionary<Button, ComboBox> mapFilterToComboBox = new Dictionary<Button, ComboBox>();

        private void OnApplicationExit(object sender, EventArgs e)
        {
            AuthService.Instance.logout();
        }

        void UpdateStatisticsLabel()
        {
            double total = 1, cmpltd = 1, tmcmpltd = 1, tmtotal = 1;
            total = modeState.Contents.Count() + 0.0;
            cmpltd = modeState.Contents.Where(x => (x.Status == "completed") || (x.Status == "retired:")).Count();
            tmcmpltd = modeState.Contents.Where(x => (x.Status == "completed") || (x.Status == "retired:")).Select(x => (int)x.Time).Sum();
            tmtotal = modeState.Contents.Select(x => (int)x.Time).Sum();

            statisticsLabel.Text = $"Completed: {cmpltd} / {total}  ({(cmpltd / total * 100):F2}%)" + '\n'
                + $"Time : {tmcmpltd} / {tmtotal} ({(tmcmpltd / tmtotal * 100):F2}%)";
        }

        private void ChangeState(ModeState state)
        {
            this.modeState = state;
            modeState.Load();
            ResetYears();
            ResetStatus();
            ResetTitles();
            RefreshTitles();
        }

        public void RefreshTitles()
        {
            modeState.Load();

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
            ChangeState(new GameService(this));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateStatisticsLabel();
            mapFilterToComboBox.Add(ByYearButton, YearSortBox);
            mapFilterToComboBox.Add(ByScoreButton, ScoreSortBox);
            mapFilterToComboBox.Add(ByNameButton, NameSortBox);
            mapFilterToComboBox.Add(ByStatusButton, StatusSortBox);

            Application.ApplicationExit += new EventHandler(OnApplicationExit!);
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
                char operationCode = '0';
                var searchResult = modeState.Search(namebox.Text);
                string foundTitle = "Not found";

                string imageUrl = $@"http://localhost:8080/images/None.jpg";
                if (searchResult.Count() == 0)
                {
                    operationCode = '1';
                }
                else
                {
                    imageUrl = $@"http://localhost:8080/images/{modeState.ToString()}/{searchResult.First().id}.jpg";
                    foundTitle = searchResult.First().title;
                }
                Image decodedImage = await Task.Run(() => RestApiSerice.Instance.GetImageAsync(imageUrl));

                if (operationCode == '0')
                {
                    if (modeState
                        .Contents
                        .Where(x => 
                            x.Title == foundTitle 
                            && x.DateRelease.Year.ToString() == searchResult.First().dateRelease.Substring(0,4))
                        .Count() > 0)
                        operationCode = '2';
                }
                // send operation code to add_content Control, perform preparations
                add_content.SetStatus(operationCode);

                add_content.RecieveResponse(foundTitle, decodedImage);
                if (add_content.ShowDialog() == DialogResult.OK)
                {
                    namebox.Text = "";
                    Content content = new Content();
                    modeState.Create(searchResult.First());
                }
            }
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
        private void AddButtons(List<Content> titles)
        {
            this.Controls.Remove(list_panel);
            list_builder.Reset();
            list_builder.SetContent(titles);
            list_panel = list_builder.Build(ButtonOnClick!);
            list_panel.Location = new Point(ByYearButton.Left, displayModeGroupBox.Bottom + 25);
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

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshTitles();
        }

        private void button1_Click(object sender, EventArgs e)
        {
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            string nmode = (sender as Button)!.Tag.ToString()!;
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
            if (nmode.ToLower() == modeState.ToString())
                return;
            list_panel.Controls.Clear();
            currentTitlePanel.Controls.Clear();
            switch (nmode)
            {
                case "Game":
                    ChangeState(new GameService(this));
                    break;
                case "Film":
                    ChangeState(new State<Film>(this));
                    break;
                case "TVSeries":
                    break;
            }
            //FilterButton_Click(ByYearButton, e);
        }
        private void displayOptionsButton_Click(object sender, EventArgs e)
        {
            list_builder = new ButtonsContentList();
            RefreshTitles();
        }

        private void displayImagesButton_Click(object sender, EventArgs e)
        {
            list_builder = new ImageContentList();
            RefreshTitles();
        }

        private void displayRowsButton_Click(object sender, EventArgs e)
        {
            list_builder = new RowContentList();
            RefreshTitles();
        }

        private void ByYearButton_Click(object sender, EventArgs e)
        {
            string cur_year = YearSortBox.SelectedItem.ToString();
            RefreshTitles();
            AddButtons(modeState.Contents.Where(x => x.DateRelease.Year.ToString() == cur_year).ToList());
        }

        private void ByScoreButton_Click(object sender, EventArgs e)
        {
            string cur_score = ScoreSortBox.SelectedIndex.ToString();
            RefreshTitles();
            AddButtons(modeState.Contents.Where(x => x.Score.ToString() == cur_score).ToList());
        }

        private void ByStatusButton_Click(object sender, EventArgs e)
        {
            string cur_status = StatusSortBox.SelectedItem.ToString().ToLower();
            RefreshTitles();
            AddButtons(modeState.Contents.Where(x => x.Status == cur_status).ToList());
        }

        private void ByNameButton_Click(object sender, EventArgs e)
        {
            char cur_letter = NameSortBox.SelectedItem.ToString().First();
            RefreshTitles();
            AddButtons(modeState.Contents.Where(x => x.Title.ToString().First() == cur_letter).ToList());

        }

        private void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}