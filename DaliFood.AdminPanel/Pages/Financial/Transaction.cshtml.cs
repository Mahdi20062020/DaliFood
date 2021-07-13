using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Financial
{
    public class TransactionModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public TransactionModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public IEnumerable<Models.Transaction> Transactions { get; set; }

        public void OnGet(int? SearchMinPrice, int? SearchMaxPrice, string SearchQ = null, string SearchStartDate = null, string SearchEndDate = null)
        {
            Transactions = unitofwork.TransactionRepository.GetAll(orderby: p => p.OrderByDescending(p => p.Id));
          
            if (SearchMinPrice.HasValue)
            {
                Transactions = Transactions.Where(p => p.Amount >= SearchMinPrice);
            }
            if (SearchMaxPrice.HasValue)
            {
                Transactions = Transactions.Where(p => p.Amount <= SearchMaxPrice);
            }
            if (SearchQ != null)
            {
                Transactions = Transactions.Where(p => p.Description.Contains(SearchQ) ||p.ItemId.ToString().Contains(SearchQ)||p.Id.ToString().Contains(SearchQ));
            }

        }
      
      
    }
}
