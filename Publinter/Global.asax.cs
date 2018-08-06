using Publinter.App_Start;
using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace Publinter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            Application["Version"] = String.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}