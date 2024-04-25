using hltb.Dto;
using hltb.Exception;
using hltb.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace hltb.Service
{
    internal class GameService : ModeState
    {
        public GameService(Mainform form) : base(form)
        {
            //httpClient.BaseAddress = new Uri("http://localhost:8080/api");
        }

        //TODO
        public override void Create(ISearchable content)
        {
            {
                GameEntryRequestDto gameEntry = GameEntryRequestDto.builder()
                    .customTitle(content.title)
                    .score(0)
                    .status("backlog")
                    .dateCompleted( DateOnly.FromDateTime(DateTime.Today))
                    .time(content.time)
                    .userId(1) // TODO
                    .gameId(content.id)
                    .build();
                
                RestApiSerice.Instance.createGameEntry(gameEntry);
            }
        }

        public override Content? GetFromJson(string json_string)
        {
            return JsonConvert.DeserializeObject<Game>(json_string);
        }

        public override IEnumerable<ISearchable> Search(string title)
        {
            return RestApiSerice.Instance.searchGames(title);
        }

        public override void Load()
        {
            contents = new List<Content> { };
            var gameDtos = RestApiSerice.Instance.findGames();
            foreach (var dto in gameDtos)
            {
                contents.Add(dtoToEnity(dto));
            }
        }

        public override void Remove(long id)
        {
            if (RestApiSerice.Instance.deleteGameEntry(id))
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
            if ((Game)content is null)
            {
                throw new InvalidCastException(nameof(content));
            }
            RestApiSerice.Instance.updateGameEntry(enitiyToRequestDto(content as Game));
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
