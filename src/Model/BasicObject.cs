namespace SQLServerSearcher.Model
{
    using System;

    public class BasicObject
    {
        public string SchemaName { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
