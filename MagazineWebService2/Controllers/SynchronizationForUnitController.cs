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
        public readonly string[] SERVICE_URLS = new string[1] { "https://localhost:44346/" };
        public readonly string[] SERVICE_LOCATIONS = new string[1] { "Unit1" };

        public static UnitWebService GetWebClient(string uri)
        {
            var client = new UnitWebService(new Uri(uri), new BasicAuthenticationCredentials());
            return client;
        }

        public IHttpActionResult Synchronize()
        {

            using (var repository = new MagazineRepository())
            { 
                var listOfMagazineEntries = repository.GetAll();
                for(int i = 0; i < SERVICE_URLS.Length; i++)
                {
                    var client = GetWebClient(SERVICE_URLS[i]);

                    IList<UnitEntry> list;
                    try {
                        list = client.Unit.GetAllProducts();
                    } catch(HttpRequestException e) {
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
            }

            return Ok();
        }


    }
}
