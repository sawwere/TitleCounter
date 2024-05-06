using hltb.Dto;
using hltb.Service;

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
            _ = AuthService.Instance.Login(userLoginDto);
            _ = AuthService.Instance.LoginPeriodicallyAsync(userLoginDto, TimeSpan.FromSeconds(15), CancellationToken.None);
            Hide();
            Mainform mainform = new Mainform();
            mainform.Show();
        }
    }
}
