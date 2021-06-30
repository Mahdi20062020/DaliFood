using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Order.Item
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public IndexModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public IEnumerable<Models.OrderItem> OrderItem { get; set; }

        public void OnGet()
        {
            OrderItem = unitofwork.OrderItemRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            foreach (var item in OrderItem)
            {
                item.Customer = unitofwork.CustomerRepository.GetById(item.CustomerId);
                item.CustomersProduct = unitofwork.CustomersProductRepository.GetById(item.CustomerProductId);
                item.CustomersProduct.Product = unitofwork.ProductRepository.GetById(item.CustomersProduct.Id);
            }
        }
        public ActionResult OnGetChangeStatus(int Id)
        {
            var orderitem = unitofwork.OrderItemRepository.GetById(Id);
            if (orderitem == null)
                return NotFound();
            switch (orderitem.Status)
            {
                case SD.CheckingOrderStatus:
                    orderitem.Status = SD.BakingOrderStatus;
                    break;
                case SD.BakingOrderStatus:
                    orderitem.Status = SD.SendingOrderStatus;
                    break;
                case SD.SendingOrderStatus:
                    orderitem.Status = SD.ReceivingOrderStatus;
                    orderitem.Order.OrderItem = unitofwork.OrderItemRepository.GetAll(where: p => p.OrderId == orderitem.OrderId);
                    var OrderStatus = false;
                    foreach (var item in orderitem.Order.OrderItem)
                    {
                        if (item.Status!=SD.ReceivingOrderStatus)
                        {
                            OrderStatus = true;
                            break;
                        }
                    }
                    orderitem.Order.Status = OrderStatus;
                    break;
                case SD.ReceivingOrderStatus:
                    return Page();
                default:
                    orderitem.Status = SD.CheckingOrderStatus;
                    break;
            }
            unitofwork.OrderItemRepository.Modifie(orderitem);
            unitofwork.OrderItemRepository.Save();
            return RedirectToPage("Index");
        }
    }
}
