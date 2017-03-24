namespace SQLServerSearcher.Model
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class ProcedureObject : DatabaseObject
    {
        public string SchemaName { get; set; }
        public string Name { get; set; }
        public string ParameterName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime LastExecutionTime { get; set; }
        public string Definition { get; set; }

        public List<string[]> ToArrayList()
        {
            var name = string.IsNullOrEmpty(ParameterName) ? string.Format("{0}.{1}", SchemaName, Name) : string.Format("{0}.{1}.{2}", SchemaName, Name, ParameterName);
            var result = new List<string[]>
            {
                new[] {"Name:", name},
                new[] {"Created:", CreatedDate.ToString(CultureInfo.CurrentCulture)},
                new[] {"Modified:", ModifiedDate.ToString(CultureInfo.CurrentCulture)},
                new[] {"Last execution time:", LastExecutionTime == DateTime.MinValue ? "N/A" : LastExecutionTime.ToString(CultureInfo.CurrentCulture)},
            };
            return result;
        }
    }
}
