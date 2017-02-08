namespace SQLServerSearcher.Model
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class TableObject
    {
        public string Name { get; set; }
        public string ColumnName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime LastScan { get; set; }
        public DateTime LastSeek { get; set; }
        public DateTime LastLookup { get; set; }
        public DateTime LastUpdate { get; set; }

        public List<string[]> ToArrayList()
        {
            var result = new List<string[]>
            {
                new[] {"Created:", CreatedDate.ToString(CultureInfo.CurrentCulture)},
                new[] {"Modified:", ModifiedDate.ToString(CultureInfo.CurrentCulture)},
                new[] {"Last update:", LastUpdate.ToString(CultureInfo.CurrentCulture)},
                new[] {"Last lookup:", LastLookup.ToString(CultureInfo.CurrentCulture)},
                new[] {"Last scan:", LastScan.ToString(CultureInfo.CurrentCulture)},
                new[] {"Last seek:", LastSeek.ToString(CultureInfo.CurrentCulture)},
            };
            return result;
        }
    }
}
