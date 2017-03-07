namespace SQLServerSearcher.DAL.Contracts
{
    using System.Collections.Generic;

    using Model;
    
    public interface IFunctions
    {
        List<Function> FindFunctions(string database, string query = null);
        List<FunctionExtendedProperty> FindFunctionExtendedProperties(string database, string query);
        string GetFindFunctionsSql(string database, string query);
        string GetFindFunctionExtendedPropertiesSql(string database, string query);
    }
}
