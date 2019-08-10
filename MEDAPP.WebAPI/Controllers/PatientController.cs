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
            //var paginated = await _context.Post.Skip(page * pageSize).Take(pageSize).ToListAsync();
            return patients;
        }

        // GET: api/Patient/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Patient
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Patient/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
