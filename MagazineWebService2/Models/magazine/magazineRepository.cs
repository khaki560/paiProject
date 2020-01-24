using MagazineModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace MagazineModel
{
    public class MagazineRepository : IDisposable
    {
        // Server=localhost;Database=MagazineWebServiceDBTest;User Id=testuser;Password=123;  --- this is an example value for connectionString="" in Web.config file
        private readonly MagazineContext db = new MagazineContext(ConfigurationManager.ConnectionStrings["MagazineConnectionStr"].ConnectionString);


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

        public void AddAllUnitsToSync(IList<UnitsToSyncItem> units)
        {
            try
            {
                var unitsToRemove = db.UnitsToSync.Select(b => b);
                db.UnitsToSync.RemoveRange(unitsToRemove);
                db.SaveChanges();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            db.UnitsToSync.AddRange(units);
            db.SaveChanges();            
        }

        public IEnumerable<UnitsToSyncItem> GetAllUnitsToSync()
        {
            var query = from b in db.UnitsToSync
                        orderby b.Id
                        select b;

            return query.AsEnumerable().Select(item =>
                new UnitsToSyncItem()
                {
                    Id = item.Id,
                    Name = item.Name,
                    HostAndPort = item.HostAndPort
                }
            );
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
