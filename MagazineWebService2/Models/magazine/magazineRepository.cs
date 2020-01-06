using MagazineModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MagazineModel
{
    public class MagazineRepository : IDisposable
    {
        private readonly MagazineContext db = new MagazineContext("MagazineWebServiceDB");


        private MagazineEntry Get(int id)
        {
            var query = from b in db.Entries
                        where b.Id == id
                        select b;

            return query.AsEnumerable().FirstOrDefault();

        }

        public void Add(MagazineEntry entry)
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
            catch(Exception e)
            {
                Console.WriteLine(e);
                // TODO: very bad design
            }
            return toReturn;
        }

        // TODO: doesn;t work
        public bool RemoveAll()
        {
            var toReturn = false;
            try
            {
                var entriesToRemove = GetAll();
                db.Entries.RemoveRange(entriesToRemove);
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


        public IEnumerable<MagazineEntry> GetAll()
        {
            var query = from b in db.Entries
                        orderby b.Localization
                        select b;

            return query.AsEnumerable().Select(item =>
                new MagazineEntry() { Id = item.Id, Name = item.Name, Count = item.Count,
                    Localization = item.Localization }
            );
        }

        public void Update(int id, int count)
        {
            var product = Get(id);
            product.Count = count;
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
