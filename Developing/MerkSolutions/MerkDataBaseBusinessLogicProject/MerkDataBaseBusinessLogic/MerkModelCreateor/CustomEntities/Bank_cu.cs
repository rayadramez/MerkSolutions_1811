using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class Bank_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<Bank_cu> _items;
		public static List<Bank_cu> ItemsList = new List<Bank_cu>();

		#region ColumnNames

		public static String Description_ColumnaName
		{
			get { return "Description"; }
		}

		#endregion

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.Bank_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "Bank_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.Bank_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		public override bool LoadFromDB
		{
			get { return true; }
		}

		public override DBCommonEntitiesType TableType
		{
			get { return DBCommonEntitiesType.CustomUserEntities; }
		}

		public override bool LoadItemsList()
		{
			ItemsList.Clear();
			ItemsList = DBContext_External.Bank_cu.Where(item => item.IsOnDuty).OrderBy(item => item.ID).ToList();
			return true;
		}

		public ChartOfAccount_cu ChartOfAccount
		{
			get
			{
				if (ChartOfAccount_CU_ID == null)
					return null;
				ChartOfAccount_cu chartOfAccount =
					ChartOfAccount_cu.ItemsList.Find(item => Convert.ToInt32(item.ID)
						                                 .Equals(Convert.ToInt32(ChartOfAccount_CU_ID)));
				return chartOfAccount;
			}
		}

		public string ChartOfAccountName
		{
			get
			{
				if (ChartOfAccount == null)
					return string.Empty;
				return ChartOfAccount.Name_P;
			}
		}
	}
}
