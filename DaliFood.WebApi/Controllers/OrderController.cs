using DaliFood.Models;
using DaliFood.Utilites;
using DaliFood.WebApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DaliFood.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {

        readonly UnitOfWork unitofwork;
        public OrderController(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        // GET: api/<OrderController>
        [HttpPost("AddToCart")]
        public IActionResult PostAddToCart(int CustomerProductId,int Count,int? OrderId)
        {
            var customerproduct = unitofwork.CustomersProductRepository.GetById(CustomerProductId);
            var discount = unitofwork.DiscountRepository.GetAll(p => p.CustomersProductId == CustomerProductId).FirstOrDefault();
            if (discount!=null)
            {
                customerproduct.Discount = discount;
            }
            Models.Order order;
            if (OrderId.HasValue)
            {              
                order = unitofwork.OrderRepository.GetAll(where: p => p.Id == OrderId && p.Status == true).FirstOrDefault();
                if (order == null)
                    return NotFound();
            }
            else
            {
                var userId = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
                order = new Models.Order()
                {
                    Status = true,
                    UserId = userId,
                    Amount = 0,
                    CreateDate = DateTime.Now,
                };
                if (unitofwork.OrderRepository.GetAll(p=>p.UserId==userId).Any())
                {
                    order.AddressId = unitofwork.OrderRepository.GetAll(p => p.UserId == userId).Last().AddressId;
                }
                else
                {
                    var Addresses = unitofwork.AddressRepository.GetAll(p => p.UserId == userId);
                    if (Addresses.Any())
                    {
                        order.AddressId = Addresses.First().Id;
                    }
                    else
                    {
                        ModelState.AddModelError("Address", "User has not any Address");
                        return BadRequest(ModelState);
                    }
                }
                if (unitofwork.OrderRepository.Create(order))
                {
                    unitofwork.OrderRepository.Save();
                }
            }
            var orderitem = new Models.OrderItem()
            {
                Count = Count,
                CustomerProductId = customerproduct.Id,
                OrderId = order.Id,
                CustomerId = customerproduct.CustomerId,
                CreateDate = DateTime.Now,
                Status=SD.CheckingOrderStatus
            };
            if (customerproduct.Discount!=null)
            {
                orderitem.Price = customerproduct.Price.UseDiscount(customerproduct.Discount.DiscountRate);
            }
            else
            {
                orderitem.Price = customerproduct.Price;
            }
            if (unitofwork.OrderItemRepository.Create(orderitem))
            {
                unitofwork.OrderItemRepository.Save();
                order.Amount += orderitem.Price*Count;
                if (unitofwork.OrderRepository.Modifie(order))
                {
                    unitofwork.OrderRepository.Save();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("Orders")]
        public  IActionResult GetOrders(int? ItemPerPage, int? PageNum)
        {
            var userId = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            var orders = unitofwork.OrderRepository.GetAll(where: p => p.UserId == userId);
             if (ItemPerPage.HasValue && PageNum.HasValue)
             {
                var Skipcount = (PageNum.Value - 1) * ItemPerPage.Value;
                orders = orders.Skip(Skipcount);
                orders = orders.Take(ItemPerPage.Value);

                if (orders == null)
                {
                    return NotFound();
                }
             }
            IList<ViewModels.Order> ordersforshow=new List<ViewModels.Order>();
            foreach (var item in orders)
            {
                foreach (var orderitem in item.OrderItem)
                {
                    orderitem.CustomersProduct = unitofwork.CustomersProductRepository.GetById(orderitem.CustomerProductId);
                    orderitem.CustomersProduct.Product = unitofwork.ProductRepository.GetById(orderitem.CustomersProduct.ProductId);
                }
                ordersforshow.Add(item);
            }
            return Ok(ordersforshow);
        }
        [HttpGet("Orders/{Id}")]
        public IActionResult GetOrder(int Id)
        {
            var userId = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            var order = unitofwork.OrderRepository.GetAll(where: p => p.UserId == userId &&p.Id==Id).FirstOrDefault();
            foreach (var orderitem in order.OrderItem)
            {
                orderitem.CustomersProduct = unitofwork.CustomersProductRepository.GetById(orderitem.CustomerProductId);
                orderitem.CustomersProduct.Product = unitofwork.ProductRepository.GetById(orderitem.CustomersProduct.ProductId);
            }
            ViewModels.Order orderforshow = order;
            return Ok(orderforshow);
        }
        [HttpPut("Orders/{Id}/ChangeAddress")]
        public IActionResult PutOrderAddress(Address model,int Id)
        {
            var userId = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            var order = unitofwork.OrderRepository.GetAll(where: p => p.UserId == userId && p.Id == Id).FirstOrDefault();
            var Address = unitofwork.AddressRepository.GetAll(where: p => p.Latitude == model.Latitude && p.Longitude == model.Longitude).FirstOrDefault();
            if (Address==null)
            { 
                if (ModelState.IsValid)
                {
                    if (model.Latitude == 0.ToString())
                    {
                        model.Latitude = null;
                    }
                    if (model.Longitude == 0.ToString())
                    {
                        model.Longitude = null;
                    }           
                    var address = new Models.Identity.Address()
                    {
                        TextAddress = model.TextAddress,
                        Latitude = model.Latitude,
                        Longitude = model.Longitude,
                        UserId = userId
                    };
                    unitofwork.AddressRepository.Create(address);
                    unitofwork.AddressRepository.Save();
                }
                Address = unitofwork.AddressRepository.GetAll(where: p => p.Latitude == model.Latitude && p.Longitude == model.Longitude).FirstOrDefault();
            }
            order.AddressId = Address.Id;
            unitofwork.OrderRepository.Modifie(order);
            unitofwork.OrderRepository.Save();
            return Ok();
        }

    }
    
}
