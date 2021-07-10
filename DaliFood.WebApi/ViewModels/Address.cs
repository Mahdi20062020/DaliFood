using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.WebApi.ViewModels
{
    public class Address
    {
        [Display(Name = "آدرس متنی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TextAddress { get; set; }

        [Display(Name = "عرص جغرافیایی")]
        public string Latitude { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public string Longitude { get; set; }
    }
}
