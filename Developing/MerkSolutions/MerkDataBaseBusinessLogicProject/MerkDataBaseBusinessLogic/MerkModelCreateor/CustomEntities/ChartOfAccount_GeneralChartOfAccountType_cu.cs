using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class ChartOfAccount_GeneralChartOfAccountType_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<ChartOfAccount_GeneralChartOfAccountType_cu> _items;
		public static List<ChartOfAccount_GeneralChartOfAccountType_cu> ItemsList = new List<ChartOfAccount_GeneralChartOfAccountType_cu>();

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
			get { return (int)DB_TableIdentity.ChartOfAccount_GeneralChartOfAccountType_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "ChartOfAccount_GeneralChartOfAccountType_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.ChartOfAccount_GeneralChartOfAccountType_cu.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.ChartOfAccount_GeneralChartOfAccountType_cu.Where(item => item.IsOnDuty).ToList();
			return true;
		}

		public ChartOfAccount_cu ChartOfAccount
		{
			get
			{
				return
					ChartOfAccount_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(ChartOfAccount_CU_ID)));
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

		public GeneralChartOfAccountType_cu GeneralChartOfAccountType
		{
			get
			{
				return
					GeneralChartOfAccountType_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(GeneralChartOfAccountType_CU_ID)));
			}
		}

		public string GeneralChartOfAccountTypeName
		{
			get
			{
				if (GeneralChartOfAccountType == null)
					return string.Empty;

				return GeneralChartOfAccountType.Name_P;
			}
		}
	}
}
