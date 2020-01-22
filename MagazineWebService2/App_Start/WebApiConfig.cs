using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Timers;
using MagazineWebService2.Controllers;

namespace MagazineWebService2
{
    public static class WebApiConfig
    {
        private static System.Timers.Timer aTimer;

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            SynchronizationForUnitController a = new SynchronizationForUnitController();
            a.Synchronize();
        }
        private static void SetTimer()
        {
            // Create a timer with a 60 second interval.
            aTimer = new System.Timers.Timer(10000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            SetTimer();
        }
    }
}
