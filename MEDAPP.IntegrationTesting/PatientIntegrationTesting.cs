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
    public class PatientIntegrationTesting
    {
        private readonly TestFixture _testHostFixture;
        
        private string urlApi= "https://localhost:44389/api/patient";

        public PatientIntegrationTesting()
        {
            _testHostFixture = new TestFixture();
        }

        // Util Creations

        async Task<Patient> CreatePatient()
        {
            Patient patient = Util.PatientDummy();
            var responsePatient = await _testHostFixture.Client.PostAsync(urlApi, Util.ObjectToFormUrl(patient));
            patient = await Util.GetPatientFromResponse(responsePatient);

            if (patient == null || patient.Id == 0) return null;

            return patient;

        }

        // End Util Creations


        [Fact]
        public async Task GetById_Success()
        {
            //Creating a Patient
            Patient patient = await CreatePatient();
            if (patient == null) Assert.True(false);
            //End Creating a Patient

            bool foundpatient = false;

            var response = await _testHostFixture.Client.GetAsync(urlApi+"/"+ patient.Id);
            Patient responseBody =  await Util.GetPatientFromResponse(response);

            if (responseBody != null && responseBody.Id!=0) foundpatient = true;

            Assert.True(foundpatient);

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


            //Delete patient
            var responsePatient = await _testHostFixture.Client.DeleteAsync(urlApi + "/" + patient.Id);
            Patient patientDeleted = await Util.GetPatientFromResponse(responsePatient);
            if (patientDeleted == null && patientDeleted.Id == 0) Assert.True(false);
            //End Delete patient


            bool foundPatient = false;

            var response = await _testHostFixture.Client.GetAsync(urlApi + "/" + patientDeleted.Id);
            Patient responseBody = await Util.GetPatientFromResponse(response);

            if (responseBody != null && responseBody.Id != 0) foundPatient = true;

            Assert.False(foundPatient);

        }
                
        /// <summary>
        /// This test validates the successful Patient register
        /// </summary>
        /// <returns></returns>
        [Fact]
        async Task ValidateSuccessPatientCreate()
        {

            //Creating a Patient

            Patient patient = await CreatePatient();

            if(patient==null) Assert.True(false);

            //End Creating a Patient

            var response = await _testHostFixture.Client.GetAsync(urlApi + "/" + patient.Id);
            Patient responseBody = await Util.GetPatientFromResponse(response);

            if (responseBody != null && responseBody.Id != 0) 
                Assert.True(true);

        }


        /// <summary>
        /// This test validates the successful patient Delete
        /// </summary>
        /// <returns></returns>
        [Fact]
        async Task ValidateDeletePatient()
        {

            //Creating a Patient
                Patient patient = await CreatePatient();
                if (patient == null) Assert.True(false);
            //End Creating a Patient


            //Delete Patient
               var responsePatient = await _testHostFixture.Client.DeleteAsync(urlApi+"/"+ patient.Id);
            Patient patientDeleted = await Util.GetPatientFromResponse(responsePatient);
                if (patientDeleted == null || patientDeleted.Id==0) Assert.True(false);
            //End Delete Patient


            //Checking if is deleted
            var responsePatientCheck = await _testHostFixture.Client.GetAsync(urlApi + "/" + patientDeleted.Id);
            Patient patientCheck= await Util.GetPatientFromResponse(responsePatientCheck);
                if(patientCheck != null && patientCheck.Id!=0) Assert.True(false);
            //Checking if is deleted
            
            Assert.True(true);

        }


        /// <summary>
        /// This test validates the successful Patient Update
        /// </summary>
        /// <returns></returns>
        [Fact] 
        async Task ValidateUpdatePatient()
        {
            //Creating a Patient
            Patient patient = await CreatePatient();
            if (patient == null) Assert.True(false);
            //End Creating a Patient

            //  Change the patient object
            patient.Name = "Dummy Name Updated";
            patient.Adddress = "Dummy Address Updated";

            patient.Appointment = null;

            var response = await _testHostFixture.Client.PutAsync(urlApi+"/"+ patient.Id, Util.ObjectToFormUrl(patient));
            Patient responseBody = await Util.GetPatientFromResponse(response);

            if (responseBody == null) Assert.True(false);
            // End Change the patient object


            var responseUpdated = await _testHostFixture.Client.GetAsync(urlApi + "/" + responseBody.Id);
            Patient responseBodyUpdated = await Util.GetPatientFromResponse(responseUpdated);

            if (responseBodyUpdated == null) Assert.True(false);

            if(responseBodyUpdated.Name.Equals("Dummy Name Updated") && responseBodyUpdated.Adddress.Equals("Dummy Address Updated"))
               Assert.True(true);
        }

    }
}
