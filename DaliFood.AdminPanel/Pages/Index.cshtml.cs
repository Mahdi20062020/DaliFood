using DaliFood.Models.Identity;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
namespace DaliFood.AdminPanel.Pages
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

        public void OnGet()
        {

        }
    }
}
