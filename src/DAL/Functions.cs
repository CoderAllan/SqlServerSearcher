namespace SQLServerSearcher.DAL
{
    using System;
    using System.Collections.Generic;

    using Contracts;
    using Model;

    public class Functions : Searches, IFunctions
    {
        public Functions(ApplicationState appState) : base(appState)
        {
        }

        public string GetFindFunctionsSql(string database, string query)
        {
            string sql = string.Format(@"SELECT s.name AS schemaName, o.name, ISNULL(pa.name,'') AS parameterName, o.create_date, ISNULL(o.modify_date,'') AS modifyDate, m.definition
										   FROM {0}.sys.objects o
										  INNER JOIN {0}.sys.schemas s ON o.schema_id = s.schema_id
										  INNER JOIN {0}.sys.sql_modules m ON o.object_id = m.object_id
										   LEFT OUTER JOIN {0}.sys.dm_exec_procedure_stats st on o.object_id = st.object_id", database);
            if (!string.IsNullOrEmpty(query))
            {
                sql += Environment.NewLine;
                sql += string.Format(@"   LEFT OUTER JOIN {0}.sys.parameters pa ON o.object_id = pa.object_id AND pa.name LIKE '%{1}%' 
										   WHERE o.type_desc like '%FUNCTION%' AND (s.name LIKE '%{1}%' OR o.name LIKE '%{1}%' OR pa.name LIKE '%{1}%' OR m.definition LIKE '%{1}%')", database, query);
            }
            return sql;
        }

        public List<Function> FindFunctions(string database, string query = null)
        {
            var functions = new List<Function>();
            string sql = GetFindFunctionsSql(database, query);
            using (var reader = ExecuteSql(sql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var parameterName = reader.GetString(reader.GetOrdinal("parameterName"));
                        var function = new Function
                        {
                            SchemaName = reader.GetString(reader.GetOrdinal("schemaName")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            ParameterName = !string.IsNullOrEmpty(query) && parameterName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ? parameterName : null,
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

        public string GetFindFunctionExtendedPropertiesSql(string database, string query)
        {
            string sql = string.Format(@"SELECT s.name AS schemaName, o.name AS procName, ISNULL(pa.name,ISNULL(pa2.name,'')) AS parameterName, epp.name, epp.value
										   FROM {0}.sys.objects o
										  INNER JOIN {0}.sys.schemas s ON o.schema_id = s.schema_id
										   LEFT OUTER JOIN {0}.sys.parameters pa ON o.object_id = pa.object_id AND pa.name LIKE '%{1}%'
										   LEFT OUTER JOIN {0}.sys.parameters pa2 ON o.object_id = pa2.object_id
										  INNER JOIN {0}.sys.extended_properties epp ON o.object_id = epp.major_id AND epp.minor_id=pa2.parameter_id
										  WHERE o.type_desc LIKE '%FUNCTION%' AND (s.name LIKE '%{1}%' OR o.name LIKE '%{1}%' OR pa.name LIKE '%{1}%' OR cast(epp.value AS varchar) LIKE '%{1}%')
										 UNION  
										 SELECT s.name AS schemaName, o.name, '', ep.name, ep.value
										   FROM {0}.sys.objects o
										  INNER JOIN {0}.sys.schemas s ON o.schema_id = s.schema_id
										  INNER JOIN {0}.sys.extended_properties ep ON o.object_id = ep.major_id AND ep.minor_id = 0
										  WHERE o.type_desc LIKE '%FUNCTION%' AND (s.name LIKE '%{1}%' OR o.name LIKE '%{1}%' OR CAST(ep.value AS varchar) LIKE '%{1}%')", database, query);

            return sql;
        }

        public List<FunctionExtendedProperty> FindFunctionExtendedProperties(string database, string query)
        {
            var properties = new List<FunctionExtendedProperty>();
            string sql = GetFindFunctionExtendedPropertiesSql(database, query);
            using (var reader = ExecuteSql(sql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var property = new FunctionExtendedProperty
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
            return properties;
        }
    }
}
