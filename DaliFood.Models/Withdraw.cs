using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Models
{
    public class Withdraw:TransactionItem
    {
        [Display(Name = "شماره کارت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Cardnumber { get; set; }
        [Display(Name = "شماره شبا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Shabanumber { get; set; }
        [Display(Name = "کد رهگیری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TrackingCode { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Status { get; set; }
        [Display(Name = "تاریخ برداشت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime DepositDate { get; set; }
        public Withdraw()
        {

        }
    }
}
