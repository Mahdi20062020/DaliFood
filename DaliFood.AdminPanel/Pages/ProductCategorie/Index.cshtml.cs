using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Models;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.ProductCategorie
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public IndexModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public IEnumerable<Models.ProductCategorie> ProductCategorie { get; set; }

        public void OnGet()
        {
            ProductCategorie = unitofwork.ProductCategorieRepository.GetAll(orderby:p=>p.OrderByDescending(p=>p.CreateDate));
        }
        public ActionResult OnGetDelete(int Id)
        {
            var productcategorie = unitofwork.ProductCategorieRepository.GetById(Id);
            if (productcategorie == null)
                return NotFound();
            unitofwork.ProductCategorieRepository.Delete(productcategorie);
            var products = unitofwork.ProductRepository.GetAll(where: p => p.CategorieId == Id);
            foreach (var item in products)
            {
                unitofwork.ProductRepository.Delete(item);
            }
            unitofwork.ProductRepository.Save();
            unitofwork.ProductCategorieRepository.Save();
            return RedirectToPage("Index");

        }
    }
}
