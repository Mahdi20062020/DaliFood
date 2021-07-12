﻿using DaliFood.Models.Identity;
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
        [BindProperty]
        public DateTime Then { get; set; }
        [BindProperty]

        public DateTime Now { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated is false)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            Then = DateTime.Now;
            await unitofwork.Configure(roleManager, userManager);
            Now = DateTime.Now;
            return Page();
        }
    }
}
