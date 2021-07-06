using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DaliFood.AdminPanel.Pages.CustomersProduct
{
    //[Authorize(Policy = SD.CustomerPolicy)]
    public class UpsertModel : PageModel
    {

        readonly UnitOfWork unitofwork;
        public UpsertModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public Models.CustomersProduct CustomersProduct { get; set; }

        [BindProperty]
        public IEnumerable<IFormFile> ImagesUpload { get; set; }

        public ActionResult OnGet(int? Id)
        {
            CustomersProduct = new Models.CustomersProduct();

            var customerId = User.Claims.Where(p => p.Type == SD.CustomerId).FirstOrDefault().Value;
            if (customerId == SD.AdminCustomerId)
            {
               ViewData["CustomerId"] = new SelectList(unitofwork.CustomerRepository.GetAll(),"Id", "Name");          
            }
            else
            {
                ViewData["CustomerId"] = new SelectList(unitofwork.CustomerRepository.GetAll(where: p => p.Id == int.Parse(customerId)), "Id", "Name");
            }

            if (Id == null)
            {
                ViewData["ProductId"] = new SelectList(unitofwork.ProductRepository.GetAll(),"Id", "Name");
                return Page();
            }
      
            CustomersProduct = unitofwork.CustomersProductRepository.GetById(Id);
           
            if (CustomersProduct == null)
            {
                return NotFound();
            }
            if (customerId != SD.AdminCustomerId)
                if (int.Parse(customerId) != CustomersProduct.CustomerId)
                    return BadRequest();
            ViewData["ProductId"] = new SelectList(unitofwork.ProductRepository.GetAll(), "Id", "Name", CustomersProduct.ProductId);
            return Page();
        }
        public ActionResult OnPost()
        {
            var customerId = User.Claims.Where(p => p.Type == SD.CustomerId).FirstOrDefault().Value;
            if (customerId != SD.AdminCustomerId)
                if (int.Parse(customerId) != CustomersProduct.CustomerId)
                    return BadRequest();
            var part = SD.GetPart(unitofwork, SD.PhotoForCustomersProducts.Name);
            if (CustomersProduct.Id == 0)
                CustomersProduct.CreateDate = DateTime.Now;
            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = new SelectList(unitofwork.ProductRepository.GetAll(), "Id", "Name", CustomersProduct.ProductId);
                ViewData["CustomerId"] = new SelectList(unitofwork.CustomerRepository.GetAll(where: p => p.Id == int.Parse(customerId)), "Id", "Name", CustomersProduct.CustomerId);
                return Page();
            }
         
            if (CustomersProduct.Id == 0)
            {
                unitofwork.CustomersProductRepository.Create(CustomersProduct);
            }
            else
            {
                unitofwork.CustomersProductRepository.Modifie(CustomersProduct);
            }
 

            unitofwork.CustomersProductRepository.Save();
            if (CustomersProduct.Id != 0 && ImagesUpload.Any())
            {
                var photos = unitofwork.PhotoRepository.GetAll(where: p => p.ItemId == CustomersProduct.Id && p.PartId == part.Id);
                Files.DeletePhotosProduct(unitofwork, photos, part);
            }
            if (ImagesUpload.Any())
            {
                var CustomerProduct = unitofwork.CustomersProductRepository.GetAll(where: p => p.ProductId == CustomersProduct.ProductId && p.CreateDate == CustomersProduct.CreateDate).Last();
                Files.ImportPhotosProduct(unitofwork, ImagesUpload, CustomerProduct, part);
            }

            return RedirectToPage("Index");
        }
    }
}
