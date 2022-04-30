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
    public class DiagnosesController : ControllerBase
    {
        private readonly HospitalAPIContext _context;

        public DiagnosesController(HospitalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Diagnoses
        [HttpGet]
        public async Task<Response> GetDiagnosis()
        {
            var diagnoses = await _context.Diagnosis.Include(p => p.patient.staff).ToListAsync();
            var response = new Response();

            response.statusCode = 400;

            if (diagnoses == null)
                response.statusDescription = "ID does not exist in the table";

            if (diagnoses != null)
            {
                response.statusCode = 200;
                response.statusDescription = "GET successful";
                response.diagnoses = diagnoses;
            }
            return response;
        }

        // GET: api/Diagnoses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetDiagnosis(int? id)
        {
            var diagnosis = await _context.Diagnosis.Include(p => p.patient.staff).Where(s => s.id == id).FirstOrDefaultAsync();

            var response = new Response();

            response.statusCode = 400;
            if (diagnosis == null)
                response.statusDescription = "ID does not exist in the table";

            if (diagnosis != null)
            {
                response.statusCode = 200;
                response.statusDescription = "GET successful";
                response.diagnoses.Add(diagnosis);
            }

            return response;
        }

        // PUT: api/Diagnoses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Response> PutDiagnosis(int? id, Diagnosis diagnosis)
        {
            var response = new Response();

            response.statusCode = 400;

            if (id != diagnosis.id)
            {
                response.statusDescription = "ID does not exist";
                return response;
            }

            if (inputReqs(diagnosis))
            {
                response.statusDescription = "Invalid Input(s)";
            }
            else
            {

                _context.Entry(diagnosis).State = EntityState.Modified;

                try
                {
                    response.statusCode = 200;
                    response.statusDescription = "PUT successful";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosisExists(id))
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

        // POST: api/Diagnoses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostDiagnosis(Diagnosis diagnosis)
        {
            var response = new Response();

            response.statusCode = 400;

            if (inputReqs(diagnosis))
            {
                response.statusDescription = "Invalid Input(s)";
            }
            else
            {
                if (!DiagnosisExists(diagnosis.id))
                {
                    _context.Diagnosis.Add(diagnosis);
                    await _context.SaveChangesAsync();
                    response.statusCode = 200;
                    response.statusDescription = "POST Successful";
                    CreatedAtAction("GetPatient", new { id = diagnosis.id }, diagnosis);
                }
                else
                {
                    response.statusDescription = "Diagnoses with this id already exist";
                }
            }
            return response;
        }

        // DELETE: api/Diagnoses/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteDiagnosis(int? id)
        {
            var diagnosis = await _context.Diagnosis.FindAsync(id);
            var response = new Response();

            response.statusCode = 400;

            if (diagnosis == null)
            {
                response.statusDescription = "ID does not exist";
            }
            else
            {
                response.statusCode = 200;
                response.statusDescription = "Delete Successful";
                _context.Diagnosis.Remove(diagnosis);
                await _context.SaveChangesAsync();
            }

            return response;
        }

        private bool DiagnosisExists(int? id)
        {
            return _context.Diagnosis.Any(e => e.id == id);
        }

        private bool inputReqs(Diagnosis diagnosis)
        {
            return diagnosis.id == null || diagnosis.patient == null || 
                diagnosis.patient.id == null || diagnosis.patient.first_name == null || diagnosis.patient.last_name == null;
        }
    }
}
