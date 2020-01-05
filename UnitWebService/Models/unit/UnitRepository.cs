using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnitWebService.Models.unit
{
    public class UnitRepository : IDisposable
    {
        private readonly UnitContext db = new UnitContext("UnitWebServiceDB");

        private UnitEntry Get(int id)
        {
            var query = from b in db.Entries
                        orderby b.Name
                        where b.Id == id
                        select b;

            return query.AsEnumerable().FirstOrDefault();

        }

        public void Add(UnitEntry entry)
        {
            db.Entries.Add(entry);
            db.SaveChanges();
        }

        public bool Remove(int id)
        {
            var toReturn = false;
            try
            {
                var entryToRemove = Get(id);
                db.Entries.Remove(entryToRemove);
                db.SaveChanges();
                toReturn = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // TODO: very bad design
            }
            return toReturn;
        }

        public IEnumerable<UnitEntry> GetAll()
        {
            var query = from b in db.Entries
                        orderby b.Id
                        select b;

            return query.AsEnumerable().Select(item =>
                new UnitEntry()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Count = item.Count,
                    Price = item.Price
                }
            );
        }

        public void Update(int id, int count, float price)
        {
            var product = Get(id);
            product.Count = count;
            product.Price = price;
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}