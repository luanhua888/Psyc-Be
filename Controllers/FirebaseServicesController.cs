using Firebase.Auth;
using Firebase.Storage;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PsychologicalCounseling.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsychologicalCounseling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirebaseServicesController : ControllerBase
    {

        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        private readonly PsychologicalCouselingContext _context;
        private static string apiKey = "AIzaSyD4wde62JjvquWn7izXRPgpaja6Aibm3-k";

        private static string Bucket = "psychologicalcounseling-28efa.appspot.com";
        private static string AuthEmail = "teamwithfirebase@gmail.com";
        private static string AuthPassword = "Test@123";
        public FirebaseServicesController(IConfiguration config, PsychologicalCouselingContext context, IHostingEnvironment env)
        {
            _config = config;
            _context = context;
            _env = env;
        }

        [HttpPost("logincustomer")]
        public async Task<IActionResult> loginCustomer(Models.User us)
        {
            var user = GetAccount_byUsername(us.UserName);
            var customer = GetAccount_byEmailCustomer(user.Email);
            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var authen = await auth.SignInWithEmailAndPasswordAsync(user.Email, user.PassWord);

            if (user != null && customer != null && authen != null)
            {
                var hasuser = _context.Users.SingleOrDefault(p => (p.UserName.ToUpper() == us.UserName.ToUpper()) && us.PassWord == p.PassWord && p.Status == "active");
                if (hasuser == null)
                {
                    return BadRequest(" The Account not exist or Invalid email or username/password!!");
                }

                if (hasuser != null)
                {

       
                    string tokenfb = authen.FirebaseToken;
                    string uidfb = authen.User.LocalId;
                    int iddb = hasuser.Id;
                  
                    if (tokenfb != "")
                    {
                        return Ok(new { StatusCode = 200, Message = "Login with role customer successful",uidfb, iddb });
                    }

                }
            }
            return BadRequest("The Account not exist or Invalid email/password!!");
        }


        [HttpPost("loginadmin")]
        public async Task<IActionResult> loginAdmin(Models.User us)
        {
            var user = GetAccount_byUsername(us.UserName);
            //  var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            //  var authen = await auth.SignInWithEmailAndPasswordAsync(user.Email, user.PassWord);

            if (user != null )
            {
                var hasuser = _context.Users.SingleOrDefault(p => (p.UserName.ToUpper() == us.UserName.ToUpper() && us.PassWord == p.PassWord && p.Status == "active" && p.IsAdmin=="true"));
                if (hasuser == null)
                {
                    return BadRequest(" The Account not exist or Invalid email or username/password!!");
                }else
                {
                    var token = GenerateJwtToken(hasuser);
                //    string tokenfb = authen.FirebaseToken;
                //    string uidfb = authen.User.LocalId;
                    int iddb = hasuser.Id;      
                        return Ok(new { StatusCode = 200, Message = "Login with role Admin successful", JWTTOKEN = token });
                    

                }
            }
            return BadRequest("The Account not exist or Invalid email/password!!");
        }



        [HttpPost("loginconsultant")]
        public async Task<IActionResult> loginConsultant(Models.User us)
        {
            var user = GetAccount_byUsername(us.UserName);
            var consultant = GetAccount_byEmailConsultant(user.Email);
            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var authen = await auth.SignInWithEmailAndPasswordAsync(user.Email, user.PassWord);

            if (user != null && consultant != null && authen !=null)
            {
                var hasuser = _context.Users.SingleOrDefault(p => (p.UserName.ToUpper() == us.UserName.ToUpper()) && us.PassWord == p.PassWord && p.Status == "active");
                if (hasuser == null)
                {
                    return BadRequest(" The Account not exist or Invalid email or username/password!!");
                }

                if (hasuser != null)
                {

           
                    string tokenfb = authen.FirebaseToken;
                    string uidfb = authen.User.LocalId;
                    int iddb = hasuser.Id;

                    if (tokenfb != "")
                    {
                        return Ok(new { StatusCode = 200, Message = "Login with role Consultant successful", uidfb, iddb });
                    }

                }
            }
            return BadRequest("The Account not exist or Invalid email/password!!");
        }


        [HttpPost("loginapp")]
        public async Task<IActionResult> loginApp(Models.User us)
        {
            var user = GetAccount_byUsername(us.UserName);
            var consultant = GetAccount_byEmailConsultant(user.Email);
            var customer = GetAccount_byEmailCustomer(user.Email);
            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var authen = await auth.SignInWithEmailAndPasswordAsync(user.Email, user.PassWord);

            if (user != null && customer != null && authen != null)
            {
                var hasuser = _context.Users.SingleOrDefault(p => (p.UserName.ToUpper() == us.UserName.ToUpper()) && us.PassWord == p.PassWord && p.Status == "active");
                if (hasuser == null)
                {
                    return BadRequest(" The Account not exist or Invalid email or username/password!!");
                }

                if (hasuser != null)
                {


                    string tokenfb = authen.FirebaseToken;
                    string uidfb = authen.User.LocalId;
                    int idcustomer = customer.Id;
                    int iddb = hasuser.Id;

                    if (tokenfb != "")
                    {
                        return Ok(new { StatusCode = 200, Message = "Login with role customer successful", uidfb, iddb, idcustomer });
                    }

                }
            }


            if (user != null && consultant != null && authen != null)
            {
                var hasuser = _context.Users.SingleOrDefault(p => (p.UserName.ToUpper() == us.UserName.ToUpper()) && us.PassWord == p.PassWord && p.Status == "active");
                if (hasuser == null)
                {
                    return BadRequest(" The Account not exist or Invalid email or username/password!!");
                }

                if (hasuser != null)
                {


                    string tokenfb = authen.FirebaseToken;
                    string uidfb = authen.User.LocalId;
                    int iddb = hasuser.Id;
                    int idconsultant = consultant.Id;
                    if (tokenfb != "")
                    {
                        return Ok(new { StatusCode = 200, Message = "Login with role consultant successful", uidfb, iddb, idconsultant });
                    }

                }
            }
            return BadRequest("The Account not exist or Invalid email/password!!");
        }






        private Models.User GetAccount_byEmail(string email)
        {
            var account = _context.Users.Where(a => a.Email.ToUpper() == email.ToUpper() || a.UserName.ToUpper() == email.ToUpper()).FirstOrDefault();

            if (account == null)
            {
                return null;
            }

            return account;
        }




        [HttpPost("upload")]
        [Authorize]
        public async Task<IActionResult> Upload(IFormFile file)
        {

            var fileupload = file;
            FileStream fs = null;
            if (fileupload.Length > 0)
            {
                {
                    string foldername = "firebaseFiles";
                    string path = Path.Combine($"Images", $"Images/{foldername}");


                    if (Directory.Exists(path))
                    {

                        using (fs = new FileStream(Path.Combine(path, fileupload.FileName), FileMode.Create))
                        {

                            await fileupload.CopyToAsync(fs);
                        }

                        fs = new FileStream(Path.Combine(path, fileupload.FileName), FileMode.Open);


                    }


                    else
                    {
                        Directory.CreateDirectory(path);
                    }
                }
                var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));

                var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);


                var cancel = new CancellationTokenSource();

                var upload = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    }
                    ).Child("images").Child(fileupload.FileName).PutAsync(fs, cancel.Token);

                // await upload;
                try
                {
                    string link = await upload;

                    return Ok(new { StatusCode = 200, Message = "Upload FIle success" , data = link});
                }
                catch (Exception ex)
                {
                    throw;
                }

            }


            return BadRequest("Failed Upload");
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
        private Models.User GetAccount_byUsername(string username)
        {
            var account = _context.Users.Where(a => a.UserName.ToUpper() == username.ToUpper()).FirstOrDefault();

            if (account == null)
            {
                return null;
            }

            return account;
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

        private string GenerateJwtToken(Models.User user)
        {
            var securitykey = Encoding.UTF8.GetBytes(_config["Jwt:Secret"]);
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Name, user.PassWord),
     
            };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(securitykey), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }



    }
}
