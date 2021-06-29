using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Product
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public IndexModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public IEnumerable<Models.Product> Product { get; set; }

        public void OnGet()
        {
            Product = unitofwork.ProductRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            foreach (var item in Product)
            {
                item.ProductCategorie = unitofwork.ProductCategorieRepository.GetById(item.CategorieId);
            }
        }
        public ActionResult OnGetDelete(int Id)
        {
            var part = SD.GetPart(unitofwork, SD.PhotoForProducts.Name);

            var Product = unitofwork.ProductRepository.GetById(Id);
            if (Product == null)
                return NotFound();
            var photos = unitofwork.PhotoRepository.GetAll(where: p => p.ItemId == Product.Id && p.PartId == part.Id);

            unitofwork.ProductRepository.Delete(Product);
            unitofwork.ProductRepository.Save();
            Files.DeletePhotos(unitofwork, photos, part);
            return RedirectToPage("Index");

        }
    }
}
