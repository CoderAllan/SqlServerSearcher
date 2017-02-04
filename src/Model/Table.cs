namespace SQLServerSearcher.Model
{
    using System;

    public class Table : BasicObject
    {
        public DateTime LastScan { get; set; }
        public DateTime LastSeek { get; set; }
        public DateTime LastLookup { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
