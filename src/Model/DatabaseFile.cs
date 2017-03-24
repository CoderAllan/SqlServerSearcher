namespace SQLServerSearcher.Model
{
    using System.Globalization;
    using System.Collections.Generic;

    public class DatabaseFile : IDatabaseObject
    {
        public string Name { get; set; }
        public string PhysicalName { get; set; }
        public long SizeMb { get; set; }

        public List<string[]> ToArrayList()
        {
            var result = new List<string[]>
            {
                new[] { "File name:", Name },
                new[] { "File physical path:", PhysicalName },
                new[] { "Filesize in Mb:", SizeMb == -1 ? "N/A" : SizeMb.ToString(CultureInfo.InvariantCulture) },
            };
            return result;
        }
    }
}
