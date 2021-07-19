using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.AdminPanel.Helpers;
using DaliFood.Models.Data;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages
{
    public class LettersenderModel : PageModel
    {
        private readonly UnitOfWork unitofwork;
        private readonly ApplicationDbContext context;
        public LettersenderModel(UnitOfWork unitofwork, ApplicationDbContext context)
        {
            this.unitofwork = unitofwork;
            this.context = context;

        }


        public JsonResult OnGet(string Name, string Message, string Email)
        {
            var result = new ReturnAjaxForm();
            var entity = new DaliFood.Models.Letter()
            {
                FullName = Name,
                Message = Message,
                Email = Email
            };


            context.Letters.Add(entity);
            var x = context.SaveChanges();

            if (x == 0)
            {
                result.ResultType = ResultType.Failure;
                result.Message = "متاسفانه ارسال با خطا مواجه گردید مجددا تلاش فرمایید";
                return new JsonResult(result);
            }
            else
            {
                result.ResultType = ResultType.Success;
                result.Message = "پیام شما با موفقیت ارسال گردید.";
                return new JsonResult(result);
            }

        }
    }
}
