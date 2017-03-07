namespace SQLServerSearcher.DAL.Contracts
{
    using System.Collections.Generic;

    using Model;

    public interface IViews
    {
        List<View> FindViews(string database, string query = null);
        List<ViewExtendedProperty> FindViewExtendedProperties(string database, string query);
        string GetFindViewsSql(string database, string query);
        string GetFindViewExtendedPropertiesSql(string database, string query);
    }
}
