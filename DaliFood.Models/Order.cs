using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Models
{
    public class Order:TransactionItem
    {
      
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int AddressId { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool Status { get; set; }

        public virtual IEnumerable<OrderItem> OrderItem { get; set; }
        public virtual Transaction Transaction { get; set; }
      
        public Order()
        {
            
        }
    }
}
