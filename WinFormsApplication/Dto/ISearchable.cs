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
        public long Id { get; set; }
        public string Title { get; set; }
        public string LinkUrl { get; set; }
        public long Time { get; set; }
        public string DateRelease { get; set; }
        public float GlobalScore { get; set; }
    }
}
