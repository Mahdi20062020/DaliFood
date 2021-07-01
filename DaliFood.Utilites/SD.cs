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
        public const string CustomerOwnerRole = "CustomerOwner";
        public const string ProductCustomerRole = "ProductCustomer";
        public const string NormalUserRole = "NormalUser";
        public const string CheckingOrderStatus = "درحال بررسی";
        public const string BakingOrderStatus = "درحال پخت";
        public const string SendingOrderStatus = "درحال ارسال";
        public const string ReceivingOrderStatus = "درحال دریافت";
        public readonly static PhotoFor PhotoForCustomersProducts = new PhotoFor() {Name="Products",PhotoSavedAddress="Images/Products",CreateDate=DateTime.Now };
        public readonly static PhotoFor PhotoForProductCategories = new PhotoFor() {Name= "ProductCategories", PhotoSavedAddress= "Images/ProductCategories", CreateDate = DateTime.Now };
        public readonly static PhotoFor PhotoForCustomers = new PhotoFor() {Name= "Customers", PhotoSavedAddress= "Images/Customers", CreateDate = DateTime.Now };
        public static PhotoFor GetPart(UnitOfWork unitofwork, string Name)
        {
            var part = unitofwork.PhotoForRepository.GetAll(where: p => p.Name == Name).FirstOrDefault();
            return part;
        }
    }
}
