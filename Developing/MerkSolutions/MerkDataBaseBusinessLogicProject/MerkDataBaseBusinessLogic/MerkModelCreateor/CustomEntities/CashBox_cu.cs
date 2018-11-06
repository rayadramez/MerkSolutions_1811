using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class CashBox_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<CashBox_cu> _items;
		public static List<CashBox_cu> ItemsList = new List<CashBox_cu>();

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
			get { return (int)DB_TableIdentity.CashBox_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "CashBox_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.CashBox_cu.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.CashBox_cu.Where(item => item.IsOnDuty).OrderBy(item => item.Name_P).ToList();
			return true;
		}

		public string CashBoxFullName
		{
			get
			{
				string fullName = Name_P;

				if (Floor_CU_ID == null)
					return fullName;
				Floor_cu floor = Floor_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Floor_CU_ID)));
				if (floor == null)
					return fullName;

				fullName = fullName + "-" + floor.Name_P;

				Location_cu location =
					Location_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(floor.Location_CU_ID)));

				if (location == null)
					return fullName;

				return fullName + "-" + location.Name_P;
			}
		}
	}
}
