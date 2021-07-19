using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.AdminPanel.Helpers;
using DaliFood.Models.Data;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Letter
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        private readonly ApplicationDbContext context;

        public IndexModel(UnitOfWork unitofwork, ApplicationDbContext context)
        {
            this.unitofwork = unitofwork;
            this.context = context;
        }

        
        [BindProperty]
        public IEnumerable<Models.Letter> Letters { get; set; }
        
        
        public void OnGet()
        {
            Letters = unitofwork.LetterRepository.GetAll();
        }

        public async Task<ActionResult> OnGetDelete(int Id)
        {
            var entity = context.Letters.Find(Id);
            if (entity == null)
                return NotFound();
            context.Letters.Remove(entity);
            await context.SaveChangesAsync();

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
