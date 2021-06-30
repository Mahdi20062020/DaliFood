using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "سفارش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int OrderId { get; set; }
        [Display(Name = "محصول فروشنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CustomerProductId { get; set; }
        [Display(Name = "فروشنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CustomerId { get; set; }
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
        public virtual Order Order { get; set; }
        public virtual CustomersProduct CustomersProduct { get; set; }
        public virtual Customer Customer { get; set; }
        public OrderItem()
        {

        }
    }
}
