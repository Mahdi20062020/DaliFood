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
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string NationalCardSaveAddress { get; set; }
        [Display(Name = "شناسنامه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string IdentityCardAddress { get; set; }
        [Display(Name = "کدملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string NationalId { get; set; }

        [Display(Name = "عرض جغرافیایی")]
        public string Latitude { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public string Longitude { get; set; }

        [Display(Name = "شماره تلفن ثابت 1")]
        public string TelePhonenumber1 { get; set; }

        [Display(Name = "شماره تلفن ثابت 2")]
        public string TelePhonenumber2 { get; set; }
        [Display(Name = "شماره همراه")]
        public string Phonenumber { get; set; }

        [Display(Name = "نام صاحب فروشگاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string OwnerName { get; set; }

        [Display(Name = "نام خانوادگی صاحب فروشگاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string OwnerFamily { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayFormat(DataFormatString ="yyyy/mm/dd")]
        public DateTime BirthDate { get; set; }
    }
}
