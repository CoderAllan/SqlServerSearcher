namespace SQLServerSearcher.DAL
{
    using System.Collections.Generic;

    using Contracts;
    using Model;

    public class Databases : Searches, IDatabases
    {
        public Databases(ApplicationState appState) : base(appState)
        {
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

        public string GetFindDatabaseMetaInfo(string database)
        {
            string sql = string.Format(@"SELECT (SELECT COUNT(*) FROM {0}.sys.tables) AS tableCount, 
	                                            (SELECT COUNT(*) FROM {0}.sys.views) AS viewCount, 
	                                            (SELECT COUNT(*) FROM {0}.sys.procedures) AS procedureCount, 
	                                            (SELECT COUNT(*) FROM {0}.sys.objects o WHERE o.type_desc LIKE '%FUNCTION%') AS functionCount, 
	                                            (SELECT COUNT(*) FROM {0}.sys.extended_properties) AS extendedPropertiesCount", database);
            return sql;
        }

        public DatabaseMetaInfo FindDatabaseMetaInfo(string database)
        {
            var databaseMetaInfo = new DatabaseMetaInfo();
            string sql = GetFindDatabaseMetaInfo(database);
            try
            {
                using (var reader = ExecuteSql(sql))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            databaseMetaInfo.Name = database;
                            databaseMetaInfo.TableCount = reader.GetInt32(reader.GetOrdinal("tableCount"));
                            databaseMetaInfo.ViewCount = reader.GetInt32(reader.GetOrdinal("viewCount"));
                            databaseMetaInfo.StoredProcedureCount = reader.GetInt32(reader.GetOrdinal("procedureCount"));
                            databaseMetaInfo.FunctionCount = reader.GetInt32(reader.GetOrdinal("functionCount"));
                            databaseMetaInfo.ExtendedPropertiesCount = reader.GetInt32(reader.GetOrdinal("extendedPropertiesCount"));
                        }
                    }
                }
                databaseMetaInfo.DatabaseFiles = FindDatabaseFiles(database);
            }
            catch
            {
                databaseMetaInfo.Name = "N/A";
                databaseMetaInfo.TableCount = -1;
                databaseMetaInfo.ViewCount = -1;
                databaseMetaInfo.StoredProcedureCount = -1;
                databaseMetaInfo.FunctionCount = -1;
                databaseMetaInfo.ExtendedPropertiesCount = -1;
            }
            return databaseMetaInfo;
        }

        public string GetFindDatabaseFileSizes(string database)
        {
            string sql = string.Format(@"select name, physical_name, (size*8)/1024 SizeMb from {0}.sys.database_files", database);
            return sql;
        }

        public List<DatabaseFile> FindDatabaseFiles(string database)
        {
            var databasefiles = new List<DatabaseFile>();
            string sql = GetFindDatabaseFileSizes(database);
            using (var reader = ExecuteSql(sql))
            {
                if (reader.HasRows)
                {
                    var databaseFile = new DatabaseFile();
                    if (reader.Read())
                    {
                        databaseFile.Name = reader.GetString(reader.GetOrdinal("name"));
                        databaseFile.PhysicalName = reader.GetString(reader.GetOrdinal("physical_name"));
                        databaseFile.SizeMb = reader.GetInt32(reader.GetOrdinal("SizeMb"));
                    }
                    databasefiles.Add(databaseFile);
                }
            }
            return databasefiles;
        }

    }
}
