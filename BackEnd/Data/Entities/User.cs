using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

#nullable disable

namespace BackEnd.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public int UserTypeId { get; set; }
        public int UserTitleId { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsActive { get; set; }


        [NotMapped]
        public string UserTitleDesc { get { return UserTitle.Description; } }
        [NotMapped]
        public string UserTypeDesc { get { return UserType.Description; } }

        [JsonIgnore]
        public virtual UserTitle UserTitle { get; set; }
        [JsonIgnore]
        public virtual UserType UserType { get; set; }
    }
}
