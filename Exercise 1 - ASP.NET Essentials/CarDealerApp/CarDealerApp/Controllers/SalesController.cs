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
using CarDealer.Models.ViewModels.AddSaleViewModels;
using CarDealer.Services;
using CarDealerApp.Security;
using AuthenticationManager = CarDealerApp.Security.AuthenticationManager;

namespace CarDealerApp.Controllers
{
    public class SalesController : Controller
    {
        private CarDealerContext db = new CarDealerContext();
        private SalesService service = new SalesService(Data.Context);

        // GET: Sales
        [Route("~/Sales")]
        public ActionResult Index()
        {
            IEnumerable<Sale> sales = this.db.Sales.ToList();
            return View(sales);
        }

        [Route("~/Sales/{id:int}")]
        public ActionResult Details(int id)
        {
            Sale sale = this.db.Sales.Find(id);
            return View(sale);
        }

        [Route("~/Sales/Discounted")]
        public ActionResult Discounted()
        {
            IEnumerable<Sale> discountedSales = this.db.Sales.Where(s => s.Discount != 0);
            return View(discountedSales);
        }

        [Route("~/Sales/Discounted/{percent:int}")]
        public ActionResult Discounted(double percent)
        {
            IEnumerable<Sale> discountedSales = this.db.Sales.Where(s => s.Discount * 100 == percent);
            return View(discountedSales);
        }

        [HttpGet]
        [Route("~/sales/add")]
        public ActionResult Add()
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            AddSaleViewModel addSaleViewModel = this.service.GetAllSalesDetails();
            return this.View(addSaleViewModel);
        }

        [HttpPost]
        [Route("~/sales/add")]
        public ActionResult Add([Bind(Include = "Customer, Car, Discount")] AddSaleBindingModel bindingModel)
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            if (this.db.Cars.Find(bindingModel.Car) != null && this.db.Customers.Find(bindingModel.Customer) != null)
            {
                return this.RedirectToAction("Review", new
                {
                    Customer = bindingModel.Customer,
                    Car = bindingModel.Car,
                    Discount = bindingModel.Discount
                });
            }

            return this.View();
        }

        [HttpGet]
        [Route("~/sales/review")]
        public ActionResult Review(AddSaleBindingModel bindingModel)
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            var reviewOfferModel = this.service.ReviewFinalOffer(bindingModel);
            return this.View(reviewOfferModel);
        }

        [HttpPost]
        [Route("~/sales/review")]
        public ActionResult Rview([Bind(Include = "CustomerId, CarId, Discount")] ReviewSaleBindingModel bindingModel)
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            if (ModelState.IsValid)
            {
                this.service.FinalizeSale(bindingModel);
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Index", "Sales");
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
