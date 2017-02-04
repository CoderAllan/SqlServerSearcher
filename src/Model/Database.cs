namespace SQLServerSearcher.Model
{
    using System;

    public class Database
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CollationName { get; set; }
        public string OnlineState { get; set; }
    }
}
