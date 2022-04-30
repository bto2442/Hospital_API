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
    public class PrescriptionsController : ControllerBase
    {
        private readonly HospitalAPIContext _context;

        public PrescriptionsController(HospitalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Prescriptions
        [HttpGet]
        public async Task<Response> GetPrescriptions()
        {
            var prescriptions =  await _context.Prescriptions.Include(p => p.patient.staff).ToListAsync();

            var response = new Response();

            response.statusCode = 400;

            if (prescriptions == null)
                response.statusDescription = "ID does not exist in the table";

            if (prescriptions != null)
            {
                response.statusCode = 200;
                response.statusDescription = "GET successful";
                response.prescriptions = prescriptions;
            }
            return response;
        }

        // GET: api/Prescriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetPrescription(int? id)
        {
            var prescription = await _context.Prescriptions.Include(p => p.patient.staff).Where(s => s.id == id).FirstOrDefaultAsync();

            var response = new Response();

            response.statusCode = 400;

            if (prescription == null)
                response.statusDescription = "ID does not exist in the table";

            if (prescription != null)
            {
                response.statusCode = 200;
                response.statusDescription = "GET successful";
                response.prescriptions.Add(prescription);
            }

            return response;
        }

        // PUT: api/Prescriptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Response> PutPrescription(int? id, Prescription prescription)
        {
            var response = new Response();

            response.statusCode = 400;

            if (id != prescription.id)
            {
                response.statusDescription = "ID does not exist";
                return response;
            }

            if (inputReqs(prescription))
            {
                response.statusDescription = "Invalid Input(s)";
            }
            else
            {

                _context.Entry(prescription).State = EntityState.Modified;

                try
                {
                    response.statusCode = 200;
                    response.statusDescription = "PUT successful";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescriptionExists(id))
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

        // POST: api/Prescriptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostPrescription(Prescription prescription)
        {
            var response = new Response();

            response.statusCode = 400;

            if (inputReqs(prescription))
            {
                response.statusDescription = "Invalid Input(s)";
            }
            else
            {
                if (!PrescriptionExists(prescription.id))
                {
                    _context.Prescriptions.Add(prescription);
                    await _context.SaveChangesAsync();
                    response.statusCode = 200;
                    response.statusDescription = "POST Successful";
                    CreatedAtAction("GetPrescription", new { id = prescription.id }, prescription);
                }
                else
                {
                    response.statusDescription = "Prescription with this id already exist";
                }
            }
            return response;
        }

        // DELETE: api/Prescriptions/5
        [HttpDelete("{id}")]
        public async Task<Response> DeletePrescription(int? id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            var response = new Response();

            response.statusCode = 400;

            if (prescription == null)
            {
                response.statusDescription = "ID does not exist";
            }
            else
            { 
                response.statusCode = 200;
                response.statusDescription = "Delete Successful";
                _context.Prescriptions.Remove(prescription);
                await _context.SaveChangesAsync();
            }

            return response;
        }

        private bool PrescriptionExists(int? id)
        {
            return _context.Prescriptions.Any(e => e.id == id);
        }
        private bool inputReqs(Prescription prescriptions)
        {
            return prescriptions.id == null || prescriptions.patient == null ||
                prescriptions.patient.id == null || prescriptions.patient.first_name == null || prescriptions.patient.last_name == null;
        }
    }
}
