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
            StringBuilder res = new StringBuilder( "Title name: ");
            var ss = title.Name.Split(' ');//.Concat((title as Film).Rus_Name.Split(' '));
            foreach (var part in ss)
            {
                if ((part.Length + res.Length % 30 ) > 30)
                    res.Append('\n');
                res.Append(part + ' ');
            }
            
            if (title is Film f)
            {
                res.Append(" || ");
                foreach (var part in f.Rus_Name.Split(' '))
                {
                    if ((part.Length + res.Length % 30) > 30)
                        res.Append('\n');
                    res.Append(part + ' ');
                }
            }


            return res.ToString();
        }

        private string GetSafeName()
        {
            var proh = @"<>:""'/\|? *";
            return string.Join("", title.Name.Where(x => proh.Contains(x) ? '' : ' ').ToArray());

        }


        public CurrentTitleContol(Title title, mode cm)
        {
            InitializeComponent();
            this.title = title;
            currentMode = cm;
            string safeName = GetSafeName();
            Console.WriteLine(DataFiles.path + "\\data\\images\\" + currentMode.ToString().ToLower() + "\\" + safeName + ".jpg");
            titlePicture.Image = new Bitmap(DataFiles.path + "\\data\\images\\" + currentMode.ToString().ToLower() + "\\" + safeName + ".jpg");
            nameLabel.Text = "Title name: " + title.Name;
            //if (currentMode != mode.GAMES)
                nameLabel.Text = GetFullName();

            switch (currentMode)
            {
                case mode.GAMES:
                    TextBox time_c = new TextBox();
                    time_c.Text = GetTime(title);
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
    }
}
