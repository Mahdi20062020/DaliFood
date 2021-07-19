using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.WebApi.ViewModels
{
    public class CustomerComment
    {
       
        [Display(Name = "شناسه فروشگاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CustomerId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Family { get; set; }

        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Text { get; set; }


     

        public static implicit operator CustomerComment(Models.CustomerComment customerComment)
        {
      
            CustomerComment _customerComment = new();
           
            _customerComment.Name = customerComment.Name;
            _customerComment.Family = customerComment.Family;
            _customerComment.Text = customerComment.Text;
            _customerComment.CustomerId = customerComment.CustomerId;
          
      
            return _customerComment;
        }
    }
}
