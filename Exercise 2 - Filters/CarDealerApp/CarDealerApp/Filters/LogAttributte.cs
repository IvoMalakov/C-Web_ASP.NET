using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealer.Models.EntityModels;
using CarDealerApp.Security;

namespace CarDealerApp.Filters
{
    public class LogAttributte : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string date = DateTime.Now.ToString();
            string ip = filterContext.HttpContext.Request.UserHostAddress;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string currentUser = "Anonymous";

            User user =
                AuthenticationManager.GetAuthenticatedUser(filterContext.HttpContext.Session.SessionID);

            if (user != null)
            {
                currentUser = user.Username;
            }

            string longMsg = $"{date} - {ip} - {currentUser} - {controllerName}.{actionName}\r\n";
            var exception = filterContext.Exception;

            if (exception != null)
            {
                longMsg =
                    $"[!] {date} - {ip} - {currentUser} - {controllerName}.{actionName} - {exception.GetType().Name} - {exception.Message}\r\n";
            }

            File.AppendAllText("D:/ASP.NET/Upragnenie 2 - Filters/CarDealerApp/log.txt", longMsg);

            base.OnActionExecuted(filterContext);
        }
    }
}