using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Models.BindingModels
{
    public class EditPartBindingModel
    {
        public int Id { get; set; }

        public double? Price { get; set; }

        public int Quantity { get; set; }
    }
}
