using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Models.Data;
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
        private readonly ApplicationDbContext context;
        public IndexModel(UnitOfWork unitofwork, UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            this.unitofwork = unitofwork;
            this.userManager = userManager;
            this.context = context;
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
            //var Customer = unitofwork.CustomerRepository.GetById(Id);
            var Customer = context
                .Customer.Find(Id);
            if (Customer == null)
                return NotFound();
            var deatail = Customer.ApplicationCustomerUser;
            context.ApplicationUserDetail.Remove(deatail);
            var user = await userManager.FindByIdAsync(Customer.UserId);
            if (user is not null)
            {
                await userManager.DeleteAsync(user);
            }
            unitofwork.CustomerRepository.Delete(Customer);

            var Customers = unitofwork.CustomerRepository
                .GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            unitofwork.CustomerRepository.Save();
            return RedirectToPage("Index");
        }

    }

}
