using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
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
            return RedirectToPage("Index");
        }
    }
}
