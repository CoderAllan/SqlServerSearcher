namespace SQLServerSearcher.DAL
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Model;

    public class Searches : ISearches
    {
        private readonly ApplicationState _appState;

        public Searches(ApplicationState appState)
        {
            _appState = appState;
        }

        public List<Database> GetDatabases()
        {
            var databases = new List<Database>();
            using (var reader = ExecuteSql("SELECT d.name, d.create_date, ISNULL(d.collation_name, '') AS collation_name, d.state_desc FROM sys.databases d"))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var database = new Database
                        {
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("create_date")),
                            CollationName = reader.GetString(reader.GetOrdinal("collation_name")),
                            OnlineState = reader.GetString(reader.GetOrdinal("state_desc"))
                        };
                        databases.Add(database);
                    }
                }
            }
            return databases;
        }

        public List<Table> FindTables(string database, string query = null)
        {
            var tables = new List<Table>();
            string sql = string.Format(@" SELECT DISTINCT s.name AS schemaName, t.name, ISNULL(c.name, '') AS columnName, t.create_date, t.modify_date, ISNULL(iu.lastSeek, '') AS lastSeek, ISNULL(iu.lastScan, '') AS lastScan, ISNULL(iu.lastLookup, '') AS lastLookup, ISNULL(iu.lastUpdate, '') AS lastUpdate
                                            FROM {0}.sys.tables t
                                           INNER JOIN {0}.sys.schemas s ON s.schema_id = t.schema_id
                                           OUTER APPLY (SELECT MAX(last_user_seek) AS lastSeek, MAX(last_user_scan) AS lastScan, MAX(last_user_lookup) AS lastLookup, MAX(last_user_update) AS lastUpdate FROM {0}.sys.dm_db_index_usage_stats ius WHERE ius.object_id = t.object_id) iu", database);
            if (!string.IsNullOrEmpty(query))
            {
                sql = sql + string.Format(@" LEFT OUTER JOIN {0}.sys.columns c ON t.object_id = c.object_id AND c.name LIKE '%{1}%'
                                            WHERE t.name LIKE '%{1}%' OR c.name LIKE '%{1}%'", database, query);
            }
            using (var reader = ExecuteSql(sql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var colName = reader.GetString(reader.GetOrdinal("columnName"));
                        var table = new Table
                        {
                            SchemaName = reader.GetString(reader.GetOrdinal("schemaName")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            ColumnName = !string.IsNullOrEmpty(query) && colName.IndexOf(query, System.StringComparison.OrdinalIgnoreCase) >= 0 ? colName : null,
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("create_date")),
                            ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modify_date")),
                            LastSeek = reader.GetDateTime(reader.GetOrdinal("lastSeek")),
                            LastScan = reader.GetDateTime(reader.GetOrdinal("lastScan")),
                            LastLookup = reader.GetDateTime(reader.GetOrdinal("lastLookup")),
                            LastUpdate = reader.GetDateTime(reader.GetOrdinal("lastUpdate")),
                        };
                        tables.Add(table);
                    }
                }
            }
            return tables;
        }

        public List<View> FindViews(string database, string query = null)
        {
            var views = new List<View>();
            string sql = string.Format(@" SELECT DISTINCT s.name AS schemaName, v.name, ISNULL(c.name,'') AS columnName, v.create_date, v.modify_date, ISNULL(iu.lastSeek, '') AS lastSeek, ISNULL(iu.lastScan, '') AS lastScan, ISNULL(iu.lastLookup, '') AS lastLookup, ISNULL(iu.lastUpdate, '') AS lastUpdate
                                            FROM {0}.sys.views v
                                           INNER JOIN {0}.sys.schemas s ON s.schema_id = v.schema_id
                                           OUTER APPLY (SELECT MAX(last_user_seek) AS lastSeek, MAX(last_user_scan) AS lastScan, MAX(last_user_lookup) AS lastLookup, MAX(last_user_update) AS lastUpdate FROM {0}.sys.dm_db_index_usage_stats ius WHERE ius.object_id = v.object_id) iu", database);
            if (!string.IsNullOrEmpty(query))
            {
                sql = sql + string.Format(@" LEFT OUTER JOIN {0}.sys.columns c ON v.object_id = c.object_id AND c.name LIKE '%{1}%'
                                            WHERE v.name LIKE '%{1}%' OR c.name LIKE '%{1}%'", database, query);
            }
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
                            ColumnName = !string.IsNullOrEmpty(query) && colName.IndexOf(query, System.StringComparison.OrdinalIgnoreCase) >= 0 ? colName : null,
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("create_date")),
                            ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modify_date")),
                            LastSeek = reader.GetDateTime(reader.GetOrdinal("lastSeek")),
                            LastScan = reader.GetDateTime(reader.GetOrdinal("lastScan")),
                            LastLookup = reader.GetDateTime(reader.GetOrdinal("lastLookup")),
                            LastUpdate = reader.GetDateTime(reader.GetOrdinal("lastUpdate")),
                        };
                        views.Add(view);
                    }
                }
            }
            return views;
        }

        public List<Index> FindIndexes(string database, string query = null)
        {
            var indexes = new List<Index>();
            string sql = string.Format(@"SELECT t.name AS tableName, i.name, ISNULL(c.name,'') AS columnName, o.create_date, o.modify_date, i.type_desc, ISNULL(iu.last_user_lookup,'') AS lastLookup, ISNULL(iu.last_user_scan,'') AS lastScan, ISNULL(iu.last_user_seek,'') AS lastSeek, ISNULL(iu.last_user_update,'') AS lastUpdate
                                           FROM {0}.sys.indexes i
										   INNER JOIN {0}.sys.tables t ON i.object_id = t.object_id
										   INNER JOIN {0}.sys.objects o on i.object_id = o.parent_object_id
                                           LEFT OUTER JOIN {0}.sys.dm_db_index_usage_stats iu ON i.object_id = iu.object_id AND i.index_id = iu.index_id", database);
            if (!string.IsNullOrEmpty(query))
            {
                sql = sql + string.Format(@" LEFT OUTER JOIN {0}.sys.index_columns ic ON i.object_id = ic.object_id
										     LEFT OUTER JOIN {0}.sys.columns c on ic.object_id=c.object_id AND ic.column_id=c.column_id AND c.name LIKE '%{1}%'
                                             WHERE i.name LIKE '%{1}%'", database, query);
            }
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
                            ColumnName = !string.IsNullOrEmpty(query) && colName.IndexOf(query, System.StringComparison.OrdinalIgnoreCase) >= 0 ? colName : null,
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

        public List<Procedure> FindProcedures(string database, string query = null)
        {
            var procedures = new List<Procedure>();
            string sql = string.Format(@"SELECT s.name AS schemaName, pr.name, ISNULL(pa.name,'') AS parameterName, pr.create_date, ISNULL(pr.modify_date,'') AS modifyDate, ISNULL(st.last_execution_time,'') AS lastExecutionTime, m.definition
                                           FROM {0}.sys.procedures pr
                                          INNER JOIN {0}.sys.schemas s ON pr.schema_id = s.schema_id
                                          INNER JOIN {0}.sys.sql_modules m ON pr.object_id = m.object_id
                                           LEFT OUTER JOIN {0}.sys.dm_exec_procedure_stats st on pr.object_id = st.object_id", database);
            if(!string.IsNullOrEmpty(query))
            {
                sql = sql + string.Format(@" LEFT OUTER JOIN {0}.sys.parameters pa ON pr.object_id = pa.object_id AND pa.name LIKE '%{1}%' 
                                            WHERE s.name LIKE '%{1}%' OR m.definition LIKE '%{1}%'", database, query);
            }
            using (var reader = ExecuteSql(sql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var parameterName = reader.GetString(reader.GetOrdinal("parameterName"));
                        var procedure = new Procedure()
                        {
                            SchemaName = reader.GetString(reader.GetOrdinal("schemaName")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            ParameterName = !string.IsNullOrEmpty(query) && parameterName.IndexOf(query, System.StringComparison.OrdinalIgnoreCase) >= 0 ? parameterName : null,
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("create_date")),
                            ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modifyDate")),
                            LastExecutionTime = reader.GetDateTime(reader.GetOrdinal("lastExecutionTime")),
                            Definition = reader.GetString(reader.GetOrdinal("definition")),
                        };
                        procedures.Add(procedure);
                    }
                }
            }
            return procedures;
        }

        public List<Function> FindFunctions(string database, string query = null)
        {
            var functions = new List<Function>();
            string sql = string.Format(@"SELECT s.name AS schemaName, o.name, ISNULL(pa.name,'') AS parameterName, o.create_date, ISNULL(o.modify_date,'') AS modifyDate, m.definition
                                           FROM {0}.sys.objects o
                                          INNER JOIN {0}.sys.schemas s ON o.schema_id = s.schema_id
                                          INNER JOIN {0}.sys.sql_modules m ON o.object_id = m.object_id
                                           LEFT OUTER JOIN {0}.sys.dm_exec_procedure_stats st on o.object_id = st.object_id", database);
            if (!string.IsNullOrEmpty(query))
            {
                sql = sql + string.Format(@" LEFT OUTER JOIN {0}.sys.parameters pa ON o.object_id = pa.object_id AND pa.name LIKE '%{1}%' 
                                            WHERE o.type_desc like '%FUNCTION%' AND (s.name LIKE '%{1}%' OR m.definition LIKE '%{1}%')", database, query);
            }
            using (var reader = ExecuteSql(sql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var parameterName = reader.GetString(reader.GetOrdinal("parameterName"));
                        var function = new Function()
                        {
                            SchemaName = reader.GetString(reader.GetOrdinal("schemaName")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            ParameterName = !string.IsNullOrEmpty(query) && parameterName.IndexOf(query, System.StringComparison.OrdinalIgnoreCase) >= 0 ? parameterName : null,
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("create_date")),
                            ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modifyDate")),
                            Definition = reader.GetString(reader.GetOrdinal("definition")),
                        };
                        functions.Add(function);
                    }
                }
            }
            return functions;
        }

        public ServerInfo GetServerInfo()
        {
            const string sql = "SELECT i.sqlserver_start_time, i.cpu_count, m.total_physical_memory_kb, m.available_physical_memory_kb FROM sys.dm_os_sys_info i join [sys].[dm_os_sys_memory] m on 1=1";
            ServerInfo serverInfo = null;
            using (var reader = ExecuteSql(sql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        serverInfo = new ServerInfo
                        {
                            ServerVersion = _appState.CurrentConnection.ServerVersion,
                            PhysicalMemory = reader.GetInt64(reader.GetOrdinal("total_physical_memory_kb")),
                            AvailablePhysicalMemory = reader.GetInt64(reader.GetOrdinal("available_physical_memory_kb")),
                            StartTime = reader.GetDateTime(reader.GetOrdinal("sqlserver_start_time")),
                            CPUCount = reader.GetInt32(reader.GetOrdinal("cpu_count")),
                        };
                    }
                }
            }
            return serverInfo;
        }

        public List<string> GetSchemas()
        {
            var schemaCollection = new List<string>();
            DataTable dt = _appState.CurrentConnection.GetSchema();
            foreach (DataRow schema in dt.Rows)
            {
                var schemaName = schema.Field<string>("CollectionName");
                schemaCollection.Add(schemaName);
            }
            return schemaCollection;
        }

        private SqlDataReader ExecuteSql(string sql)
        {
            var cmd = new SqlCommand
            {
                CommandText = sql, 
                CommandType = CommandType.Text, 
                Connection = _appState.CurrentConnection
            };
            return cmd.ExecuteReader();            
        }
    }
}
