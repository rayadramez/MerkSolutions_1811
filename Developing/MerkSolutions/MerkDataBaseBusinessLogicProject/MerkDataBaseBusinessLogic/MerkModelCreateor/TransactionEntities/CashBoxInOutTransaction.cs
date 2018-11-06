using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class CashBoxInOutTransaction : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<CashBoxInOutTransaction> _items;
		public static List<CashBoxInOutTransaction> ItemsList = new List<CashBoxInOutTransaction>();

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

			ItemsList = DBContext_External.CashBoxInOutTransactions.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.CashBoxInOutTransaction; }
		}

		public List<string> ChildrenItemsList
		{
			get
			{
				List<string> list = new List<string>();
				list.Add("CashBoxInOutTransactionDetail");
				return list;
			}
		}

		public string EntityName
		{
			get { return "CashBoxInOutTransaction"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.CashBoxInOutTransactions.FirstOrDefault(item => item.ID.Equals(id));
		}
	}
}
