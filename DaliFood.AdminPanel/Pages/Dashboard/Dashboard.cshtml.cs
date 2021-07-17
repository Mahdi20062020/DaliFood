using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Models.Identity;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Dashboard
{
    public class DashboardModel : PageModel
    {
        private readonly UnitOfWork unitofwork;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public DashboardModel(UnitOfWork unitofwork, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.unitofwork = unitofwork;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [BindProperty]
        public DateTime Then { get; set; }
        [BindProperty]

        public DateTime Now { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated is false)
            {
                await unitofwork.Configure(roleManager, userManager);
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            Then = DateTime.Now;
            Now = DateTime.Now;
            return Page();
        }
    }
}
