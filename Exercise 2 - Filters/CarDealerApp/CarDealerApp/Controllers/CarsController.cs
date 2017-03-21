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
    public class CarsController : Controller
    {
        private CarDealerContext db = new CarDealerContext();
        private CarsService service = new CarsService(Data.Context);

        // GET: Cars

        [Route("~/cars/all")]
        public ActionResult Index()
        {
            IEnumerable<Car> cars = this.db.Cars
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToList();

            return View(cars);
        }
        
        [Route("~/cars/{make:alpha}")]
        public ActionResult Index(string make)
        {
            IEnumerable<Car> cars = this.db.Cars
                .Where(c => c.Make == make)
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToList();

            return View(cars);
        }

        [Route("~/cars/parts")]
        public ActionResult Parts()
        {
            Car car = this.db.Cars.Find(1);
            return View(car);
        }

        [Route("~/cars/{id:int}/parts")]
        public ActionResult Parts(int id)
        {
            Car car = this.db.Cars.Find(id);
            return View(car);
        }

        [Route("~/cars/add")]
        public ActionResult Add()
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            IEnumerable<PartForACarViewModel> partsForACar = this.service.GetPartsForCars();
            return this.View(partsForACar);
        }

        [HttpPost]
        [Route("~/cars/add")]
        public ActionResult Add([Bind(Include = "Make, Model, TravelledDistance, Part1, Part2, Part3")] AddCarBindingModel bindingModel)
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

                this.service.AddCarToDb(bindingModel, userId);
                return this.RedirectToAction("Index");
            }

            IEnumerable<PartForACarViewModel> partsForACar = this.service.GetPartsForCars();
            return this.View(partsForACar);
        }


        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "OutOfRangeError")]
        [HandleError(ExceptionType = typeof(InvalidOperationException), View = "InvalidOperationError")]
        [Route("~/Cars/Details/{id:int}")]
        public ActionResult Details(int id)
        {
            ViewData["car"] = this.service.GetCarDetails(id);

            return this.View();
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
