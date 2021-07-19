using DaliFood_AdminPanelML.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DaliFood.Utilites
{
    public static class Utilites
    {
        public static string DoHelper(ModelOutput result)
        {
            string url = "";
            //if (result.Score.Any(p=>p <))
            //{

            //}
            switch (result.Prediction)
            {
                case "UserRegister":
                    url = "/Identity/Account/register";
                    break;
                case "CustomerRegister":
                    url = "/customer/Account/register";
                    break;
                case "Login":
                    url = "/Identity/Account/login";
                    break;
                case "CustomerProductAdd":
                    url = "/CustomersProduct/upsert";
                    break;
                case "ProductAdd":
                    url = "/Product/upsert";
                    break;
                case "CustomerTypeAdd":
                    url = "/CustomerType/upsert";
                    break;
                case "ProductTypeAdd":
                    url = "/ProductCategorie/upsert";
                    break;
                case "GetOrderItem":
                    url = "/Order/item/index";
                    break;
                case "GetOrder":
                    url = "/Order/index";
                    break;
                case "GetCustomerProduct":
                    url = "/CustomersProduct/index";
                    break;
                default:
                    return "404";  
            }
            return url;
        }
        public static bool IsPhoneNumber(string number)
        {
            var result = Regex.Match(number, @"(^(09|\+989)[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$)|(^(09|9)[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$)").Success;
            return result;
        }

    }
}
