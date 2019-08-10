using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MEDAPP.Models
{
    public class Appointment
    {
        
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int AppointmentCategoryId { get; set; }
        public  AppointmentCategory AppointmentCategory { get; set; }


    }
}
