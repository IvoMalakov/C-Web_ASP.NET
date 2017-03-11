using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDealer.Data;
using CarDealer.Models;

namespace CarDealerApp.Controllers
{
    public class CustomersController : Controller
    {
        private CarDealerContext db = new CarDealerContext();

        // GET: Customers

        [Route("~/customers/all")]
        public ActionResult Index()
        {
            IEnumerable<Customer> customers = this.db.Customers.ToList();
            return View(customers);
        }

        [Route("~/customers/all/{sorthType}")]
        public ActionResult Index(string sorthType)
        {
            IEnumerable<Customer> orderedCustomers = null;

            if (sorthType == "ascending")
            {
                orderedCustomers = this.db.Customers.OrderBy(c => c.BirthDate).ThenBy(c => c.IsYoungDriver);
            }

            else if (sorthType == "descending")
            {
                orderedCustomers = this.db.Customers.OrderByDescending(c => c.BirthDate).ThenBy(c => c.IsYoungDriver);
            }

            else
            {
                orderedCustomers = this.db.Customers;
            }

            return View(orderedCustomers.ToList());
        }

        [Route("~/customers")]
        public ActionResult TotalSales()
        {
            Customer customer = this.db.Customers.Find(1);
            return View(customer);
        }

        [Route("~/customers/{id:int}")]
        public ActionResult TotalSales(int id)
        {
            Customer customer = this.db.Customers.Find(id);
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,BirthDate,IsYoungDriver")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,BirthDate,IsYoungDriver")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
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
