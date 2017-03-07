namespace SQLServerSearcher.DAL.Contracts
{
    using System.Collections.Generic;

    using Model;

    public interface IIndexes
    {
        List<Index> FindIndexes(string database, string query = null);
        string GetFindIndexesSql(string database, string query);
    }
}
