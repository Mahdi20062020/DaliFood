using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Utilites
{
    static class Extentions
    {
        public static int UseDiscount(this int Price,int Discount)
        {
            return (Price * Discount) / 100;
        }
       
    }
}
