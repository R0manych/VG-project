using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VG_web.Models;
using Moveax.Mvc.ErrorHandler;
using VG_web.App_Start;
using log4net;

namespace VG_web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(HttpApplication));

        protected void Application_Start()
        {            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutomapperConfiguration.AddMapping();
        }

         protected void Application_Error(object sender, EventArgs e)
        {
            var errorHandler = new MvcApplicationErrorHandler(application: this, exception: this.Server.GetLastError())
            {
                EnableHttpReturnCodes = false,
                PassThroughHttp401 = false
            };

            errorHandler.Execute();
        }
    }
}
