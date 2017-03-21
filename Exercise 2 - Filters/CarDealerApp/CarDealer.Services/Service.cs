using AutoMapper;
using CarDealer.Data;
using CarDealer.Models.BindingModels;
using CarDealer.Models.EntityModels;

namespace CarDealer.Services
{
    public abstract class Service
    {
        protected Service(CarDealerContext context)
        {
            this.Context = context;        }

        public CarDealerContext Context { get; set; }
    }
}
