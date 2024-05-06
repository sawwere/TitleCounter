using hltb.Dto;
using hltb.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace hltb.Repository
{
    internal class RestGameRepository : IGameRepository
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private static RestGameRepository _restGameRepository;
#pragma warning restore CS8618
        private RestApiClient restApiClient;
        public static RestGameRepository Instance
        {
            get
            {
                if (_restGameRepository == null)
                {
                    _restGameRepository = new RestGameRepository();
                }
                return _restGameRepository;
            }
        }
        private RestGameRepository()
        {
            restApiClient = RestApiClient.Instance;
        }

        public List<GameDto> SearchByTitle(string title)
        {
            var requestUri = new Uri(restApiClient.HttpClient.BaseAddress + $"/games?q={title}");
            var gameDtos = restApiClient.HttpClient.GetFromJsonAsync<List<GameDto>>(requestUri)
                .Result;
            return gameDtos is null ? new List<GameDto>() : gameDtos;
        }

        public List<GameEntryResponseDto> FindAll()
        {
            var gameDtos = restApiClient.HttpClient.GetFromJsonAsync<List<GameEntryResponseDto>>(
                    restApiClient.HttpClient.BaseAddress + $"/users/{AuthService.Instance.UserInfo.Username}/games"
                )
                .Result;
            return gameDtos is null ? new List<GameEntryResponseDto>() : gameDtos;
        }

        public bool CreateGameEntry(GameEntryRequestDto gameEntryRequestDto)
        {
            var response = restApiClient.HttpClient.PostAsJsonAsync(
                    restApiClient.HttpClient.BaseAddress + $"/users/{AuthService.Instance.UserInfo.Username}/games",
                    gameEntryRequestDto)
                .Result;
            return response.IsSuccessStatusCode;
        }

        public bool UpdateGameEntry(GameEntryRequestDto gameEntryRequestDto)
        {
            var response = restApiClient.HttpClient.PutAsJsonAsync(
                    restApiClient.HttpClient.BaseAddress + $"/games/submissions/{gameEntryRequestDto.Id}",
                    gameEntryRequestDto)
                .Result;
            return response.IsSuccessStatusCode;
        }

        public bool DeleteGameEntry(long id)
        {
            var response = restApiClient.HttpClient.DeleteAsync(restApiClient.HttpClient.BaseAddress + $"/games/submissions/{id}").Result;
            return response.IsSuccessStatusCode;
        }
    }
}
