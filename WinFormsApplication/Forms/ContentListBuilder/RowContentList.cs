using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace hltb.Forms.ContentListBuilder
{
    internal class RowContentList: ContentListBuilder
    {
        public new const int BUTTON_WIDTH = 250;
        public new const int BUTTON_HEIGHT = 25;

        public override Panel Build(string filterValue)
        {
            list_panel.Width = 800;
            list_panel.AutoScroll = true;
            list_panel.Height = 351;

            int colCount = 1;
            int y = 5;
            int i = 1;
            foreach (var content in contentFilter.Filter(_contentList, filterValue))
            {
                RoundedButton button = new RoundedButton();

                button.Width = BUTTON_WIDTH;
                button.Height = BUTTON_HEIGHT;
                button.Left = button.Width * ((i - 1) % colCount);
                button.Top = y;
                button.BackColor = GetColor((int)content.Score);

                button.Name = "btn" + i;
                button.Tag = content.Id;
                button.Text = content.Title;

                button.Click += this.buttonClickHandler;

                list_panel.Controls.Add(button);
                if (i % colCount == 0)
                    y += button.Height + 2;
                i++;
            }
            return list_panel;
        }
    }
}
