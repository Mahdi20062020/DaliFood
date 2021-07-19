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
        /// <summary>افزودن رستوران به لیست علاقه‌مندی ها و یا حذف ات از لیست علاقه مندی ها
        /// </summary>
        /// <param name="model">داده های مربوط به لیست علاقه مندی ها</param>
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
                        if(!unitofwork.FavoriteRepository.GetAll(where:p=>p.UserId==favorite.UserId && p.CustomerId == favorite.CustomerId).Any())
                        {
                            IsSuccess = unitofwork.FavoriteRepository.Create(favorite);
                        }
                    }
                    else
                    {
                        if (unitofwork.FavoriteRepository.GetAll(where: p => p.UserId == favorite.UserId && p.CustomerId == favorite.CustomerId).Any())
                        {
                            IsSuccess = unitofwork.FavoriteRepository.Delete(favorite);
                        }
                    }
                    if (IsSuccess)
                    {
                        unitofwork.FavoriteRepository.Save();
                        return Ok();
                    }
                }
            }
            return BadRequest(ModelState);

        }
        /// <summary>دریافت لیست علاقه‌مندی ها
        /// </summary>
        /// <param name="ItemPerPage">تعداد آیتم های نمایشی</param>
        /// <param name="PageNum">صفحه مورد نمایش</param>
        [HttpGet()]
        public ActionResult GetMyFavorites(int? ItemPerPage, int? PageNum)
        {
            var userId = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            if (userManager.Users.Any(p => p.Id == userId))
            {
                List<ViewModels.Customer> ItemsforShow = new();

                var favorites = unitofwork.FavoriteRepository.GetAll(where: p => p.UserId == userId);
                foreach (var item in favorites)
                {
                    var customer = unitofwork.CustomerRepository.GetById(item.Id);
                    ViewModels.Customer itemToViewModel = new()
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                        OwnerName = customer.OwnerName,
                        OwnerFamily = customer.OwnerFamily,
                        TelePhonenumber1 = customer.TelePhonenumber1,
                        TelePhonenumber2 = customer.TelePhonenumber2,
                        SendingPrice = customer.SendingPrice,
                        Latitude = customer.Latitude,
                        Longitude = customer.Longitude,
                        City = unitofwork.CityRepository.GetById(customer.CityId).Name,
                        Phonenumber = customer.Phonenumber,
                        Address = customer.Address,
                        Description = customer.Description,
                        Type = unitofwork.CustomerTypeRepository.GetById(customer.TypeId).Name,
                        IsInMyFavorite=true
                    };
                    ItemsforShow.Add(itemToViewModel);
                }
                IEnumerable<ViewModels.Customer> result = ItemsforShow;

                if (ItemPerPage.HasValue && PageNum.HasValue)
                {
                    var Skipcount = (PageNum.Value - 1) * ItemPerPage.Value;
                    result = result.Skip(Skipcount);
                    result = result.Take(ItemPerPage.Value);

                    if (result == null)
                    {
                        return NotFound();
                    }
                }
                return Ok(ItemsforShow);
            }
            return BadRequest();

        }
    }
}
