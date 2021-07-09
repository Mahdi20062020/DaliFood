using DaliFood.Models.Data;
using DaliFood.Models.Identity;
using DaliFood.Utilites;
using DaliFood.WebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly UnitOfWork unitofwork;
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration configuration;



        public AccountController(
            UserManager<ApplicationUser> userManager,
             ApplicationDbContext db,
             UnitOfWork _unitofwork,
             IConfiguration configuration)
        {
            _userManager = userManager;
            _db = db;
            unitofwork = _unitofwork;
            this.configuration = configuration;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string Phonenumber, string password)
        {
            var users = _userManager.Users;
            if (users.Any(p => p.PhoneNumber == Phonenumber))
            {
                var user = _userManager.Users.Where(p => p.PhoneNumber == Phonenumber).FirstOrDefault();
                if (user == null)
                {
                    return BadRequest(new { Message = "Username or password is incorrect" });
                }
                if (!(await _userManager.CheckPasswordAsync(user, password)))
                    return BadRequest(new { Message = "Username or password is incorrect" });
                var result = TokenBuilder(user);

                return Ok(new { token = result });
            }
            else
            {
                return NotFound();
            }
        }

        private string TokenBuilder(ApplicationUser user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.GivenName, $"{user.Name} {user.Family}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
              configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddDays(30),
              signingCredentials: creds);
            var result = new JwtSecurityTokenHandler().WriteToken(token);

            return result;
        }

        private string GenerateToken(string Phonenumber)
        {
            string token;
            do
            {

                token = Guid.NewGuid().ToString().Substring(0, 8);

            }
            while (unitofwork.PhoneNumbersTokenRepository.GetAll(where: p => p.TokenHash == token.GetHashCode().ToString()).Any());

            //if (unitofwork.PhoneNumbersTokenRepository.GetAll().Any(p=>p.Phonenumber== Phonenumber))
            //{
            //  var Tokens=  unitofwork.PhoneNumbersTokenRepository.GetAll(where: p => p.Phonenumber== Phonenumber);
            //    foreach (var Token in Tokens)
            //    {
            //        unitofwork.PhoneNumbersTokenRepository.Delete(token);
            //    }
            //    unitofwork.PhoneNumbersTokenRepository.Save();
            //}
            var phonenumbertoken = new PhoneNumbersToken()
            {
                CreateDate = DateTime.Now,
                Phonenumber = Phonenumber,
                TokenHash = token.GetHashCode().ToString(),
                Status = true,
                IsConfirm = false
            };
            unitofwork.PhoneNumbersTokenRepository.Create(phonenumbertoken);
            unitofwork.PhoneNumbersTokenRepository.Save();
            return token;
        }
        [HttpPost("Register/VerifyPhoneNumber")]
        public IActionResult VerifyPhoneNumber(string phonenumber, string token)
        {
            var usertoken = unitofwork.PhoneNumbersTokenRepository.GetAll(where: p => p.Phonenumber == phonenumber && p.TokenHash == token.GetHashCode().ToString() && p.Status == true).FirstOrDefault();
            if (usertoken != null)
            {
                usertoken.IsConfirm = true;
                usertoken.Status = false;
                unitofwork.PhoneNumbersTokenRepository.Modifie(usertoken);
                unitofwork.PhoneNumbersTokenRepository.Save();
                return Ok(usertoken);
            }
            ModelState.AddModelError("Token Validation", "Token Is inValid");
            return BadRequest(ModelState);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(Register model, string phonenumber, int token)
        {
            if (ModelState.IsValid)
            {
                if (unitofwork.PhoneNumbersTokenRepository.GetAll(where: p => p.Phonenumber == phonenumber && p.TokenHash == token.ToString() && p.Status == false && p.IsConfirm == true).Any())
                {
                    ApplicationUser user = new ApplicationUser()
                    {
                        Name = model.Name,
                        Family = model.Family,
                        PhoneNumber = phonenumber,
                        PhoneNumberConfirmed = true,
                        UserName = model.Name + "_" + model.Family
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (!result.Succeeded)
                    {
                        return BadRequest(result.Errors);
                    }
                    await _userManager.AddToRoleAsync(user, SD.NormalUserRole);
                    var usertoken = unitofwork.PhoneNumbersTokenRepository.GetAll(where: p => p.Phonenumber == phonenumber && p.TokenHash == token.ToString());
                    if (unitofwork.PhoneNumbersTokenRepository.Delete(usertoken))
                    {
                        unitofwork.PhoneNumbersTokenRepository.Save();
                    }
                    return Ok(user);
                }
            }
            return BadRequest();
        }

        [HttpPost("Register/SetPhoneNumber")]
        public IActionResult SetPhoneNumber(string phonenumber)
        {
            if (Utilites.Utilites.IsPhoneNumber(phonenumber))
            {
                var users = _userManager.Users;
                if (!users.Any(p => p.PhoneNumber == phonenumber))
                {
                    //Gnerate Token
                    string token = GenerateToken(phonenumber);
                    return Ok(token);
                }
                ModelState.AddModelError("PhoneNumber", "The PhoneNumber Is Exist");
            }
            else
            {
                ModelState.AddModelError("PhoneNumber", "The PhoneNumber Is InCorrect");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Register/ForgetPassword")]
        public IActionResult ForgetPassword(string phonenumber)
        {
            if (Utilites.Utilites.IsPhoneNumber(phonenumber))
            {
                var users = _userManager.Users;
                if (users.Any(p => p.PhoneNumber == phonenumber))
                {
                    //Gnerate Token
                    string token = GenerateToken(phonenumber);
                    return Ok(token);
                }
                ModelState.AddModelError("PhoneNumber", "The PhoneNumber Is Not Exist");
            }
            else
            {
                ModelState.AddModelError("PhoneNumber", "The PhoneNumber Is InCorrect");
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Register/SetPassword")]
        public async Task<IActionResult> SetPassword(SetPassword model,[Phone(ErrorMessage ="شماره تلفن وارد شده نامعتبر است")] string phonenumber, int token)
        {
            if (ModelState.IsValid)
            {
                if (Utilites.Utilites.IsPhoneNumber(phonenumber))
                {
                if (unitofwork.PhoneNumbersTokenRepository.GetAll(where: p => p.Phonenumber == phonenumber && p.TokenHash == token.ToString() && p.Status == false && p.IsConfirm == true).Any())
                {
                    var user = _userManager.Users.Where(p => p.PhoneNumber == phonenumber).FirstOrDefault();

                    var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, resetToken, model.Password);
                    if (!result.Succeeded)
                    {
                        return BadRequest(result.Errors);
                    }
                    var usertoken = unitofwork.PhoneNumbersTokenRepository.GetAll(where: p => p.Phonenumber == phonenumber && p.TokenHash == token.ToString());
                    if (unitofwork.PhoneNumbersTokenRepository.Delete(usertoken))
                    {
                        unitofwork.PhoneNumbersTokenRepository.Save();
                    }
                    return Ok(user);
                }
                }
                else
                {
                    ModelState.AddModelError("PhoneNumber", "The PhoneNumber Is InCorrect");
                }
            }
            return BadRequest(ModelState);
        }

    }
}
