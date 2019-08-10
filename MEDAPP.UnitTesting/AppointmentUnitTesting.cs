using MEDAPP.Models;
using MEDAPP.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace MEDAPP.UnitTesting
{
    public class AppointmentUnitTesting
    {

        private readonly AppointmentService _svAppointment;

        public AppointmentUnitTesting()
        {
            _svAppointment = new AppointmentService(null);
        }

        [Fact]
        public void ValidateOneAppointmentPatient_Fail()
        {


            Appointment appointment = new Appointment()
            {
                Id = 1,
                AppointmentCategoryId = 1,
                Date = DateTime.Now.Date,
                PatientId = 4
            };

            Patient patient = new Patient()
            {
                Id = 4,
                Appointment = new List<Appointment>() {

                    new Appointment
                    {
                        Id=2,
                        PatientId = 4,
                        AppointmentCategoryId = 2,
                        Date = DateTime.Now.Date

                    },
                    new Appointment
                    {
                        Id=3,
                        PatientId = 4,
                        AppointmentCategoryId = 2,
                        Date = DateTime.Now.Date.AddDays(1)

                    }

                }
            };

            bool validAppointment = _svAppointment.ValidateOneAppointmentPatient(appointment, (List<Appointment>)patient.Appointment);

            Assert.False(validAppointment);


        }



        [Fact]
        public void ValidateOneAppointmentPatient_Success()
        {


            Appointment appointment = new Appointment()
            {
                Id = 1,
                AppointmentCategoryId = 1,
                Date = DateTime.Now.Date,
                PatientId = 4
            };

            Patient patient = new Patient()
            {
                Id = 4,
                Appointment = new List<Appointment>() {

                    new Appointment
                    {
                        Id=2,
                        PatientId = 4,
                        AppointmentCategoryId = 2,
                        Date = DateTime.Now.Date.AddDays(2)

                    },
                    new Appointment
                    {
                        Id=3,
                        PatientId = 4,
                        AppointmentCategoryId = 2,
                        Date = DateTime.Now.Date.AddDays(1)

                    }

                }
            };

            bool validAppointment = _svAppointment.ValidateOneAppointmentPatient(appointment, (List<Appointment>)patient.Appointment);

            Assert.True(validAppointment);


        }


        [Fact]
        public void ValidateCancelationDate_Fail()
        {
            var appointmentDeleted = new Appointment()
            {
                Id = 8,
                Date = DateTime.Now.Date
            };

            bool validAppointment = _svAppointment.ValidateCancelationDate(appointmentDeleted, DateTime.Now.Date);

            Assert.False(validAppointment);
        }

        [Fact]
        public void ValidateCancelationDate_Success()
        {
            var appointmentDeleted = new Appointment()
            {
                Id = 9,
                Date = DateTime.Now.Date.AddDays(1)
            };

            bool validAppointment = _svAppointment.ValidateCancelationDate(appointmentDeleted, DateTime.Now.Date);

            Assert.True(validAppointment);
        }


    }
}
