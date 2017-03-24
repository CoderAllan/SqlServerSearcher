namespace SQLServerSearcher.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Model;

    public class StoredProcedures : Searches, IStoredProcedures
    {
        public StoredProcedures(ApplicationState appState) : base(appState)
        {
        }

        public string GetFindStoredProceduresSql(string database, string query)
        {
            string sql = string.Format(@"SELECT pr.object_id, s.name AS schemaName, pr.name, ISNULL(pa.name,'') AS parameterName, pr.create_date, ISNULL(pr.modify_date,'') AS modifyDate, m.definition
										   FROM {0}.sys.procedures pr
										  INNER JOIN {0}.sys.schemas s ON pr.schema_id = s.schema_id
										  INNER JOIN {0}.sys.sql_modules m ON pr.object_id = m.object_id", database);
            if (!string.IsNullOrEmpty(query))
            {
                sql += Environment.NewLine;
                sql += string.Format(@"   LEFT OUTER JOIN {0}.sys.parameters pa ON pr.object_id = pa.object_id AND pa.name LIKE '%{1}%' 
										   WHERE s.name LIKE '%{1}%' OR pr.name LIKE '%{1}%' OR pa.name LIKE '%{1}%' OR m.definition LIKE '%{1}%'", database, query);
            }
            return sql;
        }

        public List<Procedure> FindProcedures(string database, string query = null)
        {
            var procedures = new List<Procedure>();
            string sql = GetFindStoredProceduresSql(database, query);
            try
            {
                using (var reader = ExecuteSql(sql))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var parameterName = reader.GetString(reader.GetOrdinal("parameterName"));
                            var procedure = new Procedure
                            {
                                ObjectId = reader.GetInt32(reader.GetOrdinal("object_id")),
                                SchemaName = reader.GetString(reader.GetOrdinal("schemaName")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                ParameterName = !string.IsNullOrEmpty(query) && parameterName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ? parameterName : null,
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("create_date")),
                                ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modifyDate")),
                                LastExecutionTime = reader.GetDateTime(reader.GetOrdinal("lastExecutionTime")),
                                Definition = reader.GetString(reader.GetOrdinal("definition")),
                            };
                            procedures.Add(procedure);
                        }
                        FindLastInteractionInfo(database, procedures);
                    }
                }
            }
            catch
            {
                // Do nothing
            }
            return procedures;
        }

        public string GetLastInteractionInfoSql(string database, IEnumerable<long> objectIds)
        {
            string sql = string.Format(@"SELECT ius.object_id, ISNULL(st.last_execution_time,'') AS lastExecutionTime
                                           FROM {0}.sys.dm_exec_procedure_stats st 
                                          WHERE st.object_id IN ({1})", database, String.Join(",", objectIds));
            return sql;
        }

        public void FindLastInteractionInfo(string database, List<Procedure> procedureList)
        {
            try
            {
                var objectIds = procedureList.Select(p => p.ObjectId);
                string sql = GetLastInteractionInfoSql(database, objectIds);
                using (var reader = ExecuteSql(sql))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var objectId = reader.GetInt64(reader.GetOrdinal("object_id"));
                            var view = procedureList.FirstOrDefault(p => p.ObjectId == objectId);
                            if (view != null)
                            {
                                view.LastExecutionTime = reader.GetDateTime(reader.GetOrdinal("lastExecutionTime"));
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

        public string GetFindStoredProcedureExtendedPropertiesSql(string database, string query)
        {
            string sql = string.Format(@"SELECT s.name AS schemaName, pr.name AS procName, ISNULL(pa.name,ISNULL(pa2.name,'')) AS parameterName, epp.name, epp.value
										   FROM {0}.sys.procedures pr
										  INNER JOIN {0}.sys.schemas s ON pr.schema_id = s.schema_id
										   LEFT OUTER JOIN {0}.sys.parameters pa ON pr.object_id = pa.object_id AND pa.name LIKE '%{1}%'
										   LEFT OUTER JOIN {0}.sys.parameters pa2 ON pr.object_id = pa2.object_id
										  INNER JOIN {0}.sys.extended_properties epp ON pr.object_id = epp.major_id AND epp.minor_id=pa2.parameter_id
										  WHERE s.name LIKE '%{1}%' OR pr.name LIKE '%{1}%' OR pa.name LIKE '%{1}%' OR cast(epp.value AS varchar) LIKE '%{1}%'
										 UNION  
										 SELECT s.name AS schemaName, pr.name, '', ep.name, ep.value
										   FROM {0}.sys.procedures pr
										  INNER JOIN {0}.sys.schemas s ON pr.schema_id = s.schema_id
										  INNER JOIN {0}.sys.extended_properties ep ON pr.object_id = ep.major_id AND ep.minor_id = 0
										  WHERE s.name LIKE '%{1}%' OR pr.name LIKE '%{1}%' OR cast(ep.value AS varchar) LIKE '%{1}%'", database, query);
            return sql;
        }

        public List<ProcedureExtendedProperty> FindProcedureExtendedProperties(string database, string query)
        {
            var properties = new List<ProcedureExtendedProperty>();
            string sql = GetFindStoredProcedureExtendedPropertiesSql(database, query);
            try
            {
                using (var reader = ExecuteSql(sql))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var property = new ProcedureExtendedProperty
                            {
                                SchemaName = reader.GetString(reader.GetOrdinal("schemaName")),
                                ProcedureName = reader.GetString(reader.GetOrdinal("procName")),
                                ParameterName = reader.GetString(reader.GetOrdinal("parameterName")),
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
