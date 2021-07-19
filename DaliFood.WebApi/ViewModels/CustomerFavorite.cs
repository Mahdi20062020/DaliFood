using DaliFood.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.WebApi.ViewModels
{
    public class CustomerFavorite
    {
        [Display(Name = "فروشگاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CustomerId { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool Status { get; set; }
        public static implicit operator Favorite(CustomerFavorite _CustomerFavorite)
        {
            Favorite favorite = new();
            favorite.CustomerId = _CustomerFavorite.CustomerId;
            favorite.CreateDate = DateTime.Now;
            return favorite;
        }
    }
}
