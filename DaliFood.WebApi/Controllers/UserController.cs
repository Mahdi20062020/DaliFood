using DaliFood.Models.Identity;
using DaliFood.Utilites;
using DaliFood.WebApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DaliFood.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]


    public class UserController : Controller
    {
        readonly UnitOfWork unitofwork;
        readonly UserManager<ApplicationUser> userManager;
        public UserController(UnitOfWork unitofwork, UserManager<ApplicationUser> userManager)
        {
            this.unitofwork = unitofwork;
            this.userManager = userManager;
        }
     
        [HttpPost("AddAddress")]
        public IActionResult PostAddAddress(ViewModels.Address model)
        {
            if (ModelState.IsValid)
            {
                if (model.Latitude== 0.ToString())
                {
                    model.Latitude = null;
                } 
                if (model.Longitude== 0.ToString())
                {
                    model.Longitude = null;
                }
                var userId = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
                var Address = new Models.Identity.Address()
                {
                    TextAddress = model.TextAddress,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    UserId = userId
                };
                unitofwork.AddressRepository.Create(Address);
                unitofwork.AddressRepository.Save();
            }
            return Ok();
        }
       
        [HttpGet("Profile")]
        public IActionResult GetProfileDetail()
        {
            var userId = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

            var user = userManager.Users.Where(p => p.Id == userId).FirstOrDefault();
            var userdetail = unitofwork.ApplicationNormalUserRepository.GetById(userId);
            return Ok(new{name= user.Name,family=user.Family, Wallet= userdetail.Wallet});
        }
        [HttpGet("MyComments")]
        public IActionResult GetMyComments()
        {
            var userId = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

  
            return Ok(unitofwork.CustomerCommentRepository.GetAll(where:p=>p.UserId==userId));
        }
    }
}
