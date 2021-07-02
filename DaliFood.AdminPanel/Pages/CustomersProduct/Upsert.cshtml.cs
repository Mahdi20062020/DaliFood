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
    [Authorize(Policy = SD.CustomerPolicy)]
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
            var customerId = int.Parse(User.Claims.Where(p => p.Type == SD.CustomerId).FirstOrDefault().Value);
           
            if (Id == null)
            {
                ViewData["ProductId"] = new SelectList(unitofwork.ProductRepository.GetAll(),"Id", "Name");
                ViewData["CustomerId"] = new SelectList(unitofwork.CustomerRepository.GetAll(where:p=>p.Id==customerId),"Id", "Name");          
                return Page();
            }
            if (CustomersProduct.CustomerId != customerId)
            {
                return BadRequest();
            }
            CustomersProduct = unitofwork.CustomersProductRepository.GetById(Id);
           
            if (CustomersProduct == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(unitofwork.ProductRepository.GetAll(), "Id", "Name", CustomersProduct.ProductId);
            ViewData["CustomerId"] = new SelectList(unitofwork.CustomerRepository.GetAll(where: p => p.Id == customerId), "Id", "Name",CustomersProduct.CustomerId);
            return Page();
        }
        public ActionResult OnPost()
        {
            var customerId = int.Parse(User.Claims.Where(p => p.Type == SD.CustomerId).FirstOrDefault().Value);
            var part = SD.GetPart(unitofwork, SD.PhotoForCustomersProducts.Name);
            if (CustomersProduct.Id == 0)
                CustomersProduct.CreateDate = DateTime.Now;
            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = new SelectList(unitofwork.ProductRepository.GetAll(), "Id", "Name", CustomersProduct.ProductId);
                ViewData["CustomerId"] = new SelectList(unitofwork.CustomerRepository.GetAll(where: p => p.Id == customerId), "Id", "Name", CustomersProduct.CustomerId);
                return Page();
            }
            if (CustomersProduct.CustomerId != customerId)
            {
                return BadRequest();
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
            //if (CustomersProduct.Id != 0 && ImagesUpload.Any())
            //{
            //    var photos = unitofwork.PhotoRepository.GetAll(where: p => p.ItemId == CustomersProduct.Id && p.PartId == part.Id);
            //    Files.DeletePhotos(unitofwork, photos,part);
            //}
            //if (ImagesUpload.Any())
            //{
            //    var CustomersProduct = unitofwork.CustomersProductRepository.GetAll(where: p => p.Product.Name == CustomersProduct.Product && p.CreateDate == CustomersProduct.CreateDate).Last();
            //    Files.ImportPhotos(unitofwork, ImagesUpload, CustomersProduct, part);
            //}
           
            return RedirectToPage("Index");
        }
    }
}
