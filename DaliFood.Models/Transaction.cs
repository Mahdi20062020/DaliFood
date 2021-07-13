using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int TypeId { get; set; }
        [Display(Name = "اطلاعات آیتم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ItemId { get; set; }
        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserId { get; set; }
        [Display(Name = "اعتبار سابق")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string OldAmount { get; set; }
        [Display(Name = "اعتبار جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string NewAmount { get; set; }
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Amount { get; set; }
    }
}
