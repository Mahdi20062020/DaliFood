using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Models.Identity
{
    public class PhoneNumbersToken
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Phonenumber { get; set; }
        [Display(Name = "توکن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TokenHash { get; set; }
        [Display(Name = "وضعیت توکن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool Status { get; set; } 
        [Display(Name = "وضعیت فعال سازی شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool IsConfirm { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime CreateDate { get; set; }
    }
}
