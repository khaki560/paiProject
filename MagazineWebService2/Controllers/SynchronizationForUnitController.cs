﻿using MagazineModel;
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

        public IHttpActionResult isSynchronize()
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
                    for(int i = 0; i < SERVICE_URLS.Length; i++)
                    {
                        var client = GetWebClient(SERVICE_URLS[i]);
                        var list = client.Unit.GetAllProducts();

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
