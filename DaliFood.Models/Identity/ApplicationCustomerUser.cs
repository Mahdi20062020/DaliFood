using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Models.Identity
{
    public class ApplicationCustomerUser: ApplicationUserDetail
    {
        [StringLength(100,ErrorMessage ="{1} باید کمتر از {0} باشد")]
        public string CustomerName { get; set; }
        //public string LicenseSaveAddress { get; set; }
        [Display(Name = "کارت ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string NationalCardSaveAddress { get; set; }
        [Display(Name = "شناسنامه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string IdentityCardAddress { get; set; }
        [Display(Name = "کدملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string NationalId { get; set; }
        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayFormat(DataFormatString ="yyyy/mm/dd")]
        public DateTime BirthDate { get; set; }
    }
}
