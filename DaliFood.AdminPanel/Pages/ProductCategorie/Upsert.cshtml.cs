using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.ProductCategorie
{
    public class UpsertModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public UpsertModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public Models.ProductCategorie ProductCategorie { get; set; }

        public ActionResult OnGet(int? Id)
        {
            ProductCategorie = new Models.ProductCategorie();
            if (Id == null)
            {
                return Page();
            }

            ProductCategorie = unitofwork.ProductCategorieRepository.GetById(Id);

            if (ProductCategorie == null)
            {
                return NotFound();
            }

            return Page();
        }
        public ActionResult OnPost()
        {
            ProductCategorie.CreateDate= DateTime.Now;
            if (!ModelState.IsValid)
                return Page();
            if (ProductCategorie.Id==0)
            {
                unitofwork.ProductCategorieRepository.Create(ProductCategorie);
            }
            else
            {
                unitofwork.ProductCategorieRepository.Modifie(ProductCategorie);
            }
            unitofwork.ProductCategorieRepository.Save();
            return RedirectToPage("Index");
        }
    }
}
