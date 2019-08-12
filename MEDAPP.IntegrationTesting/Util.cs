
using MEDAPP.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Text;

namespace MEDAPP.IntegrationTesting
{
    public class Util
    {
        public async static Task<T> Deserialize<T>(HttpResponseMessage response)
        {
            string stringJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(stringJson);
        }

        public async static Task<Appointment> GetAppointmentFromResponse(HttpResponseMessage response)
        {
            return await Deserialize<Appointment>(response);
        }

        public async static Task<List<Appointment>> GetListAppointmentFromResponse(HttpResponseMessage response)
        {
            return await Deserialize<List<Appointment>>(response);
        }

        public async static Task<Patient> GetPatientFromResponse(HttpResponseMessage response)
        {
            return await Deserialize<Patient>(response);
        }

        public async static Task<List<Patient>> GetListPatientFromResponse(HttpResponseMessage response)
        {
            return await Deserialize<List<Patient>>(response);
        }

        public static FormUrlEncodedContent ObjectToFormUrl(object entity)
        {
            return new FormUrlEncodedContent(JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(entity)));
        }

        public static StringContent GetStringContent(object entity)
        {
            return new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        }


        public static Patient PatientDummy()
        {
            return new Patient()
            {
                Name = "Dummy",
                Address = "SJ CR",
                Email = "dummy@dummy.com",
                Phone = "8888-88888"
            };
        }

        public static Appointment AppointmentDummy(int idPatient, DateTime date)
        {
            return new Appointment()
            {
                PatientId = idPatient,
                Date = date,
                AppointmentCategoryId = 1
            };
        }
    }
}
