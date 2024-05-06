using hltb.Dto;
using hltb.Exception;
using hltb.Models;
using hltb.Repository;
using Newtonsoft.Json;

namespace hltb.Service
{
    internal class FilmService : AbstractContentService
    {
        private IFilmRepository _filmRepository;
#pragma warning disable CS8618
        private static FilmService _instance;
#pragma warning restore CS8618 

        public static FilmService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FilmService();
                return _instance;
            }
        }
        public FilmService()
        {
            _filmRepository = RestFilmRepository.Instance;
        }

        //TODO
        public override void Create(ISearchable content)
        {
            {
                FilmEntryRequestDto filmEntry = FilmEntryRequestDto.builder()
                    .CustomTitle(content.Title)
                    .Score(0)
                    .Status("backlog")
                    .DateCompleted(DateOnly.FromDateTime(DateTime.Today))
                    .UserId(AuthService.Instance.UserInfo.Id)
                    .FilmId(content.Id)
                    .Build();

                _filmRepository.CreateFilmEntry(filmEntry);
            }
        }

        public override Content? GetFromJson(string json_string)
        {
            return JsonConvert.DeserializeObject<Film>(json_string);
        }

        public override IEnumerable<ISearchable> SearchByTitle(string title)
        {
            return _filmRepository.SearchByTitle(title);
        }

        public override void Load()
        {
            contents = new List<Content> { };
            var filmDtos = _filmRepository.FindAll();
            foreach (var dto in filmDtos)
            {
                contents.Add(DtoToEnity(dto));
            }
        }

        public override void Remove(long id)
        {
            if (_filmRepository.DeleteFilmEntry(id))
            {
                contents.Remove(contents.First(x => x.EntryId == id));
            }
        }

        public override string ToString()
        {
            return "films";
        }

        public override void Update(Content content)
        {
            if ((Film)content is null)
            {
                throw new InvalidCastException(nameof(content));
            }
            _filmRepository.UpdateFilmEntry(enitiyToRequestDto(content as Film));
        }

        public Film DtoToEnity(FilmEntryResponseDto filmEntryDto)
        {
            if (!DateOnly.TryParse(filmEntryDto.DateCompleted, out DateOnly dateC))
            {
                throw new JsonParseException($"Error parsing dateCompleted of film with id {filmEntryDto.Film.Id}");
            }
            if (!DateOnly.TryParse(filmEntryDto.Film.DateRelease, out DateOnly dateR))
            {
                throw new JsonParseException($"Error parsing dateRelease of film with id {filmEntryDto.Film.Id}");
            }
            return Film.Builder()
                .Id(filmEntryDto.Film.Id)
                .EntryId(filmEntryDto.Id)
                .UserId(filmEntryDto.UserId)
                .Title(filmEntryDto.CustomTitle)
                .DateCompleted(dateC)
                .DateRelease(dateR)
                .Time(filmEntryDto.Film.Time)
                .LinkUrl(filmEntryDto.Film.LinkUrl)
                .Status(filmEntryDto.Status)
                .Score(filmEntryDto.Score)
                .GlobalScore(filmEntryDto.Film.GlobalScore)
                .Note(filmEntryDto.Note)
                .Build();
        }

        public FilmEntryRequestDto enitiyToRequestDto(Film filmEntity)
        {
            return FilmEntryRequestDto.builder()
                .Id(filmEntity.EntryId)
                .FilmId(filmEntity.Id)
                .UserId(filmEntity.UserId)
                .CustomTitle(filmEntity.Title)
                .DateCompleted(filmEntity.DateCompleted)
                .Status(filmEntity.Status)
                .Score(filmEntity.Score)
                .Note(filmEntity.Note)
                .Build();
        }
    }
}
