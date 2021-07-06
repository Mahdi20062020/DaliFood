using DaliFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.Utilites
{
    public static class SD
    {
        public const string AdminRole = "Admin";
        public const string AdminPolicy = "Admin";
        public const string CustomerOwnerRole = "CustomerOwner";
        public const string CustomerOwnerPolicy = "CustomerOwner";
        public const string CustomerPolicy = "Customer";
        public const string ProductEditorPolicy = "ProductEditor";
        public const string ProductEditorRole = "ProductEditor";
        public const string CustomerId = "CustomerId";
        public const string AdminCustomerId = "all";
        public const string CheckingOrderStatus = "درحال بررسی";
        public const string BakingOrderStatus = "درحال پخت";
        public const string SendingOrderStatus = "درحال ارسال";
        public const string ReceivingOrderStatus = "درحال دریافت";
        public readonly static PhotoFor PhotoForCustomersProducts = new() {Name="Products",PhotoSavedAddress="Images\\Products",CreateDate=DateTime.Now };
        public readonly static PhotoFor PhotoForProductCategories = new() {Name= "ProductCategories", PhotoSavedAddress= "Images\\ProductCategories", CreateDate = DateTime.Now };
        public readonly static PhotoFor PhotoForCustomers = new() {Name= "Customers", PhotoSavedAddress= "Images\\Customers", CreateDate = DateTime.Now };
        public static PhotoFor GetPart(UnitOfWork unitofwork, string Name)
        {
            var part = unitofwork.PhotoForRepository.GetAll(where: p => p.Name == Name).FirstOrDefault();
            return part;
        }
    }
}
