namespace SQLServerSearcher.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Model;

    public class Indexes : Searches, IIndexes
    {
        public Indexes(ApplicationState appState) : base(appState)
        {
        }

        public string GetFindIndexesSql(string database, string query)
        {
            string sql = string.Format(@"SELECT i.object_id, t.name AS tableName, i.index_id, i.name, ISNULL(c.name,'') AS columnName, o.create_date, ISNULL(o.modify_date,'') AS modifyDate, i.type_desc
										   FROM {0}.sys.indexes i
										   INNER JOIN {0}.sys.tables t ON i.object_id = t.object_id
										   INNER JOIN {0}.sys.objects o on i.object_id = o.object_id", database);
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
            try
            {
                using (var reader = ExecuteSql(sql))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var colName = reader.GetString(reader.GetOrdinal("columnName"));
                            var index = new Index
                            {
                                ObjectId = reader.GetInt32(reader.GetOrdinal("object_id")),
                                TableName = reader.GetString(reader.GetOrdinal("tableName")),
                                IndexId = reader.GetInt64(reader.GetOrdinal("index_id")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("create_date")),
                                ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modifyDate")),
                                ColumnName = !string.IsNullOrEmpty(query) && colName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ? colName : null,
                                TypeDescription = reader.GetString(reader.GetOrdinal("type_desc")),
                            };
                            indexes.Add(index);
                        }
                        FindViewLastInteractionInfo(database, indexes);
                    }
                }
            }
            catch
            {
                // Do nothing
            }
            return indexes;
        }

        public string GetLastInteractionInfoSql(string database, List<Index> indexList)
        {
            var sqlList = new List<string>();
            foreach (var index in indexList)
            {
                sqlList.Add(string.Format(@"SELECT ius.object_id, ius.index_id, ISNULL(MAX(last_user_seek), '') AS lastSeek, ISNULL(MAX(last_user_scan),'') AS lastScan, ISNULL(MAX(last_user_lookup),'') AS lastLookup, ISNULL(MAX(last_user_update),'') AS lastUpdate 
                                           FROM {0}.sys.dm_db_index_usage_stats ius 
                                          WHERE ius.object_id = {1} AND ius.index_id = {2}; ", database, index.ObjectId, index.IndexId));
            }
            string sql = String.Join(" UNION ", sqlList);
            return sql;
        }

        public void FindViewLastInteractionInfo(string database, List<Index> indexList)
        {
            try
            {
                string sql = GetLastInteractionInfoSql(database, indexList);
                using (var reader = ExecuteSql(sql))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var objectId = reader.GetInt64(reader.GetOrdinal("object_id"));
                            var indexId = reader.GetInt64(reader.GetOrdinal("index_id"));
                            var index = indexList.FirstOrDefault(p => p.ObjectId == objectId && p.IndexId == indexId);
                            if (index != null)
                            {
                                index.LastSeek = reader.GetDateTime(reader.GetOrdinal("lastSeek"));
                                index.LastScan = reader.GetDateTime(reader.GetOrdinal("lastScan"));
                                index.LastLookup = reader.GetDateTime(reader.GetOrdinal("lastLookup"));
                                index.LastUpdate = reader.GetDateTime(reader.GetOrdinal("lastUpdate"));
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

    }
}
