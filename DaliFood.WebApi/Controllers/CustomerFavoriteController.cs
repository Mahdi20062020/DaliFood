using DaliFood.Models;
using DaliFood.Models.Identity;
using DaliFood.Utilites;
using DaliFood.WebApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DaliFood.WebApi.Controllers
{
    [Route("api/MyFavorite")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerFavoriteController : ControllerBase
    {
        readonly UnitOfWork unitofwork;
        readonly UserManager<ApplicationUser> userManager;
        public CustomerFavoriteController(UnitOfWork unitofwork,
            UserManager<ApplicationUser> userManager)
        {
            this.unitofwork = unitofwork;
            this.userManager = userManager;
        }
        [HttpPost("OnMyFavorite")]
        public ActionResult PostOnMyFavorite(CustomerFavorite model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
                if(userManager.Users.Any(p=>p.Id==userId))
                {
                    Favorite favorite = model;
                    favorite.UserId = userId;
                    bool IsSuccess = false;
                    if (model.Status)
                    {
                        IsSuccess= unitofwork.FavoriteRepository.Create(favorite);
                    }
                    else
                    {
                        IsSuccess = unitofwork.FavoriteRepository.Delete(favorite);
                    }
                    if (IsSuccess)
                    {
                        unitofwork.FavoriteRepository.Save();
                    }
                }
            }
            return BadRequest(ModelState);

        }
        [HttpGet()]
        public ActionResult GetMyFavorites()
        {
            var userId = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            if (userManager.Users.Any(p => p.Id == userId))
            {
                return Ok(unitofwork.FavoriteRepository.GetAll(where:p => p.UserId == userId));
            }
            return BadRequest();

        }
    }
}
