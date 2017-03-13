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

        public void AddCustomer(AddCustomerBindingModel bindingModel)
        {
            Customer customer = new Customer()
            {
                Name = bindingModel.Name,
                BirthDate = bindingModel.BirthDate,
                IsYoungDriver = DateTime.Now.Year - bindingModel.BirthDate.Year < 21

            };

            this.Context.Customers.Add(customer);
            this.Context.SaveChanges();
        }

        public EditCustomerBindingModel GetEditedCustomer()
        {
            Customer customer = this.Context.Customers.Find(1);
            EditCustomerBindingModel bindingModel = new EditCustomerBindingModel()
            {
                Id = 1,
                Name = customer.Name,
                BirthDate = customer.BirthDate
            };

            return bindingModel;

        }

        public EditCustomerBindingModel GetEditedCustomer(int id)
        {
            Customer customer = this.Context.Customers.Find(id);
            EditCustomerBindingModel bindingModel = new EditCustomerBindingModel()
            {
                Id = id,
                Name = customer.Name,
                BirthDate = customer.BirthDate
            };
            return bindingModel;
        }

        public void EditCustomer(EditCustomerBindingModel bindingModel)
        {
            Customer customer = this.Context.Customers.Find(bindingModel.Id);
            customer.Name = bindingModel.Name;
            customer.BirthDate = bindingModel.BirthDate;
            this.Context.SaveChanges();
        }

        public void DeleteCustomer()
        {
            Customer customer = this.Context.Customers.Find(1);
            this.Context.Customers.Remove(customer);
            this.Context.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            Customer customer = this.Context.Customers.Find(id);
            this.Context.Customers.Remove(customer);
            this.Context.SaveChanges();
        }
    }
}
