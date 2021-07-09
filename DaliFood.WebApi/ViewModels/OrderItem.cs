using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.WebApi.ViewModels
{
    public class OrderItem
    {
        [Display(Name = "شناسه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Id { get; set; }
        [Display(Name = "سفارش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int OrderId { get; set; }
        [Display(Name = "محصول فروشنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CustomerProductName{ get; set; }
        [Display(Name = "فروشنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CustomerName { get; set; }
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }
        [Display(Name = "تعداد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Count { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Status { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime CreateDate { get; set; }
    }
}
