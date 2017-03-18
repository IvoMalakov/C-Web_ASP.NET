using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealer.Data;
using CarDealer.Models.BindingModels;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;
using CarDealer.Models.ViewModels.AddSaleViewModels;

namespace CarDealer.Services
{
    public class SalesService : Service
    {
        public SalesService(CarDealerContext context) : base(context)
        {
        }

        public AddSaleViewModel GetAllSalesDetails()
        {
            AddSaleViewModel addSaleViewModel = new AddSaleViewModel();

            var allCustomers = this.Context.Customers.ToList();
            var allCars = this.Context.Cars.ToList();

            foreach (var customer in allCustomers)
            {
                addSaleViewModel.Customers.Add(new CustomerSaleViewModel()
                {
                    Id = customer.Id,
                    Name = customer.Name
                });
            }

            foreach (var car in allCars)
            {
                addSaleViewModel.Cars.Add(new CarSaleViewModel()
                {
                    Id = car.Id,
                    Name = $"{car.Make} {car.Model}"
                });
            }

            return addSaleViewModel;
        }

        public ReviewSaleViewModel ReviewFinalOffer(AddSaleBindingModel model)
        {
            Car carToSale = this.Context.Cars.Find(model.Car);
            Customer customer = this.Context.Customers.Find(model.Customer);

            string discountPercent = $"{model.Discount}%";

            if (customer.IsYoungDriver)
            {
                discountPercent += " (+5%)";
                model.Discount += 5;
            }

            double? discountFinal = model.Discount / 100.00;
            double? price = this.Context.Parts.Sum(p => p.Price);
            double? finalPrice = price - (price * discountFinal);

            ReviewSaleViewModel viewModel = new ReviewSaleViewModel()
            {
                CarId = carToSale.Id,
                CarName = $"{carToSale.Make} {carToSale.Model}",
                CustomerId = customer.Id,
                CustomerName = customer.Name,
                DiscountPercent = discountPercent,
                FinalDiscountPercent = discountFinal,
                Price = price,
                FinalPrice = finalPrice
            };

            return viewModel;
        }

        public void FinalizeSale(ReviewSaleBindingModel bindingModel)
        {
            Sale sale = new Sale()
            {
                Car = this.Context.Cars.Find(bindingModel.CarId),
                Customer = this.Context.Customers.Find(bindingModel.CustomerId),
                Discount = bindingModel.Discount
            };

            this.Context.Sales.Add(sale);
            this.Context.SaveChanges();
        }
    }
}
