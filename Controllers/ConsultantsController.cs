using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsychologicalCounseling.Models;

namespace PsychologicalCounseling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantsController : ControllerBase
    {
        private readonly PsychologicalCouselingContext _context;

        public ConsultantsController(PsychologicalCouselingContext context)
        {
            _context = context;
        }

        // GET: api/Consultants
        [HttpGet("Getallconsultant")]
        [Authorize]
        public IActionResult GetAllList(string search)



        {
            var result = (from s in _context.Consultants
              
                          select new
                          {
                              Id = s.Id,
                              FullName=s.FullName,
                              ImageUrl = s.ImageUrl,
                              Email = s.Email,                             
                              Address = s.Address,
                              Dob = s.Dob,
                              Gender = s.Gender,
                              Phone = s.Phone,
                              Status = s.Status,
                              Experience = s.Experience,
                              Rating = s.Rating,                
                          }).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                result = (from s in _context.Consultants
                          where s.FullName.Contains(search) || s.Experience.ToString().Contains(search) || s.Status.Contains(search)
                          select new
                          {
                              Id = s.Id,
                              FullName = s.FullName,
                              ImageUrl = s.ImageUrl,
                              Email = s.Email,                
                              Address = s.Address,
                              Dob = s.Dob,
                              Gender = s.Gender,
                              Phone = s.Phone,
                              Status = s.Status,
                              Experience = s.Experience,                               
                              Rating = s.Rating
       
                          }).ToList();
            }

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result , });
        }

        // GET: api/Consultants/5
        [HttpGet("getbyid")]
        [Authorize]
        public async Task<ActionResult> GetConsultant(int id)
        {
            var all = _context.Consultants.AsQueryable();

            all = _context.Consultants.Where(us => us.Id.Equals(id));
            var result = all.ToList();

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        // PUT: api/Consultants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> PutConsultant(Consultant con)
        {

            try
            {
                var consultant = GetAccount_byEmailConsultant(con.Email);
             //   var consu = await _context.Consultants.FindAsync(con.Id);
                if (consultant == null)
                {
                    return NotFound();
                }

                consultant.FullName = con.FullName;
                consultant.Email = con.Email;

                consultant.Address = con.Address;
                consultant.Dob = con.Dob;
                consultant.Gender = con.Gender;
                consultant.Phone = con.Phone;
                consultant.Status = con.Status;
                consultant.Experience = con.Experience;
                consultant.Rating = con.Rating;



                _context.Consultants.Update(consultant);
                await _context.SaveChangesAsync();

                return Ok(new { StatusCode = 201, Message = "Update Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Consultants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        [Authorize]

        public async Task<ActionResult<Consultant>> PostConsultant(Consultant con)
        {
            try
            {
                var consu = new Consultant();
                {
                    consu.FullName = con.FullName;
                    consu.Email = con.Email;

                    consu.Address = con.Address;
                    consu.Dob = con.Dob;
                    consu.Gender = con.Gender;
                    consu.Phone = con.Phone;
                    consu.Status = con.Status;
                    consu.Experience = con.Experience;
                    consu.Rating = con.Rating;
   
                }
                _context.Consultants.Add(consu);
                await _context.SaveChangesAsync();


                return Ok(new { StatusCode = 201, Message = "Add Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/Consultants/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteConsultant(int id)
        {
            var consu = await _context.Consultants.FindAsync(id);
            if (consu == null)
            {
                return NotFound();
            }
            if (consu.Status == "active")
                consu.Status = "inactive";
            else
                consu.Status = "active";
            
            _context.Consultants.Update(consu);
            await _context.SaveChangesAsync();

            return Ok(new { StatusCode = 200, Content = "The Consultant was deleted successfully!!" });
        }
        private Models.User GetAccount_byEmail(string email)
        {
            var account = _context.Users.Where(a => a.Email.ToUpper() == email.ToUpper()).FirstOrDefault();

            if (account == null)
            {
                return null;
            }

            return account;
        }
        private bool ConsultantExists(int id)
        {
            return _context.Consultants.Any(e => e.Id == id);
        }


        private Models.Consultant GetAccount_byEmailConsultant(string email)
        {
            var account = _context.Consultants.Where(a => a.Email.ToUpper() == email.ToUpper()).FirstOrDefault();

            if (account == null)
            {
                return null;
            }

            return account;
        }
    }
}
