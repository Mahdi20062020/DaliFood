using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Models
{
    public class Customer
    {

        [Key]
        public int Id { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }
        [Display(Name = "نوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [ForeignKey("CustomerType")]
        public int TypeId { get; set; }
        [Display(Name = "جواز کسب")]
     
        public string LicenseSaveAddress { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Address { get; set; }
        [Display(Name = "شماره شبا")]
        public string ShabaNumber { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime CreateDate { get; set; }
        public virtual IEnumerable<Product> Product { get; set; }
        public virtual IEnumerable<OrderItem> OrderItem { get; set; }
        public virtual CustomerType CustomerType { get; set; }
        public Customer()
        {

        }
    }
}
