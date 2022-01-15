using System;
using System.Windows.Forms;

namespace hltb
{
    static class Program
    {


        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {



            //AddButtons(cur_games, cur_year);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Mainform());
        }
    }
}
