namespace SQLServerSearcher.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Model;

    public class Views : Searches, IViews
    {
        public Views(ApplicationState appState) : base(appState)
        {
        }

        public string GetFindViewsSql(string database, string query)
        {
            string sql = string.Format(@" SELECT DISTINCT v.object_id, s.name AS schemaName, v.name, ISNULL(c.name,'') AS columnName, v.create_date, ISNULL(v.modify_date,'') AS modifyDate, m.definition
											FROM {0}.sys.views v
										   INNER JOIN {0}.sys.schemas s ON s.schema_id = v.schema_id
										   INNER JOIN {0}.sys.sql_modules m ON m.object_id = v.object_id", database);
            if (!string.IsNullOrEmpty(query))
            {
                sql += Environment.NewLine;
                sql += string.Format(@" LEFT OUTER JOIN {0}.sys.columns c ON v.object_id = c.object_id AND c.name COLLATE sql_latin1_general_cp1_ci_as LIKE '%{1}%'
											WHERE s.name COLLATE sql_latin1_general_cp1_ci_as LIKE '%{1}%' OR v.name COLLATE sql_latin1_general_cp1_ci_as LIKE '%{1}%' OR c.name COLLATE sql_latin1_general_cp1_ci_as LIKE '%{1}%' OR m.definition COLLATE sql_latin1_general_cp1_ci_as LIKE '%{1}%'", database, query);
            }
            return sql;
        }

        public List<View> FindViews(string database, string query = null)
        {
            var views = new List<View>();
            string sql = GetFindViewsSql(database, query);
            try
            {
                using (var reader = ExecuteSql(sql))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var colName = reader.GetString(reader.GetOrdinal("columnName"));
                            var view = new View
                            {
                                ObjectId = reader.GetInt32(reader.GetOrdinal("object_id")),
                                SchemaName = reader.GetString(reader.GetOrdinal("schemaName")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                ColumnName = !string.IsNullOrEmpty(query) && colName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ? colName : null,
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("create_date")),
                                ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modifyDate")),
                                Definition = reader.GetString(reader.GetOrdinal("definition")),
                            };
                            views.Add(view);
                        }
                        FindViewLastInteractionInfo(database, views);
                    }
                }
            }
            catch
            {
                // Do nothing
            }
            return views;
        }

        public string GetLastInteractionInfoSql(string database, IEnumerable<long> objectIds)
        {
            string sql = string.Format(@"SELECT ius.object_id, ISNULL(MAX(last_user_seek), '') AS lastSeek, ISNULL(MAX(last_user_scan),'') AS lastScan, ISNULL(MAX(last_user_lookup),'') AS lastLookup, ISNULL(MAX(last_user_update),'') AS lastUpdate 
                                           FROM {0}.sys.dm_db_index_usage_stats ius 
                                          WHERE ius.object_id IN ({1})", database, String.Join(",", objectIds));
            return sql;
        }

        public void FindViewLastInteractionInfo(string database, List<View> viewList)
        {
            try
            {
                var objectIds = viewList.Select(p => p.ObjectId);
                string sql = GetLastInteractionInfoSql(database, objectIds);
                using (var reader = ExecuteSql(sql))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var objectId = reader.GetInt32(reader.GetOrdinal("object_id"));
                            var view = viewList.FirstOrDefault(p => p.ObjectId == objectId);
                            if (view != null)
                            {
                                view.LastSeek = reader.GetDateTime(reader.GetOrdinal("lastSeek"));
                                view.LastScan = reader.GetDateTime(reader.GetOrdinal("lastScan"));
                                view.LastLookup = reader.GetDateTime(reader.GetOrdinal("lastLookup"));
                                view.LastUpdate = reader.GetDateTime(reader.GetOrdinal("lastUpdate"));
                            }
                        }
                    }
                }
            }
            catch
            {
                // Do nothing
            }
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
            try
            {
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
            }
            catch
            {
                // Do nothing
            }
            return properties;
        }

    }
}
