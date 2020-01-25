using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnitWebService.Models;
using UnitWebService.Models.unit;

namespace UnitWebService.Controllers
{
    public class OrderController : ApiController
    {
        public string SERVICE_URL =  "";
        public string MAGAZINE_LOCATION = "" ;


        public OrderController()
        {
            SERVICE_URL = ConfigurationManager.AppSettings["URL"];
            MAGAZINE_LOCATION = ConfigurationManager.AppSettings["VALUE"];
        }
        public static MagazineWebService2 GetWebClient(string uri)
        {
            var client = new MagazineWebService2(new Uri(uri), new BasicAuthenticationCredentials());
            return client;
        }


        public IHttpActionResult Order(UnitEntry entry, int orderCount)
        {
   
            using (var repository = new UnitRepository())
            {
                var listOfUnitEntries = repository.GetAll();

                var client = GetWebClient(SERVICE_URL);
                var list = client.Magazine.GetAllProducts();

                var magazineEntry = list.Where(l => (l.Name == entry.Name && l.Localization == MAGAZINE_LOCATION))
                    .FirstOrDefault();
                var unitEntry = listOfUnitEntries.Where(l => l.Id == entry.Id).FirstOrDefault();

                if(magazineEntry != null && unitEntry != null)
                {
                    if( magazineEntry.Count >= orderCount)
                    {
                        repository.Update(unitEntry.Id, unitEntry.Count + orderCount, unitEntry.Price);
                        client.Magazine.ModifyMagazineProduct(magazineEntry.Id.GetValueOrDefault(), magazineEntry.Count.GetValueOrDefault() - orderCount);
                        return Ok();
                    }
                }
            }

            return BadRequest();
        }
    }
}
