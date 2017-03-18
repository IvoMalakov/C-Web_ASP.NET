using System;
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
    public class SuppliersService : Service
    {
        public SuppliersService(CarDealerContext context) : base(context)
        {
        }

        public void AddSupplier(AddSupplierBindingModel bindingModel)
        {
            Supplier supplier = new Supplier()
            {
                Name = bindingModel.Name,
                IsImporter = bindingModel.IsImporter
            };

            this.Context.Suppliers.Add(supplier);
            this.Context.SaveChanges();
        }

        public EditSupplierViewModel GetSupplierToEdit(int id)
        {
            Supplier supplier = this.Context.Suppliers.Find(id);

            EditSupplierViewModel viewModel = new EditSupplierViewModel()
            {
                Id = supplier.Id,
                Name = supplier.Name,
                IsImporter = supplier.IsImporter
            };

            return viewModel;
        }

        public void EditSuppller(EditSupplierBindingModel bindingModel)
        {
            Supplier supplier = this.Context.Suppliers.Find(bindingModel.Id);
            supplier.Name = bindingModel.Name;
            supplier.IsImporter = bindingModel.IsImporter;
            this.Context.SaveChanges();
        }

        public DeleteSupplierViewModel GetSupplierForDeletion(int id)
        {
            Supplier supplier = this.Context.Suppliers.Find(id);

            DeleteSupplierViewModel viewModel = new DeleteSupplierViewModel()
            {
                Id = supplier.Id,
                Name = supplier.Name
            };

            return viewModel;
        }

        public void DeleteSupplier(DeleteSupplierBindingModel bindingModel)
        {
            Supplier supplier = this.Context.Suppliers.Find(bindingModel.Id);
            this.Context.Suppliers.Remove(supplier);
            this.Context.SaveChanges();
        }
    }
}
