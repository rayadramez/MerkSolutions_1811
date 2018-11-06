using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class MerkAuditingEntities
	{
		public MerkAuditingEntities(string connectionString)
			: base(connectionString)
		{
		}

		public static MerkAuditingEntities _context;

		public static MerkAuditingEntities DBAuditingEntities
		{
			get
			{
				if (_context == null)
					return new MerkAuditingEntities(
						DBConnectionManager.GetMerkAuditingConnectionString(DBCommon.ServerName, DBCommon.DBName));
				return _context;
			}
		}
	}
}
