using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InventoryItemTransaction : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<InventoryItemTransaction> _items;
		public static List<InventoryItemTransaction> ItemsList = new List<InventoryItemTransaction>();

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
			get { return (int)DB_TableIdentity.InventoryItemTransaction; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "InventoryItemTransaction"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.InventoryItemTransactions.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.InventoryItemTransactions.Where(item => item.IsOnDuty).ToList();
			return true;
		}

		public InventoryItem_cu InventoryItem
		{
			get
			{
				if (InventoryItem_CU_ID == null)
					return null;

				InventoryItem_cu inventoryItem =
					InventoryItem_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32((int) InventoryItem_CU_ID)));
				return inventoryItem;
			}
		}

		public UnitMeasurment_cu UnitMeasurment
		{
			get
			{
				if (UnitMeasurment_CU_ID == null)
					return null;

				UnitMeasurment_cu unitMeasurment =
					UnitMeasurment_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32((int) UnitMeasurment_CU_ID)));
				return unitMeasurment;
			}
		}

		public InventoryHousing_cu InventoryHousing
		{
			get
			{
				if (InventoryHousing_CU_ID == null)
					return null;

				InventoryHousing_cu inventoryHousing =
					InventoryHousing_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32((int) InventoryHousing_CU_ID)));
				return inventoryHousing;
			}
		}

		public string InventoryItemName
		{
			get
			{
				if (InventoryItem != null)
					return InventoryItem.Name_P;
				return String.Empty;
			}
		}

		public string UnitMeasurmentName
		{
			get
			{
				if (UnitMeasurment != null)
					return UnitMeasurment.Name_P;
				return String.Empty;
			}
		}

		public string InventoryHousingName
		{
			get
			{
				if (InventoryHousing != null)
					return InventoryHousing.Name_P;
				return String.Empty;
			}
		}
	}
}
