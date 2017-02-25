namespace SQLServerSearcher.Model
{
    using System.Collections.Generic;

    public class TableExtendedProperty : ExtendedProperty, IDatabaseObject
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }

        public new List<string[]> ToArrayList()
        {
            var result = base.ToArrayList();
            var name = string.IsNullOrEmpty(ColumnName) ? string.Format("{0}.{1}.{2}", SchemaName, TableName, Name) : string.Format("{0}.{1}.{2}.{3}", SchemaName, TableName, ColumnName, Name);
            result.Insert(0, new[] { "Name:", name });
            return result;
        }
    }
}
