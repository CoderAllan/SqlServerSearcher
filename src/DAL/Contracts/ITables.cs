namespace SQLServerSearcher.DAL.Contracts
{
    using System.Collections.Generic;

    using Model;

    public interface ITables
    {
        List<Table> FindTables(string database, string query = null);
        List<TableExtendedProperty> FindTableExtendedProperties(string database, string query);
        long FindTableRowCount(string database, string table);
        string GetFindTablesSql(string database, string query);
        string GetFindTableExtendedPropertiesSql(string database, string query);
        string GetFindTableRowCountSql(string database, string table);
    }
}
