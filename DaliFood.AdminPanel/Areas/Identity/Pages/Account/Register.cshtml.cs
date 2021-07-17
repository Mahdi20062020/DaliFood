using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DaliFood.Models.Data;
using DaliFood.Models.Identity;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace DaliFood.AdminPanel.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _db;


        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _db = db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [BindProperty]
        public string SelectedRole { get; set; }

        public SelectList Roles { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress(ErrorMessage = "{0} وارد شده نامعتبر است")]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }

            [Required]
            [Phone(ErrorMessage = "{0} وارد شده نامعتبر است")]
            [Display(Name = "شماره تلفن")]
            public string Phone { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0} باید کمتر از {2} و بیشتر از {1} باشد", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "رمز عبور")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "تکرار رمز عبور")]
            [Compare("Password", ErrorMessage = "رمز عبور وارده ناهماهنگ است")]
            public string ConfirmPassword { get; set; }
            [Required]
            [Display(Name = "نام")]
            [MaxLength(100)]
            public string Name { get; set; }
            [Required]
            [MaxLength(100)]
            [Display(Name = "نام خانوادگی")]
            public string Family { get; set; }

          
            //[MaxLength(250)]
            //public string profileImage { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var roles = _roleManager.Roles.Where(p => p.Name != SD.CustomerOwnerRole);
          
            Roles = new SelectList(roles, "Name", "Name");

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Name = Input.Name,
                    Family = Input.Family,
                    PhoneNumber=Input.Phone,           
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, SelectedRole);
                    if (SelectedRole==SD.AdminRole)
                    {
                     await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(SD.CustomerId, SD.AdminCustomerId));
                    }
                _logger.LogInformation("User created a new account with password.");
                      await _emailSender.SendConfirmationEmail(_userManager, user, Url, Request);
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                       {
                           return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                       }
                       else
                       {
                         await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                       }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
            }
            var roles = _roleManager.Roles.Where(p => p.Name != SD.CustomerOwnerRole);

            Roles = new SelectList(roles, "Name", "Name");
            //// If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
