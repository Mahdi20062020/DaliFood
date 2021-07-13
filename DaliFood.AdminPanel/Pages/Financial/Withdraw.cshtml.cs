using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Financial
{
    public class WithdrawModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public WithdrawModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public IEnumerable<Models.Withdraw> Withdraws { get; set; }

        public void OnGet()
        {
            Withdraws = unitofwork.WithdrawRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
        }
        public ActionResult OnGetConfirm(int Id)
        {
            var Withdraw = unitofwork.WithdrawRepository.GetById(Id);
            Withdraw.Status = 1;
            unitofwork.WithdrawRepository.Modifie(Withdraw);
            unitofwork.WithdrawRepository.Save();
            return Page();
        }
    }
}
