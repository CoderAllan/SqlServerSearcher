namespace SQLServerSearcher.Model
{
    using System;

    public class Database : BasicObject
    {
        private new string SchemaName { get; set; }
        private new DateTime ModifiedDate { get; set; }
        public string CollationName { get; set; }
        public string OnlineState { get; set; }
    }
}
