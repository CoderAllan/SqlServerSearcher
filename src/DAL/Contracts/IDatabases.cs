namespace SQLServerSearcher.DAL.Contracts
{
    using System.Collections.Generic;
    using Model;

    public interface IDatabases
    {
        List<Database> GetDatabases();
        DatabaseMetaInfo FindDatabaseMetaInfo(string database);
        string GetFindDatabaseMetaInfo(string database);
    }
}
