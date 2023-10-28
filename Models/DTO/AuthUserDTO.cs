using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class AuthUserDTO
    {
        public string UserName { get; }
        public string Password { get; }

        public AuthUserDTO(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
