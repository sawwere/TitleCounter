using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hltb.Dto
{
    public interface ISearchable
    {
        public long id { get; set; }
        public string title { get; set; }
        public string imageUrl { get; set; }
        public string linkUrl { get; set; }
        public long time { get; set; }
        public string dateRelease { get; set; }
        public float globalScore { get; set; }
    }
}
