namespace SQLServerSearcher.Model
{
    using System.Collections.Generic;
    using System.Globalization;

    public class DatabaseMetaInfo : IDatabaseObject
    {
        public string Name { get; set; }
        public int TableCount { get; set; }
        public int ViewCount { get; set; }
        public int StoredProcedureCount { get; set; }
        public int FunctionCount { get; set; }
        public int ExtendedPropertiesCount { get; set; }
        public List<DatabaseFile> DatabaseFiles { get; set; }

        public List<string[]> ToArrayList()
        {
            var result = new List<string[]>
            {
                new []{ "Database name:", Name },
                new []{ "Tables:", TableCount.ToString(CultureInfo.InvariantCulture) },
                new []{ "Views:", ViewCount.ToString(CultureInfo.InvariantCulture) },
                new []{ "Stored procedures:", StoredProcedureCount.ToString(CultureInfo.InvariantCulture) },
                new []{ "Functions:", FunctionCount.ToString(CultureInfo.InvariantCulture) },
                new []{ "Extended properties:", ExtendedPropertiesCount.ToString(CultureInfo.InvariantCulture) },
            };
            return result;
        }
    }
}
