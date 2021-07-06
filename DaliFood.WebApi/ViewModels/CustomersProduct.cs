using DaliFood.Models;
using DaliFood.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.WebApi.ViewModels
{
    public class CustomersProduct
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public int Price { get; set; }
        public int PriceWithDiscount { get; set; }
        public string ImageAddress { get; set; }

        public static implicit operator CustomersProduct(Models.CustomersProduct _customersproduct) 
        {
            CustomersProduct customersProduct = new();
            customersProduct.Id = _customersproduct.Id;
            customersProduct.ProductName = _customersproduct.Product.Name;
            customersProduct.ProductType = _customersproduct.Product.ProductCategorie.Name;
            customersProduct.ProductTypeId = _customersproduct.Product.CategorieId;
            customersProduct.Price = _customersproduct.Price;
            customersProduct.CustomerName = _customersproduct.Customer.Name;
            customersProduct.Description = _customersproduct.Description;
            if (_customersproduct.Discount!=null)
            {
                customersProduct.PriceWithDiscount = customersProduct.Price.UseDiscount(_customersproduct.Discount.DiscountRate);
            }
            return customersProduct;
        }
      
    }
}
