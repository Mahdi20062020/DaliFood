using DaliFood.Utilites;
using DaliFood.WebApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public UserController(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [HttpPost("AddAddress")]
        public IActionResult PostAddAddress(Address model)
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
    }
}
