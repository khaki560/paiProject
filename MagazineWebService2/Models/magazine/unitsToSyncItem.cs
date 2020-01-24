using System;

namespace MagazineModel
{
    public class UnitsToSyncItem
    {
        public UnitsToSyncItem(int id, string name, string hostAndPort)
        {
            this.Id = id;
            this.Name = name;
            this.HostAndPort = hostAndPort;
        }

        public UnitsToSyncItem(string name, string hostAndPort)
        {
            this.Name = name;
            this.HostAndPort = hostAndPort;
        }

        public UnitsToSyncItem()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string HostAndPort { get; set; }

    }
}
