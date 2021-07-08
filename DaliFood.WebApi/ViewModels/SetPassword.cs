﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.WebApi.ViewModels
{
    public class SetPassword
    {
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password",ErrorMessage = "رمز عبور وارده ناهماهنگ است")]
        public string ConfrimPassword { get; set; }
    }
}
