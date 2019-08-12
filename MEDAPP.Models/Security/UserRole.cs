using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MEDAPP.Models.Security
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [JsonIgnore]
        public User User;

        [JsonIgnore]
        public Role Role;
    }
}
