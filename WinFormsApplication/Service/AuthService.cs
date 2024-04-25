using hltb.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb.Service
{
    internal class AuthService
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private static AuthService authService;
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно,
        public UserLoginDto LoginInfo { get; private set; }

        public static AuthService Instance
        {
            get
            {
                if (authService == null)
                {
                    authService = new AuthService();
                }
                return authService;
            }
        }
        private AuthService() {  }

        public async Task loginPeriodicallyAsync(UserLoginDto userLoginDto, TimeSpan period, CancellationToken cancellationToken)
        {
            LoginInfo = userLoginDto;
            while (!cancellationToken.IsCancellationRequested)
            {
                login(userLoginDto);
                await Task.Delay(period, cancellationToken);
            }
        }

        private void login(UserLoginDto userLoginDto)
        {
            Debug.WriteLine("Login operation started.");
            RestApiSerice.Instance.login(userLoginDto);
            Debug.WriteLine("Login operation completed.");
        }

        public void logout()
        {
            RestApiSerice.Instance.logout();
        }
    }
}
