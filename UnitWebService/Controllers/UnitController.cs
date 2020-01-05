using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnitWebService.Models.unit;

namespace UnitWebService.Controllers
{
    public class UnitController : ApiController
    {
        public IEnumerable<UnitEntry> GetAllProducts()
        {
            using (var repository = new UnitRepository())
            {
                var a = repository.GetAll().ToList();
                return a;
            }
        }

        // DO i need this ????
        public IHttpActionResult GetProduct(int? id)
        {
            var product = id;
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut]
        public IHttpActionResult AddMagazineProduct(string name, int count, float Price)
        {
            using (var repository = new UnitRepository())
            {
                var entry = new UnitEntry();
                entry.Name = name;
                entry.Count = count;
                entry.Price = Price;
                repository.Add(entry);
            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult ModifyMagazineProduct(int id, int count, float price)
        {
            using (var repository = new UnitRepository())
            {
                repository.Update(id, count, price);
            }
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult RemoveMagazineProduct(int id)
        {
            using (var repository = new UnitRepository())
            {
                if (repository.Remove(id) == false)
                {
                    return BadRequest();
                }

            }
            return Ok();
        }


        // TODO: doesn;t work
        [HttpDelete]
        public IHttpActionResult RemoveAllMagazineProducts()
        {
            //using (var repository = new UnitRepository())
            //{
            //    var products = repository.GetAll();

            //    foreach (var product in products)
            //    {
            //        if (repository.RemoveAll() == false)
            //        {
            //            return BadRequest();
            //        }
            //    }
            //}

            return NotFound();
        }
    }
}
