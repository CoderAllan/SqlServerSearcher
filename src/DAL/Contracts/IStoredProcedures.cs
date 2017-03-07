namespace SQLServerSearcher.DAL.Contracts
{
    using System.Collections.Generic;

    using Model;

    public interface IStoredProcedures
    {
        List<Procedure> FindProcedures(string database, string query = null);
        List<ProcedureExtendedProperty> FindProcedureExtendedProperties(string database, string query);
        string GetFindStoredProceduresSql(string database, string query);
        string GetFindStoredProcedureExtendedPropertiesSql(string database, string query);
    }
}
