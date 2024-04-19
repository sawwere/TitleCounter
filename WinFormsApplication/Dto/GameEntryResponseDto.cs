using hltb.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static hltb.Models.Game;

namespace hltb.Dto
{
    public class GameEntryResponseDto
    {
        public long id { get; set; }

        [JsonPropertyName("custom_title")]
        public string customTitle { get; set; }

        public string note { get; set; }
        public long score { get; set; }
        public string status { get; set; }

        [JsonPropertyName("date_completed")]
        public string dateCompleted { get; set; }
        public long time { get; set; }
        public string platform { get; set; }

        [JsonPropertyName ("client_id")]
        public long userId { get; set; }

        [JsonPropertyName  ("game")]
        public GameDto game { get; set; }

        public static GameEntryDtoBuilder builder() { return new GameEntryDtoBuilder(); }

        public class GameEntryDtoBuilder
        { 
            private GameEntryResponseDto _result;
            public GameEntryDtoBuilder()
            {
                _result = new GameEntryResponseDto();
            }

            public GameEntryDtoBuilder id(long id)
            {
                _result.id = id;
                return this;
            }

            public GameEntryDtoBuilder gameId(GameDto gameDto)
            {
                _result.game = gameDto;
                return this;
            }

            public GameEntryDtoBuilder userId(long userId)
            {
                _result.userId = userId;
                return this;
            }

            public GameEntryDtoBuilder customTitle(string title)
            {
                _result.customTitle = title;
                return this;
            }

            public GameEntryDtoBuilder dateCompleted(DateOnly date)
            {
                _result.dateCompleted = $"{date.Year}-{date.Month}-{date.Day}";
                return this;
            }

            public GameEntryDtoBuilder platform(string platform)
            {
                _result.platform = platform;
                return this;
            }

            public GameEntryDtoBuilder score(long score)
            {
                _result.score = score;
                return this;
            }

            public GameEntryDtoBuilder status(string status)
            {
                _result.status = status;
                return this;
            }

            public GameEntryDtoBuilder time(long time)
            {
                _result.time = time;
                return this;
            }

            public GameEntryDtoBuilder note(string note)
            {
                _result.note = note;
                return this;
            }

            public GameEntryResponseDto build()
            {
                return _result;
            }
        }
    }
}
