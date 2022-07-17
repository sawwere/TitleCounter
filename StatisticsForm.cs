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
    public partial class StatisticsForm : Form
    {
        private List<Title> titles;

        public StatisticsForm(List<Title> _titles)
        {
            InitializeComponent();
            titles = _titles;
            
        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;


            foreach (var ts in Enum.GetValues(typeof(TitleStatus)))
            {
                chart1.Series[0].Points.AddY(titles.Where(x => x.Status == (TitleStatus)ts).Count());
            }

            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            chart2.Series[0].Points.Clear();
            SortedDictionary<int, int> dict = new SortedDictionary<int, int>();
            foreach (var t in titles)
            {
                if (dict.ContainsKey(t.Year))
                    dict[t.Year]++;
                else
                    dict[t.Year] = 1;
            }

            chart2.ChartAreas[0].AxisX.Title = "Years";
            chart2.ChartAreas[0].AxisY.Title = "Titles count";

            chart2.ChartAreas[0].AxisX.Minimum = dict.First(x=>x.Key!=-1).Key;
            foreach (var year in dict.Keys )
            {
                
                chart2.Series[0].Points.AddXY(year, dict[year]);
            }
        }
    }
}
