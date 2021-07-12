using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Order
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public IndexModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public IEnumerable<Models.Order> Order { get; set; }

        public void OnGet()
        {
            Order = unitofwork
                .OrderRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            
        }
    }
}
