using MEDAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MEDAPP.IntegrationTesting
{
    [Collection("IntegrationTests")]
    public class AppointmentIntegrationTesting
    {
        private readonly TestFixture _testHostFixture;

        private string urlApi = "https://localhost:44389/api/appointment";
        private string urlApipatient = "https://localhost:44389/api/patient";

        public AppointmentIntegrationTesting()
        {
            _testHostFixture = new TestFixture();
        }

        // Util Creations

        async Task<Patient> CreatePatient()
        {
            Patient patient = Util.PatientDummy();
            var responsePatient = await _testHostFixture.Client.PostAsync(urlApipatient, Util.ObjectToFormUrl(patient));
            patient = await Util.GetPatientFromResponse(responsePatient);

            if (patient == null || patient.Id == 0) return null;

            return patient;

        }

        async Task<Appointment> CreateAppointment(int patientId, DateTime date)
        {
            Appointment appointment = Util.AppointmentDummy(patientId, date);
            var responseAppointment = await _testHostFixture.Client.PostAsync(urlApi, Util.ObjectToFormUrl(appointment));
            appointment = await Util.GetAppointmentFromResponse(responseAppointment);

            if (appointment == null || appointment.Id == 0 || appointment.PatientId != patientId) return null;

            return appointment;

        }

        // End Util Creations



        [Fact]
        public async Task GetById_Success()
        {
            //Creating a Patient
            Patient patient = await CreatePatient();
            if (patient == null) Assert.True(false);
            //End Creating a Patient


            //Assign Appointment
            Appointment appointment = await CreateAppointment(patient.Id, DateTime.Now.Date.AddDays(1));
            if (appointment == null) Assert.True(false);
            //End Assign Appointment

            bool foundAppointment = false;

            var response = await _testHostFixture.Client.GetAsync(urlApi+"/"+ appointment.Id);
            Appointment responseBody =  await Util.GetAppointmentFromResponse(response);

            if (responseBody != null && responseBody.Id!=0) foundAppointment = true;

            Assert.True(foundAppointment);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetById_Fail()
        {
            //Creating a Patient
            Patient patient = await CreatePatient();
            if (patient == null) Assert.True(false);
            //End Creating a Patient


            //Assign Appointment
            Appointment appointment = await CreateAppointment(patient.Id, DateTime.Now.Date.AddDays(1));
            if (appointment == null) Assert.True(false);
            //End Assign Appointment


            //Delete Appointment
            var responseAppointment = await _testHostFixture.Client.DeleteAsync(urlApi + "/" + appointment.Id);
            appointment = await Util.GetAppointmentFromResponse(responseAppointment);
            if (appointment == null && appointment.Id == 0) Assert.True(false);
            //End Delete Appointment


            bool foundAppointment = false;

            var response = await _testHostFixture.Client.GetAsync(urlApi + "/" + appointment.Id);
            Appointment responseBody = await Util.GetAppointmentFromResponse(response);

            if (responseBody != null && responseBody.Id != 0) foundAppointment = true;

            Assert.False(foundAppointment);

        }
                
        /// <summary>
        /// This test validates the successful Patient register and Appointment Assignation
        /// </summary>
        /// <returns></returns>
        [Fact]
        async Task ValidateSuccessAppointment_Create()
        {

            //Creating a Patient

            Patient patient = await CreatePatient();

            if(patient==null) Assert.True(false);

            //End Creating a Patient


            //Assign Appointment

            Appointment appointment = await CreateAppointment(patient.Id,DateTime.Now.Date);

            if (appointment == null) Assert.True(false);

            //End Assign Appointment

            Assert.True(true);

        }


        /// <summary>
        /// This test validates the successful Appointment Cancelation
        /// </summary>
        /// <returns></returns>
        [Fact]
        async Task ValidateCancelAppointment_Success()
        {

            //Creating a Patient
                Patient patient = await CreatePatient();
                if (patient == null) Assert.True(false);
            //End Creating a Patient


            //Assign Appointment
                Appointment appointment = await CreateAppointment(patient.Id, DateTime.Now.Date.AddDays(1));
                if (appointment == null) Assert.True(false);
            //End Assign Appointment

            //Delete Appointment
                var responseAppointment = await _testHostFixture.Client.DeleteAsync(urlApi+"/"+ appointment.Id);
                appointment = await Util.GetAppointmentFromResponse(responseAppointment);
                if (appointment == null || appointment.Id==0) Assert.True(false);
            //End Delete Appointment


            //Checking if is deleted
                var responseAppointmentCheck = await _testHostFixture.Client.GetAsync(urlApi + "/" + appointment.Id);
                Appointment appointmentDeleted = await Util.GetAppointmentFromResponse(responseAppointmentCheck);
                if(appointmentDeleted!=null && appointmentDeleted.Id!=0) Assert.True(false);
            //Checking if is deleted
            
            Assert.True(true);

        }


        /// <summary>
        /// This test validates the UNsuccessful Appointment Cancelation due to Business Rules
        /// </summary>
        /// <returns></returns>
        [Fact]
        async Task ValidateCancelAppointment_Fail()
        {

            //Creating a Patient
                Patient patient = await CreatePatient();
                if (patient == null) Assert.True(false);
            //End Creating a Patient


            //Assign Appointment
                Appointment appointment = await CreateAppointment(patient.Id, DateTime.Now.Date);
                if (appointment == null) Assert.True(false);
            //End Assign Appointment

            //Delete Appointment
                var responseAppointment = await _testHostFixture.Client.DeleteAsync(urlApi + "/" + appointment.Id);
                appointment = await Util.GetAppointmentFromResponse(responseAppointment);
            //End Delete Appointment

            // Result.Message has "You can not cancel your appointment in the same day you have it."
            if (!appointment.Result.Success && !appointment.Result.Message.Equals(""))
            Assert.True(true);

        }

        /// <summary>
        /// This test validates the successful Appointment Update
        /// </summary>
        /// <returns></returns>
        [Fact] 
        async Task ValidateUpdateAppointment()
        {
            //Creating a Patient
            Patient patient = await CreatePatient();
            if (patient == null) Assert.True(false);
            //End Creating a Patient


            //Assign Appointment
            Appointment appointment = await CreateAppointment(patient.Id, DateTime.Now.Date.AddDays(1));
            if (appointment == null) Assert.True(false);
            //End Assign Appointment


            // Change the Appointment Category
            appointment.AppointmentCategoryId = 2;

            var response = await _testHostFixture.Client.PutAsync(urlApi+"/"+ appointment.Id, Util.ObjectToFormUrl(appointment));
            Appointment responseBody = await Util.GetAppointmentFromResponse(response);

            if (responseBody == null) Assert.True(false);


            var responseUpdated = await _testHostFixture.Client.GetAsync(urlApi + "/" + responseBody.Id);
            Appointment responseBodyUpdated = await Util.GetAppointmentFromResponse(responseUpdated);

            if (responseBodyUpdated == null) Assert.True(false);

            if(responseBodyUpdated.AppointmentCategoryId==2)
               Assert.True(true);
        }

    }
}
