using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DaliFood.AdminPanel.Pages.Product.Discount
{
    public class UpsertModel : PageModel
    {

        readonly UnitOfWork unitofwork;
        public UpsertModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public Models.Discount Discount { get; set; }

        public ActionResult OnGet(int? Id)
        {
            Discount = new Models.Discount();
            if (Id == null)
            {
                ViewData["ProductId"] = new SelectList(unitofwork.ProductRepository.GetAll(), "Id", "Name");
                return Page();
            }

            Discount = unitofwork.DiscountRepository.GetById(Id);

            if (Discount == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(unitofwork.ProductRepository.GetAll(), "Id", "Name",Discount.ProductId);

            return Page();
        }
        public ActionResult OnPost()
        {
            if (Discount.Id == 0)
                Discount.CreateDate = DateTime.Now;
            if (!ModelState.IsValid)
                return Page();
            if (Discount.Id == 0)
            {
                unitofwork.DiscountRepository.Create(Discount);
            }
            else
            {
                unitofwork.DiscountRepository.Modifie(Discount);
            }
            unitofwork.DiscountRepository.Save();
            return RedirectToPage("Index");
        }
    }
}
