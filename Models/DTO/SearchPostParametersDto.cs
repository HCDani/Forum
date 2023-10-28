using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class SearchPostParametersDto
    {
        public string? Username { get; }
        public int? UserId { get; }
        public string? TitleContains { get; }

        public SearchPostParametersDto(string? username, int? userId, string? titleContains)
        {
            Username = username;
            UserId = userId;
            TitleContains = titleContains;
        }
    }
}
