namespace SQLServerSearcher.DAL
{
    using Contracts;
    using Model;

    public class Server : Searches, IServer
    {
        private readonly ApplicationState _appState;

        public Server(ApplicationState appState) : base(appState)
        {
            _appState = appState;
        }

        public ServerInfo GetServerInfo()
        {
            const string sql = "SELECT i.sqlserver_start_time, i.cpu_count, m.total_physical_memory_kb, m.available_physical_memory_kb FROM sys.dm_os_sys_info i join [sys].[dm_os_sys_memory] m on 1=1";
            ServerInfo serverInfo = null;
            try
            {
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
            }
            catch
            {
                serverInfo = new ServerInfo
                {
                    ServerVersion = _appState.CurrentConnection.ServerVersion,
                    PhysicalMemory = -1,
                    AvailablePhysicalMemory = -1,
                    CPUCount = -1,
                };
            }
            return serverInfo;
        }
    }
}
