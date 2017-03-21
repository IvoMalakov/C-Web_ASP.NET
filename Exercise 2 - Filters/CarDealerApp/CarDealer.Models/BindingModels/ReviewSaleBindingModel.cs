using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Models.BindingModels
{
    public class ReviewSaleBindingModel
    {
        public int CustomerId { get; set; }

        public int CarId { get; set; }

        public double Discount { get; set; }
    }
}
