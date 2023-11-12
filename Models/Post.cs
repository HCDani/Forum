using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [JsonInclude]
        public User Owner { get; private set; }
        public int OwnerId { get; set; }
        public string Title { get; private set; }
        public string Body { get; private set; }

        public Post(int ownerId, string Title, string Body)
        {
            this.OwnerId = ownerId;
            this.Title = Title;
            this.Body = Body;
        }
        private Post() { }
    }
}
