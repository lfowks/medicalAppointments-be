using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MEDAPP.Models
{
    public class AppointmentCategory
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Appointment> Appointment { get; set; }

    }
}
