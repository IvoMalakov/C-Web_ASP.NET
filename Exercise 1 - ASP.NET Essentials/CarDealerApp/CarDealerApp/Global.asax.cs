using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using CarDealer.Models.BindingModels;
using CarDealer.Models.EntityModels;

namespace CarDealerApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.ConfigureAutommaper();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureAutommaper()
        {
            Mapper.Initialize(expression => expression.CreateMap<AddCustomerBindingModel, Customer>());
            Mapper.Initialize(expression => expression.CreateMap<EditCustomerBindingModel, Customer>());
            Mapper.Initialize(expression => expression.CreateMap<Customer, EditCustomerBindingModel>());
        }
    }
}
