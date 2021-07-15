using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Financial
{
    public class DepositModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public DepositModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public IEnumerable<Models.Deposit> Deposits { get; set; }

        public void OnGet(int? SearchStatus, int? SearchMinPrice, int? SearchMaxPrice, string SearchQ = null, string SearchStartDate = null, string SearchEndDate = null)
        {
            Deposits = unitofwork.DepositRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            if (SearchStatus.HasValue)
            {
                Deposits = Deposits.Where(p => p.Status == SearchStatus);
            }
            if (SearchMinPrice.HasValue)
            {
                Deposits = Deposits.Where(p => p.Amount >= SearchMinPrice);
            }
            if (SearchMaxPrice.HasValue)
            {
                Deposits = Deposits.Where(p => p.Amount <= SearchMaxPrice);
            }
            if (SearchQ !=null)
            {
                Deposits = Deposits.Where(p => p.Description.Contains(SearchQ) || p.Id.ToString().Contains(SearchQ) || p.UserId.Contains(SearchQ)).Distinct();
            }
            if (SearchStartDate!=null)
            {     
                var date= SearchStartDate.Split('/');
                int year = int.Parse(date[0]);
                int month = int.Parse(date[1]);
                int day = int.Parse(date[2]);
                var Startdate = new DateTime(year,month,day);
                Deposits = Deposits.Where(p => p.DepositDate >= Startdate);
            }
            if (SearchEndDate != null)
            {
                var date = SearchEndDate.Split('/');
                int year = int.Parse(date[0]);
                int month = int.Parse(date[1]);
                int day = int.Parse(date[2]);
                var Startdate = new DateTime(year, month, day);
                Deposits = Deposits.Where(p => p.DepositDate <= Startdate);
            }

        }
        public ActionResult OnGetConfirm(int Id, int Status)
        {
            var Deposit = unitofwork.DepositRepository.GetById(Id);
            Deposit.Status = Status;
            if (unitofwork.DepositRepository.Modifie(Deposit))
            {
                if (unitofwork.DepositRepository.Save())
                {
                    return RedirectToPage("Deposit");
                }
            }
            return BadRequest();
        }
    }
}
