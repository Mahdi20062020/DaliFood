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
    public class IndexGridModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        readonly UserManager<ApplicationUser> userManager;
        public IndexGridModel(UnitOfWork unitofwork, UserManager<ApplicationUser> userManager)
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
    }
}
