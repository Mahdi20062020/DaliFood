using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Models.Identity
{
    public class ApplicationNormalUser:ApplicationUserDetail
    {
        [StringLength(300)]
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Address { get; set; }

        [Display(Name = "کیف پول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Wallet { get; set; }
    }
}
