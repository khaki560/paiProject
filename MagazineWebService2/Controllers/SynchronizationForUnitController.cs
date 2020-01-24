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

        public IList<UnitsToSyncItem> GetAllUnitsToSync()
        {
            using (var repository = new MagazineRepository())
            {
                var a = repository.GetAllUnitsToSync().ToList();
                return a;
            }
        }

        public IHttpActionResult SynchronizeUnits(IList<UnitsToSyncItem> units)
        {
            using (var repository = new MagazineRepository())
            {
                repository.AddAllUnitsToSync(units);
            }
            
            return Ok();
        }

        public IHttpActionResult IsSynchronize()
        {
            return Ok(synchronize);
        }


        public IHttpActionResult Synchronize()
        {
            bool success = true;
            try
            {
                using (var repository = new MagazineRepository())
                { 
                    var listOfMagazineEntries = repository.GetAll();
                    var unitsToSync = new MagazineRepository().GetAllUnitsToSync();
                    foreach(var unit in unitsToSync)
                    {
                        var client = GetWebClient(unit.HostAndPort);

                        IList<UnitEntry> list;
                        try
                        {
                            list = client.Unit.GetAllProducts();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Cannot connect to the " + unit.Name + "reason: " + e.ToString());
                            continue;
                        }

                        var entiresToRemove = listOfMagazineEntries.Where(x => x.Localization == unit.Name);

                        foreach (var entry in entiresToRemove)
                        {
                            repository.Remove(entry.Id);
                        }

                        foreach (var unitEntry in list)
                        {
                            MagazineEntry entry = new MagazineEntry(unitEntry.Name, unitEntry.Count.GetValueOrDefault(), unit.Name);
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
