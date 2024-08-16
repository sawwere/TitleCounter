using hltb.Dto;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Net;
using System.Net.Http.Headers;

namespace hltb.Service
{
    internal class AuthService
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private static AuthService authService;
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно,
        private RestApiClient _restApiClient;
        public UserDto UserInfo { get; private set; }
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }

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
            AccessToken = string.Empty;
            RefreshToken = string.Empty;
        }

        public bool Login(UserLoginDto userLoginDto)
        {
            Debug.WriteLine("Login operation started.");
            UserInfo = _login(userLoginDto);
            return true;
        }

        public async Task RefreshTokenPeriodicallyAsync(string refreshToken, TimeSpan period, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _refreshToken(refreshToken);
                await Task.Delay(period, cancellationToken);
            }
        }

        private void _refreshToken(string refreshToken)
        {
            var content = JsonContent.Create(new KeyValuePair<string, string>("refreshToken", refreshToken));
            HttpResponseMessage result = _restApiClient
                .HttpClient.PostAsync("http://localhost:80/api/auth/token", content)
                .Result
                .EnsureSuccessStatusCode();
            _setTokens(JsonConvert.DeserializeObject<JwtAuthenticationResponse>(result.Content.ReadAsStringAsync().Result)!);
        }

        private void _setTokens(JwtAuthenticationResponse jwt)
        {
            AccessToken = jwt.accessToken;
            RefreshToken = jwt.refreshToken;
            _restApiClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.accessToken);
            Debug.WriteLine(AccessToken);
        }

        private UserDto _login(UserLoginDto userLoginDto)
        {
            Debug.WriteLine("Login operation started.");
            var content = JsonContent.Create(userLoginDto);
            var result = _restApiClient
                .HttpClient.PostAsync("http://localhost:80/api/auth/login", content)
                .Result.EnsureSuccessStatusCode();
            var jwt = JsonConvert.DeserializeObject<JwtAuthenticationResponse>(result.Content.ReadAsStringAsync().Result)!;
            _setTokens(jwt);
            result = _restApiClient.HttpClient.GetAsync("http://localhost:80/api/user").Result.EnsureSuccessStatusCode();
            var userDto = JsonConvert.DeserializeObject<UserDto>(result.Content.ReadAsStringAsync().Result)!;
            return userDto;
        }

        public void logout()
        {
            var result = _restApiClient.HttpClient.GetAsync("http://localhost:80/api/auth/logout").Result;
            //result.EnsureSuccessStatusCode();
            Debug.WriteLine("Logout succesfully");
        }
    }
}
