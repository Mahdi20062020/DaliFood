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
                    TotalPrice = 0,
                    CreateDate = DateTime.Now,
                };
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
                order.TotalPrice += orderitem.Price*Count;
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
        public  IActionResult GetOrders(int ItemPerPage, int PageNum)
        {
            var userId = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            var orders = unitofwork.OrderRepository.GetAll(where: p => p.UserId == userId);
            var Skipcount = (PageNum - 1) * ItemPerPage;
            orders = orders.Skip(Skipcount);
            orders = orders.Take(ItemPerPage);
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
    }
}
