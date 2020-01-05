using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnitWebService.Models.unit
{
    public class UnitEntry
    {
        public UnitEntry(string name, int count, float price)
        {
            this.Name = name;
            this.Count = count;
            this.Price = price;
        }

        public UnitEntry(int id, string name, int count, float price)
        {
            this.Id = id;
            this.Name = name;
            this.Count = count;
            this.Price = price;
        }

        public UnitEntry()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public float Price{ get; set; }
    }
}