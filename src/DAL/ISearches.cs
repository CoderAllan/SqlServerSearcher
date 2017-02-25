namespace SQLServerSearcher.DAL
{
    using System.Collections.Generic;

    using Model;

    public interface ISearches
    {
        List<Database> GetDatabases();
        List<Table> FindTables(string database, string query = null);
        List<TableExtendedProperty> FindTableExtendedProperties(string database, string query = null);
        List<View> FindViews(string database, string query = null);
        List<Index> FindIndexes(string database, string query = null);
        List<Procedure> FindProcedures(string database, string query = null);
        List<ProcedureExtendedProperty> FindProcedureExtendedProperties(string database, string query = null);
        List<Function> FindFunctions(string database, string query = null);
        ServerInfo GetServerInfo();
        string GetFindTablesSql(string database, string query);
        string GetFindTableExtendedPropertiesSql(string database, string query);
        string GetFindViewsSql(string database, string query);
        string GetFindIndexesSql(string database, string query);
        string GetFindStoredProceduresSql(string database, string query);
        string GetFindStoredProcedureExtendedPropertiesSql(string database, string query);
        string GetFindFunctionsSql(string database, string query);
    }
}