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
using hltb.Dto;
using hltb.Forms;
using hltb.Models;
using static hltb.Models.Film;
using hltb.Models.Outdated;

namespace hltb
{
    public partial class AddContent : Form
    {
        private List<EntryCreationListElement> entryCreationListElements = new List<EntryCreationListElement>();
        public int Result { get; private set; }
        public class AddContentBuilder
        {
            private AddContent _result;
            public AddContentBuilder(AddContent initial)
            {
                _result = new AddContent();
            }

            public AddContentBuilder EntryElement(EntryCreationListElement element)
            {
                element.Location = new Point(15, _result.label1.Bottom + 5 + _result.entryCreationListElements.Count * (5 + element.Height));

                element.GetButton().DialogResult = DialogResult.OK;
                element.GetButton().Tag = _result.entryCreationListElements.Count;
#pragma warning disable CS8622 // Допустимость значений NULL для ссылочных типов в типе параметра не соответствует целевому объекту делегирования (возможно, из-за атрибутов допустимости значений NULL).
                element.GetButton().Click += _result.SetResult;
#pragma warning restore CS8622 // Допустимость значений NULL для ссылочных типов в типе параметра не соответствует целевому объекту делегирования (возможно, из-за атрибутов допустимости значений NULL).

                _result.entryCreationListElements.Add(element);
                _result.Controls.Add(element);
                return this;
            }

            public AddContentBuilder Errors(string error)
            {
                _result.statusLabel.Visible = true;
                _result.statusLabel.Text = error;
                _result.statusLabel.ForeColor = Color.Red;
                return this;
            }

            public AddContent Build() { return _result; }
        }

        public AddContent()
        {
            InitializeComponent();
        }

        private void SetResult(object sender, EventArgs e)
        {
            Result = (int)((Button)sender).Tag;
        }
    }
}
