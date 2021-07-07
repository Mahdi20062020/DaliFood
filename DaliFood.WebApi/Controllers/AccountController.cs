using DaliFood.Models.Data;
using DaliFood.Models.Identity;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UnitOfWork unitofwork;
        private readonly ApplicationDbContext _db;


        public AccountController(
            UserManager<ApplicationUser> userManager,
             ApplicationDbContext db,
             UnitOfWork _unitofwork)
        {
            _userManager = userManager;
            _db = db;
            unitofwork = _unitofwork;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string Phonenumber)
        {
            if (_userManager.Users.Any(p=>p.PhoneNumber==Phonenumber))
            {
                return Ok("باشه");
            }
            else
            {
               
                //Gnerate Token
                string token = GenerateToken(Phonenumber);
                return Ok(token);

            }
        }

        private string GenerateToken(string Phonenumber)
        {
            var token = Guid.NewGuid().ToString().Substring(0, 8);
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
        [HttpPost("VerifyPhoneNumber")]
        public IActionResult VerifyPhoneNumber(string phonenumber,string token)
        {
            var usertoken = unitofwork.PhoneNumbersTokenRepository.GetAll(where: p => p.Phonenumber == phonenumber && p.TokenHash == token.GetHashCode().ToString()&&p.Status==true).FirstOrDefault();
            if (usertoken !=null)
            {
                usertoken.IsConfirm = true;
                usertoken.Status = false;
                unitofwork.PhoneNumbersTokenRepository.Modifie(usertoken);
                unitofwork.PhoneNumbersTokenRepository.Save();
                return Ok();
            }
            return BadRequest("Token Is InValid");
        }
        //public IActionResult Register()
        //{

        //}
    }
}
