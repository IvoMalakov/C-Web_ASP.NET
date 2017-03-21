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
using AuthenticationManager = CarDealerApp.Security.AuthenticationManager;

namespace CarDealerApp.Controllers
{
    [RoutePrefix("Logs")]
    public class LogsController : Controller
    {
        private CarDealerContext db = new CarDealerContext();
        private LogsService service = new LogsService(Data.Context);

        [HttpGet]
        [Route("All/{page=1}")]
        public ActionResult All(int page)
        {
            var logViewModels = this.service.GetAllLogs(page);
            return this.View(logViewModels);
        }

        [HttpGet]
        [Route("Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            var deleteLogViewModel = this.service.GetLogToDelition(id);
            return this.View(deleteLogViewModel);
        }

        [HttpPost]
        [Route("Delete/{id:int}")]
        public ActionResult Delete([Bind(Include = "Id")] DeleteLogBindingModel bindingModel)
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            this.service.DeleteLog(bindingModel.Id);
            return this.RedirectToAction("All", "Logs", new {page = 1});
        }

        [HttpGet]
        [Route("Find/{username}")]
        public ActionResult Find(string username)
        {
            if (this.db.Users.Any(u => u.Username.Contains(username)))
            {
                var userLogsViewModels = this.service.GetFiltredLogs(username);
                return this.View(userLogsViewModels);
            }

            return this.RedirectToAction("All", "Logs", new { page = 1 });
        }

        [HttpPost]
        [Route("Find")]
        public ActionResult Find([Bind(Include = "Username")] FindUserLogsBindingModel bindingModel)
        {
            return this.Redirect($"~/Logs/Find/{bindingModel.Username}");
        }

        [HttpGet]
        [Route("ClearAll")]
        public ActionResult ClearAll()
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            return this.View();
        }

        [HttpPost]
        [Route("ClearAll")]
        public ActionResult ClearAllLogs()
        {
            var httpCoockie = this.Request.Cookies.Get("sessionId");

            if (httpCoockie == null || !AuthenticationManager.IsAuthenticated(httpCoockie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            this.service.ClearAllLogs();
            return this.RedirectToAction("All", "Logs", new {page = 1});
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
