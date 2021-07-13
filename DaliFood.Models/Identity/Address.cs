using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Models.Identity
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserId { get; set; }

        [Display(Name = "آدرس متنی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TextAddress { get; set; }

        [Display(Name = "عرض جغرافیایی")]
        public string Latitude { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public string Longitude { get; set; }
    }
}
