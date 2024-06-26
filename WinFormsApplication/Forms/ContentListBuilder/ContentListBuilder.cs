﻿using hltb.Models;

namespace hltb.Forms.ContentListBuilder
{
    public abstract class ContentListBuilder
    {
        public const int BUTTON_WIDTH = 250;
        public const int BUTTON_HEIGHT = 120;

        protected List<Content> _contentList;
        protected Panel list_panel = new Panel();
        protected IFilterContentStrategy contentFilter;
        protected EventHandler buttonClickHandler;

#pragma warning disable CS8618
        public ContentListBuilder()
#pragma warning restore CS8618 
        {
            list_panel = new Panel();
            _contentList = new List<Content>();
            contentFilter = new FilterByYear();
        }

        public void Reset()
        {
            list_panel.Controls.Clear();
            _contentList = new List<Content>();
        }

        public void SetFilter(IFilterContentStrategy filterContent)
        {
            this.contentFilter = filterContent;
        }

        public void SetContent(List<Content> contents)
        {
            _contentList = contents;
        }

        public void SetButtonClickHandler(EventHandler action)
        {
            buttonClickHandler = action;
        }

        public abstract Panel Build(string filterValue);

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
