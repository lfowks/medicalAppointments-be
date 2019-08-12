using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEDAPP.Models
{
    public class Appointment
    {

        public int Id { get; set; }

        public DateTime Date { get; set; }

        [NotMapped]
        public string Hours{get;set;}

        [NotMapped]
        public string Minutes { get; set; }

        public int PatientId { get; set; }
        [JsonIgnore]
        public  Patient Patient { get; set; }

        public int AppointmentCategoryId { get; set; }

        [NotMapped]
        public string AppointmentCategoryName { get; set; }

        [JsonIgnore]
        public  AppointmentCategory AppointmentCategory { get; set; }

        [NotMapped]
        public ResultEntity Result { get; set; }


    }
}
