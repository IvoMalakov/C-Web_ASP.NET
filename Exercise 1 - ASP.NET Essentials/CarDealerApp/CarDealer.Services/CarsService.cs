using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealer.Data;
using CarDealer.Models.BindingModels;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;

namespace CarDealer.Services
{
    public class CarsService : Service
    {
        private LogsService logsService;

        public CarsService(CarDealerContext context) : base(context)
        {
            this.logsService = new LogsService(Data.Data.Context);
        }

        public void AddCarToDb(AddCarBindingModel bindingModel, int userId)
        {
            if (bindingModel.TravelledDistance >= 0)
            {
                Car car = new Car()
                {
                    Make = bindingModel.Make,
                    Model = bindingModel.Model,
                    TravelledDistance = bindingModel.TravelledDistance
                };

                Part part1 = this.Context.Parts.Find(bindingModel.Part1);
                Part part2 = this.Context.Parts.Find(bindingModel.Part2);
                Part part3 = this.Context.Parts.Find(bindingModel.Part3);

                if (part1 != null)
                {
                    car.Parts.Add(part1);
                }

                if (part2 != null)
                {
                    car.Parts.Add(part2);
                }

                if (part3 != null)
                {
                    car.Parts.Add(part3);
                }

                this.Context.Cars.Add(car);
                this.logsService.GenerateLog(Operation.Add, ModifiedTable.Car, userId);
                this.Context.SaveChanges();
            }
        }

        public IEnumerable<PartForACarViewModel> GetPartsForCars()
        {
            IList<PartForACarViewModel> partsViewModels = new List<PartForACarViewModel>();

            IEnumerable<Part> parts = this.Context.Parts.ToList();

            foreach (Part part in parts)
            {
                PartForACarViewModel viewModel = new PartForACarViewModel()
                {
                    Id = part.Id,
                    Name = part.Name
                };

                partsViewModels.Add(viewModel);
            }

            return partsViewModels;
        }
    }
}
