using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Customer
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public IndexModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }

        
        [BindProperty]
        public IEnumerable<Models.Customer> Customer { get; set; }


        public void OnGet()
        {
            Customer = unitofwork.CustomerRepository
                .GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            foreach (var item in Customer)
            {
                item.CustomerType = unitofwork
                    .CustomerTypeRepository.GetById(item.TypeId);
            }

        }



    }
}
