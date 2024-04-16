using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hltb.Dto
{
    public class GameDto
    {
        public long id { get; set; }
        public string title { get; set; }

        [JsonPropertyName("image_url")]
        public string imageUrl { get; set; }

        [JsonPropertyName("link_url")]
        public string linkUrl { get; set; }

        public long time { get; set; }

        [JsonPropertyName("date_release")]
        public string dateRelease { get; set; }

        [JsonPropertyName("global_score")]
        public float globalScore { get; set; }
        public GameDto() { }
    }
}
