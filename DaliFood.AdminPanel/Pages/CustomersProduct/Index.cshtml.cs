using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.CustomersProduct
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public IndexModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public IEnumerable<Models.CustomersProduct> CustomersProduct { get; set; }

        public void OnGet()
        {
            CustomersProduct = unitofwork.CustomersProductRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            foreach (var item in CustomersProduct)
            {
                item.Product = unitofwork.ProductRepository.GetById(item.ProductId);
            }
        }
        public ActionResult OnGetDelete(int Id)
        {
            //var part = SD.GetPart(unitofwork, SD.PhotoForCustomersProducts.Name);

            var CustomersProduct = unitofwork.CustomersProductRepository.GetById(Id);
            if (CustomersProduct == null)
                return NotFound();
            //var photos = unitofwork.PhotoRepository.GetAll(where: p => p.ItemId == CustomersProduct.Id && p.PartId == part.Id);

            unitofwork.CustomersProductRepository.Delete(CustomersProduct);
            unitofwork.CustomersProductRepository.Save();
            //Files.DeletePhotos(unitofwork, photos, part);
            return RedirectToPage("Index");

        }
    }
}
