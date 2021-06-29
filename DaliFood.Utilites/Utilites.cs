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
    }
}
