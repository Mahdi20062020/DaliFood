using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.CustomerType
{
    public class UpsertModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public UpsertModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        
        [BindProperty]
        public Models.CustomerType CustomerType { get; set; }

        public ActionResult OnGet(int? Id)
        {
            CustomerType = new Models.CustomerType();
            if (Id == null)
            {
                return Page();
            }

            CustomerType = unitofwork.CustomerTypeRepository.GetById(Id);

            if (CustomerType == null)
            {
                return NotFound();
            }

            return Page();
        }
        public ActionResult OnPost()
        {
            if (CustomerType.Id == 0)
                CustomerType.CreateDate = DateTime.Now;
            if (!ModelState.IsValid)
                return Page();

            if (CustomerType.Id == 0)
            {
                unitofwork.CustomerTypeRepository.Create(CustomerType);
            }
            else
            {
                unitofwork.CustomerTypeRepository.Modifie(CustomerType);
            }
            unitofwork.CustomerTypeRepository.Save();
            return RedirectToPage("Index");
        }
    }

}
