using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MEDAPP.Models.Security
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<UserRole> UserRole { get; set; }
    }
}
