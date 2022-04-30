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
    public class StaffsController : ControllerBase
    {
        private readonly HospitalAPIContext _context;

        public StaffsController(HospitalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Staffs
        [HttpGet]
        public async Task<Response> GetStaffs()
        {
            var staffs = await _context.Staffs.ToListAsync();

            var response = new Response();

            response.statusCode = 400;

            if (staffs == null)
            {
                response.statusDescription = "ID does not exist in the table";
            }
            else
            {
                response.statusCode = 200;
                response.statusDescription = "GET successful";
                response.staffs = staffs;
            }
            return response;
        }

        // GET: api/Staffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetStaff(int? id)
        {
            var staff = await _context.Staffs.FindAsync(id);

            var response = new Response();

            response.statusCode = 400;

            if (staff == null)
            {
                response.statusDescription = "ID does not exist in the table";
            }
            else
            {
                response.statusCode = 200;
                response.statusDescription = "GET successful";
                response.staffs.Add(staff);
            }

            return response;
        }

        // PUT: api/Staffs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Response> PutStaff(int? id, Staff staff)
        {
            var response = new Response();

            response.statusCode = 400;

            if (id != staff.id)
            {
                response.statusDescription = "ID does not exist";
                return response;
            }

            if (inputReqs(staff))
            {
                response.statusDescription = "Invalid Input(s)";
            }
            else
            {

                _context.Entry(staff).State = EntityState.Modified;

                try
                {
                    response.statusCode = 200;
                    response.statusDescription = "PUT successful";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(id))
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

        // POST: api/Staffs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostStaff(Staff staff)
        {        
            var response = new Response();

            response.statusCode = 400;         

            if(inputReqs(staff))
            {
                response.statusDescription = "Invalid Input(s)";
            }
            else
            {
                if (!StaffExists(staff.id))
                {
                    _context.Staffs.Add(staff);
                    await _context.SaveChangesAsync();
                    response.statusCode = 200;
                    response.statusDescription = "POST Successful";
                    CreatedAtAction("GetStaff", new { id = staff.id }, staff);
                }
                else
                {
                    response.statusDescription = "Staff with this id already exist";
                }
            }
            return response;
        }

        // DELETE: api/Staffs/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteStaff(int? id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            var response = new Response();

            response.statusCode = 400;

            if (staff == null)
            {
                response.statusDescription = "ID does not exist";
            }
            else
            { 
                response.statusCode = 200;
                response.statusDescription = "Delete Successful";
                _context.Staffs.Remove(staff);
                await _context.SaveChangesAsync();
            }

            return response;
        }

        private bool StaffExists(int? id)
        {
            return _context.Staffs.Any(e => e.id == id);
        }

        private bool inputReqs(Staff staff)
        {
            return staff.id == null || staff.first_name == null || staff.last_name == null || staff.position == null;
        }
    }
}
