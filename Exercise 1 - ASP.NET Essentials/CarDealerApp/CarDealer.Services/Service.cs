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
            this.Context = context;
            ConfigureAutommaper();
        }

        public CarDealerContext Context { get; set; }

        private static void ConfigureAutommaper()
        {
            Mapper.Initialize(expression => expression.CreateMap<AddCustomerBindingModel, Customer>());
            Mapper.Initialize(expression => expression.CreateMap<EditCustomerBindingModel, Customer>());
            Mapper.Initialize(expression => expression.CreateMap<Customer, EditCustomerBindingModel>());
        }
    }
}
