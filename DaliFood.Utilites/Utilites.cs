using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Utilites
{
    public static class Utilites
    {
        public static int PriceWithDicount(int Price,int Discount)
        {
            return Price-((Price * Discount) / 100);
        }
        public static void DatabaseConfigration(UnitOfWork _unitOfWork)
        {
            UnitOfWork unitOfWork = _unitOfWork;
            var PhotoFors = unitOfWork.PhotoForRepository.GetAll();
            if (!PhotoFors.Any(p=>p.Name== SD.PhotoForProductCategories.Name))
                unitOfWork.PhotoForRepository.Create(SD.PhotoForProductCategories);

            if (!PhotoFors.Any(p => p.Name == SD.PhotoForProducts.Name))
                unitOfWork.PhotoForRepository.Create(SD.PhotoForProducts);

            if (!PhotoFors.Any(p => p.Name == SD.PhotoForCustomers.Name))
                unitOfWork.PhotoForRepository.Create(SD.PhotoForCustomers);
           
            if (PhotoFors.Count()<3)
                unitOfWork.PhotoForRepository.Save();
            
        }
    }
}
