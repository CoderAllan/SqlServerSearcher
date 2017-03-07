namespace SQLServerSearcher.DAL
{
	using System.Data;
	using System.Data.SqlClient;

	using Model;

	public class Searches
	{
		private readonly ApplicationState _appState;

		public Searches(ApplicationState appState)
		{
			_appState = appState;
		}

		protected SqlDataReader ExecuteSql(string sql)
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
