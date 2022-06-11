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
        private Panel list_panel = new Panel();

        private Dictionary<mode, List<Title>> titles;
        private Title cur_title;
        private AddTitle add_title = new AddTitle();

        private mode currentMode = mode.GAMES;


        private void OnApplicationExit(object sender, EventArgs e)
        {
            SaveTitles(titles[mode.GAMES], mode.GAMES);
            SaveTitles(titles[mode.FILMS], mode.FILMS);
            SaveTitles(titles[mode.TVSERIES], mode.TVSERIES);
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
            AddOwnedForm(add_title);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckDataFiles();

            titles = new Dictionary<mode, List<Title>>(3);

            titles[mode.GAMES] = GetTitles(mode.GAMES);
            titles[mode.FILMS] = GetTitles(mode.FILMS);
            titles[mode.TVSERIES] = GetTitles(mode.TVSERIES);

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
                string title = namebox.Text + "#" + statusbox.Text + "#" + scorebox.SelectedItem + '#' + currentMode.ToString().ToLower();
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
                var r = t.Split('#');
                Console.WriteLine(r[0]);

                if (r[0] == "ERROR")
                {
                    add_title.Controls["addButton"].Visible = false;
                    switch (r[1])
                    {
                        case "a":
                            add_title.Controls["statusLabel"].Text += (": title is already in list");
                            break;
                        case "f":
                            add_title.Controls["statusLabel"].Text += (": title has not found");
                            break;
                        case "t":
                            add_title.Controls["statusLabel"].Text += (": incorrect type. Choose correct mode");
                            break;
                    }
                }
                //else
                //{
                //    add_title.Controls["statusLabel"].Text += "Found succesfuly";
                //}
                

                if (add_title.ShowDialog() == DialogResult.OK )
                {
                    namebox.Text = "";
                    statusbox.SelectedIndex = 1;
                    scorebox.SelectedIndex = 0;

                    titles[currentMode] = GetTitles(currentMode);
                    Console.WriteLine(titles[currentMode].Count());
                    titles[currentMode].Add(GetTitles(currentMode, true).First());
                    Console.WriteLine(titles[currentMode].Count());
                }

                
            }
            operationLabel.Text = status.ToString();
            UpdateStatisticsLabel();
        }
        
        
        //TODO Erase deleted title's image
        private void deleteButtonClick(object sender, EventArgs eventArgs)
        {
            currentTitlePanel.Controls.Clear();
            titles[currentMode].Remove(cur_title);
            cur_title = new Title();
            SaveTitles(titles[currentMode], currentMode);
            UpdateStatisticsLabel();
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            currentTitlePanel.Controls.Clear();
            //TODO Update information about current title before swap 
            //currentTitlePanel.Controls.Find("", true);

            var button = (Button)sender;

            var g = button.Text.Where(x => (x != ':') & (x != '/')).ToArray();
            StringBuilder s = new StringBuilder();
            foreach (var ss in g)
            {
                s.Append(ss.ToString());
            }

            cur_title = titles[currentMode].Find(x => x.Name == button.Text);
            currentTitlePanel.Controls.Add(new CurrentTitleContol(cur_title, currentMode));

            
            
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