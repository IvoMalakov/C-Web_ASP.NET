using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Models.BindingModels
{
    public class AddPartBindingModel
    {
        public string Name { get; set; }

        public double? Price { get; set; }

        public int Quantity { get; set; }

        public int SupplierId { get; set; }
    }
}
