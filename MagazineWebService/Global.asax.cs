using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;


namespace WebApplication5
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        BackgroundJobServer _server;
        protected void Application_Start()
        {
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);

            //Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage("YOUR_CONNECTION_STRING");
            //_server = new BackgroundJobServer();

        }
    }
}
