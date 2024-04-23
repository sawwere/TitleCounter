using hltb.Dto;
using hltb.Exception;
using hltb.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace hltb.Service
{
    internal class GameService : ModeState
    {
        private readonly RestApiSerice restApiSerice = RestApiSerice.Instance;
        public GameService(Mainform form) : base(form)
        {
            //httpClient.BaseAddress = new Uri("http://localhost:8080/api");
        }

        //TODO
        public override void Create(Content content)
        {
            if (!(content is Game))
                throw new InvalidCastException("Cant cast content to T");
            else
            {
                GameDto game = new GameDto();
                game.title = "Battlefield 3";
                game.dateRelease = "2020-11-11";
                game.globalScore = 0;
                game.linkUrl = "";
                game.imageUrl = "";
                game.time = 5;
                HttpContent httpContent = JsonContent.Create(game);
                //var res = httpClient.PostAsync(httpClient.BaseAddress + "/games", httpContent);
                //Console.WriteLine(res.Result.StatusCode);
            }
        }

        public override Content? GetFromJson(string json_string)
        {
            return JsonConvert.DeserializeObject<Game>(json_string);
        }

        public override void Load()
        {
            contents = new List<Content> { };
            var gameDtos = restApiSerice.findGames();
            foreach (var dto in gameDtos)
            {
                contents.Add(dtoToEnity(dto));
            }
        }

        public override void Remove(long id)
        {
            if (restApiSerice.deleteGame(id))
            {
                contents.Remove(contents.First(x => x.EntryId == id));
            }
        }

        public override string ToString()
        {
            return "games";
        }

        public override void Update(Content content)
        {
            restApiSerice.updateGame(enitiyToRequestDto(content as Game));
        }

        public Game dtoToEnity(GameEntryResponseDto gameEntryDto)
        {
            if (!DateOnly.TryParse(gameEntryDto.dateCompleted, out DateOnly dateC))
            {
                throw new JsonParseException($"Error parsing dateCompleted of game with id {gameEntryDto.game.id}");
            }
            if (!DateOnly.TryParse(gameEntryDto.game.dateRelease, out DateOnly dateR))
            {
                throw new JsonParseException($"Error parsing dateRelease of game with id {gameEntryDto.game.id}");
            }
            return Game.builder()
                .id(gameEntryDto.game.id)
                .entryId(gameEntryDto.id)
                .userId(gameEntryDto.userId)
                .title(gameEntryDto.customTitle)
                .dateCompleted(dateC)
                .dateRelease(dateR)
                .time(gameEntryDto.time)
                .globalTime(gameEntryDto.game.time)
                .linkUrl(gameEntryDto.game.linkUrl)
                .imageUrl(gameEntryDto.game.imageUrl)
                .status(gameEntryDto.status)
                .score(gameEntryDto.score)
                .globalScore(gameEntryDto.game.globalScore)
                .note(gameEntryDto.note is null ? "" : gameEntryDto.note)
                .platform(gameEntryDto.platform)
                .build();
        }

        public GameEntryRequestDto enitiyToRequestDto(Game gameEntity)
        {
            return GameEntryRequestDto.builder()
                .id(gameEntity.EntryId)
                .gameId(gameEntity.Id)
                .userId(gameEntity.UserId)
                .customTitle(gameEntity.Title)
                .dateCompleted(gameEntity.DateCompleted)
                .time(gameEntity.Time)
                .status(gameEntity.Status)
                .score(gameEntity.Score)
                .note(gameEntity.Note)
                .platform(gameEntity.Platform)
                .build();
        }
    }
}
