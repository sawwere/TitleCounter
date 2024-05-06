using hltb.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace hltb.Repository
{
    internal interface IGameRepository
    {
        public List<GameDto> SearchByTitle(string title);

        public List<GameEntryResponseDto> FindAll();

        public bool CreateGameEntry(GameEntryRequestDto gameEntryRequestDto);

        public bool UpdateGameEntry(GameEntryRequestDto gameEntryRequestDto);

        public bool DeleteGameEntry(long id);
    }
}
