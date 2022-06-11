using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace hltb
{
    public partial class AddTitle : Form
    {
        Title title;
        mode currentMode;

        private string GetSafeName()
        {
            char[] proh = { '<', '>', ':', '"', '"', '/', '\\', '|', '?', '*' };
            return new string(title.Name.Where(x => !proh.Contains(x)).ToArray());

        }
        public AddTitle()
        {
            InitializeComponent();
        }

        private void AddTitle_VisibleChanged(object sender, EventArgs e)
        {
            switch (Owner.Controls["ModeBox"].Text)
            {
                case "Games":
                    {
                        currentMode = mode.GAMES;
                        break;
                    }
                case "Films":
                    {
                        currentMode = mode.FILMS;
                        break;
                    }
                case "TVSeries":
                    {
                        currentMode = mode.TVSERIES;
                        break;
                    }
                default: break;
            }
            title = DataFiles.GetTitles(currentMode, true).First();
            nameLabel.Text = title.Name;
            string safeName = GetSafeName();
            titlePicture.Image = new Bitmap(DataFiles.path + "\\data\\images\\" + currentMode.ToString().ToLower() + "\\" + safeName + ".jpg");

        }

        private void addButton_Click(object sender, EventArgs e)
        {

        }
    }
}
