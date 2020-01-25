using System.Configuration;
using MagazineModel;
using MagazineWebService2.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace MagazineWebService2.Controllers
{
    public class SynchronizationForUnitController : ApiController
    {

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool synchronize = false;

        private const int MAX = 2;
        public string[] SERVICE_URLS = new string[MAX];
        public string[] SERVICE_LOCATIONS = new string[MAX];

        public SynchronizationForUnitController()
        {
            var a = ConfigurationManager.AppSettings["URL"].Split(';');
            var b= ConfigurationManager.AppSettings["VALUE"].Split(';');

            for(int i = 0; i < MAX; i++)
            {
                SERVICE_URLS[i] = a[i];
                SERVICE_LOCATIONS[i] = b[i];
            }
        }
        public static UnitWebService GetWebClient(string uri)
        {
            var client = new UnitWebService(new Uri(uri), new BasicAuthenticationCredentials());
            return client;
        }

        public IHttpActionResult IsSynchronize()
        {
            return Ok(synchronize);
        }

        public IHttpActionResult GetLocations()
        {
            int len = SERVICE_LOCATIONS.Length + 2;
            string[] tmp = new string[len];

            tmp[0] = "All";
            tmp[1] = "Magazine";
            for (int i = 0; i < SERVICE_LOCATIONS.Length; i++)
            {
                tmp[i + 2] = SERVICE_LOCATIONS[i];
            }

            return Ok(tmp);
        }

        public IHttpActionResult Synchronize()
        {
            bool success = true;
            try
            {
                using (var repository = new MagazineRepository())
                { 
                    var listOfMagazineEntries = repository.GetAll();
                    for(int i = 0; i < SERVICE_URLS.Length; i++)
                    {
                        IList<UnitEntry> list;
                        try
                        {
                            var client = GetWebClient(SERVICE_URLS[i]);
                            list = client.Unit.GetAllProducts();
                        }
                        catch (HttpRequestException e)
                        {
                            Console.WriteLine("Cannot connect to the " + SERVICE_LOCATIONS[i] + "reason: " + e.ToString());
                            continue;
                        }

                        var entiresToRemove = listOfMagazineEntries.Where(x => x.Localization == SERVICE_LOCATIONS[i]);

                        _log.InfoFormat("entiresToRemove");
                        foreach (var entry in entiresToRemove)
                        {
                            _log.InfoFormat($"{entry.Id.ToString()}");
                            repository.Remove(entry.Id);
                        }

                        foreach (var unitEntry in list)
                        {
                            MagazineEntry entry = new MagazineEntry(unitEntry.Name, unitEntry.Count.GetValueOrDefault(), SERVICE_LOCATIONS[i]);
                            repository.Add(entry);
                        }
                    }
                    synchronize = true;
                }
            }
            catch(System.Net.Http.HttpRequestException e)
            {
                success = false;
                synchronize = false;
            }
            if(success)
            {
                return Ok();
            }
            return InternalServerError(new Exception("Cannot connect to unit webservice"));


        }
    }
}
