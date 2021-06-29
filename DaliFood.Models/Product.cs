using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "فروشنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CustomerId { get; set; }
        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")] 
        public int CategorieId { get; set; }
        [Display(Name ="نام")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Name { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime CreateDate { get; set; }

        public virtual ProductCategorie ProductCategorie { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Discount Discount { get; set; }
        public Product()
        {

        }
    }
}
