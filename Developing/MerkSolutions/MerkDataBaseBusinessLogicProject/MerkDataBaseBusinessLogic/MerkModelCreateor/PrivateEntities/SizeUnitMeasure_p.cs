using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class SizeUnitMeasure_p : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<SizeUnitMeasure_p> _items;
		public static List<SizeUnitMeasure_p> ItemsList = new List<SizeUnitMeasure_p>();

		#region ColumnNames

		public static String Serial_ColumnaName
		{
			get { return "Serial"; }
		}

		#endregion

		public override bool LoadFromDB
		{
			get { return true; }
		}

		public override DBCommonEntitiesType TableType
		{
			get { return DBCommonEntitiesType.PrivateInternalEntities; }
		}

		public override bool LoadItemsList()
		{
			ItemsList.Clear();
			ItemsList = DBContext_External.SizeUnitMeasure_p.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.SizeUnitMeasure_p; }
		}

		public string EntityName
		{
			get { return "SizeUnitMeasure_p"; }
		}


		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}
	}
}
