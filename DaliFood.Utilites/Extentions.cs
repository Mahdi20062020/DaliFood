using DaliFood.Models;
using DaliFood.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DaliFood.Utilites
{
    public static class Extentions
    {
        public static int UseDiscount(this int Price,int Discount)
        {
            double _price = (double)Price;
            double _discount = (double)Discount;
            return (int)(_price - ((_price * _discount) / 100));
        }     
        public async static Task Configure(this UnitOfWork unitOfWork,RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            var PhotoFors = unitOfWork.PhotoForRepository.GetAll();
            if (!PhotoFors.Any(p => p.Name == SD.PhotoForProductCategories.Name))
                unitOfWork.PhotoForRepository.Create(SD.PhotoForProductCategories);

            if (!PhotoFors.Any(p => p.Name == SD.PhotoForCustomersProducts.Name))
                unitOfWork.PhotoForRepository.Create(SD.PhotoForCustomersProducts);

            if (!PhotoFors.Any(p => p.Name == SD.PhotoForCustomersProducts.Name))
                unitOfWork.PhotoForRepository.Create(SD.PhotoForCustomersProducts);

            if (PhotoFors.Count() < 3)
                unitOfWork.PhotoForRepository.Save();

            if (!await roleManager.RoleExistsAsync(SD.AdminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(SD.AdminRole));
            }
            if (!await roleManager.RoleExistsAsync(SD.ProductEditorRole))
            {
                await roleManager.CreateAsync(new IdentityRole(SD.ProductEditorRole));
            }
            if (!await roleManager.RoleExistsAsync(SD.CustomerOwnerRole))
            {
                await roleManager.CreateAsync(new IdentityRole(SD.CustomerOwnerRole));
            }
            if (!await roleManager.RoleExistsAsync(SD.NormalUserRole))
            {
                await roleManager.CreateAsync(new IdentityRole(SD.NormalUserRole));
            }
            var user = new ApplicationUser()
            {
                Name = "Admin",
                UserName= "hoseinpoor2006@gmail.com",
                Email = "hoseinpoor2006@gmail.com",
                Family = "Admin",
                EmailConfirmed = true,
                PhoneNumber = "09225457958"
            };
          await  userManager.CreateAsync(user, "@@2021Admin@@2021Admin@@1");
            await userManager.AddToRoleAsync(user, SD.AdminRole);


        }

        public async static Task SendConfirmationEmail(this IEmailSender _emailSender,UserManager<ApplicationUser> _userManager,ApplicationUser user,IUrlHelper Url, HttpRequest Request,string returnUrl="")
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "ایمیل فعال سازی حساب دالی فود",
                $"برای فعال سازی حساب خود <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>اینجا</a> کلیک کنید.");
        } 
        public async static Task SendResetPassword(this IEmailSender _emailSender,UserManager<ApplicationUser> _userManager,ApplicationUser user,IUrlHelper Url, HttpRequest Request,string returnUrl="")
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { area = "Identity", code },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(
                user.Email,
                "فراموشی رمز عبور",
                $"برای تنظیم مجدد رمز عبور خود <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>اینجا</a>کلیک کنید.");
        } public async static Task SendConfirmationEmailChange(this IEmailSender _emailSender,UserManager<ApplicationUser> _userManager,ApplicationUser user,IUrlHelper Url, HttpRequest Request,string NewEmail,string returnUrl="")
        {
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateChangeEmailTokenAsync(user, NewEmail);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmailChange",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, email = NewEmail, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(user.Email, "ایمیل فعال سازی حساب دالی فود",
           $"برای فعال سازی حساب خود <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>اینجا</a> کلیک کنید.");
        }


    }
}
