using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealerApp.Filters
{
    public class TimerAttribute : ActionFilterAttribute
    {
        private DateTime time;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.time = DateTime.Now;

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            TimeSpan span = DateTime.Now - time;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;

            string timerMsg = $"{DateTime.Now} - {controllerName}.{actionName} - {span}\r\n";
            File.AppendAllText("D:/ASP.NET/Upragnenie 2 - Filters/CarDealerApp/action-times.txt", timerMsg);

            base.OnActionExecuted(filterContext);
        }
    }
}