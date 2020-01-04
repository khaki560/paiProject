using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineModel
{
    class MagazineContext : DbContext
    {
    
        public MagazineContext(string a) : base(a)
        {

        }

        public DbSet<MagazineEntry> Entries { get; set; }
    }
}
