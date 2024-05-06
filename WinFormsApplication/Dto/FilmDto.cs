using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hltb.Dto
{
    public class FilmDto : ISearchable
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }


        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("rus_title")]
        public string RusTitle { get; set; }


        [JsonPropertyName("link_url")]
        public string LinkUrl { get; set; }


        [JsonPropertyName("time")]
        public long Time { get; set; }


        [JsonPropertyName("date_release")]
        public string DateRelease { get; set; }


        [JsonPropertyName("global_score")]
        public float GlobalScore { get; set; }
    }
}
