namespace SQLServerSearcher.DAL
{
    using System.Collections.Generic;

    using Model;

    public interface ISearches
    {
        List<Database> GetDatabases();
        DatabaseMetaInfo FindDatabaseMetaInfo(string database);
        List<Table> FindTables(string database, string query = null);
        List<TableExtendedProperty> FindTableExtendedProperties(string database, string query);
        List<View> FindViews(string database, string query = null);
        List<ViewExtendedProperty> FindViewExtendedProperties(string database, string query);
        List<Index> FindIndexes(string database, string query = null);
        List<Procedure> FindProcedures(string database, string query = null);
        List<ProcedureExtendedProperty> FindProcedureExtendedProperties(string database, string query);
        List<Function> FindFunctions(string database, string query = null);
        List<FunctionExtendedProperty> FindFunctionExtendedProperties(string database, string query);
        ServerInfo GetServerInfo();
        string GetFindDatabaseMetaInfo(string database);
        string GetFindTablesSql(string database, string query);
        string GetFindTableExtendedPropertiesSql(string database, string query);
        string GetFindViewsSql(string database, string query);
        string GetFindViewExtendedPropertiesSql(string database, string query);
        string GetFindIndexesSql(string database, string query);
        string GetFindStoredProceduresSql(string database, string query);
        string GetFindStoredProcedureExtendedPropertiesSql(string database, string query);
        string GetFindFunctionsSql(string database, string query);
        string GetFindFunctionExtendedPropertiesSql(string database, string query);
    }
}