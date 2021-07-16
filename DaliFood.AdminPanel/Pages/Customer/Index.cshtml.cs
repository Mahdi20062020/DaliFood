using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Models.Identity;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Customer
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        readonly UserManager<ApplicationUser> userManager;
        public IndexModel(UnitOfWork unitofwork, UserManager<ApplicationUser> userManager)
        {
            this.unitofwork = unitofwork;
            this.userManager = userManager;
        }

        
        [BindProperty]
        public IEnumerable<Models.Customer> Customer { get; set; }


        public void OnGet()
        {
            Customer = unitofwork.CustomerRepository
                .GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            foreach (var item in Customer)
            {
                item.CustomerType = unitofwork
                    .CustomerTypeRepository.GetById(item.TypeId);
            }

        }

        public async Task<ActionResult> OnGetDelete(int Id)
        {
            var Customer = unitofwork.CustomerRepository.GetById(Id);
            if (Customer == null)
                return NotFound();
            var customeruser = userManager.Users.Where(p => p.Id == Customer.UserId).FirstOrDefault();
            await userManager.DeleteAsync(customeruser);
            unitofwork.CustomerRepository.Delete(Customer);
            
            var Customers = unitofwork.CustomerRepository
                .GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            unitofwork.CustomerRepository.Save();
            return RedirectToPage("Index");
        }

    }
}
