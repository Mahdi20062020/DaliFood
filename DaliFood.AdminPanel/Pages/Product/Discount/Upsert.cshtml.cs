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
        [BindProperty]
        public int ProductId { get; set; }
        [BindProperty]
        public string ProductName { get; set; }
        [BindProperty]
        public bool IsEditing { get; set; }
        
        public ActionResult OnGet(int Id)
        {
            if (unitofwork.ProductRepository.GetById(Id) == null)
                return NotFound();
            Discount = new Models.Discount();
            IsEditing = unitofwork.DiscountRepository.GetAll(where: p => p.ProductId == Id) != null;
            ProductId = Id;
            ProductName = unitofwork.ProductRepository.GetById(Id).Name;
            if (!IsEditing)
                return Page();  
            Discount = unitofwork.DiscountRepository.GetAll(where: p => p.ProductId == Id).FirstOrDefault();
            return Page();
        }
        public ActionResult OnPost()
        {
            if (!IsEditing)
            {
                Discount.CreateDate = DateTime.Now;
            }
            else
            {
                Discount.ProductId = ProductId;
            }
            if (!ModelState.IsValid)
                return Page();
            if (!IsEditing)
            {
                unitofwork.DiscountRepository.Create(Discount);
            }
            else
            {
                unitofwork.DiscountRepository.Modifie(Discount);
            }
            unitofwork.DiscountRepository.Save();
            return RedirectToPage("../Index");
        }
    }
}
