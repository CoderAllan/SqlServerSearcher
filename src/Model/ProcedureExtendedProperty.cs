namespace SQLServerSearcher.Model
{
    using System.Collections.Generic;

    public class ProcedureExtendedProperty : ExtendedProperty, IDatabaseObject
    {
        public string ProcedureName { get; set; }
        public string ParameterName { get; set; }

        public new List<string[]> ToArrayList()
        {
            var result = base.ToArrayList();
            var name = string.IsNullOrEmpty(ParameterName) ? string.Format("{0}.{1}.{2}", SchemaName, ProcedureName, Name) : string.Format("{0}.{1}.{2}.{3}", SchemaName, ProcedureName, ParameterName, Name);
            result.Insert(0, new[] { "Name:", name });
            return result;
        }
    }
}
