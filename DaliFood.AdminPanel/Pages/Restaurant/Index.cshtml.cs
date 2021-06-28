using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Restaurant
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public IndexModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public IEnumerable<Models.Restaurant> Restaurant { get; set; }

        public void OnGet()
        {
            Restaurant = unitofwork.RestaurantRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
        }
        public ActionResult OnGetDelete(int Id)
        {
            var restaurant = unitofwork.RestaurantRepository.GetById(Id);
            if (restaurant == null)
                return NotFound();
            unitofwork.RestaurantRepository.Delete(restaurant);
            unitofwork.RestaurantRepository.Save();
            return RedirectToPage("Index");

        }
    }
}
