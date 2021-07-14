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

        public void OnGet(int? SearchStatus, int? SearchMinPrice, int? SearchMaxPrice, string SearchQ = null, string SearchStartDate = null, string SearchEndDate = null)
        {
            Withdraws = unitofwork.WithdrawRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            if (SearchStatus.HasValue)
            {
                Withdraws = Withdraws.Where(p => p.Status == SearchStatus);
            }
            if (SearchMinPrice.HasValue)
            {
                Withdraws = Withdraws.Where(p => p.Amount >= SearchMinPrice);
            }
            if (SearchMaxPrice.HasValue)
            {
                Withdraws = Withdraws.Where(p => p.Amount <= SearchMaxPrice);
            }
            if (SearchQ != null)
            {
                Withdraws = Withdraws.Where(p => p.Description.Contains(SearchQ) || p.Id.ToString().Contains(SearchQ) || p.UserId.Contains(SearchQ)).Distinct();
            }
            if (SearchStartDate != null)
            {
                var date = SearchStartDate.Split('/');
                int year = int.Parse(date[0]);
                int month = int.Parse(date[1]);
                int day = int.Parse(date[2]);
                var Startdate = new DateTime(year, month, day);
                Withdraws = Withdraws.Where(p => p.DepositDate >= Startdate);
            }
            if (SearchEndDate != null)
            {
                var date = SearchEndDate.Split('/');
                int year = int.Parse(date[0]);
                int month = int.Parse(date[1]);
                int day = int.Parse(date[2]);
                var Startdate = new DateTime(year, month, day);
                Withdraws = Withdraws.Where(p => p.DepositDate <= Startdate);
            }

        }
        public ActionResult OnGetConfirm(int Id,int Status)
        {
            var Withdraw = unitofwork.WithdrawRepository.GetById(Id);
            Withdraw.Status = Status;
            unitofwork.WithdrawRepository.Modifie(Withdraw);
            unitofwork.WithdrawRepository.Save();
            return RedirectToPage("Withdraw");

        }
    }
}
