using System;
using System.Web;
using System.Web.Mvc;
using CarDealerApp.Filters;

namespace CarDealerApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()
            {
                ExceptionType = typeof(ArgumentOutOfRangeException),
                View = "OutOfRangeError.cshtml"
            });

            filters.Add(new HandleErrorAttribute()
            {
                ExceptionType = typeof(InvalidOperationException),
                View = "InvalidOperationError.cshtml"
            });

            filters.Add(new LogAttributte());
            
            filters.Add(new TimerAttribute());
        }
    }
}
