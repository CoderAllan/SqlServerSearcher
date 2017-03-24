namespace SQLServerSearcher.Model
{
    using System.Collections.Generic;

    public class Index : TableObject, IDatabaseObject
    {
        public long IndexId { get; set; }
        public string TableName { get; set; }
        public string TypeDescription { get; set; }

        public new List<string[]> ToArrayList()
        {
            var result = base.ToArrayList();
            var name = string.IsNullOrEmpty(ColumnName) ? string.Format("{0}.{1}", TableName, Name) : string.Format("{0}.{1}.{2}", TableName, Name, ColumnName);
            result.Insert(0, new[] { "Name:", name });
            result.Add(new[] {"Type description:", TypeDescription});
            return result;
        }
    }
}
