using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hltb.Dto
{
    public class GameEntryDto
    {
        private long id;
        private string note;
        private long score;
        private string status;
        private string dateCompleted;
        private long time;
        private string platform;
        [JsonPropertyName ("client_id")]
        private long userId;
        [JsonPropertyName  ("game_id")]
        private long gameId;

        [JsonPropertyName ("game")]
        private GameDto gameDto;
    }
}
