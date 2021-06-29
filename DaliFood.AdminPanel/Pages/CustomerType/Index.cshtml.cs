using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.CustomerType
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public IndexModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public IEnumerable<Models.CustomerType> CustomerType { get; set; }

        public void OnGet()
        {
            CustomerType = unitofwork.CustomerTypeRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
        }
        public ActionResult OnGetDelete(int Id)
        {
            var CustomerType = unitofwork.CustomerTypeRepository.GetById(Id);
            if (CustomerType == null)
                return NotFound();
            unitofwork.CustomerTypeRepository.Delete(CustomerType);
            var products = unitofwork.ProductRepository.GetAll(where: p => p.CategorieId == Id);
            foreach (var item in products)
            {
                unitofwork.ProductRepository.Delete(item);
            }
            unitofwork.ProductRepository.Save();
            unitofwork.CustomerTypeRepository.Save();
            return RedirectToPage("Index");

        }
    }

}
