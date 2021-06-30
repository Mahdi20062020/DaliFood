using DaliFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public static void Configure(this UnitOfWork unitOfWork)
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
           
        }
    }
}
