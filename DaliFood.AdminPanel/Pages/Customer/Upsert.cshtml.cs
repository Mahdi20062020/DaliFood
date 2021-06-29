using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DaliFood.AdminPanel.Pages.Customer
{
    public class UpsertModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public UpsertModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public Models.Customer Customer { get; set; }

        public ActionResult OnGet(int? Id)
        {
            Customer = new Models.Customer();
            if (Id == null)
            {
                ViewData["CustomerTypeId"] = new SelectList(unitofwork.CustomerTypeRepository.GetAll(), "Id", "Name");
                return Page();
            }

            Customer = unitofwork.CustomerRepository.GetById(Id);

            if (Customer == null)
            {
                return NotFound();
            }
            ViewData["CustomerTypeId"] = new SelectList(unitofwork.CustomerTypeRepository.GetAll(), "Id", "Name");
            return Page();
        }
        public ActionResult OnPost()
        {
            if (Customer.Id==0)
                Customer.CreateDate = DateTime.Now;
            if (!ModelState.IsValid)
                return Page();
            

            if (Customer.Id==0)
            {
                unitofwork.CustomerRepository.Create(Customer);
            }
            else
            {
                unitofwork.CustomerRepository.Modifie(Customer);
            }
            unitofwork.CustomerRepository.Save();
            return RedirectToPage("Index");
        }
    }
}
