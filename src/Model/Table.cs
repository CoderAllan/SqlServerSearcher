namespace SQLServerSearcher.Model
{
    using System.Collections.Generic;

    public class Table : TableObject, IDatabaseObject
    {
        public string SchemaName { get; set; }

        public new List<string[]> ToArrayList()
        {
            var result = base.ToArrayList();
            var name = string.IsNullOrEmpty(ColumnName) ? string.Format("{0}.{1}", SchemaName, Name) : string.Format("{0}.{1}.{2}", SchemaName, Name, ColumnName);
            result.Insert(0, new []{ "Name:", name});
            return result;
        }
    }
}
