using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InventoryItemTransactionType_p : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<InventoryItemTransactionType_p> _items;
		public static List<InventoryItemTransactionType_p> ItemsList = new List<InventoryItemTransactionType_p>();

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
			ItemsList = DBContext_External.InventoryItemTransactionType_p.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.InventoryItemTransactionType_p; }
		}

		public string EntityName
		{
			get { return "InventoryItemTransactionType_p"; }
		}


		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}
	}
}
