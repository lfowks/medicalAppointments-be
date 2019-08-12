using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEDAPP.Models.Security
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [NotMapped]
        public string PasswordHash { get; set; }

        [NotMapped]
        public string RoleSelected { get; set; }

        [NotMapped]
        public ICollection<Role> Roles { get; set; }

        [JsonIgnore]
        public ICollection<UserRole> UserRole { get; set; }
    }
}
