using hltb.Forms;
using hltb.Forms.ContentListBuilder;
using hltb.Models;
using hltb.Models.Outdated;
using hltb.Service;
using Newtonsoft.Json;
using System.Data;
using System.Text;

using static hltb.DataManager;

#pragma warning disable CS8601 // Возможно, назначение-ссылка, допускающее значение NULL.
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
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("http://127.0.0.1:5000"),
        };


        private void OnApplicationExit(object sender, EventArgs e)
        {
            //modeState.Save();
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
            CheckDataFiles();
            UpdateStatisticsLabel();
            mapFilterToComboBox.Add(ByYearButton, YearSortBox);
            mapFilterToComboBox.Add(ByScoreButton, ScoreSortBox);
            mapFilterToComboBox.Add(ByNameButton, NameSortBox);
            mapFilterToComboBox.Add(ByStatusButton, StatusSortBox);

            Application.ApplicationExit += new EventHandler(OnApplicationExit!);
        }

        static async Task<byte[]> GetImageAsync(HttpClient httpClient, string imageUrl)
        {
            var response = await httpClient.GetStringAsync($"find/image?image_url={imageUrl}");
            return System.Convert.FromBase64String(response); ;
        }

        private async Task<Content> GetContentAsync(string title)
        {
            var response = await sharedClient.GetStringAsync($"find/{modeState.ToString().ToLower()}s?title={title}");
            var content = modeState.GetFromJson(response);
            if (content is null)
            {
                throw new ArgumentNullException("Invalid Json Deserialize");
            }
            //TODO
            content.Status = "backlog";
            content.Score = 0;
            //content.FixedTitle = GetSafeName(content.Title);
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
                    modeState.Create(content);
                    DataManager.SaveImage(modeState, content, decodedImage);
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
    }
}
#pragma warning restore CS8601 // Возможно, назначение-ссылка, допускающее значение NULL.