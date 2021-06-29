using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DaliFood.AdminPanel.Pages.Product
{
    public class UpsertModel : PageModel
    {

        readonly UnitOfWork unitofwork;
        public UpsertModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public Models.Product Product { get; set; }

        [BindProperty]
        public IEnumerable<IFormFile> ImagesUpload { get; set; }

        public ActionResult OnGet(int? Id)
        {
            Product = new Models.Product();
            if (Id == null)
            {
                ViewData["CategorieId"] = new SelectList(unitofwork.ProductCategorieRepository.GetAll(),"Id", "Name");
                ViewData["CustomerId"] = new SelectList(unitofwork.CustomerRepository.GetAll(),"Id", "Name");          
                return Page();
            }
            
            Product = unitofwork.ProductRepository.GetById(Id);
           
            if (Product == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(unitofwork.ProductCategorieRepository.GetAll(), "Id", "Name", Product.CategorieId);
            ViewData["CustomerId"] = new SelectList(unitofwork.CustomerRepository.GetAll(), "Id", "Name",Product.CustomerId);
            return Page();
        }
        public ActionResult OnPost()
        {
            var part = SD.GetPart(unitofwork, SD.PhotoForProducts.Name);
            if (Product.Id == 0)
                Product.CreateDate = DateTime.Now;
            if (!ModelState.IsValid)
                return Page();

            if (Product.Id == 0)
            {
                unitofwork.ProductRepository.Create(Product);
            }
            else
            {
                unitofwork.ProductRepository.Modifie(Product);
            }
 

            unitofwork.ProductRepository.Save();
            if (Product.Id != 0 && ImagesUpload.Any())
            {
                var photos = unitofwork.PhotoRepository.GetAll(where: p => p.ItemId == Product.Id && p.PartId == part.Id);
                Files.DeletePhotos(unitofwork, photos,part);
            }
            if (ImagesUpload.Any())
            {
                var product = unitofwork.ProductRepository.GetAll(where: p => p.Name == Product.Name && p.CreateDate == Product.CreateDate).Last();
                Files.ImportPhotos(unitofwork, ImagesUpload, product, part);
            }
           
            return RedirectToPage("Index");
        }
    }
}
