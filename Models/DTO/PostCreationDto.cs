using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class PostCreationDto
    {
        public int OwnerId { get; }
        public string Title { get; }
        public string Body { get; }

        public PostCreationDto(int ownerId, string title, string body)
        {
            OwnerId = ownerId;
            Title = title;
            Body = body;
        }
    }
}
