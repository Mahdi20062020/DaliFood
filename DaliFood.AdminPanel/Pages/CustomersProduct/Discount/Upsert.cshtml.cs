using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DaliFood.AdminPanel.Pages.CustomersProduct.Discount
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
        public Models.Discount Discount { get; set; }
        [BindProperty]
        public int CustomersProductId { get; set; }
        [BindProperty]
        public string CustomersProductName { get; set; }
        [BindProperty]
        public bool IsEditing { get; set; }

        public ActionResult OnGet(int Id)
        {
            var customerId = int.Parse(User.Claims.Where(p => p.Type == SD.CustomerId).FirstOrDefault().Value);
            var customer = unitofwork.CustomersProductRepository.GetById(Id);
            if (unitofwork.CustomersProductRepository.GetById(Id) == null)
                return NotFound();
            if (customer.CustomerId != customerId)
            {
                return BadRequest();
            }
            Discount = new Models.Discount();
            var Dicounts = unitofwork.DiscountRepository.GetAll(where: p => p.CustomersProductId == Id);
            IsEditing = Dicounts.Any();
            CustomersProductId = Id;

            CustomersProductName = unitofwork.ProductRepository.GetById(unitofwork.CustomersProductRepository.GetById(Id).ProductId).Name;
            if (!IsEditing)
            {
                Discount.StartDate = DateTime.Now;
                Discount.ExpirationDate = DateTime.Now;
                return Page(); 
            }

            Discount = unitofwork.DiscountRepository.GetAll(where: p => p.CustomersProductId == Id).FirstOrDefault();
            return Page();
        }
        public ActionResult OnPost()
        {
            if (!IsEditing)
            {
                Discount.CreateDate = DateTime.Now;
            }
            Discount.CustomersProductId = CustomersProductId;
            if (!ModelState.IsValid)
                return Page();
            var customerId = int.Parse(User.Claims.Where(p => p.Type == SD.CustomerId).FirstOrDefault().Value);
            var customer = unitofwork.CustomersProductRepository.GetById(Discount.CustomersProductId);
         
            if (customer.CustomerId != customerId)
            {
                return BadRequest();
            }
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
