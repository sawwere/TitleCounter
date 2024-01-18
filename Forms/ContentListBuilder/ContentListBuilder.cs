using hltb.Forms;
using hltb.Models;
using hltb.Models.Outdated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb.Forms.ContentListBuilder
{
    public abstract class ContentListBuilder
    {
        public const int BUTTON_WIDTH = 250;
        public const int BUTTON_HEIGHT = 120;

        protected List<Content> _contentList;
        protected Panel list_panel = new Panel();

        public ContentListBuilder()
        {
            list_panel = new Panel();
            _contentList = new List<Content>();
        }

        public void Reset()
        {
            list_panel.Controls.Clear();
            _contentList = new List<Content>();
        }

        public void SetContent(List<Content> contents)
        {
            _contentList = contents;
        }

        public abstract Panel Build(EventHandler action);

        protected Color GetColor(int score)
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
    }
}
