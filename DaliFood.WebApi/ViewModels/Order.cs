using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.WebApi.ViewModels
{
    public class Order
    {
        [Display(Name = "شناسه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Id { get; set; }
        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserId { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool Status { get; set; }
        [Display(Name = "قیمت کل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int TotalPrice { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime CreateDate { get; set; }
        public List<OrderItem> orderItems { get; set; }
        public static implicit operator Order(Models.Order _order)
        {
            var orderItems=new List<OrderItem>();
           Order order = new();
            order.Id = _order.Id;
            order.UserId = _order.UserId;
            order.Status = _order.Status;
            order.TotalPrice = _order.TotalPrice;
            order.CreateDate = _order.CreateDate;
            foreach (var item in _order.OrderItem)
            {
                var orderitem = new OrderItem();
                orderitem.Id = item.Id;
                orderitem.OrderId = item.OrderId;
                orderitem.CustomerProductName = item.CustomersProduct.Product.Name;
                orderitem.CustomerName = item.Customer.Name;
                orderitem.Count = item.Count;
                orderitem.Status = item.Status;
                orderitem.Price = item.Price;
                orderItems.Add(orderitem);
            }
            order.orderItems= orderItems;
            return order;
        }
    }
}
