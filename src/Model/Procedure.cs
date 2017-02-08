namespace SQLServerSearcher.Model
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class Procedure : ProcedureObject, IDatabaseObject
    {
        public DateTime LastExecutionTime { get; set; }

        public new List<string[]> ToArrayList()
        {
            var result = base.ToArrayList();
            result.Add(new[] { "Last execution time: ", LastExecutionTime.ToString(CultureInfo.CurrentCulture) });
            return result;
        }
    }
}
