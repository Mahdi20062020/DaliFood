using DaliFood.Utilites;
using DaliFood.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.WebApi.Controllers
{
    [Route("/Api/[controller]")]
    [ApiController]
    public class CustomerProductController : Controller
    {
        readonly UnitOfWork unitofwork;
        public CustomerProductController(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        /// <summary>دریافت لیست غذا ها
        /// </summary>
        /// <param name="ItemPerPage">تعداد آیتم های نمایشی</param>
        /// <param name="PageNum">صفحه مورد نمایش</param>
        /// <param name="TypeId">شناسه دسته بندی که قرار است غذا های آن نمایش داده شود، در صورت خالی بودن، غذای تمام دسته بندی ها نمایش داده میشود</param>
        /// <param name="CustomerId">شناسه فورشگاهی که قرار است غذا های آن نمایش داده شود، در صورت خالی بودن، غذای تمام فروشگاه ها نمایش داده میشود</param>
        /// <param name="MinPrice">حداقل قیمت</param>
        /// <param name="MaxPrice">حداکثر قیمت</param>
        [HttpGet]    
        public IActionResult GetCustomerProducts(int? ItemPerPage, int? PageNum, int? TypeId,int? CustomerId,int? MinPrice,int? MaxPrice)
        {
            var Items = unitofwork.CustomersProductRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            List<CustomersProduct> ItemsforShow = new();
            foreach (var item in Items)
            {
                item.Product = unitofwork.ProductRepository.GetById(item.ProductId);
                item.Product.ProductCategorie = unitofwork.ProductCategorieRepository.GetById(item.Product.CategorieId);
                item.Customer = unitofwork.CustomerRepository.GetById(item.CustomerId);
                CustomersProduct itemToViewModel = item;
                itemToViewModel.ImageAddress = Files.GetPhotoPaths(unitofwork, itemToViewModel.Id, SD.PhotoForCustomersProducts.Name, Request.Host.Value).FirstOrDefault();
                if (itemToViewModel.ImageAddress!=null)
                { 
                    var address = itemToViewModel.ImageAddress.Replace(@"\\","/");
                    itemToViewModel.ImageAddress = address;
                }
                ItemsforShow.Add(itemToViewModel);
            }
            IEnumerable<CustomersProduct> result = ItemsforShow;
            if (TypeId.HasValue)
            {
                result = result.Where(p => p.ProductTypeId == TypeId);
            }
            if (CustomerId.HasValue)
            {
                result = result.Where(p => p.CustomerId== CustomerId);
            }
            if (MinPrice.HasValue)
            {
                result = result.Where(p => p.Price >= MinPrice);
            }
            if (MaxPrice.HasValue)
            {
                result = result.Where(p => p.Price <= MaxPrice);
            }
            if (MaxPrice.HasValue&& MinPrice.HasValue)
            {
                if (MaxPrice.Value<MinPrice.Value)
                {
                    return BadRequest("Out Of the Range");
                }
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
        /// <summary>دریافت لیست غذا ها
        /// </summary>
        /// <param name="ItemPerPage">تعداد آیتم های نمایشی</param>
        /// <param name="PageNum">صفحه مورد نمایش</param>
        /// <param name="TypeId">شناسه دسته بندی که قرار است غذا های آن نمایش داده شود، در صورت خالی بودن، غذای تمام دسته بندی ها نمایش داده میشود</param>
        /// <param name="CustomerId">شناسه فورشگاهی که قرار است غذا های آن نمایش داده شود، در صورت خالی بودن، غذای تمام فروشگاه ها نمایش داده میشود</param>
        /// <param name="MinPrice">حداقل قیمت</param>
        /// <param name="MaxPrice">حداکثر قیمت</param>
        /// <param name="q">متن سرچ شده</param>
        [HttpGet("CustomerProducts/Search")]
        public IActionResult GetCustomerProductSearch(int? ItemPerPage, int? PageNum, int? TypeId, int? CustomerId, int? MinPrice, int? MaxPrice, string q="")
        {
            var Items = unitofwork.CustomersProductRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            List<CustomersProduct> ItemsforShow = new();
            foreach (var item in Items)
            {
                item.Product = unitofwork.ProductRepository.GetById(item.ProductId);
                item.Product.ProductCategorie = unitofwork.ProductCategorieRepository.GetById(item.Product.CategorieId);
                item.Customer = unitofwork.CustomerRepository.GetById(item.CustomerId);
                CustomersProduct itemToViewModel = item;
                itemToViewModel.ImageAddress = Files.GetPhotoPaths(unitofwork, itemToViewModel.Id, SD.PhotoForCustomersProducts.Name, Request.Host.Value).FirstOrDefault();
                if (itemToViewModel.ImageAddress != null)
                {
                    var address = itemToViewModel.ImageAddress.Replace(@"\\", "/");
                    itemToViewModel.ImageAddress = address;
                }
                ItemsforShow.Add(itemToViewModel);
            }
            IEnumerable<CustomersProduct> result = ItemsforShow;
            foreach (var item in q.Split(' '))
            {
                result = result.Where(p => p.CustomerName.Contains(item) || p.ProductName.Contains(item) || p.Description.Contains(item)||p.ProductType.Contains(item)).Distinct();
            }
            if (TypeId.HasValue)
            {
                result = result.Where(p => p.ProductTypeId == TypeId);
            }
            if (CustomerId.HasValue)
            {
                result = result.Where(p => p.CustomerId == CustomerId);
            }
            if (MinPrice.HasValue)
            {
                result = result.Where(p => p.Price >= MinPrice);
            }
            if (MaxPrice.HasValue)
            {
                result = result.Where(p => p.Price <= MaxPrice);
            }
            if (MaxPrice.HasValue && MinPrice.HasValue)
            {
                if (MaxPrice.Value < MinPrice.Value)
                {
                    return BadRequest("Out Of the Range");
                }
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
        /// <summary>دریافت غذا
        /// </summary>
        /// <param name="Id">شناسه غذای درخواستی</param>

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
