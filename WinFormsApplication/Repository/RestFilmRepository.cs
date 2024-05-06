using hltb.Dto;
using hltb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace hltb.Repository
{
    internal class RestFilmRepository : IFilmRepository
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private static RestFilmRepository _restFilmRepository;
#pragma warning restore CS8618
        private RestApiClient restApiClient;
        public static RestFilmRepository Instance
        {
            get
            {
                if (_restFilmRepository == null)
                {
                    _restFilmRepository = new RestFilmRepository();
                }
                return _restFilmRepository;
            }
        }
        private RestFilmRepository()
        {
            restApiClient = RestApiClient.Instance;
        }

        public bool CreateFilmEntry(FilmEntryRequestDto filmEntry)
        {
            var response = restApiClient.HttpClient.PostAsJsonAsync(
                    restApiClient.HttpClient.BaseAddress + $"/users/{AuthService.Instance.UserInfo.Username}/films",
                    filmEntry)
                .Result;
            return response.IsSuccessStatusCode;
        }

        public List<FilmDto> SearchByTitle(string title)
        {
            var requestUri = new Uri(restApiClient.HttpClient.BaseAddress + $"/films?q={title}");
            var filmDtos = restApiClient.HttpClient.GetFromJsonAsync<List<FilmDto>>(
                    requestUri
                )
                .Result;
            return filmDtos is null ? new List<FilmDto>() : filmDtos;
        }

        public List<FilmEntryResponseDto> FindAll()
        {
            var filmDtos = restApiClient.HttpClient.GetFromJsonAsync<List<FilmEntryResponseDto>>(
                    restApiClient.HttpClient.BaseAddress + $"/users/{AuthService.Instance.UserInfo.Username}/films"
                )
                .Result;
            return filmDtos is null ? new List<FilmEntryResponseDto>() : filmDtos;
        }

        public bool DeleteFilmEntry(long id)
        {
            var response = restApiClient.HttpClient.DeleteAsync(restApiClient.HttpClient.BaseAddress + $"/films/submissions/{id}").Result;
            return response.IsSuccessStatusCode;
        }

        public bool UpdateFilmEntry(FilmEntryRequestDto filmEntryRequestDto)
        {
            var response = restApiClient.HttpClient.PutAsJsonAsync(
                    restApiClient.HttpClient.BaseAddress + $"/films/submissions/{filmEntryRequestDto.Id}",
                    filmEntryRequestDto)
                .Result;
            return response.IsSuccessStatusCode;
        }
    }
}
