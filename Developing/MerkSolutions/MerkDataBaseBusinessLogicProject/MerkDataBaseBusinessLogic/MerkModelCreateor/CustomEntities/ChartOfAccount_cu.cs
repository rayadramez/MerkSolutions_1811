using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class ChartOfAccount_cu : DBCommon, IDBCommon
	{
		public static List<ChartOfAccount_cu> ItemsList = new List<ChartOfAccount_cu>();

		#region ColumnNames

		public static String ParentChartOfAccount_CU_ID_ColumnaName
		{
			get { return "ParentChartOfAccount_CU_ID"; }
		}

		public static String Serial_ColumnaName
		{
			get { return "Serial"; }
		}

		public static String ChartOfAccountCodeMargin_P_ID_ColumnaName
		{
			get { return "ChartOfAccountCodeMargin_P_ID"; }
		}

		public static String IsDebit_ColumnaName
		{
			get { return "IsDebit"; }
		}

		public static String Description_ColumnaName
		{
			get { return "Description"; }
		}

		#endregion

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
			ItemsList = DBContext_External.ChartOfAccount_cu.Where(item => item.IsOnDuty).OrderBy(item => item.Name_P).ToList();
			return true;
		}

		#region Implementation of IDBCommon

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.ChartOfAccount_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "ChartOfAccount_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.ChartOfAccount_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion

		public ChartOfAccount_cu ParentChartOfAccount
		{
			get
			{
				if (ParentChartOfAccount_CU_ID == null)
					return null;
				return ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(ParentChartOfAccount_CU_ID)));
			}
		}

		public string ParentChartOfAccountName
		{
			get
			{
				if (ParentChartOfAccount == null)
					return string.Empty;
				return ParentChartOfAccount.Name_P;
			}
		}

		public List<ChartOfAccount_cu> ChildrenChartOfAccountsList
		{
			get
			{
				return
					ItemsList.FindAll(
						item =>
							item.ParentChartOfAccount_CU_ID != null &&
							Convert.ToDouble(item.ParentChartOfAccount_CU_ID).Equals(Convert.ToDouble(ID)));
			}
		}

		public ChartOfAccountCodeMargin_p ChartOfAccountCodeMargin
		{
			get
			{
				ChartOfAccountCodeMargin_p chartOfAccountCodeMargin =
					ChartOfAccountCodeMargin_p.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(ChartOfAccountCodeMargin_P_ID)));
				return chartOfAccountCodeMargin;
			}
		}

		public string ChartOfAccountCodeMarginName
		{
			get
			{
				if (ChartOfAccountCodeMargin == null)
					return string.Empty;
				return ChartOfAccountCodeMargin.Name_P;
			}
		}

		public bool HasParent
		{
			get { return ParentChartOfAccount_CU_ID != null; }
		}

		public bool HasChildren
		{
			get
			{
				ChartOfAccount_cu anyChild = ItemsList.Find(item => item.ParentChartOfAccount_CU_ID.Equals(ID));
				return anyChild != null;
			}
		}
	}
}
