using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class PostUpdateDto
    {
        public int Id { get; }
        public int? OwnerId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }

        public PostUpdateDto(int id)
        {
            Id = id;
        }
    }
}
