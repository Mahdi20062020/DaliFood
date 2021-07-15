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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace DaliFood.AdminPanel.Pages.Customer
{
    public class UpdateModel : PageModel
    {

        #region Lazy loading
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UnitOfWork unitofwork;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _db;

        public UpdateModel(
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

            [Display(Name = "عرض جغرافیایی")]
            public string Latitude { get; set; }

            [Display(Name = "طول جغرافیایی")]
            public string Longitude { get; set; }


            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [MaxLength(10, ErrorMessage = "کد ملی باید حداکثر 10 رقم باشد.")]
            [MinLength(10, ErrorMessage = "کد ملی باید حداقل 10 رقم باشد.")]
            [Display(Name = "کدملی")]
            [NationalId(ErrorMessage = "{0} وارد شده نامعتبر است")]
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

            public int CustomerId { get; set; }

            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "نام مالک فروشگاه")]
            public string CustomerOwnerName { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "نام خانوادگی مالک فروشگاه")]
            public string CustomerOwnerFamily { get; set; }

            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "شماره تلفن")]
            public string PhoneNumber { get; set; }

            public bool Status { get; set; }
            //[MaxLength(250)]
            //public string profileImage { get; set; }
        }

        public async Task OnGetAsync(int id, string returnUrl = null)
        {

            var Shop = unitofwork.CustomerRepository.GetById(id);

            Input = new InputModel();

            Input.CustomerId = id;
            Input.CustomerName = Shop.Name;
            Input.CustomerOwnerFamily = Shop.OwnerFamily;
            Input.CustomerOwnerName = Shop.OwnerName;
            Input.Status = Shop.Status;
            Input.CustomerAddress = Shop.Address;
            Input.City = Shop.City.Id;
            Input.Latitude = Shop.Latitude;
            Input.Longitude = Shop.Longitude;
            Input.NationalId = Shop.ApplicationCustomerUser.NationalId;
            Input.CustomerType = Shop.CustomerType.Id;
            Input.PhoneNumber = Shop.Phonenumber;

            ViewData["CustomerTypeId"] = new
                    SelectList(unitofwork.CustomerTypeRepository.GetAll(), "Id", "Name");

            ViewData["City"] =
                new SelectList(unitofwork.CityRepository.GetAll(), "Id", "Name");

        }

        public async Task<IActionResult> OnpostAsync(string returnUrl = null)
        { 
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var Shop = unitofwork
                    .CustomerRepository.GetById(Input.CustomerId);


                Shop.Name = Input.CustomerName;
                Shop.OwnerName = Input.CustomerOwnerName;
                Shop.OwnerFamily = Input.CustomerOwnerFamily;
                Shop.Longitude = Input.Longitude;
                Shop.Latitude = Input.Latitude;
                Shop.Status = Input.Status;
                Shop.Phonenumber = Input.PhoneNumber;
                Shop.Address = Input.CustomerAddress;
                Shop.ApplicationCustomerUser.NationalId = Input.NationalId;
                Shop.CityId = Input.City;
                Shop.TypeId = Input.CustomerType;


                unitofwork.CustomerRepository.Modifie(Shop);
                await unitofwork
                        .CustomerRepository.SaveAsync();

                return Redirect("/Customer/Index");
            }

            ViewData["CustomerTypeId"] = new SelectList(unitofwork.CustomerTypeRepository.GetAll(), "Id", "Name");
            ViewData["City"] =
                new SelectList(unitofwork.CityRepository.GetAll(), "Id", "Name");
            return Page();
        }

    }
}
