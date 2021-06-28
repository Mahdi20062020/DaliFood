using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Restaurant
{
    public class UpsertModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public UpsertModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public Models.Restaurant Restaurant { get; set; }

        public ActionResult OnGet(int? Id)
        {
            Restaurant = new Models.Restaurant();
            if (Id == null)
            {
                return Page();
            }

            Restaurant = unitofwork.RestaurantRepository.GetById(Id);

            if (Restaurant == null)
            {
                return NotFound();
            }

            return Page();
        }
        public ActionResult OnPost()
        {
            if (Restaurant.Id==0)
                Restaurant.CreateDate = DateTime.Now;
            if (!ModelState.IsValid)
                return Page();
            

            if (Restaurant.Id==0)
            {
                unitofwork.RestaurantRepository.Create(Restaurant);
            }
            else
            {
                unitofwork.RestaurantRepository.Modifie(Restaurant);
            }
            unitofwork.RestaurantRepository.Save();
            return RedirectToPage("Index");
        }
    }
}
