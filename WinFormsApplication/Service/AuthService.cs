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
        private UserLoginDto _LoginInfo { get; set; }
        public UserDto UserInfo { get; private set; }

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
            _LoginInfo = userLoginDto;
            UserInfo = login(userLoginDto);
            while (!cancellationToken.IsCancellationRequested)
            {
                login(userLoginDto);
                await Task.Delay(period, cancellationToken);
            }
        }

        private UserDto login(UserLoginDto userLoginDto)
        {
            Debug.WriteLine("Login operation started.");
            return RestApiSerice.Instance.login(userLoginDto);
        }

        public void logout()
        {
            RestApiSerice.Instance.logout();
        }
    }
}
