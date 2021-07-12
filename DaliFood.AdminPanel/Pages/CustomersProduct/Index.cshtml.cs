using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.CustomersProduct
{
    //[Authorize(Policy =SD.CustomerPolicy)]
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public IndexModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public IEnumerable<Models.CustomersProduct> CustomersProduct { get; set; }

        public ActionResult OnGet()
        {
            var customerId = User.Claims.Where(p => p.Type == SD.CustomerId).FirstOrDefault().Value;
            if (customerId==SD.AdminCustomerId)
            {
                CustomersProduct = unitofwork.CustomersProductRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            }
            else
            {
                CustomersProduct = unitofwork.CustomersProductRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate), where: p => p.CustomerId == int.Parse(customerId));
            }

            foreach (var item in CustomersProduct)
            {
                item.Product = unitofwork.ProductRepository.GetById(item.ProductId);
            }
            return Page();
        }
        public ActionResult OnGetDelete(int Id)
        {
            //var part = SD.GetPart(unitofwork, SD.PhotoForCustomersProducts.Name);

            var CustomersProduct = unitofwork.CustomersProductRepository.GetById(Id);

            if (CustomersProduct == null)
                return NotFound();
            var customerId = User.Claims.Where(p => p.Type == SD.CustomerId).FirstOrDefault().Value;
            if (customerId != SD.AdminCustomerId)
                if (int.Parse(customerId) != CustomersProduct.CustomerId)
                    return BadRequest();
            
            //var photos = unitofwork.PhotoRepository.GetAll(where: p => p.ItemId == CustomersProduct.Id && p.PartId == part.Id);

            unitofwork.CustomersProductRepository.Delete(CustomersProduct);
            unitofwork.CustomersProductRepository.Save();
            //Files.DeletePhotos(unitofwork, photos, part);
            return RedirectToPage("Index");

        }
    }
}
