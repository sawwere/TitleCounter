using hltb.Dto;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Net;

namespace hltb.Service
{
    internal class AuthService
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private static AuthService authService;
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно,
        private RestApiClient _restApiClient;
        private UserLoginDto _LoginInfo { get; set; }
        public UserDto UserInfo { get; private set; }
        private string SESSIONID;
        public string SessionId { get { return SESSIONID; } }

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
        private AuthService() 
        {
            _restApiClient = RestApiClient.Instance;
        }

        public bool Login(UserLoginDto userLoginDto)
        {
            Debug.WriteLine("Login operation started.");
            _LoginInfo = userLoginDto;
            UserInfo = _login(userLoginDto);
            return true;
        }

        public async Task LoginPeriodicallyAsync(UserLoginDto userLoginDto, TimeSpan period, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _login(userLoginDto);
                await Task.Delay(period, cancellationToken);
            }
        }

        private UserDto _login(UserLoginDto userLoginDto)
        {
            Debug.WriteLine("Login operation started.");
            var content = JsonContent.Create(userLoginDto);
            var result = _restApiClient.HttpClient.PostAsync("http://localhost:8080/api/login", content).Result;
            var userDto = JsonConvert.DeserializeObject<UserDto>(result.Content.ReadAsStringAsync().Result);
            SESSIONID = _restApiClient.Handler.CookieContainer.GetAllCookies().Cast<Cookie>().First(x => x.Name == "JSESSIONID").Value;
            _restApiClient.SetSessionCookie(SESSIONID);
            Debug.WriteLine(SESSIONID);
            return userDto;
        }

        public void logout()
        {
            var result = _restApiClient.HttpClient.GetAsync("http://localhost:8080/logout").Result;
            result.EnsureSuccessStatusCode();
            Debug.WriteLine("Logout succesfully");
        }
    }
}
