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

        private int isUnitEntryInDatabase(UnitEntry entry, IEnumerable<MagazineEntry> magazineEntries, string location)
        {
            foreach (var magazineEntry in magazineEntries)
            {
                if (entry.Name == magazineEntry.Name && location == magazineEntry.Localization)
                {
                    return magazineEntry.Id;
                }
            }

            return -1;
        }
        public IHttpActionResult Synchronize()
        {

            using (var repository = new MagazineRepository())
            { 
                var listOfMagazineEntries = repository.GetAll();
                for(int i = 0; i < SERVICE_URLS.Length; i++)
                {
                    var client = GetWebClient(SERVICE_URLS[i]);
                    var list = client.Unit.GetAllProducts();

                    foreach (var unitEntry in list)
                    {
                        var magazineEntryId = isUnitEntryInDatabase(unitEntry, listOfMagazineEntries, SERVICE_LOCATIONS[i]);
                        if (-1 == magazineEntryId)
                        {
                            MagazineEntry entry = new MagazineEntry(unitEntry.Name, unitEntry.Count.GetValueOrDefault(), SERVICE_LOCATIONS[i]);
                            repository.Add(entry);
                        }
                        else
                        {
                            repository.Update(magazineEntryId, unitEntry.Count.GetValueOrDefault());
                        }
                    }
                }
            }

            return Ok();
        }


    }
}
