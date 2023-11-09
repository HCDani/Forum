using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public User Owner { get; private set; }
        public int OwnerId { get; set; }
        public string Title { get; private set; }
        public string Body { get; private set; }

        public Post(User owner, string Title, string Body)
        {
            this.Owner = owner;
            this.Title = Title;
            this.Body = Body;
        }
        private Post() { }
    }
}
