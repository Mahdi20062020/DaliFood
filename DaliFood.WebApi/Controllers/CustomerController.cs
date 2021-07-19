using DaliFood.Utilites;
using DaliFood.WebApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DaliFood.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        readonly UnitOfWork unitofwork;
        public CustomerController(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
            
        }
        /// <summary>دریافت اطلاعات فروشگاه ها
        /// </summary>
        /// <param name="ItemPerPage">تعداد آیتم های نمایشی</param>
        /// <param name="PageNum">صفحه مورد نمایش</param>
        /// <param name="CustomerId">شناسه فورشگاهی که قرار است نمایش داده شود، در صورت خالی بودن، تمام فروشگاه ها نمایش داده میشود</param>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetCustomers(int? ItemPerPage, int? PageNum,int? CustomerId)
        {

            var Customers = unitofwork.CustomerRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            List<Customer> ItemsforShow = new();
            foreach (var item in Customers)
            {
               
                Customer itemToViewModel = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    OwnerName = item.OwnerName,
                    OwnerFamily = item.OwnerFamily,
                    TelePhonenumber1 = item.TelePhonenumber1,
                    TelePhonenumber2 = item.TelePhonenumber2,
                    SendingPrice = item.SendingPrice,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    City = unitofwork.CityRepository.GetById(item.CityId).Name,
                    Phonenumber = item.Phonenumber,
                    Address = item.Address,
                    Description = item.Description,
                    Type = unitofwork.CustomerTypeRepository.GetById(item.TypeId).Name,
                };
                var user = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier);
                if (user.Any())
                {
                    var favorites = unitofwork.FavoriteRepository.GetAll();
                    itemToViewModel.IsInMyFavorite = favorites.Any(p => p.UserId == user.FirstOrDefault().Value && p.CustomerId == item.Id);
                }



                ItemsforShow.Add(itemToViewModel);
            }
            IEnumerable<Customer> result =ItemsforShow;
            if (CustomerId.HasValue)
            {
                result = result.Where(p => p.Id == CustomerId);

            }
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
            return Ok(result);
        }
    }
}
