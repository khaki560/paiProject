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
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

                var entryToRemove = db.Entries.Where(x => x.Id == id).FirstOrDefault();

                db.Entries.Remove(entryToRemove);
                db.SaveChanges();
                toReturn = true;
            }
            catch(Exception e)
            {
                _log.InfoFormat(e.InnerException.Message);
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
