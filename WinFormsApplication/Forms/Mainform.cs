using hltb.Dto;
using hltb.Forms;
using hltb.Forms.ContentListBuilder;
using hltb.Models;
using hltb.Models.Outdated;
using hltb.Service;
using System.Collections;
using System.Data;
using System.Text;

namespace hltb
{
    public enum mode { GAMES, FILMS, TVSERIES };

    public partial class Mainform : Form
    {
        private Panel _listPanel = new Panel();
        private ContentListBuilder _listBuilder = new ButtonsContentList();
        private AddContent _addContent = new AddContent();
        private Dictionary<ComboBox, IFilterContentStrategy> _mapFilterToComboBox = new Dictionary<ComboBox, IFilterContentStrategy>();

        public AbstractContentService modeState;

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

        private void ChangeState(AbstractContentService state)
        {
            this.modeState = state;
            modeState.Load();
            ResetYears();
            ResetStatus();
            ResetTitles();
            filter_Click(YearSortBox, new EventArgs());
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
            AddOwnedForm(_addContent);
            Controls.Add(_listPanel);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _mapFilterToComboBox.Add(YearSortBox, new FilterByYear());
            _mapFilterToComboBox.Add(ScoreSortBox, new FilterByScore());
            _mapFilterToComboBox.Add(NameSortBox, new FilterByName());
            _mapFilterToComboBox.Add(StatusSortBox, new FilterByStatus());
            ChangeState(GameService.Instance);
            UpdateStatisticsLabel();

            Application.ApplicationExit += new EventHandler(OnApplicationExit!);
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            currentTitlePanel.Controls.Clear();
            var button = (Button)sender;
            CurrentContentContol ccc = new CurrentContentContol(this, modeState.Contents.First(x => x.Id == (long)button.Tag));
            currentTitlePanel.Controls.Add(ccc);
            this.Controls.Add(currentTitlePanel);
        }
        private void UpdateListPanel(List<Content> titles, string filterValue)
        {
            this.Controls.Remove(_listPanel);
            _listBuilder.Reset();
            _listBuilder.SetContent(titles);
            _listBuilder.SetButtonClickHandler(this.ButtonOnClick!);
            _listPanel = _listBuilder.Build(filterValue);
            _listPanel.Location = new Point(ByYearButton.Left, displayModeGroupBox.Bottom + 25);
            this.Controls.Add(_listPanel);
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
            StatusSortBox.Items.Clear();
            var set = new SortedSet<string>();
            foreach (var g in modeState.Contents)
                set.Add(g.Status);
            object[] a = new object[set.Count];
            int i = 0;
            foreach (string s in set)
            {
                a[i] = s;
                i++;
            }
            StatusSortBox.Items.AddRange(a);
            if (StatusSortBox.Items.Count > 0)
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
            if (NameSortBox.Items.Count > 0)
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
            _listPanel.Controls.Clear();
            currentTitlePanel.Controls.Clear();
            switch (nmode)
            {
                case "Game":
                    ChangeState(GameService.Instance);
                    break;
                case "Film":
                    ChangeState(FilmService.Instance);
                    break;
                case "TVSeries":
                    break;
            }
        }
        private void displayOptionsButton_Click(object sender, EventArgs e)
        {
            _listBuilder = new ButtonsContentList();
            RefreshTitles();
        }

        private void displayImagesButton_Click(object sender, EventArgs e)
        {
            _listBuilder = new ImageContentList();
            RefreshTitles();
        }

        private void displayRowsButton_Click(object sender, EventArgs e)
        {
            _listBuilder = new RowContentList();
            RefreshTitles();
        }

        private void filter_Click(object sender, EventArgs e)
        {
            RefreshTitles();
            ComboBox comboBox = (ComboBox)sender;
            _listBuilder.SetFilter(_mapFilterToComboBox[comboBox]);
            if (comboBox.SelectedItem == null)
                return;
            string filterValue = comboBox.SelectedItem.ToString()!;
            UpdateListPanel(modeState.Contents, filterValue);
        }

        private void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private async void addEntry_Click(object sender, EventArgs e)
        {
            AddContent.AddContentBuilder builder = new AddContent.AddContentBuilder(_addContent);
            StringBuilder status = new StringBuilder();
            if (namebox.Text == "")
            {
                status.Append("Error: empty name field");
                return;
            }
            IEnumerable<ISearchable> searchResult = new List<ISearchable>();//improve somehow??
            try
            {
                searchResult = modeState.SearchByTitle(namebox.Text);
                string imageUrl = $@"http://localhost:8080/images/None.jpg";
                if (!searchResult.Any())
                {
                    builder.Errors("Nothing has been found");
                }
                else
                {
                    foreach (var content in searchResult)
                    {
                        var entryCreationListElement = new EntryCreationListElement();

                        imageUrl = $@"http://localhost:8080/images/{modeState.ToString()}/{content.Id}.jpg";

                        Content? c = modeState
                            .Contents
                            .Where(x => x.LinkUrl == content.LinkUrl)
                            .FirstOrDefault();
                        if (c is null)
                            entryCreationListElement.SetStatusLabel("");
                        else
                            entryCreationListElement.SetStatusLabel(c.Status);
                        entryCreationListElement.SetImage(RestApiSerice.Instance.GetImage(imageUrl));
                        entryCreationListElement.SetText($"{content.Title}");
                        entryCreationListElement.SetYear($"Release date: {content.DateRelease}");
                        
                        builder.EntryElement(entryCreationListElement);
                    }
                }
            }
            catch (HttpRequestException ex) {
                builder.Errors(ex.Message);
            }
            
            _addContent = builder.Build();
            AddOwnedForm(_addContent);
            if (_addContent.ShowDialog() == DialogResult.OK)
            {
                namebox.Text = "";
                modeState.Create(searchResult.ElementAt(_addContent.Result));
            }
            UpdateStatisticsLabel();
        }
    }
}