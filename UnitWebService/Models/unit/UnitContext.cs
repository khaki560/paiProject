using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UnitWebService.Models.unit
{
    public class UnitContext : DbContext
    {
        public UnitContext(string a) : base(a)
        {

        }

        public DbSet<UnitEntry> Entries { get; set; }
    }
}