﻿using System.Web.Http;
using System.Web.Mvc;

namespace MonitoringSite
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {   
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);            
        }
    }
}
