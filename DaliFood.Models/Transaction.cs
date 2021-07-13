using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int OldAmount { get; set; }
        [Display(Name = "اعتبار جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int NewAmount { get; set; }
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }
  
        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime CreateDate { get; set; }
        [ForeignKey(nameof(ItemId))]
        public virtual TransactionItem TransactionItem { get; set; }
        public Transaction()
        {

        }
    }
}
