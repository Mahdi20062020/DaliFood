using DaliFood.AdminPanel.Helpers;
using DaliFood.Models;
using DaliFood.Models.Identity;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace DaliFood.AdminPanel.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly UnitOfWork unitofwork;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public IndexModel(UnitOfWork unitofwork, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.unitofwork = unitofwork;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }


        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated is false)
            {
                await unitofwork.Configure(roleManager, userManager);
            }

            return Page();
        }

        

    }
}
