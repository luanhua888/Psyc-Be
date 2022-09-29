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
    public class CustomersController : ControllerBase
    {
        private readonly PsychologicalCouselingContext _context;

        public CustomersController(PsychologicalCouselingContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [Authorize]
        [HttpGet("Getallcustomer")]
        public IActionResult GetAllList(string search)
        {
            var result = (from s in _context.Customers
                          select new
                          {
                              Id = s.Id,
                              Fullname=s.Fullname,
            ImageUrl =s.ImageUrl,
            Email =s.Email,
            Address =s.Address,
            Dob =s.Dob,
            HourBirth =s.HourBirth,
            MinuteBirth =s.MinuteBirth,
            SecondBirth =s.SecondBirth,
            Gender =s.Gender,
            Phone =s.Phone,
            Status =s.Status
                          }).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                result = (from s in _context.Customers
                          where s.Fullname.Contains(search) || s.Status.Contains(search)
                          select new
                          {
                              Id = s.Id,
                              Fullname = s.Fullname,
                              ImageUrl = s.ImageUrl,
                              Email = s.Email,
                              Address = s.Address,
                              Dob = s.Dob,
                              HourBirth = s.HourBirth,
                              MinuteBirth = s.MinuteBirth,
                              SecondBirth = s.SecondBirth,
                              Gender = s.Gender,
                              Phone = s.Phone,
                              Status = s.Status
                          }).ToList();
            }

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }
        // GET: api/Customers/5
        [HttpGet("getbyid")]
        [Authorize]
        public async Task<ActionResult> GetCustomer(int id)
        {
            var all = _context.Customers.AsQueryable();

            all = _context.Customers.Where(us => us.Id.Equals(id));
            var result = all.ToList();

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutCustomer(Customer cus)
        {

            try
            {
                var custom = GetAccount_byEmailCustomer(cus.Email);
                var customer = await _context.Customers.FindAsync(cus.Id);
                if (custom == null)
                {
                    return NotFound();
                }
                custom.Fullname = cus.Fullname;
                custom.ImageUrl = cus.ImageUrl;
                custom.Email = cus.Email;
                custom.Address = cus.Address;
                custom.Dob = cus.Dob;
                custom.HourBirth = cus.HourBirth;
                custom.MinuteBirth = cus.MinuteBirth;
                custom.SecondBirth = cus.SecondBirth;
                custom.Gender = cus.Gender;
                custom.Phone = cus.Phone;
                custom.Status = cus.Status;


                _context.Customers.Update(custom);
                await _context.SaveChangesAsync();

                return Ok(new { StatusCode = 201, Message = "Update Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        [Authorize]

        public async Task<ActionResult<Customer>> PostCustomer(Customer cus)
        {
            try
            {
                var customer = new Customer();
                {
                    customer.Fullname = cus.Fullname;
                    customer.ImageUrl = cus.ImageUrl;
                    customer.Email = cus.Email;
                    customer.Address = cus.Address;
                    customer.Dob = cus.Dob;
                    customer.HourBirth = cus.HourBirth;
                    customer.MinuteBirth = cus.MinuteBirth;
                    customer.SecondBirth = cus.SecondBirth;
                    customer.Gender = cus.Gender;
                    customer.Phone = cus.Phone;
                    customer.Status = cus.Status;
                }
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();


                return Ok(new { StatusCode = 201, Message = "Add Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            if (customer.Status == "active")
                customer.Status = "inactive";
            else
                customer.Status = "active";

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return Ok(new { StatusCode = 200, Content = "The Customer was deleted successfully!!" });
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
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
