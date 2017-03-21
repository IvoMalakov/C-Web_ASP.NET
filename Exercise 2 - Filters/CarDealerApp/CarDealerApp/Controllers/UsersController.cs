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
using CarDealer.Services;
using CarDealerApp.Security;
using AuthenticationManager = CarDealerApp.Security.AuthenticationManager;

namespace CarDealerApp.Controllers
{
    public class UsersController : Controller
    {
        private CarDealerContext db = new CarDealerContext();
        private UsersService service = new UsersService(Data.Context);

        [Route("~/users/register")]
        public ActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [Route("~/users/register")]
        public ActionResult Register([Bind(Include = "Username, Email, Password, ConfirmPassword")] RegisterUserBindingModel bindingModel)
        {
            if (this.ModelState.IsValid)
            {
                this.service.RegisterUser(bindingModel);
                return this.Redirect("~/home/index");
            }

            return this.View();
        }

        [Route("~/users/login")]
        public ActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        [Route("~/users/login")]
        public ActionResult Login([Bind(Include = "Username, Password")] LoginUserBindingModel bindingModel)
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie != null && AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Index", "Cars");
            }

            if (this.ModelState.IsValid && this.service.UserExists(bindingModel))
            {
                this.service.LoginUser(bindingModel, this.Session.SessionID);
                this.Response.SetCookie(new HttpCookie("sessionId", this.Session.SessionID));
                return this.RedirectToAction("Index", "Cars");
            }

            return this.View();
        }

        [Route("~/users/logout")]
        public ActionResult Logout()
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login");
            }

            AuthenticationManager.Logout(this.Request.Cookies.Get("sessionId").Value);
            return this.RedirectToAction("Index", "Cars");
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
