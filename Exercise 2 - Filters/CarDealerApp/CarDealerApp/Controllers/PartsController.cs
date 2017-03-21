using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDealer.Data;
using CarDealer.Models.BindingModels;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;
using CarDealer.Services;

namespace CarDealerApp.Controllers
{
    public class PartsController : Controller
    {
        private CarDealerContext db = new CarDealerContext();
        private PartsService service = new PartsService(Data.Context);

        [Route("~/Parts/All")]
        public ActionResult All()
        {
            IEnumerable<PartViewModel> partViewModels = this.service.GetAllParts();
            return this.View(partViewModels);
        }

        [Route("~/Parts/Add")]
        public ActionResult Add()
        {
            IEnumerable<AddPartsSupplierViewModel> viewModels = this.service.GetAddViewModels();
            return View(viewModels);
        }

        [HttpPost]
        [Route("~/Parts/Add")]
        public ActionResult Add([Bind(Include = "Name, Price, Quantity, SupplierId")] AddPartBindingModel bindingModel)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddPartToDb(bindingModel);
                return this.RedirectToAction("All");
            }

            IEnumerable<AddPartsSupplierViewModel> viewModels = this.service.GetAddViewModels();
            return View(viewModels);
        }

        [Route("~/Parts/Delete")]
        public ActionResult Delete()
        {
            DeletePartViewModel partViewModel = this.service.GetPartFromDeletion();
            return View(partViewModel);
        }

        [Route("~/Parts/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            DeletePartViewModel partViewModel = this.service.GetPartFromDeletion(id);
            return View(partViewModel);
        }

        [HttpPost]
        [Route("~/Parts/Delete/{id:int}")]
        public ActionResult Delete([Bind(Include = "Id")] DeletePartBindingModel bindingModel)
        {
            this.service.DeletePartById(bindingModel.Id);
            return this.RedirectToAction("All");
        }

        [Route("~/Parts/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            EditPartViewModel editPartViewModel = this.service.GetPartForEdit(id);
            return this.View(editPartViewModel);
        }

        [HttpPost]
        [Route("~/Parts/Edit/{id:int}")]
        public ActionResult Edit([Bind(Include = "Id, Price, Quantity")] EditPartBindingModel bindingModel)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditPartById(bindingModel);
                return this.RedirectToAction("All");
            }

            EditPartViewModel editPartViewModel = this.service.GetPartForEdit(bindingModel.Id);
            return this.View(editPartViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
