using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEDAPP.Models;
using MEDAPP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MEDAPP.WebAPI.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _svPatient;

        public PatientController(IPatientService patient)
        {
            _svPatient = patient;
        }
        // GET: api/Patient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> Get()
        {

            var patients = await _svPatient.FindAll<Patient>();
            //var paginated = await _svPatient.Post.Skip(page * pageSize).Take(pageSize).ToListAsync();
            return patients;
        }

        // GET: api/Patient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> Get(int id)
        {
            var patient = await _svPatient.FindById<Patient>(id);
            return patient;
        }

        // POST: api/Patient
        [HttpPost]
        public async Task<Patient> Post([FromBody] Patient patient)
        {
            try
            {
              await _svPatient.CreateAsync<Patient>(patient);
            }
            catch (Exception e)
            {

                throw;
            }

            //try
            //{
            //  await _svPatient.CreateAsync<Patient>(patient);

            //}
            //catch (Exception e)
            //{
            //    return Result.ResultBuilder(patient, true);
            //}

            //return Result.ResultBuilder(patient, false);

            return patient;
        }

        // PUT: api/Patient/5
        [HttpPut("{id}")]
        public async Task<Patient> Put(int id,[FromBody] Patient patient)
        {
            try
            {
                patient.Id = id;
                await _svPatient.UpdateAsync<Patient>(patient);
            }
            catch (Exception e)
            {

                throw;
            }

            return patient;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<Patient> Delete(int id)
        {
            var patientDeleted = new Patient();

            try
            {
                patientDeleted = await _svPatient.FindById<Patient>(id);
                await _svPatient.DeleteAsync<Patient>(patientDeleted);
            }
            catch (Exception e)
            {

                throw;
            }

            return patientDeleted;
        }
    }
}
