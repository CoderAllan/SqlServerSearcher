namespace SQLServerSearcher.DAL
{
    using System.Collections.Generic;

    using Model;

    public interface ISearches
    {
        List<Database> GetDatabases();
        List<Table> FindTables(string database, string query = null);
    }
}