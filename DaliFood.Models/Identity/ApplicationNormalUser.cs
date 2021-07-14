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
   
        [Display(Name = "کیف پول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Wallet { get; set; }
        public virtual IEnumerable<Address> Addresses { get; set; }

    }
}
