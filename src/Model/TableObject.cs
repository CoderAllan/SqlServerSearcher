namespace SQLServerSearcher.Model
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class TableObject : DatabaseObject
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
            };
            if (LastUpdate != DateTime.MaxValue)
            {
                result.Add(new[] {"Last update:", LastUpdate == DateTime.MinValue ? "N/A" : LastUpdate.ToString(CultureInfo.CurrentCulture)});
            }
            if (LastLookup != DateTime.MinValue)
            {
                result.Add(new[] { "Last lookup:", LastLookup == DateTime.MinValue ? "N/A" : LastLookup.ToString(CultureInfo.CurrentCulture) });
            }
            if (LastScan != DateTime.MinValue)
            {
                result.Add(new[] { "Last scan:", LastScan == DateTime.MinValue ? "N/A" : LastScan.ToString(CultureInfo.CurrentCulture) });
            }
            if (LastSeek != DateTime.MinValue)
            {
                result.Add(new[] { "Last seek:", LastSeek == DateTime.MinValue ? "N/A" : LastSeek.ToString(CultureInfo.CurrentCulture) });
            }
            return result;
        }
    }
}
