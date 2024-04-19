using hltb.Dto;
using hltb.Exception;
using hltb.Models;
using hltb.Models.Outdated;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hltb
{
    public class State<T> : ModeState where T : Content
    {
        //private EFGenericRepository<T> repository = new EFGenericRepository<T>(new TitleCounterContext());
        HttpClient httpClient = new HttpClient();
        public State(Mainform form) : base(form) {
            httpClient.BaseAddress = new Uri("http://localhost:8080/api");
        }

        public override void Create(Content content)
        {
            if (!(content is T))
                throw new InvalidCastException($"Cant cast content to {typeof(T)}");
            else
            {
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                //repository.Create(content as T);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            }
        }

        

        public override void Load()
        {
            contents = new List<Content> { };
            var gameDtos = httpClient.GetFromJsonAsync<List<GameEntryResponseDto>>(httpClient.BaseAddress + "/users/admin/games").Result;
            foreach (var dto in gameDtos)
            {
                //contents.Add(parseGameJson(dto));
            }
        }        

        public override void Update(Content content)
        {
            if (!(content is T))
                throw new InvalidCastException("Cant cast content to T");
            else
            {
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                //repository.Update(content as T);
                
                GameDto game = new GameDto();
                game.title = "Battlefield 3";
                game.dateRelease = "2020-11-11";
                game.globalScore = 0;
                game.linkUrl = "";
                game.imageUrl = "";
                game.time = 5;
                HttpContent httpContent = JsonContent.Create(game);
                var res = httpClient.PostAsync(httpClient.BaseAddress+"/games", httpContent);
                Console.WriteLine(res.Result.StatusCode);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            }

        }

        public override void Remove(long id)
        {
            //repository.Remove(repository.FindById(id));
        }

        public override Content? GetFromJson(string json_string)
        {
            return JsonConvert.DeserializeObject<T>(json_string);
        }

        public override string ToString()
        {
            return typeof(T).ToString().Split('.').Last();
        }
    }
}
