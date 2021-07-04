using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using DaliFood.Models.Identity;

namespace DaliFood.AdminPanel.Pages.Customer.Account.Manage
{
    public class CustomerProfileModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public CustomerProfileModel(UnitOfWork unitofwork,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.unitofwork = unitofwork;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public Models.Customer Customer { get; set; }
        [BindProperty]
        public IFormFile ImageUpload { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        public async Task<ActionResult> OnGet()
        {
            Customer = new Models.Customer();

            var user =await _userManager.GetUserAsync(User);
            Customer = unitofwork.CustomerRepository.GetAll(where:p=>p.UserId==user.Id).FirstOrDefault();
            if (Customer == null)
            {
                return NotFound();
            }
            ViewData["CustomerTypeId"] = new SelectList(unitofwork.CustomerTypeRepository.GetAll(), "Id", "Name");
            return Page();
        }
        public ActionResult OnPost()
        {
            
            if (ImageUpload != null)
            {
                Customer.LicenseSaveAddress = Guid.NewGuid().ToString() + Path.GetExtension(ImageUpload.FileName);
                Files.SavePhoto(ImageUpload, "CustomerLisence", Customer.LicenseSaveAddress);
            }
            if (!ModelState.IsValid)
            {
                ViewData["CustomerTypeId"] = new SelectList(unitofwork.CustomerTypeRepository.GetAll(), "Id", "Name");
                return Page();
            }
        
                unitofwork.CustomerRepository.Modifie(Customer);
            
            unitofwork.CustomerRepository.Save();
            return RedirectToPage("Index");
        }
    }
}
