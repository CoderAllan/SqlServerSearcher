namespace SQLServerSearcher.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Model;

    public class Tables : Searches, ITables
    {
        public Tables(ApplicationState appState) : base(appState)
        {
        }

        public string GetFindTablesSql(string database, string query)
        {
            string sql = string.Format(@" SELECT DISTINCT t.object_id, s.name AS schemaName, t.name, ISNULL(c.name, '') AS columnName, t.create_date, ISNULL(t.modify_date,'') AS modifyDate
											FROM {0}.sys.tables t
										   INNER JOIN {0}.sys.schemas s ON s.schema_id = t.schema_id", database);
            if (!string.IsNullOrEmpty(query))
            {
                sql += Environment.NewLine;
                sql += string.Format(@" LEFT OUTER JOIN {0}.sys.columns c ON t.object_id = c.object_id AND c.name COLLATE sql_latin1_general_cp1_ci_as LIKE '%{1}%'
											WHERE s.name COLLATE sql_latin1_general_cp1_ci_as LIKE '%{1}%' OR t.name COLLATE sql_latin1_general_cp1_ci_as LIKE '%{1}%' OR c.name COLLATE sql_latin1_general_cp1_ci_as LIKE '%{1}%'", database, query);
            }
            return sql;
        }

        public List<Table> FindTables(string database, string query = null)
        {
            var tables = new List<Table>();
            string sql = GetFindTablesSql(database, query);
            try
            {
                using (var reader = ExecuteSql(sql))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var colName = reader.GetString(reader.GetOrdinal("columnName"));
                            var table = new Table
                            {
                                ObjectId = reader.GetInt32(reader.GetOrdinal("object_id")),
                                SchemaName = reader.GetString(reader.GetOrdinal("schemaName")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                ColumnName = !string.IsNullOrEmpty(query) && colName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ? colName : null,
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("create_date")),
                                ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modifyDate")),
                            };
                            tables.Add(table);
                        }
                        FindTableLastInteractionInfo(database, tables);
                    }
                }
            }
            catch
            {
                // Do nothing
            }
            return tables;
        }

        public string GetLastInteractionInfoSql(string database, IEnumerable<long> objectIds)
        {
            string sql = string.Format(@"SELECT ius.object_id, ISNULL(MAX(last_user_seek), '') AS lastSeek, ISNULL(MAX(last_user_scan),'') AS lastScan, ISNULL(MAX(last_user_lookup),'') AS lastLookup, ISNULL(MAX(last_user_update),'') AS lastUpdate 
                                           FROM {0}.sys.dm_db_index_usage_stats ius 
                                          WHERE ius.object_id IN ({1})", database, String.Join(",", objectIds));
            return sql;
        }

        public void FindTableLastInteractionInfo(string database, List<Table> tableList)
        {
            try
            {
                var objectIds = tableList.Select(p => p.ObjectId);
                string sql = GetLastInteractionInfoSql(database, objectIds);
                using (var reader = ExecuteSql(sql))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var objectId = reader.GetInt64(reader.GetOrdinal("object_id"));
                            var table = tableList.FirstOrDefault(p => p.ObjectId == objectId);
                            if (table != null)
                            {
                                table.LastSeek = reader.GetDateTime(reader.GetOrdinal("lastSeek"));
                                table.LastScan = reader.GetDateTime(reader.GetOrdinal("lastScan"));
                                table.LastLookup = reader.GetDateTime(reader.GetOrdinal("lastLookup"));
                                table.LastUpdate = reader.GetDateTime(reader.GetOrdinal("lastUpdate"));
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

        public string GetFindTableExtendedPropertiesSql(string database, string query)
        {
            string sql = string.Format(@"SELECT s.name AS schemaName, t.name AS tableName, ISNULL(c.name,'') AS columnName, epp.name, epp.value
										   FROM {0}.sys.tables t
										  INNER JOIN {0}.sys.schemas s ON t.schema_id = s.schema_id
										   LEFT OUTER JOIN {0}.sys.columns c ON t.object_id = c.object_id AND c.name LIKE '%{1}%'
										  INNER JOIN {0}.sys.extended_properties epp ON t.object_id = epp.major_id AND epp.minor_id=c.column_id
										  WHERE s.name LIKE '%{1}%' OR t.name LIKE '%{1}%' OR c.name LIKE '%{1}%' OR cast(epp.value AS varchar) LIKE '%{1}%'
										  UNION
										 SELECT s.name AS schemaName, t.name, '', ep.name, ep.value
										   FROM {0}.sys.tables t
										  INNER JOIN {0}.sys.schemas s ON t.schema_id = s.schema_id
										  INNER JOIN {0}.sys.extended_properties ep ON t.object_id = ep.major_id AND ep.minor_id = 0
										  WHERE s.name LIKE '%{1}%' OR t.name LIKE '%{1}%' OR cast(ep.value AS varchar) LIKE '%{1}%'", database, query);
            return sql;
        }

        public List<TableExtendedProperty> FindTableExtendedProperties(string database, string query)
        {
            var properties = new List<TableExtendedProperty>();
            string sql = GetFindTableExtendedPropertiesSql(database, query);
            try
            {
                using (var reader = ExecuteSql(sql))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var colName = reader.GetString(reader.GetOrdinal("columnName"));
                            var property = new TableExtendedProperty
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

        public string GetFindTableRowCountSql(string database, string table)
        {
            string sql = string.Format(@"DECLARE @rowCount BIGINT
                                         SELECT @rowCount=SUM(st.row_count) FROM {0}.sys.dm_db_partition_stats st WHERE st.[object_id] = object_id('{0}.{1}') AND (st.index_id < 2)
                                         IF @rowCount < 5000000 BEGIN
	                                         SELECT CAST(COUNT(1) AS BIGINT) AS [tableRowCount] FROM {0}.{1}
                                         END ELSE BEGIN 
	                                         SELECT CAST(@rowCount AS BIGINT) AS [tableRowCount]
                                         END", database, table);
            return sql;
        }

        public long FindTableRowCount(string database, string table)
        {
            string sql = GetFindTableRowCountSql(database, table);
            long rowCount = -1;
            try
            {
                using (var reader = ExecuteSql(sql))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rowCount = reader.GetInt64(reader.GetOrdinal("tableRowCount"));
                    }
                }
            }
            catch
            {
                // Do nothing
            }
            return rowCount;
        }

    }
}
