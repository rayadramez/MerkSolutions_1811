namespace MerkDataBaseBusinessLogicProject
{
	public class DBConnectionManager : MerkFinanceEntities
	{
		private static string _entityName = "MerkFinanceEntities";
		private static string _userName = "merkuser";
		private static string _password = "m3rkus3r";

		private static DBConnectionManager _dbConnectionManager;

		public DBConnectionManager(string serverName, string databaseName)
			: base(GetMerkFinanceConnectionString(serverName, databaseName))
		{
			
		}

		public static DBConnectionManager Instance(string serverName = "", string dataBaseName = "")
		{
			if (_dbConnectionManager == null && serverName == "")
				return null;
			if (_dbConnectionManager == null)
				_dbConnectionManager = new DBConnectionManager(serverName, dataBaseName);

			return _dbConnectionManager;
		}

		public static string GetMerkFinanceConnectionString(string serverName, string databaseName)
		{
			string connectionstring2 =
				string.Format(
				@"metadata=res://*/MerkFinanceEntities.csdl|res://*/MerkFinanceEntities.ssdl|res://*/MerkFinanceEntities.msl;
				provider=System.Data.SqlClient;
				provider connection string=""data source={0};
				initial catalog={1};
				persist security info=True;
				user id={2};password={3};
				MultipleActiveResultSets=True;
				App=EntityFramework"""
				, serverName, databaseName, _userName, _password);

			return connectionstring2;
		}

		public static string GetMerkAuditingConnectionString(string serverName, string databaseName)
		{
			string connectionstring2 =
				string.Format(
					@"metadata=res://*/MerkAuditing.csdl|res://*/MerkAuditing.ssdl|res://*/MerkAuditing.msl;
				provider=System.Data.SqlClient;
				provider connection string=""data source={0};
				initial catalog={1};
				persist security info=True;
				user id={2};password={3};
				MultipleActiveResultSets=True;
				App=EntityFramework"""
					, serverName, databaseName, _userName, _password);

			return connectionstring2;
		}

		private static string GetProviderName(string providerName)
		{
			return string.Format(" providerName=\"{0}\"", providerName);
		}

		
	}
}
