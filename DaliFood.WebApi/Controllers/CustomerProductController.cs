using DaliFood.Utilites;
using DaliFood.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.WebApi.Controllers
{
    [Route("/Api")]
    [ApiController]
    public class CustomerProductController : Controller
    {
        readonly UnitOfWork unitofwork;
        public CustomerProductController(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [HttpGet]
        public IActionResult GetCustomerProducts(int ItemPerPage, int PageNum, int? TypeId)
        {
            var Items = unitofwork.CustomersProductRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            List<CustomersProduct> ItemsforShow = new List<CustomersProduct>();
            foreach (var item in Items)
            {
                item.Product = unitofwork.ProductRepository.GetById(item.ProductId);
                item.Product.ProductCategorie = unitofwork.ProductCategorieRepository.GetById(item.Product.CategorieId);
                item.Customer = unitofwork.CustomerRepository.GetById(item.CustomerId);

                ItemsforShow.Add(item);
            }
            IEnumerable<CustomersProduct> result = ItemsforShow;
            if (TypeId.HasValue)
            {
                result = ItemsforShow.Where(p => p.ProductTypeId == TypeId);
            }
            var Skipcount = (PageNum - 1) * ItemPerPage;
            result = result.Skip(Skipcount);
            result = result.Take(ItemPerPage);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("CustomerProducts/{Id}")]
        public IActionResult GetCustomerProduct(int Id)
        {
            var item = unitofwork.CustomersProductRepository.GetById(Id);
            if (item == null)
            {
                return NotFound();
            }
            CustomersProduct ItemforShow;
            item.Product = unitofwork.ProductRepository.GetById(item.ProductId);
            item.Product.ProductCategorie = unitofwork.ProductCategorieRepository.GetById(item.Product.CategorieId);
            item.Customer = unitofwork.CustomerRepository.GetById(item.CustomerId);
            ItemforShow = item;
            return Ok(ItemforShow);
        }
    }
}
