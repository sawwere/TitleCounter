using hltb.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb.Repository
{
    internal interface IFilmRepository
    {
        public List<FilmDto> SearchByTitle(string title);

        public List<FilmEntryResponseDto> FindAll();

        public bool CreateFilmEntry(FilmEntryRequestDto filmEntryRequestDto);

        public bool UpdateFilmEntry(FilmEntryRequestDto filmEntryRequestDto);

        public bool DeleteFilmEntry(long id);
    }
}
