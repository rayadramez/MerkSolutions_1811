using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class PatientDepositeBalance : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<PatientDepositeBalance> _items;
		public static List<PatientDepositeBalance> ItemsList = new List<PatientDepositeBalance>();
		public static List<PatientDepositeBalance> AllItemsList = new List<PatientDepositeBalance>();

		#region ColumnNames

		public static String Serial_ColumnaName
		{
			get { return "Serial"; }
		}

		#endregion

		public override bool LoadFromDB
		{
			get { return false; }
		}

		public override DBCommonEntitiesType TableType
		{
			get { return DBCommonEntitiesType.TransactionsEntities; }
		}

		public override bool LoadItemsList()
		{
			ItemsList.Clear();

			ItemsList = AllItemsList = DBContext_External.PatientDepositeBalances.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.PatientDepositeBalance; }
		}

		public List<string> ChildrenItemsList { get; set; }

		public string EntityName
		{
			get { return "PatientDepositeBalance"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.PatientDepositeBalances.FirstOrDefault(item => item.ID.Equals(id));
		}
	}
}
