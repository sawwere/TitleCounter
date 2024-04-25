using hltb.Dto;
using hltb.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Web;

namespace hltb.Service
{
    internal class RestApiSerice
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private static RestApiSerice restApiSerice;
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private RestApiClient restApiClient;
        private string SESSIONID;

        public static RestApiSerice Instance
        {
            get
            {
                if (restApiSerice == null)
                {
                    restApiSerice = new RestApiSerice();
                }
                return restApiSerice;
            }
        }
        private RestApiSerice() { 
            restApiClient = RestApiClient.Instance;
        }

        public string SessionId { get { return SESSIONID; } }


        public void login(UserLoginDto userLoginDto)
        {
            var content = JsonContent.Create(userLoginDto);
            var result = restApiClient.HttpClient.PostAsync("http://localhost:8080/api/login", content).Result;
            var a = result.Headers;
            SESSIONID = restApiClient.Handler.CookieContainer.GetAllCookies().Cast<Cookie>().First(x => x.Name == "SESSION").Value;
            Debug.WriteLine(SESSIONID);
        }

        public void logout()
        {
            var result = restApiClient.HttpClient.GetAsync("http://localhost:8080/logout").Result;
            result.EnsureSuccessStatusCode();
            Debug.WriteLine("Logout succesfully");
        }

        public List<GameDto> searchGames(string title)
        {
            var first = restApiClient.HttpClient.DefaultRequestHeaders.Any(x => x.Key == "Cookie");
            if (!first)
                restApiClient.HttpClient.DefaultRequestHeaders.Add("Cookie", $"SESSION={SESSIONID}");
            var requestUri = new Uri(restApiClient.HttpClient.BaseAddress + $"/games?q={title}");
            var bb = HttpUtility.UrlEncode(restApiClient.HttpClient.BaseAddress + $"/games?q={title}");
            Debug.WriteLine($"{bb}");
            var gameDtos = restApiClient.HttpClient.GetFromJsonAsync<List<GameDto>>(
                    requestUri
                )
                .Result;
            return gameDtos is null ? new List<GameDto>() : gameDtos;
        }

        public List<GameEntryResponseDto> findGames()
        {
            var first = restApiClient.HttpClient.DefaultRequestHeaders.Any(x => x.Key == "Cookie");
            if (!first)
                restApiClient.HttpClient.DefaultRequestHeaders.Add("Cookie", $"SESSION={SESSIONID}");
            var gameDtos = restApiClient.HttpClient.GetFromJsonAsync<List<GameEntryResponseDto>>(
                    restApiClient.HttpClient.BaseAddress + $"/users/{AuthService.Instance.LoginInfo.username}/games"
                )
                .Result;
            return gameDtos is null ? new List<GameEntryResponseDto>() : gameDtos;
        }

        public bool createGameEntry(GameEntryRequestDto gameEntryRequestDto)
        {
            var first = restApiClient.HttpClient.DefaultRequestHeaders.Any(x => x.Key == "Cookie");
            if (!first)
                restApiClient.HttpClient.DefaultRequestHeaders.Add("Cookie", $"SESSION={SESSIONID}");
            var response = restApiClient.HttpClient.PostAsJsonAsync(
                    restApiClient.HttpClient.BaseAddress + $"/users/{AuthService.Instance.LoginInfo.username}/games",
                    gameEntryRequestDto)
                .Result;
            return response.IsSuccessStatusCode;
        }

        public bool updateGameEntry(GameEntryRequestDto gameEntryRequestDto)
        {
            var first = restApiClient.HttpClient.DefaultRequestHeaders.Any(x => x.Key == "Cookie");
            if (!first)
                restApiClient.HttpClient.DefaultRequestHeaders.Add("Cookie", $"SESSION={SESSIONID}");
            var response = restApiClient.HttpClient.PutAsJsonAsync(
                    restApiClient.HttpClient.BaseAddress + $"/games/submissions/{gameEntryRequestDto.id}",
                    gameEntryRequestDto)
                .Result;
            return response.IsSuccessStatusCode;
        }

        public bool deleteGameEntry(long id)
        {
            var first = restApiClient.HttpClient.DefaultRequestHeaders.Any(x => x.Key == "Cookie");
            if (!first)
                restApiClient.HttpClient.DefaultRequestHeaders.Add("Cookie", $"SESSION={SESSIONID}");
            var response = restApiClient.HttpClient.DeleteAsync(restApiClient.HttpClient.BaseAddress + $"/api/games/submissions/{id}").Result;
            return response.IsSuccessStatusCode;
        }

        public async Task<Image> GetImageAsync(string imageUrl)
        {
            var response = restApiClient.HttpClient.GetStreamAsync($"{imageUrl}").Result;
            var img = Image.FromStream(response);
            return img;
        }

        //TODO
        public async Task<Content> GetContentAsync(string title)
        {
            var response = await restApiClient.HttpClient.GetStringAsync($"find/games/s?title={title}");
            Content content = null;
            if (content is null)
            {
                throw new ArgumentNullException("Invalid Json Deserialize");
            }
            //TODO
            content.Status = "backlog";
            content.Score = 0;
            //content.FixedTitle = GetSafeName(content.Title);
            return content;
        }

    }
}
