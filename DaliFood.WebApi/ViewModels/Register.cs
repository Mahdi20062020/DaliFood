using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.WebApi.ViewModels
{
    public class Register
    {
        //[Display(Name = "شماره تلفن")]
        //public string PhoneNumber { get; set; }
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[StringLength(100, ErrorMessage = "{0} باید کمتر از {2} و بیشتر از {1} باشد", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "رمز عبور")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "تکرار رمز عبور")]
        //[Compare("Password", ErrorMessage = "رمز عبور وارده ناهماهنگ است")]
        public string ConfirmPassword { get; set; }
        public string Password { get; set; }
        public string PasswordConfrim { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Phonenumber { get; set; }
    }
}
