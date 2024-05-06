using hltb.Dto;
using hltb.Exception;
using hltb.Models;
using hltb.Repository;
using Newtonsoft.Json;
using System.Diagnostics;

namespace hltb.Service
{
    internal class GameService : AbstractContentService
    {
        private IGameRepository _gameRepository;
#pragma warning disable CS8618
        private static GameService _instance;
#pragma warning restore CS8618 

        public static GameService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameService();
                return _instance;
            }
        }
        private GameService()
        {
            _gameRepository = RestGameRepository.Instance;
        }

        //TODO
        public override void Create(ISearchable content)
        {
            {
                GameEntryRequestDto gameEntry = GameEntryRequestDto.builder()
                    .CustomTitle(content.Title)
                    .Score(0)
                    .Status("backlog")
                    .DateCompleted( DateOnly.FromDateTime(DateTime.Today))
                    .Time(content.Time)
                    .UserId(AuthService.Instance.UserInfo.Id)
                    .GameId(content.Id)
                    .Build();
                _gameRepository.CreateGameEntry(gameEntry);
            }
        }

        public override Content? GetFromJson(string json_string)
        {
            return JsonConvert.DeserializeObject<Game>(json_string);
        }

        public override IEnumerable<ISearchable> SearchByTitle(string title)
        {
            return _gameRepository.SearchByTitle(title);
        }

        public override void Load()
        {
            contents = new List<Content> { };
            var gameDtos = _gameRepository.FindAll();
            foreach (var dto in gameDtos)
            {
                contents.Add(dtoToEnity(dto));
            }
            Debug.WriteLine("Load");
        }

        public override void Remove(long id)
        {
            if (_gameRepository.DeleteGameEntry(id))
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
            _gameRepository.UpdateGameEntry(enitiyToRequestDto(content as Game));
        }

        public Game dtoToEnity(GameEntryResponseDto gameEntryDto)
        {
            if (!DateOnly.TryParse(gameEntryDto.DateCompleted, out DateOnly dateC))
            {
                throw new JsonParseException($"Error parsing dateCompleted of game with id {gameEntryDto.Game.Id}");
            }
            if (!DateOnly.TryParse(gameEntryDto.Game.DateRelease, out DateOnly dateR))
            {
                throw new JsonParseException($"Error parsing dateRelease of game with id {gameEntryDto.Game.Id}");
            }
            return Game.builder()
                .Id(gameEntryDto.Game.Id)
                .EntryId(gameEntryDto.Id)
                .UserId(gameEntryDto.UserId)
                .Title(gameEntryDto.CustomTitle)
                .DateCompleted(dateC)
                .DateRelease(dateR)
                .Time(gameEntryDto.Time)
                .GlobalTime(gameEntryDto.Game.Time)
                .LinkUrl(gameEntryDto.Game.LinkUrl)
                .Status(gameEntryDto.Status)
                .Score(gameEntryDto.Score)
                .GlobalScore(gameEntryDto.Game.GlobalScore)
                .Note(gameEntryDto.Note)
                .Platform(gameEntryDto.Platform)
                .Build();
        }

        public GameEntryRequestDto enitiyToRequestDto(Game gameEntity)
        {
            return GameEntryRequestDto.builder()
                .Id(gameEntity.EntryId)
                .GameId(gameEntity.Id)
                .UserId(gameEntity.UserId)
                .CustomTitle(gameEntity.Title)
                .DateCompleted(gameEntity.DateCompleted)
                .Time(gameEntity.Time)
                .Status(gameEntity.Status)
                .Score(gameEntity.Score)
                .Note(gameEntity.Note)
                .Platform(gameEntity.Platform)
                .Build();
        }
    }
}
