using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Product
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public IndexModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public IEnumerable<Models.Product> Product { get; set; }

        public void OnGet()
        {
            Product = unitofwork.ProductRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
        }
        public ActionResult OnGetDelete(int Id)
        {
            var Product = unitofwork.ProductRepository.GetById(Id);
            if (Product == null)
                return NotFound();
            unitofwork.ProductRepository.Delete(Product);
            unitofwork.ProductRepository.Save();
            return RedirectToPage("Index");

        }
    }
}
