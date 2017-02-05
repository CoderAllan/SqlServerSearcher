namespace SQLServerSearcher.DAL
{
    using System.Collections.Generic;

    using Model;

    public interface ISearches
    {
        List<Database> GetDatabases();
        List<Table> FindTables(string database, string query = null);
        List<View> FindViews(string database, string query = null);
        List<Index> FindIndexes(string database, string query = null);
        List<Procedure> FindProcedures(string database, string query = null);
    }
}