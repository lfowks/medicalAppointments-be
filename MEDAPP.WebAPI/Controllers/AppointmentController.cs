using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEDAPP.Models;
using MEDAPP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MEDAPP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {

        private readonly IAppointmentService _svAppointment;
        private readonly IPatientService _svPatient;

        public AppointmentController(IAppointmentService appointment, IPatientService patient)
        {
            _svAppointment = appointment;
            _svPatient = patient;
        }
        // GET: api/Appointment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> Get()
        {
            var appointments = await _svAppointment.FindAll<Appointment>();
            //var paginated = await _svAppointment.Post.Skip(page * pageSize).Take(pageSize).ToListAsync();
            return appointments;
        }

        // GET: api/Appointment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> Get(int id)
        {
            var appointment = await _svAppointment.FindById<Appointment>(id);
            return appointment;
        }

        // POST: api/Appointment
        [HttpPost]
        public async Task<Appointment> Post([FromForm] Appointment appointment)
        {
            try
            {
                Patient patient = await _svPatient.FindById<Patient>(appointment.PatientId);

                var listAppointmentsPatient = _svAppointment.FindByCondition<Patient>(x => x.PatientId == appointment.PatientId);
                bool validAppointment = _svAppointment.ValidateOneAppointmentPatient(appointment, listAppointmentsPatient);

                if (!validAppointment)
                    return new Appointment
                    {
                        PatientId = appointment.PatientId,
                        Date = appointment.Date,
                        AppointmentCategoryId = appointment.AppointmentCategoryId,
                        Result = _svAppointment.ResultBuilder(appointment, true, "", "This patient already has an appointment for the same day.")
                    };

                await _svAppointment.CreateAsync<Appointment>(appointment);
            }
            catch (Exception e)
            {

                throw;
            }
            
            return appointment;
        }

        // PUT: api/Appointment/5
        [HttpPut("{id}")]
        public async Task<Appointment> Put(int id, [FromForm] Appointment appointment)
        {
            try
            {
                appointment.Id = id;
                await _svAppointment.UpdateAsync<Appointment>(appointment);
            }
            catch (Exception e)
            {

                throw;
            }

            return appointment;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<Appointment> Delete(int id)
        {
            var appointmentDeleted = new Appointment();

            try
            {
                appointmentDeleted = await _svAppointment.FindById<Appointment>(id);


                bool validAppointment = _svAppointment.ValidateCancelationDate(appointmentDeleted, DateTime.Now.Date);

                if (!validAppointment)
                    return new Appointment
                    {
                        Id = appointmentDeleted.Id,
                        PatientId = appointmentDeleted.PatientId,
                        Date = appointmentDeleted.Date,
                        AppointmentCategoryId = appointmentDeleted.AppointmentCategoryId,
                        Result = _svAppointment.ResultBuilder(appointmentDeleted, true, "", "You can not cancel your appointment in the same day you have it.")
                    };


                await _svAppointment.DeleteAsync<Appointment>(appointmentDeleted);
            }
            catch (Exception e)
            {

                throw;
            }

            return appointmentDeleted;
        }
    }
}
