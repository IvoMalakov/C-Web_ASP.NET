using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Models.BindingModels
{
    public class AddSaleBindingModel
    {
        public int Customer { get; set; }

        public int Car { get; set; }

        public int Discount { get; set; }
    }
}
