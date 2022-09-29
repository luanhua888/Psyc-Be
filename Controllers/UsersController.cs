using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsychologicalCounseling.Common;
using PsychologicalCounseling.Models;
using PsychologicalCounseling.Services;

namespace PsychologicalCounseling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private Random randomGenerator = new Random();
        private readonly IMailService _mailService;
        private readonly PsychologicalCouselingContext _context;
        private static string apiKey = "AIzaSyD4wde62JjvquWn7izXRPgpaja6Aibm3-k";

        private static string Bucket = "psychologicalcounseling-28efa.appspot.com";
        private static string AuthEmail = "teamwithfirebase@gmail.com";
        private static string AuthPassword = "Test@123";
        public UsersController(PsychologicalCouselingContext context , IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        // GET: api/Users
        [HttpGet("Getalluser")]
        [Authorize]
        public IActionResult GetAllList(string search)
        {
            var result = (from s in _context.Users
                          select new
                          {
                              Id = s.Id,
                              Username = s.UserName,
                              Password = s.PassWord,
                              Email = s.Email,
                              Code = s.Code,
                              Status = s.Status
                          }).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                result = (from s in _context.Users
                          where s.UserName.Contains(search)
                          select new
                          {
                              Id = s.Id,
                              Username = s.UserName,
                              Password = s.PassWord,
                              Email = s.Email,
                              Code = s.Code,
                              Status = s.Status
                          }).ToList();
            }

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        // GET: api/Users/5
        [HttpGet("getbyid")]
        [Authorize]
        public async Task<ActionResult> GetUser(int id)
        {
            var all = _context.Users.AsQueryable();

            all = _context.Users.Where(us => us.Id.Equals(id));
            var result = all.ToList();

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> PutUser(Models.User user)
        {

            try
            {
                var us = await _context.Users.FindAsync(user.Id);
                if (us == null)
                {
                    return NotFound();
                }
                us.UserName = user.UserName;
                us.PassWord = user.PassWord;
                us.Email = user.Email;


                _context.Users.Update(us);
                await _context.SaveChangesAsync();

                return Ok(new { StatusCode = 201, Message = "Update Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("createconsultant")]

        public async Task<ActionResult<Models.User>> PostUserConsultant(Models.User user)
        {
            try
            {
                var isuser = GetAccount_byUsername(user.UserName);
                var a = GetAccount_byEmail(user.Email);
                if(a != null) return StatusCode(409, new { StatusCode = 409, message = "Email has already been, please try again with another email!" });
                if (isuser != null) return StatusCode(409, new { StatusCode = 409, message = "Username has already been, please try again with another!" });
                var us = new Models.User();
                {
                    us.UserName = user.UserName;
                    us.PassWord = user.PassWord;
                    us.Email = user.Email;
                    us.Status = "inactive";
                }
                _context.Users.Add(us);
         

                var consu = new Consultant();
                {
                    consu.Email = user.Email;
                    consu.Status = "inactive";
                }
           
                _context.Consultants.Add(consu);
                await _context.SaveChangesAsync();
                
           //     var b = GetAccount_byEmail(us.Email);
                var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                await auth.CreateUserWithEmailAndPasswordAsync(us.Email,us.PassWord);
 
                return Ok(new { StatusCode = 201, Message = "Add Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        [HttpPost("createconsultantv2")]

        public async Task<ActionResult<Models.User>> PostUserConsultantv2(Models.User user)
        {
            try
            {
                var isuser = GetAccount_byUsername(user.UserName);
                var a = GetAccount_byEmail(user.Email);
                if (a != null) return StatusCode(409, new { StatusCode = 409, message = "Email has already been, please try again with another email!" });
                if (isuser != null) return StatusCode(409, new { StatusCode = 409, message = "Username has already been, please try again with another!" });
                var us = new Models.User();
                {
                    us.UserName = user.UserName;
                    us.PassWord = user.PassWord;
                    us.Email = user.Email;
                    us.Status = "inactive";
                }
                _context.Users.Add(us);
             

                var consu = new Consultant();
                {
                    consu.Email = user.Email;
                    consu.Status = "inactive";
                }

                _context.Consultants.Add(consu);
                await _context.SaveChangesAsync();
                if (us != null)
                {
                    var b = GetAccount_byEmail(us.Email);
                    await sendCodeEmail(b);
                }
                //     var b = GetAccount_byEmail(us.Email);
                var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                await auth.CreateUserWithEmailAndPasswordAsync(us.Email, us.PassWord);
         
                return Ok(new { StatusCode = 201, Message = "Add Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }




        [HttpPost("createcustomer")]

        public async Task<ActionResult<Models.User>> PostUserCustomer(Models.User user)
        {
            try
            {
                var isuser = GetAccount_byUsername(user.UserName); 
                var a = GetAccount_byEmail(user.Email);
                if (a != null) return StatusCode(409, new { StatusCode = 409, message = "Email has already been, please try again with another email!" });
                if(isuser !=null) return StatusCode(409, new { StatusCode = 409, message = "Username has already been, please try again with another!" });
                var us = new Models.User();
                {
                    us.UserName = user.UserName;
                    us.PassWord = user.PassWord;
                    us.Email = user.Email;
                    us.Status = "inactive";
                }
                _context.Users.Add(us);
             

                var customer = new Customer();
                {
                    customer.Email = user.Email;
                    customer.Status = "inactive";
                }
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                await auth.CreateUserWithEmailAndPasswordAsync(us.Email, us.PassWord);

                return Ok(new { StatusCode = 201, Message = "Add Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var us = await _context.Users.FindAsync(id);
            if (us == null)
            {
                return NotFound();
            }
            if (us.Status == "active")
                us.Status = "inactive";
            else
                us.Status = "active";

            _context.Users.Update(us);
            await _context.SaveChangesAsync();

            return Ok(new { StatusCode = 200, Content = "The User was deleted successfully!!" });
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

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }


        private async Task<bool> sendCodeEmail(Models.User a)
        {
            try
            {             
              
                a.Code = randomGenerator.Next(1000, 9999).ToString(); //moi lan resend la reset code
                _context.Users.Update(a);
                await _context.SaveChangesAsync();

                var mailRequest = new MailRequest();
                mailRequest.ToEmail = a.Email;
                var username = mailRequest.ToEmail.Split('@')[0];
                mailRequest.Subject = " Welcome to Psychological Counseling App";
                mailRequest.Description = "Your Verify code: " + "    " + a.Code;
                mailRequest.Value = a.Code;
                await _mailService.SendEmailAsync(mailRequest.ToEmail, mailRequest.Subject, mailRequest.Description, mailRequest.Value);
                Console.WriteLine(mailRequest.ToEmail);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private Models.User GetAccount_byUsername(string username)
        {
            var account = _context.Users.Where(a => a.UserName.ToUpper() == username.ToUpper()).FirstOrDefault();

            if (account == null)
            {
                return null;
            }

            return account;
        }
        private string addTailEmail(string email)
        {
            if (!email.Contains("@")) //auto add ".com"              
            {
                return email + "@gmail.com";
            }
            return email;
        }
    }
}
