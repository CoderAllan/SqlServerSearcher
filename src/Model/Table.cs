namespace SQLServerSearcher.Model
{
    using System;

    public class Table
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime LastScan { get; set; }
        public DateTime LastSeek { get; set; }
        public DateTime LastLookup { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
