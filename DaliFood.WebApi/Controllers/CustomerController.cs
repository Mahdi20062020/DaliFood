using DaliFood.Utilites;
using DaliFood.WebApi.ViewModels;
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

    public class CustomerController : ControllerBase
    {
        readonly UnitOfWork unitofwork;
        public CustomerController(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
            
        }
        [HttpGet]
        public IActionResult GetCustomers(int? ItemPerPage, int? PageNum,int? CustomerId)
        {

            var Customers = unitofwork.CustomerRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            List<Customer> ItemsforShow = new();
            foreach (var item in Customers)
            {
                var user = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier);
               
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
                if (user != null)
                {
                    itemToViewModel.IsInMyFavorite = unitofwork.FavoriteRepository.GetAll(where: p => p.UserId == user.FirstOrDefault().Value && p.CustomerId == item.Id).Any();
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
