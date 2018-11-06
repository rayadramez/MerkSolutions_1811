using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class PersonType_ChartOfAccount_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<PersonType_ChartOfAccount_cu> _items;
		public static List<PersonType_ChartOfAccount_cu> ItemsList = new List<PersonType_ChartOfAccount_cu>();

		#region ColumnNames

		public static String Floor_CU_ID_ColumnaName
		{
			get { return "Floor_CU_ID"; }
		}

		public static String IsMain_ColumnaName
		{
			get { return "IsMain"; }
		}

		public static String ChartOfAccount_CU_ID_ColumnaName
		{
			get { return "ChartOfAccount_CU_ID"; }
		}

		public static String InternalCode_ColumnaName
		{
			get { return "InternalCode"; }
		}

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
			get { return (int)DB_TableIdentity.PersonType_ChartOfAccount_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "PersonType_ChartOfAccount_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.PersonType_ChartOfAccount_cu.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.PersonType_ChartOfAccount_cu.Where(item => item.IsOnDuty).ToList();
			return true;
		}

		public ChartOfAccount_cu ChartOfAccount
		{
			get
			{
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

		public PersonType_p PersonType
		{
			get
			{
				PersonType_p personType =
					PersonType_p.ItemsList.Find(item => Convert.ToInt32(item.ID)
														 .Equals(Convert.ToInt32(PersonType_P_ID)));
				return personType;
			}
		}

		public string PersonTypeName
		{
			get
			{
				if (PersonType == null)
					return string.Empty;
				return PersonType.Name_P;
			}
		}

		public PersonChartOtAccountType_p PersonChartOtAccountType
		{
			get
			{
				PersonChartOtAccountType_p personType =
					PersonChartOtAccountType_p.ItemsList.Find(item => Convert.ToInt32(item.ID)
													.Equals(Convert.ToInt32(PersonChartOfAccountType_P_ID)));
				return personType;
			}
		}

		public string PersonChartOtAccountTypeName
		{
			get
			{
				if (PersonChartOtAccountType == null)
					return string.Empty;
				return PersonChartOtAccountType.Name_P;
			}
		}
	}
}
