using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
     public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        [JsonIgnore]
        public ICollection<Post> Posts { get; set; }
    }
}
