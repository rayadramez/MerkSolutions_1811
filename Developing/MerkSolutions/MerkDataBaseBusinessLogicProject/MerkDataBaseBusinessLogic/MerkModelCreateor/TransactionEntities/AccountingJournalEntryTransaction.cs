using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class AccountingJournalEntryTransaction : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<AccountingJournalEntryTransaction> _items;
		public static List<AccountingJournalEntryTransaction> ItemsList = new List<AccountingJournalEntryTransaction>();

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

			ItemsList = DBContext_External.AccountingJournalEntryTransactions.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.AccountingJournalEntryTransaction; }
		}

		public List<string> ChildrenItemsList
		{
			get
			{
				List<string> list = new List<string>();
				list.Add("AccountingJournalEntryTransactionDetail");
				return list;
			}
		}

		public string EntityName
		{
			get { return "AccountingJournalEntryTransaction"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.AccountingJournalEntryTransactions.FirstOrDefault(item => item.ID.Equals(id));
		}
	}
}
