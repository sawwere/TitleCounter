using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb.Exception
{
    internal class JsonParseException : ArgumentException
    {
        public JsonParseException(string message):base(message) { }
    }
}
