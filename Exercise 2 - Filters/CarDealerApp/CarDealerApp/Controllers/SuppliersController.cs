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
using CarDealerApp.Security;
using AuthenticationManager = CarDealerApp.Security.AuthenticationManager;

namespace CarDealerApp.Controllers
{
    public class SuppliersController : Controller
    {
        private CarDealerContext db = new CarDealerContext();
        private SuppliersService service = new SuppliersService(Data.Context);

        [Route("~/suppliers")]
        public ActionResult Index()
        {
            IEnumerable<Supplier> suppliers = this.db.Suppliers.ToList();
            return View(suppliers);
        }

        // GET: Suppliers
        [Route("~/suppliers/{type}")]
        public ActionResult Index(string type)
        {
            IEnumerable<Supplier> suppliers = null;

            if (type.ToLower() == "local")
            {
                suppliers = this.db.Suppliers.Where(s => s.IsImporter == false);
            }

            else if (type.ToLower() == "importers")
            {
               suppliers = this.db.Suppliers.Where(s => s.IsImporter);
            }

            else
            {
                suppliers = this.db.Suppliers;
            }

            return View(suppliers.ToList());
        }

        [HttpGet]
        [Route("~/suppliers/add")]
        public ActionResult Add()
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            return this.View();
        }

        [HttpPost]
        [Route("~/suppliers/add")]
        public ActionResult Add([Bind(Include = "Name, IsImporter")] AddSupplierBindingModel bindingModel)
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            if (ModelState.IsValid)
            {
                int userId =
                    this.db.Sessions.FirstOrDefault(s => s.IsActive && s.SessionId == httpCoockie.Value).UserId;

                this.service.AddSupplier(bindingModel, userId);
                return this.RedirectToAction("Index");
            }

            return this.View();
        }

        [HttpGet]
        [Route("~/suppliers/edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            EditSupplierViewModel supplierViewModel = this.service.GetSupplierToEdit(id);
            return this.View(supplierViewModel);
        }

        [HttpPost]
        [Route("~/suppliers/edit/{id:int}")]
        public ActionResult Edit([Bind(Include = "Id, Name, IsImporter")] EditSupplierBindingModel bindingModel)
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            if (ModelState.IsValid)
            {
                int userId =
                    this.db.Sessions.FirstOrDefault(s => s.IsActive && s.SessionId == httpCoockie.Value).UserId;

                this.service.EditSuppller(bindingModel, userId);
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Edit", "Suppliers", new {id = bindingModel.Id});
        }

        [HttpGet]
        [Route("~/suppliers/delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            DeleteSupplierViewModel viewModel = this.service.GetSupplierForDeletion(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Route("~/suppliers/delete/{id:int}")]
        public ActionResult Delete([Bind(Include = "Id")] DeleteSupplierBindingModel bindingModel)
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            if (this.ModelState.IsValid)
            {
                int userId =
                    this.db.Sessions.FirstOrDefault(s => s.IsActive && s.SessionId == httpCoockie.Value).UserId;

                this.service.DeleteSupplier(bindingModel, userId);
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Delete", "Suppliers", new {id = bindingModel.Id});
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
