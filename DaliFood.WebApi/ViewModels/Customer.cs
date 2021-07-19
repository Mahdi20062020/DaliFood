using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.WebApi.ViewModels
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
        public string Type { get; set; }

        [Display(Name = "عرض جغرافیایی")]
        public string Latitude { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public string Longitude { get; set; }

        [Display(Name = "شهر")]
        public string City { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Address { get; set; }
        [Display(Name = "هزینه ارسال")]
        public int SendingPrice { get; set; }

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

        [Display(Name = "علاقه‌مندی")]
        public bool IsInMyFavorite { get; set; }

    }

}
