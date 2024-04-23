using hltb.Dto;
using hltb.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;

namespace hltb.Service
{
    internal class RestApiSerice
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private static RestApiSerice restApiSerice;
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private RestApiClient restApiClient;
        private string JSESSIONID;

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

        public string JsessionId { get { return JSESSIONID; } }

        public void login(UserLoginDto userLoginDto)
        {
            var content = JsonContent.Create(userLoginDto);
            var result = restApiClient.HttpClient.PostAsync("http://localhost:8080/api/login", content).Result;
            var a = result.Headers;
            JSESSIONID = restApiClient.Handler.CookieContainer.GetAllCookies().Cast<Cookie>().First(x => x.Name == "JSESSIONID").Value;
            Debug.WriteLine(JSESSIONID);
        }

        public List<GameEntryResponseDto> findGames()
        {
            var first = restApiClient.HttpClient.DefaultRequestHeaders.Any(x => x.Key == "Cookie");
            if (!first)
                restApiClient.HttpClient.DefaultRequestHeaders.Add("Cookie", $"JSESSIONID={JSESSIONID}");
            var gameDtos = restApiClient.HttpClient.GetFromJsonAsync<List<GameEntryResponseDto>>(
                    restApiClient.HttpClient.BaseAddress + $"/users/{Mainform.USER_INFO.username}/games"
                )
                .Result;
            return gameDtos is null ? new List<GameEntryResponseDto>() : gameDtos;
        }

        public bool updateGame(GameEntryRequestDto gameEntryRequestDto)
        {
            var response = restApiClient.HttpClient.PutAsJsonAsync(
                    restApiClient.HttpClient.BaseAddress + $"/games/submissions/{gameEntryRequestDto.id}",
                    gameEntryRequestDto)
                .Result;
            return response.IsSuccessStatusCode;
        }

        public bool deleteGame(long id)
        {
            var response = restApiClient.HttpClient.DeleteAsync(restApiClient.HttpClient.BaseAddress + $"/api/games/submissions/{id}").Result;
            return response.IsSuccessStatusCode;
        }

        //TODO
        public async Task<byte[]> GetImageAsync(string imageUrl)
        {
            var response = await restApiClient.HttpClient.GetStringAsync($"find/image?image_url={imageUrl}");
            return System.Convert.FromBase64String(response); ;
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
