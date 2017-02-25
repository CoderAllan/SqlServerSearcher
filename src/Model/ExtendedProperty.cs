namespace SQLServerSearcher.Model
{
    using System.Collections.Generic;

    public class ExtendedProperty
    {
        public string SchemaName { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public List<string[]> ToArrayList()
        {
            var result = new List<string[]>
            {
                new[] {"Value:", Value},
            };
            return result;
        }
    }
}
