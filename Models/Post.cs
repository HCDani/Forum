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
        public int Id { get; set; }
        public User Owner { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public Post(User owner, string Title, string Body)
        {
            this.Owner = owner;
            this.Title = Title;
            this.Body = Body;
        }
    }
}
