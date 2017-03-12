using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDealer.Data;
using CarDealer.Models.BindingModels;
using CarDealer.Models.EntityModels;

namespace CarDealer.Services
{
    public class CustomerService : Service
    {
        public CustomerService(CarDealerContext context) : base(context)
        {
        }

        public void AddCustomer(AddCustomerBindingModel model)
        {
            Customer customer = Mapper.Map<AddCustomerBindingModel, Customer>(model);

            customer.IsYoungDriver = DateTime.Now.Year - customer.BirthDate.Year < 21;

            this.Context.Customers.Add(customer);
            this.Context.SaveChanges();
        }

        public EditCustomerBindingModel GetEditedCustomer()
        {
            Customer customer = this.Context.Customers.Find(1);
            EditCustomerBindingModel model = Mapper.Map<Customer, EditCustomerBindingModel>(customer);
            return model;

        }

        public EditCustomerBindingModel GetEditedCustomer(int id)
        {
            Customer customer = this.Context.Customers.Find(id);
            EditCustomerBindingModel model = Mapper.Map<Customer, EditCustomerBindingModel>(customer);
            return model;
        }

        public void EditCustomer(EditCustomerBindingModel model)
        {
            Customer customer = Mapper.Map<EditCustomerBindingModel, Customer>(model);
            this.Context.Customers.AddOrUpdate(customer);
            this.Context.SaveChanges();
        }
    }
}
