using DaliFood.Utilites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace DaliFood.AdminPanel.Pages
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        readonly RoleManager<IdentityRole> roleManager;
        public IndexModel(UnitOfWork unitofwork, RoleManager<IdentityRole> roleManager)
        {
            this.unitofwork = unitofwork;
            this.roleManager = roleManager;
        }
        [BindProperty]
        public DateTime Then { get; set; }
        [BindProperty]

        public DateTime Now { get; set; }
        public async Task OnGet()
        {
            Then = DateTime.Now;
           await unitofwork.Configure(roleManager);
            Now = DateTime.Now;
        }
    }
}
