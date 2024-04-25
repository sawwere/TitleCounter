using hltb.Dto;
using hltb.Service;
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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserLoginDto userLoginDto = new UserLoginDto(textBox1.Text, textBox2.Text);
            _ = AuthService.Instance.loginPeriodicallyAsync(userLoginDto, TimeSpan.FromSeconds(15), CancellationToken.None);
            Hide();
            Mainform mainform = new Mainform();
            mainform.Show();
        }
    }
}
