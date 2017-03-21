using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealer.Data;
using CarDealer.Models.BindingModels;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;

namespace CarDealer.Services
{
    public class PartsService : Service
    {
        public PartsService(CarDealerContext context) : base(context)
        {
        }

        public IEnumerable<AddPartsSupplierViewModel> GetAddViewModels()
        {
            IEnumerable<AddPartsSupplierViewModel> viewModels =
                this.Context.Suppliers.Select(advm => new AddPartsSupplierViewModel()
                {
                    Id = advm.Id,
                    Name = advm.Name
                });

            return viewModels;
        }

        public void AddPartToDb(AddPartBindingModel bindingmodel)
        {
            Supplier supplier = this.Context.Suppliers.FirstOrDefault(s => s.Id == bindingmodel.SupplierId);
            Part part = new Part()
            {
                Name = bindingmodel.Name,
                Price = bindingmodel.Price,
                Quantity = bindingmodel.Quantity,
                Supplier = supplier
            };

            if (part.Price > 0 && part.Quantity > 0)
            {
                this.Context.Parts.Add(part);
                this.Context.SaveChanges();
            }
        }

        public IEnumerable<PartViewModel> GetAllParts()
        {
            IEnumerable<PartViewModel> partsViewModels = this.Context.Parts.Select(pvm => new PartViewModel()
            {
                Id = pvm.Id,
                Name = pvm.Name,
                Price = pvm.Price,
                Quantity = pvm.Quantity,
                Supplier = pvm.Supplier.Name
            });

            return partsViewModels.ToList();
        }

        public DeletePartViewModel GetPartFromDeletion()
        {
            DeletePartViewModel partViewModel = this.Context.Parts.Select(partvm => new DeletePartViewModel()
            {
                Id = partvm.Id,
                Name = partvm.Name
            })
                .FirstOrDefault(p => p.Id == 1);

            return partViewModel;
        }

        public DeletePartViewModel GetPartFromDeletion(int id)
        {
            DeletePartViewModel partViewModel = this.Context.Parts.Select(partvm => new DeletePartViewModel()
            {
                Id = partvm.Id,
                Name = partvm.Name
            })
               .FirstOrDefault(p => p.Id == id);

            return partViewModel;
        }

        public void DeletePartById(int id)
        {
            Part part = this.Context.Parts.Find(id);
            this.Context.Parts.Remove(part);
            this.Context.SaveChanges();
        }

        public EditPartViewModel GetPartForEdit(int id)
        {
            Part part = this.Context.Parts.Find(id);
            EditPartViewModel partViewModel = new EditPartViewModel()
            {
                Id = part.Id,
                Name = part.Name,
                Price = part.Price,
                Quantity = part.Quantity
            };

            return partViewModel;
        }

        public void EditPartById(EditPartBindingModel bindingModel)
        {
            Part part = this.Context.Parts.Find(bindingModel.Id);

            if (part != null && bindingModel.Price > 0 && bindingModel.Quantity > 0)
            {
                part.Quantity = bindingModel.Quantity;
                part.Price = bindingModel.Price;
                this.Context.SaveChanges();
            }
        }
    }
}
