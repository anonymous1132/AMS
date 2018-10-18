using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AMS.CIM.Caojin.RPTWebApp.Models;
using System.Threading;

namespace AMS.CIM.Caojin.RPTWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Thread t = new Thread(PrepareData);
            t.Start();
        }

        private void PrepareData()
        {
            ShareDataEntity.GetSingleEntity();
            ShareDataEntity.GetSingleEntity().Rpt018 = new ReqRpt018ViewModel();
            ShareDataEntity.GetSingleEntity().Rpt018.GetData();
        }
    }
}
