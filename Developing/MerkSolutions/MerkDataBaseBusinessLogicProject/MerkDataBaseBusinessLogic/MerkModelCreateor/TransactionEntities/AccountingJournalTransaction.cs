using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class AccountingJournalTransaction : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<AccountingJournalTransaction> _items;
		public static List<AccountingJournalTransaction> ItemsList = new List<AccountingJournalTransaction>();

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

			ItemsList = DBContext_External.AccountingJournalTransactions.ToList();
			return true;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.AccountingJournalTransaction; }
		}

		public List<string> ChildrenItemsList
		{
			get
			{
				List<string> list = new List<string>();
				list.Add("AccountingJournalTransaction");
				return list;
			}
		}

		public string EntityName
		{
			get { return "AccountingJournalTransaction"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.AccountingJournalTransactions.FirstOrDefault(item => item.ID.Equals(id));
		}

		private List<AccountingJournalEntryTransaction> _list_AccountingJournalEntryTransaction;
		public List<AccountingJournalEntryTransaction> List_AccountingJournalEntryTransaction
		{
			get { return _list_AccountingJournalEntryTransaction ?? (_list_AccountingJournalEntryTransaction = AccountingJournalEntryTransactions.ToList()); }
			set { _list_AccountingJournalEntryTransaction = value; }
		}
	}
}
