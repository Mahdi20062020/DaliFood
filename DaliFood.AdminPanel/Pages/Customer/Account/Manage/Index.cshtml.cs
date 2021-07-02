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

namespace DaliFood.AdminPanel.Pages.Customer.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UnitOfWork unitofwork;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "نام")]
            [MaxLength(100)]
            public string Name { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [MaxLength(100)]
            [Display(Name = "نام خانوادگی")]
            public string Family { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [MaxLength(100)]
            [Display(Name = "کدملی")]
            public string NationalId { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "تاریخ تولد")]
            public DateTime Birthday { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "شماره تلفن")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var UserDetail= (ApplicationCustomerUser)user.ApplicationUserDetail;
            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Name=user.Name,
                Family=user.Family,
                NationalId= UserDetail.NationalId,
                Birthday=UserDetail.BirthDate
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.Name = Input.Name;
            user.Family = Input.Family;
            user.PhoneNumber = Input.PhoneNumber;
            var UserDetail = (ApplicationCustomerUser)user.ApplicationUserDetail;
            UserDetail.BirthDate = Input.Birthday;
            UserDetail.NationalId = Input.NationalId;
            user.ApplicationUserDetail = UserDetail;
          
           
            var UpdateResult = await _userManager.UpdateAsync(user);
            if (!UpdateResult.Succeeded)
                {
             
                StatusMessage = "Unexpected error when trying to Edit Profile.";
                    return RedirectToPage();
                }
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
