namespace SQLServerSearcher.DAL
{
    using System;
    using System.Collections.Generic;

    using Contracts;
    using Model;

    public class Views : Searches, IViews
    {
        public Views(ApplicationState appState) : base(appState)
        {
        }

        public string GetFindViewsSql(string database, string query)
        {
            string sql = string.Format(@" SELECT DISTINCT s.name AS schemaName, v.name, ISNULL(c.name,'') AS columnName, v.create_date, ISNULL(v.modify_date,'') AS modifyDate, ISNULL(iu.lastSeek, '') AS lastSeek, ISNULL(iu.lastScan, '') AS lastScan, ISNULL(iu.lastLookup, '') AS lastLookup, ISNULL(iu.lastUpdate, '') AS lastUpdate, m.definition
											FROM {0}.sys.views v
										   INNER JOIN {0}.sys.schemas s ON s.schema_id = v.schema_id
										   INNER JOIN {0}.sys.sql_modules m ON m.object_id = v.object_id
										   OUTER APPLY (SELECT MAX(last_user_seek) AS lastSeek, MAX(last_user_scan) AS lastScan, MAX(last_user_lookup) AS lastLookup, MAX(last_user_update) AS lastUpdate FROM {0}.sys.dm_db_index_usage_stats ius WHERE ius.object_id = v.object_id) iu", database);
            if (!string.IsNullOrEmpty(query))
            {
                sql += Environment.NewLine;
                sql += string.Format(@" LEFT OUTER JOIN {0}.sys.columns c ON v.object_id = c.object_id AND c.name LIKE '%{1}%'
											WHERE s.name LIKE '%{1}%' OR v.name LIKE '%{1}%' OR c.name LIKE '%{1}%' OR m.definition LIKE '%{1}%'", database, query);
            }
            return sql;
        }

        public List<View> FindViews(string database, string query = null)
        {
            var views = new List<View>();
            string sql = GetFindViewsSql(database, query);
            using (var reader = ExecuteSql(sql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var colName = reader.GetString(reader.GetOrdinal("columnName"));
                        var view = new View
                        {
                            SchemaName = reader.GetString(reader.GetOrdinal("schemaName")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            ColumnName = !string.IsNullOrEmpty(query) && colName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ? colName : null,
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("create_date")),
                            ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modifyDate")),
                            LastSeek = reader.GetDateTime(reader.GetOrdinal("lastSeek")),
                            LastScan = reader.GetDateTime(reader.GetOrdinal("lastScan")),
                            LastLookup = reader.GetDateTime(reader.GetOrdinal("lastLookup")),
                            LastUpdate = reader.GetDateTime(reader.GetOrdinal("lastUpdate")),
                            Definition = reader.GetString(reader.GetOrdinal("definition")),
                        };
                        views.Add(view);
                    }
                }
            }
            return views;
        }

        public string GetFindViewExtendedPropertiesSql(string database, string query)
        {
            string sql = string.Format(@"SELECT s.name AS schemaName, v.name AS tableName, ISNULL(c.name,'') AS columnName, epp.name, epp.value
										   FROM {0}.sys.views v
										  INNER JOIN {0}.sys.schemas s ON v.schema_id = s.schema_id
										   LEFT OUTER JOIN {0}.sys.columns c ON v.object_id = c.object_id AND c.name LIKE '%{1}%'
										  INNER JOIN {0}.sys.extended_properties epp ON v.object_id = epp.major_id AND epp.minor_id=c.column_id
										  WHERE s.name LIKE '%{1}%' OR v.name LIKE '%{1}%' OR c.name LIKE '%{1}%' OR cast(epp.value AS varchar) LIKE '%{1}%'
										  UNION
										 SELECT s.name AS schemaName, v.name, '', ep.name, ep.value
										   FROM {0}.sys.views v
										  INNER JOIN {0}.sys.schemas s ON v.schema_id = s.schema_id
										  INNER JOIN {0}.sys.extended_properties ep ON v.object_id = ep.major_id AND ep.minor_id = 0
										  WHERE s.name LIKE '%{1}%' OR v.name LIKE '%{1}%' OR cast(ep.value AS varchar) LIKE '%{1}%'", database, query);
            return sql;
        }

        public List<ViewExtendedProperty> FindViewExtendedProperties(string database, string query)
        {
            var properties = new List<ViewExtendedProperty>();
            string sql = GetFindViewExtendedPropertiesSql(database, query);
            using (var reader = ExecuteSql(sql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var colName = reader.GetString(reader.GetOrdinal("columnName"));
                        var property = new ViewExtendedProperty
                        {
                            SchemaName = reader.GetString(reader.GetOrdinal("schemaName")),
                            TableName = reader.GetString(reader.GetOrdinal("tableName")),
                            ColumnName = !string.IsNullOrEmpty(query) && colName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ? colName : null,
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Value = reader.GetString(reader.GetOrdinal("value")),
                        };
                        properties.Add(property);
                    }
                }
            }
            return properties;
        }

    }
}
