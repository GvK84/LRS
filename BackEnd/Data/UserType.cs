using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace BackEnd.Data
{
    public partial class UserType
    {
        /// <summary>Initializes a new instance of the <see cref="UserType" /> class.</summary>
        public UserType()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
