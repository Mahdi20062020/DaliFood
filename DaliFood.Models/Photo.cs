using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PartId { get; set; }
        [Display(Name = "آیتم مربوطه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ItemId { get; set; }
        [Display(Name = "پسوند")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Extention { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime CreateDate { get; set; }
        public virtual PhotoFor PhotoFor { get; set; }
        public virtual object Item { get; set; }
        public Photo()
        {

        }
    }
}
