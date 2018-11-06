using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InventoryItem_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<InventoryItem_cu> _items;
		public static List<InventoryItem_cu> ItemsList = new List<InventoryItem_cu>();

		#region ColumnNames

		public static String InventoryHousing_CU_ID_ColumnaName
		{
			get { return "InventoryHousing_CU_ID"; }
		}

		public static String InventoryItemCategory_CU_ID_ColumnaName
		{
			get { return "InventoryItemCategory_CU_ID"; }
		}

		public static String InventoryItemBrand_CU_ID_ColumnaName
		{
			get { return "InventoryItemBrand_CU_ID"; }
		}

		public static String DefaultBarcode_ColumnaName
		{
			get { return "DefaultBarcode"; }
		}

		public static String SellingPrice_ColumnaName
		{
			get { return "SellingPrice"; }
		}

		public static String ExpirationDate_ColumnaName
		{
			get { return "ExpirationDate"; }
		}

		public static String InventoryItemType_P_ID_ColumnaName
		{
			get { return "InventoryItemType_P_ID"; }
		}

		public static String Description_ColumnaName
		{
			get { return "Description"; }
		}

		public static String InternalCode_ColumnaName
		{
			get { return "InternalCode"; }
		}

		#endregion

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.InventoryItem_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "InventoryItem_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.InventoryItem_cu.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.InventoryItem_cu.Where(item => item.IsOnDuty).OrderBy(item => item.Name_P).ToList();
			return true;
		}

		public UnitMeasurment_cu InventoryTrackingUnitMeasurment
		{
			get
			{
				if (InventoryTrackingUnitMeasurment_CU_ID == null)
					return null;

				UnitMeasurment_cu unitMeasurment =
					UnitMeasurment_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InventoryTrackingUnitMeasurment_CU_ID)));
				return unitMeasurment;
			}
		}

	}
}
