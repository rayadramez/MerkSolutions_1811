using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class ActiveSalaryEffect_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<Person_cu> _items;
		public static List<ActiveSalaryEffect_cu> ItemsList = new List<ActiveSalaryEffect_cu>();

		#region Columns Names

		public static string InternalCode_ColumnaName
		{
			get { return "InternalCode"; }
		}

		#endregion
	}
}
