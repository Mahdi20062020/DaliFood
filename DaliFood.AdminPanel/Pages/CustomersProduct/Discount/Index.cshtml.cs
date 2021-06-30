using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.CustomersProduct.Discount
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public IndexModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public IEnumerable<Models.Discount> Discount { get; set; }

        public void OnGet()
        {
            Discount = unitofwork.DiscountRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
        }
        public ActionResult OnGetDelete(int Id)
        {
            var Discount = unitofwork.DiscountRepository.GetById(Id);
            if (Discount == null)
                return NotFound();
            unitofwork.DiscountRepository.Delete(Discount);
            unitofwork.DiscountRepository.Save();
            return RedirectToPage("Index");

        }
    }
}
