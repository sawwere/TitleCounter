using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb.Forms.ContentListBuilder
{
    class ButtonsContentList : ContentListBuilder
    {
        public new const int BUTTON_WIDTH = 250;
        public new const int BUTTON_HEIGHT = 25;

        public override Panel Build(EventHandler action)
        {
            list_panel.Width = 800;
            list_panel.AutoScroll = true;
            list_panel.Height = 351;

            int colCount = 3;
            int y = 5;
            int i = 1;
            foreach (var content in _contentList)
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

                button.Click += action;

                list_panel.Controls.Add(button);
                if (i % colCount == 0)
                    y += button.Height + 2;
                i++;
            }
            return list_panel;
        }
    }
}
