using MagazineModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;



namespace WebApplication3.Controllers
{
    public class MagazineController : ApiController
    {
        public IEnumerable<MagazineEntry> GetAllProducts()
        {
            using (var repository = new MagazineRepository())
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
        public IHttpActionResult AddMagazineProduct(string name, int count, string localization)
        {
            using (var repository = new MagazineRepository())
            {
                var entry = new MagazineEntry();
                entry.Name = name;
                entry.Count = count;
                entry.Localization = localization;
                repository.Add(entry);
            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult ModifyMagazineProduct(int id, int count)
        {
            using (var repository = new MagazineRepository())
            {
                repository.Update(id, count);
            }
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult RemoveMagazineProduct(int id)
        {
            using (var repository = new MagazineRepository())
            {
                if (repository.Remove(id) == false)
                {
                    return BadRequest();
                }
                
            }
            return Ok();
        }

        //[HttpPut]
        //public IHttpActionResult ReceiveOrder(string Name, string location, int orderCount)
        //{
        //    using (var repository = new MagazineRepository())
        //    {
        //        var products = repository.GetAll();
        //    }
        //        return Ok();
        //}


        // TODO: doesn;t work
        [HttpDelete]
        public IHttpActionResult RemoveAllMagazineProducts()
        {
            using (var repository = new MagazineRepository())
            {
                var products = repository.GetAll();

                foreach(var product in products)
                {
                    if (repository.RemoveAll() == false)
                    {
                        return BadRequest();
                    }
                }
            }
            
            return Ok();
        }
    }
}




