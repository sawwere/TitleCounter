using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hltb.Dto
{
    public class UserLoginDto
    {
        public string username { get; set; }
        public string password { get; set; }

        public UserLoginDto(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public UserLoginDto()
        {
        }
    }
}
