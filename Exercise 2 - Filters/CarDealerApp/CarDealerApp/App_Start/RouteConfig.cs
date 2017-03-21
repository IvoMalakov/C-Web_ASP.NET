using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using CarDealer.Models.BindingModels;
using CarDealer.Models.EntityModels;

namespace CarDealerApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "All Customers",
            //    url: "customers/all/{sorthType}",
            //    defaults: new
            //    {
            //        controller = "Customers",
            //        action = "Index",
            //        sorthType = "none"
            //    });
        }
    }
}
