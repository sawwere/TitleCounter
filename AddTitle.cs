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
    public partial class AddContent : Form
    {
        Content content;
        mode currentMode;

        private string GetSafeName()
        {
            char[] proh = { '<', '>', ':', '"', '"', '/', '\\', '|', '?', '*' };
            return new string(content.Title.Where(x => !proh.Contains(x)).ToArray());

        }
        public AddContent()
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
            content = DataFiles.GetContent(currentMode, true).First();
            nameLabel.Text = content.Title;
            string safeName = GetSafeName();
            titlePicture.Image = new Bitmap(DataFiles.PATH + "\\data\\images\\" + currentMode.ToString().ToLower() + "\\" + safeName + ".jpg");

        }
    }
}
