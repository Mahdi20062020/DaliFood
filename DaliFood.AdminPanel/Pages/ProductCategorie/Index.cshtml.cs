using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Models;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.ProductCategorie
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public IndexModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public IEnumerable<Models.ProductCategorie> ProductCategorie { get; set; }

        public void OnGet()
        {
            ProductCategorie = unitofwork.ProductCategorieRepository.GetAll(orderby:p=>p.OrderByDescending(p=>p.CreateDate));
        }
    }
}
