using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace BackEnd.Data
{
    public partial class UserTitle
    {
        public UserTitle()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
