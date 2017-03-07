namespace SQLServerSearcher.DAL
{
    using System;
    using System.Collections.Generic;

    using Contracts;
    using Model;

    public class Indexes : Searches, IIndexes
    {
        public Indexes(ApplicationState appState) : base(appState)
        {
        }

        public string GetFindIndexesSql(string database, string query)
        {
            string sql = string.Format(@"SELECT t.name AS tableName, i.name, ISNULL(c.name,'') AS columnName, o.create_date, ISNULL(o.modify_date,'') AS modifyDate, i.type_desc, ISNULL(iu.last_user_lookup,'') AS lastLookup, ISNULL(iu.last_user_scan,'') AS lastScan, ISNULL(iu.last_user_seek,'') AS lastSeek, ISNULL(iu.last_user_update,'') AS lastUpdate
										   FROM {0}.sys.indexes i
										   INNER JOIN {0}.sys.tables t ON i.object_id = t.object_id
										   INNER JOIN {0}.sys.objects o on i.object_id = o.object_id
										   LEFT OUTER JOIN {0}.sys.dm_db_index_usage_stats iu ON i.object_id = iu.object_id AND i.index_id = iu.index_id", database);
            if (!string.IsNullOrEmpty(query))
            {
                sql += Environment.NewLine;
                sql += string.Format(@"   LEFT OUTER JOIN {0}.sys.index_columns ic ON i.object_id = ic.object_id and i.index_id = ic.index_id
											LEFT OUTER JOIN {0}.sys.columns c on ic.object_id=c.object_id AND ic.column_id=c.column_id AND c.name LIKE '%{1}%'
											WHERE i.name LIKE '%{1}%' OR c.name LIKE '%{1}%'", database, query);
            }
            return sql;
        }

        public List<Index> FindIndexes(string database, string query = null)
        {
            var indexes = new List<Index>();
            string sql = GetFindIndexesSql(database, query);
            using (var reader = ExecuteSql(sql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var colName = reader.GetString(reader.GetOrdinal("columnName"));
                        var index = new Index
                        {
                            TableName = reader.GetString(reader.GetOrdinal("tableName")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("create_date")),
                            ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modifyDate")),
                            ColumnName = !string.IsNullOrEmpty(query) && colName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ? colName : null,
                            TypeDescription = reader.GetString(reader.GetOrdinal("type_desc")),
                            LastSeek = reader.GetDateTime(reader.GetOrdinal("lastSeek")),
                            LastScan = reader.GetDateTime(reader.GetOrdinal("lastScan")),
                            LastLookup = reader.GetDateTime(reader.GetOrdinal("lastLookup")),
                            LastUpdate = reader.GetDateTime(reader.GetOrdinal("lastUpdate")),
                        };
                        indexes.Add(index);
                    }
                }
            }
            return indexes;
        }
    }
}
