using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InvoiceShare : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<InvoiceShare> _items;
		public static List<InvoiceShare> ItemsList = new List<InvoiceShare>();
		public static List<InvoiceShare> AllItemsList = new List<InvoiceShare>();

		#region ColumnNames

		public static String Serial_ColumnaName
		{
			get { return "Serial"; }
		}

		#endregion

		public override int ID
		{
			get { return InvoiceID; }
			set { InvoiceID = Convert.ToInt32(value); }
		}

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
			
			ItemsList = AllItemsList = DBContext_External.InvoiceShares.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.InvoiceShare; }
		}

		public List<string> ChildrenItemsList { get; set; }

		public string EntityName
		{
			get { return "InvoiceShare"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.InvoiceShares.FirstOrDefault(item => item.InvoiceID.Equals(id));
		}
	}
}
