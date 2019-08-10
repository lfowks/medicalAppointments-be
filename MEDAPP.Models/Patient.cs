using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MEDAPP.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Adddress { get; set; }
        public string Phone { get; set; }

        public ICollection<Appointment> Appointment { get; set; }
    }
}
