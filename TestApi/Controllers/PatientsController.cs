#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly HospitalAPIContext _context;

        public PatientsController(HospitalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<Response> GetPatients()
        {
            var patients = await _context.Patients.Include(p => p.staff).ToListAsync();

            var response = new Response();

            response.statusCode = 400;

            if (patients == null)
                response.statusDescription = "ID does not exist in the table";

            if (patients != null)
            {
                response.statusCode = 200;
                response.statusDescription = "GET successful";
                response.patients = patients;
            }
            return response;
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetPatient(int id)
        {
            var patient = await _context.Patients.Include(s => s.staff).Where(s => s.id == id).FirstOrDefaultAsync();

            var response = new Response();

            response.statusCode = 400;
            
            if(patient == null)
             response.statusDescription = "ID does not exist in the table";

            if (patient != null)
            {
                response.statusCode = 200;
                response.statusDescription = "GET successful";
                response.patients.Add(patient);
            }

            return response;
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Response> PutPatient(int id, Patient patient)
        {
            var response = new Response();

            response.statusCode = 400;

            if (id != patient.id)
            {
                response.statusDescription = "ID does not exist";
                return response;
            }

            if (inputReqs(patient))
            {
                response.statusDescription = "Invalid Input(s)";
            }
            else
            {

                _context.Entry(patient).State = EntityState.Modified;

                try
                {
                    response.statusCode = 200;
                    response.statusDescription = "PUT successful";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(id))
                    {
                        response.statusCode = 400;
                        response.statusDescription = "ID does not exist"; ;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return response;
        }

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostPatient(Patient patient)
        {
            var response = new Response();

            response.statusCode = 400;

            if (inputReqs(patient))
            {
                response.statusDescription = "Invalid Input(s)";
            }
            else
            {
                if (!PatientExists(patient.id))
                {
                    _context.Patients.Add(patient);
                    await _context.SaveChangesAsync();
                    response.statusCode = 200;
                    response.statusDescription = "POST Successful";
                    CreatedAtAction("GetPatient", new { id = patient.id }, patient);
                }
                else
                {
                    response.statusDescription = "Patient with this id already exist";
                }
            }
            return response;
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<Response> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            var response = new Response();

            response.statusCode = 400;

            if (patient == null)
                response.statusDescription = "ID does not exist";

            if (patient != null)
            {
                response.statusCode = 200;
                response.statusDescription = "Delete Successful";
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }

            return response;
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.id == id);
        }

        private bool inputReqs(Patient patient)
        {
            return patient.id == null || patient.first_name == null || patient.last_name == null;
        }
    }
}
