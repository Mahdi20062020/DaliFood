using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.AdminPanel.Areas.Identity.Pages.Account;
using DaliFood.Models.Data;
using DaliFood.Models.Identity;
using DaliFood.Utilites;
using DaliFood.Utilites.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace DaliFood.AdminPanel.Pages.Customer
{
    public class UpsertModel : PageModel
    {
        #region Lazy loading
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UnitOfWork unitofwork;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _db;

        public UpsertModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext db,
            UnitOfWork unitofwork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _db = db;
            this.unitofwork = unitofwork;
        }
        #endregion


        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }


        public class InputModel
        {
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "نام")]
            [MaxLength(100)]
            public string Name { get; set; }

            [Display(Name = "عرض جغرافیایی")]
            public string Latitude { get; set; }

            [Display(Name = "طول جغرافیایی")]
            public string Longitude { get; set; }

            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "نام خانوادگی")]
            public string Family { get; set; }

            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [MaxLength(10, ErrorMessage = "کد ملی باید حداکثر 10 رقم باشد.")]
            [MinLength(10, ErrorMessage = "کد ملی باید حداقل 10 رقم باشد.")]
            [Display(Name = "کدملی")]
            //[NationalId(ErrorMessage = "{0} وارد شده نامعتبر است")]
            public string NationalId { get; set; }

            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "شهر")]
            public int City { get; set; }

            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "نام فروشگاه")]
            [MaxLength(100)]
            public string CustomerName { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "نوع فروشگاه")]
            public int CustomerType { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "آدرس فروشگاه")]
            [MaxLength(100)]
            public string CustomerAddress { get; set; }

            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "نام مالک فروشگاه")]
            public string CustomerOwnerName { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "نام خانوادگی مالک فروشگاه")]
            public string CustomerOwnerFamily { get; set; }

            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [EmailAddress(ErrorMessage = "{0} وارد شده نامعتبر است")]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "شماره تلفن")]
            public string PhoneNumber { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
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
            ViewData["CustomerTypeId"] =
                new SelectList(unitofwork.CustomerTypeRepository.GetAll(), "Id", "Name");

            ViewData["City"] =
                new SelectList(unitofwork.CityRepository.GetAll(), "Id", "Name");

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {

                ApplicationCustomerUser userDetail = new ApplicationCustomerUser()
                {
                    CustomerName = Input.CustomerName,
                    NationalId = Input.NationalId,
                    CreateDate = DateTime.Now
                    

                };

                ApplicationUser user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Name = Input.Name,
                    Family = Input.Family,
                    PhoneNumber = Input.PhoneNumber,
                    ApplicationUserDetail = userDetail
                };
                Models.Customer customer = new Models.Customer()
                {

                    Name = Input.CustomerName,
                    CreateDate = DateTime.Now,
                    TypeId = Input.CustomerType,
                    Address = Input.CustomerAddress,
                    CityId = Input.City,              
                    OwnerName=Input.CustomerOwnerName,
                    OwnerFamily=Input.CustomerOwnerFamily,
                    Longitude = Input.Longitude,
                    Latitude = Input.Latitude,
                    ApplicationCustomerUser = userDetail
                };
      

                var result = await 
                    _userManager.CreateAsync(user, Input.Password);
                
                if (result.Succeeded)
                {

                    string UserId = await _userManager.GetUserIdAsync(user);
                    
                    customer.UserId = UserId;
                    unitofwork.CustomerRepository
                        .Create(customer);
                    await unitofwork
                        .CustomerRepository.SaveAsync();

                    string CustomerId = 
                        unitofwork.CustomerRepository.GetAll(where: p => p.UserId == UserId).FirstOrDefault().Id.ToString();
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(SD.CustomerId, CustomerId));
                    await _userManager.AddToRoleAsync(user, SD.CustomerOwnerRole);
                    _logger.LogInformation("User created a new account with password.");

                    //await _emailSender.SendConfirmationEmail(_userManager, user, Url, Request, returnUrl);
                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return Redirect($"/Identity/Account/RegisterConfirmation?Email={Input.Email}");
                    //}
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return LocalRedirect(returnUrl);
                    //}

                    return RedirectToPage("/Customer/Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            //// If we got this far, something failed, redisplay form
            ViewData["CustomerTypeId"] = new SelectList(unitofwork.CustomerTypeRepository.GetAll(), "Id", "Name");
            ViewData["City"] =
                new SelectList(unitofwork.CityRepository.GetAll(), "Id", "Name");

            return Page();
        }
    }
}
