using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "غذا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductId { get; set; }

        [Display(Name = "میزان تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
     
        public int DiscountRate { get; set; }

        [Display(Name = "تاریخ شروع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime StartDate { get; set; }
        [Display(Name = "تاریخ انقضا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime ExpirationDate { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime CreateDate { get; set; }
        public virtual Product Product { get; set; }
        public Discount()
        {

        }
    }
}
