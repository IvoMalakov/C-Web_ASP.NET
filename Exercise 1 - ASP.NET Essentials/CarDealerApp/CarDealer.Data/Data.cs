using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Data
{
    public class Data
    {
        private static CarDealerContext context;

        public static CarDealerContext Context => context ?? (context = new CarDealerContext());
    }
}
