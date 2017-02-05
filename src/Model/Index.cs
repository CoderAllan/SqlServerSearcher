namespace SQLServerSearcher.Model
{
    using System;

    public class Index
    {
        public string TableName { get; set; }
        public string Name { get; set; }
        public string ColumnName { get; set; }
        public string TypeDescription { get; set; }
        public DateTime LastScan { get; set; }
        public DateTime LastSeek { get; set; }
        public DateTime LastLookup { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
