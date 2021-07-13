using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Models
{
    public class Deposit:TransactionItem
    {

        [Display(Name = "بانک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int BankId { get; set; }

        public string SaleReferenceId { get; set; }

        public string RefId { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Status { get; set; }

        [Display(Name = "شماره کارت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Cardnumber { get; set; }

        [Display(Name = "تاریخ واریز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime DepositDate { get; set; }

        public Deposit()
        {

        }
    }
}
