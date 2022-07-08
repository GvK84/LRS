using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace BackEnd.Data
{
    public partial class UserTitle
    {
        /// <summary>Initializes a new instance of the <see cref="UserTitle" /> class.</summary>
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
