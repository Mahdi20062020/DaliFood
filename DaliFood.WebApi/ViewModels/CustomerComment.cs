using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.WebApi.ViewModels
{
    public class CustomerComment
    {
        [Display(Name = "شناسه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Id { get; set; }

        [Display(Name = "شناسه فروشگاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CustomerId { get; set; }

        [Display(Name = "نام فروشگاه")]
        public string CustomerName { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Family { get; set; }

        [Display(Name = "شناسه کاربر")]
        public string UserId { get; set; }

        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Text { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Status { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime CreateDate { get; set; }

        public static implicit operator CustomerComment(Models.CustomerComment customerComment)
        {
      
            CustomerComment _customerComment = new();
            _customerComment.Id = customerComment.Id;
            _customerComment.UserId = customerComment.UserId;
            _customerComment.Status = customerComment.Status;
            _customerComment.Name = customerComment.Name;
            _customerComment.Family = customerComment.Family;
            _customerComment.Text = customerComment.Text;
            _customerComment.CustomerName = customerComment.Customer.Name;
            _customerComment.CustomerId = customerComment.CustomerId;
            _customerComment.CreateDate = customerComment.CreateDate;
      
            return _customerComment;
        }
    }
}
