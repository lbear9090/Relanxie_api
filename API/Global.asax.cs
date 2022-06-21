using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using Avigma.Repository.Lib;
using Avigma.Models;
namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("******************************************************");
            log.Info("Application Initialized");

            //GoogleLocation googleLocation = new GoogleLocation();
            //GoogleLocationDTO googleLocationDTO = new GoogleLocationDTO();
            //googleLocationDTO.Address = "1831 Brooks Drive NW, Atlanta GA, 30318";
            //googleLocation.GetLatitudeLogitudeWithAPIKey(googleLocationDTO);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
