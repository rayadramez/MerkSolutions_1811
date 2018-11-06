using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class FinanceInvoiceShare : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<FinanceInvoiceShare> _items;
		public static List<FinanceInvoiceShare> ItemsList = new List<FinanceInvoiceShare>();

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

			ItemsList = DBContext_External.FinanceInvoiceShares.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.FinanceInvoiceShare; }
		}

		public List<string> ChildrenItemsList { get; private set; }

		public string EntityName
		{
			get { return "FinanceInvoiceShare"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.FinanceInvoiceShares.FirstOrDefault(item => item.FinanceInvoiceID.Equals(id));
		}
	}
}
