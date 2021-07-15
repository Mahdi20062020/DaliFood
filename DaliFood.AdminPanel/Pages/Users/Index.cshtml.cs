using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Models.Identity;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Users
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        readonly UserManager<ApplicationUser> userManager;
        
        readonly RoleManager<IdentityRole> roleManager;
        public IndexModel(UnitOfWork unitofwork, 
            UserManager<ApplicationUser> userManager)
        {
            this.unitofwork = unitofwork;
            this.userManager = userManager;        
         
        }
        [BindProperty]
        public IEnumerable<ApplicationUser> Users { get; set; }
        public async Task OnGet()
        {
            var users = new List<ApplicationUser>();
            foreach (var user in userManager.Users)
            {
                if (!await userManager.IsInRoleAsync(user,SD.NormalUserRole) && !await userManager.IsInRoleAsync(user, SD.CustomerOwnerRole))
                {
                    users.Add(user);
                }
            }
            Users = users;
        }
    }
}
