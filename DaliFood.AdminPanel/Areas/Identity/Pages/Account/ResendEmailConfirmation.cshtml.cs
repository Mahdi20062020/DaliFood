using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using DaliFood.Models.Identity;

namespace DaliFood.AdminPanel.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResendEmailConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ResendEmailConfirmationModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [EmailAddress(ErrorMessage = "{0} وارد شده نامعتبر است")]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "حسابی با ایمیل وارد شده یافت نشد");
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            await _emailSender.SendConfirmationEmail(_userManager, (Models.Identity.ApplicationUser)user, Url,Request);


            ModelState.AddModelError(string.Empty, "ایمیل فعالسازی به حساب شما ارسال گردید. لطفا ایمیل خود را چک نمایید");
            return Page();
        }
    }
}
