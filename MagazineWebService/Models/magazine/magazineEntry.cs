using System;

namespace MagazineModel
{
    public class MagazineEntry
    {
        public MagazineEntry(string name, int count, string localization)
        {
            this.Name = name;
            this.Count = count;
            this.Localization = localization;
        }

        public MagazineEntry(int id, string name, int count, string localization)
        {
            this.Id = id;
            this.Name = name;
            this.Count = count;
            this.Localization = localization;
        }

        public MagazineEntry()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Localization { get; set; }

    }
}
