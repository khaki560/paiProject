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

        public static bool synchronize = false;

        public readonly string[] SERVICE_URLS = new string[1] { "https://localhost:44346/" };
        public readonly string[] SERVICE_LOCATIONS = new string[1] { "Unit1" };


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


    //    public IHttpActionResult A()
    //    {
    //        int len = SERVICE_LOCATIONS.Length + 2;

    //        public string[] tmp = new string[_len];

    //    tmp[0] = "All";
    //        tmp[1] = "Magazine";
    //        for(int i = 0; i<SERVICE_LOCATIONS.Length; i++)
    //        {
    //            tmp[i + 2] =  SERVICE_LOCATIONS[i];
        
    //        return Ok(synchronize);
    //}

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
                        var client = GetWebClient(SERVICE_URLS[i]);

                        IList<UnitEntry> list;
                        try
                        {
                            list = client.Unit.GetAllProducts();
                        }
                        catch (HttpRequestException e)
                        {
                            Console.WriteLine("Cannot connect to the " + SERVICE_LOCATIONS[i] + "reason: " + e.ToString());
                            continue;
                        }

                        var entiresToRemove = listOfMagazineEntries.Where(x => x.Localization == SERVICE_LOCATIONS[i]);

                        foreach (var entry in entiresToRemove)
                        {
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
