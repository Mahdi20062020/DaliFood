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

namespace DaliFood.AdminPanel.Pages.Customer.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UnitOfWork unitofwork;
        private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _db;


        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
             RoleManager<IdentityRole> roleManager, ApplicationDbContext db, UnitOfWork _unitofwork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender;
            _roleManager = roleManager;
            _db = db;
            unitofwork = _unitofwork;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }


        public class InputModel
        {
           


            [Required]
            [Display(Name = "نام")]
            [MaxLength(100)]
            public string name { get; set; }
            [Required]
            [MaxLength(100)]
            [Display(Name = "نام خانوادگی")]
            public string family { get; set; }
            [Required]
            [MaxLength(100)]
            [Display(Name = "کدملی")]
            public string NationalId { get; set; }
            [Required]
            [Display(Name = "تاریخ تولد")]
         
            public DateTime Birthday { get; set; }


            [Required]
            [Display(Name = "نام فروشگاه")]
            [MaxLength(100)]
            public string CustomerName { get; set; }
            [Required]
            [Display(Name = "نوع فروشگاه")]
        
            public int CustomerType { get; set; }
            [Required]
            [Display(Name = "آدرس فروشگاه")]
            [MaxLength(100)]
            public string CustomerAddress { get; set; }


            [Required]
            [EmailAddress(ErrorMessage = "{0} وارد شده نامعتبر است")]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }
            [Required]
          
            [Display(Name = "شماره تلفن")]
            public string PhoneNumber { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "{0} باید کمتر از {2} و بیشتر از {1} باشد", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "رمز عبور")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "تکرار رمز عبور")]
            [Compare("Password", ErrorMessage = "رمز عبور وارده ناهماهنگ است")]
            public string ConfirmPassword { get; set; }
            //[MaxLength(250)]
            //public string profileImage { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ViewData["CustomerTypeId"] = new SelectList(unitofwork.CustomerTypeRepository.GetAll(), "Id", "Name");

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
               
                var userDetail = new ApplicationCustomerUser()
                {
                    CustomerName = Input.CustomerName,
                    BirthDate = Input.Birthday,
                    NationalId = Input.NationalId,
                    CreateDate = DateTime.Now
                };
               
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Name = Input.name,
                    Family = Input.family,
                    PhoneNumber=Input.PhoneNumber,
                    ApplicationUserDetail = userDetail
                };
                var customer = new Models.Customer()
                {
                    Name = Input.CustomerName,
                    CreateDate = DateTime.Now,
                    TypeId = Input.CustomerType,
                    Address = Input.CustomerAddress,
                    ApplicationCustomerUser= userDetail
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, SD.CustomerOwnerRole);
                    //{
                    //    if (!await _roleManager.RoleExistsAsync(SD.AdminRole))
                    //    {
                    //        await _roleManager.CreateAsync(new IdentityRole(SD.AdminRole));
                    //    }
                    //    if (!await _roleManager.RoleExistsAsync(SD.ProductCustomerRole))
                    //    {
                    //        await _roleManager.CreateAsync(new IdentityRole(SD.ProductCustomerRole));
                    //    }
                    //    if (!await _roleManager.RoleExistsAsync(SD.BlogCustomerRole))
                    //    {
                    //        await _roleManager.CreateAsync(new IdentityRole(SD.BlogCustomerRole));
                    //    }
                    //    if (!await _roleManager.RoleExistsAsync(SD.NormalUserRole))
                    //    {
                    //        await _roleManager.CreateAsync(new IdentityRole(SD.NormalUserRole));
                    //    }
                    //    if (Input.IsAdmin || Input.IsBlogCustomer || Input.IsProductCustomer)
                    //    {
                    //        if (Input.IsAdmin)
                    //        {
                    //            await _userManager.AddToRoleAsync(user, SD.AdminRole);
                    //        }
                    //        if (Input.IsBlogCustomer)
                    //        {
                    //            await _userManager.AddToRoleAsync(user, SD.BlogCustomerRole);
                    //        }
                    //        if (Input.IsProductCustomer)
                    //        {
                    //            await _userManager.AddToRoleAsync(user, SD.ProductCustomerRole);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        await _userManager.AddToRoleAsync(user, SD.NormalUserRole);
                    //    }

                    _logger.LogInformation("User created a new account with password.");

                    //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //    var callbackUrl = Url.Page(
                    //        "/Account/ConfirmEmail",
                    //        pageHandler: null,
                    //        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //        protocol: Request.Scheme);

                    //    await _emailSender.SendEmailAsync(Input.Email, "ایمیل فعال سازی بانوفاخر",
                    //        $"برای فعال سازی حساب خود <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>اینجا</a> کلیک کنید.");

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

            //// If we got this far, something failed, redisplay form
            return Page();
        }
    
}
}
