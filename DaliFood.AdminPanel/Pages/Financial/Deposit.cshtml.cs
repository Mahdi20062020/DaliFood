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
                Deposits = Deposits.Where(p=>p.Description.Contains(SearchQ));
            }

        }
        public ActionResult OnGetConfirm(int Id, int Status)
        {
            var Deposit = unitofwork.DepositRepository.GetById(Id);
            Deposit.Status = Status;
            unitofwork.DepositRepository.Modifie(Deposit);
            unitofwork.DepositRepository.Save();
            return RedirectToPage("Deposit");

        }
    }
}
