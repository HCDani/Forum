using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class PostBasicDto
    {
        public int Id { get; }
        public string OwnerName { get; }
        public string Title { get; }
        public string Body {  get; }

        public PostBasicDto(int id, string ownerName, string title, string body)
        {
            Id = id;
            OwnerName = ownerName;
            Title = title;
            Body = body;
        }
    }
}
