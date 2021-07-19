using DaliFood.AdminPanel.Helpers;
using DaliFood.Models;
using DaliFood.Models.Identity;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
namespace DaliFood.AdminPanel.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UnitOfWork unitofwork;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public IndexModel(UnitOfWork unitofwork, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.unitofwork = unitofwork;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [BindProperty]
        public DateTime Then { get; set; }
        [BindProperty]

        public DateTime Now { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated is false)
            {
                await unitofwork.Configure(roleManager, userManager);
            }

            Then = DateTime.Now;
            Now = DateTime.Now;

            return Page();
        }

        [HttpPost]
        public IActionResult OnPost(string Name, string Message, string Email)
        {
            var result = new ReturnAjaxForm();
            var entity = new DaliFood.Models.Letter()
            {
                FullName = Name,
                Message = Message,
                Email = Email
            };

            try
            {

                unitofwork.LetterRepository
                    .Create(entity);


                result.ResultType = ResultType.Success;
                result.Message = "پیام شما با موفقیت ارسال گردید.";
                return new JsonResult(result);
            }
            catch (Exception e)
            {

                result.ResultType = ResultType.Failure;
                result.Message = "متاسفانه ارسال با خطا مواجه گردید مجددا تلاش فرمایید";
                return new JsonResult(result);
            }
        }

    }
}
