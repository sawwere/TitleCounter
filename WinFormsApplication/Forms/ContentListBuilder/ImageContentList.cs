using hltb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb.Forms.ContentListBuilder
{
    internal class ImageContentList : ContentListBuilder
    {
        public new const int BUTTON_WIDTH = 120;
        public new const int BUTTON_HEIGHT = 200;

        public override Panel Build(string filterValue)
        {
            list_panel.Width = 800;
            list_panel.AutoScroll = true;
            list_panel.Height = 351;

            int colCount = 6;
            int y = 5;
            int i = 1;
            foreach (var content in contentFilter.Filter(_contentList, filterValue))
            {
                RoundedButton button = new RoundedButton();
                button.Width = BUTTON_WIDTH;
                button.Height = BUTTON_HEIGHT;
                button.Left = button.Width * ((i - 1) % colCount);
                button.Top = y;

                button.Name = "btn" + i;
                button.Tag = content.Id;

                button.BackgroundImage = RestApiSerice.Instance.GetImage( "\\data\\images\\"
                        + "Game".ToString() + "\\"
                        + content.Id + " " + content.Title + ".jpg");
                button.Click += this.buttonClickHandler;
                button.BackgroundImageLayout = ImageLayout.Stretch;
                button.ForeColor = Color.Transparent;
                list_panel.Controls.Add(button);
                if (i % colCount == 0)
                    y += button.Height + 2;
                i++;
            }
            return list_panel;
        }
    }
}
