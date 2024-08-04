using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hltb.Forms
{
    public partial class EntryCreationListElement : UserControl
    {
        public EntryCreationListElement()
        {
            InitializeComponent();
        }

        public void SetYear(string text)
        {
            yearLabel.Text = text;
        }

        public void SetText(string text)
        {
            label1.Text = text;
        }

        public void SetImage(Image image)
        {
            pictureBox1.Image = image;
        }

        public void SetStatusLabel(string text)
        {
            switch (text)
            {
                case "completed": label2.ForeColor = Color.FromArgb(255, 128, 128, 255) ; break;
                case "backlog": label2.ForeColor = Color.FromArgb(255, 255, 128, 128); break;
                case "in progress": label2.ForeColor = Color.LightGreen; break;
                default: break;
            }
            label2.Text = text;
        }

        public Button GetButton()
        {
            return roundedButton1;
        }
    }
}
