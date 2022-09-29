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
    public class ProfilesController : ControllerBase
    {
        private readonly PsychologicalCouselingContext _context;

        public ProfilesController(PsychologicalCouselingContext context)
        {
            _context = context;
        }

        // GET: api/Profiles
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            return await _context.Profiles.ToListAsync();
        }

        // GET: api/Profiles/5
        [HttpGet("getbyid")]
        [Authorize]
        public async Task<ActionResult> GetProfile(int id)
        {
            var all = _context.Profiles.AsQueryable();

            all = _context.Profiles.Where(us => us.Id.Equals(id));
            var result = all.ToList();

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        [HttpGet("getbyidcustomer")]
        [Authorize]
        public async Task<ActionResult> GetProfileByIdCustomer(int id)
        {
            var all = _context.Profiles.AsQueryable();
            all = _context.Profiles.Where(us => us.CustomerId.Equals(id));
            var result = all.ToList();

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        // PUT: api/Profiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> PutProfile(Profile profile)
        {

            try
            {
 
                var pro = await _context.Profiles.FindAsync(profile.Id);
                if (pro == null)
                {
                    return NotFound();
                }

                pro.Name = profile.Name;
                pro.Dob = profile.Dob;
                pro.HourBirth = profile.HourBirth;
                pro.MinuteBirth = profile.MinuteBirth;
                pro.Gender = profile.Gender;
                pro.Status = profile.Status;
                pro.CustomerId = profile.CustomerId;
                pro.ZodiacId = profile.ZodiacId;
                pro.PlanetId = profile.PlanetId;
                pro.HouseId = profile.HouseId;



                _context.Profiles.Update(pro);
                await _context.SaveChangesAsync();

                return Ok(new { StatusCode = 201, Message = "Update Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Profiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<Profile>> PostProfile(Profile profile)
        {
            try
            {
                var pro = new Profile();
                {
                    pro.Name = profile.Name;
                    pro.Dob = profile.Dob;
                    pro.HourBirth = profile.HourBirth;
                    pro.MinuteBirth = profile.MinuteBirth;
                    pro.Gender = profile.Gender;
                    pro.Status = profile.Status;
                    pro.CustomerId = profile.CustomerId;
                    pro.ZodiacId = profile.ZodiacId;
          pro.PlanetId = profile.PlanetId;
                    pro.HouseId = profile.HouseId;


                }
                _context.Profiles.Add(pro);
                await _context.SaveChangesAsync();


                return Ok(new { StatusCode = 201, Message = "Add Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/Profiles/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }

        private Models.Customer GetAccount_byEmailCustomer(string email)
        {
            var account = _context.Customers.Where(a => a.Email.ToUpper() == email.ToUpper()).FirstOrDefault();

            if (account == null)
            {
                return null;
            }

            return account;
        }
    }
}
