using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Models.Identity;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DaliFood.AdminPanel.Pages.Users
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager; 
        private readonly RoleManager<IdentityRole> _roleManager;


        public ProfileModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
             RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public string Username { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        public string SelectedRole { get; set; }

        public SelectList Roles { get; set; }
        public class InputModel
        {
            [Required]
            [Display(Name = "شناسه")]          
            public string Id { get; set; }
            [Required]
            [Display(Name = "نام")]
            [MaxLength(100)]
            public string Name { get; set; }
            [Required]
            [MaxLength(100)]
            [Display(Name = "نام خانوادگی")]
            public string Family { get; set; }
            [Required]
            [Phone(ErrorMessage = "{0} وارد شده نامعتبر است")]
            [Display(Name = "شماره تلفن")]
            public string Phone { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Id= user.Id,
                Phone = phoneNumber,
                Name= user.Name,
                Family= user.Family
            };
            var roles = _roleManager.Roles.Where(p => p.Name != SD.CustomerOwnerRole);
            var currentrole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            Roles = new SelectList(roles.Where(p=>p.Name!=SD.NormalUserRole), "Name", "Name",(await _userManager.GetRolesAsync(user)).FirstOrDefault());
        }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            var user = _userManager.Users.Where(p=>p.Id==Id).FirstOrDefault();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _userManager.Users.Where(p => p.Id == Input.Id).FirstOrDefault();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.Phone != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.Phone);         
            }
            user.Name = Input.Name;
            user.Family = Input.Family;
            await _userManager.UpdateAsync(user);
            if (!await _userManager.IsInRoleAsync(user,SelectedRole))
            {
                await _userManager.RemoveFromRolesAsync(user, (await _userManager.GetRolesAsync(user)));
                await _userManager.AddToRoleAsync(user, SelectedRole);
            }
            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage("Index");
        }
    }
}
