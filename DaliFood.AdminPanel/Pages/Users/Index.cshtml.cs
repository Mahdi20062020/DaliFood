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
        public async Task OnGet(bool? SearchStatus, string SearchName = null, string SearchFamily = null, string SearchQ = null,string SearchEmail=null)
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
            if (SearchStatus.HasValue)
            {
                Users = Users.Where(p => p.EmailConfirmed == SearchStatus);
            }
            if (SearchName != null)
            {
                Users = Users.Where(p => p.Name.ToLower().Contains(SearchName.ToLower()));
            }
            if (SearchFamily != null)
            {
                Users = Users.Where(p => p.Family.ToLower().Contains(SearchFamily.ToLower()));
            } 
            if (SearchEmail != null)
            {
                Users = Users.Where(p => p.Email == SearchEmail);
            }
            if (SearchQ != null)
            {

                foreach (var item in SearchQ.ToLower().Split(' '))
                {
                    Users = Users.Where(p => p.Name.ToLower().Contains(item) ||
                                p.Family.ToLower().Contains(item) ||
                                p.Id.ToLower().Contains(item));
                }
            
            }
          
        }
        public async Task<ActionResult> OnGetDelete(string Id)
        {
           
            var user = userManager.Users.Where(p => p.Id == Id).FirstOrDefault();
            await userManager.DeleteAsync(user);
         
            return RedirectToPage("Index");
        }
    }
}
