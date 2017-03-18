using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Models.ViewModels.AddSaleViewModels
{
    public class AddSaleViewModel
    {
        public AddSaleViewModel()
        {
            this.Cars = new HashSet<CarSaleViewModel>();
            this.Customers = new HashSet<CustomerSaleViewModel>();
        }

        public virtual ICollection<CarSaleViewModel> Cars { get; set; }

        public virtual ICollection<CustomerSaleViewModel> Customers { get; set; }
    }
}
