using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InventoryItemPrice_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<InventoryItemPrice_cu> _items;
		public static List<InventoryItemPrice_cu> ItemsList = new List<InventoryItemPrice_cu>();

		#region ColumnNames

		public static String InventoryItem_CU_ID_ColumnaName
		{
			get { return "InventoryItem_CU_ID"; }
		}

		public static String InventoryItem_UnitMeasurment_CU_ID_ColumnaName
		{
			get { return "InventoryItem_UnitMeasurment_CU_ID"; }
		}

		public static String Date_ColumnaName
		{
			get { return "Date"; }
		}

		public static String SellingPrice_ColumnaName
		{
			get { return "SellingPrice"; }
		}

		public static String DefaultPurchasingPrice_ColumnaName
		{
			get { return "DefaultPurchasingPrice"; }
		}

		#endregion

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.InventoryItemPrice_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "InventoryItemPrice_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.InventoryItemPrice_cu.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.InventoryItemPrice_cu.Where(item => item.IsOnDuty).ToList();
			return true;
		}

		public string InventoryItemName
		{
			get
			{
				InventoryItem_cu inventoryItem =
					InventoryItem_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InventoryItem_CU_ID)));
				if (inventoryItem != null)
					return inventoryItem.Name_P;

				return string.Empty;
			}
		}

		public string UnitMeasurmentName
		{
			get
			{
				InventoryItem_UnitMeasurment_cu inventoryItem =
					InventoryItem_UnitMeasurment_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InventoryItem_UnitMeasurment_CU_ID)));
				if (inventoryItem != null)
				{
					UnitMeasurment_cu unitMeasurment =
						UnitMeasurment_cu.ItemsList.Find(
							item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(inventoryItem.UnitMeasurment_CU_ID)));
					if (unitMeasurment != null)
						return unitMeasurment.Name_P;
				}

				return string.Empty;
			}
		}

		public string CustomerName
		{
			get
			{
				if (Customer_CU_ID == null)
					return string.Empty;

				Customer_cu customer =
					Customer_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Customer_CU_ID)));
				if (customer != null)
					if (customer.FullName != null)
						return customer.FullName.ToString();

				return string.Empty;
			}
		}

		public string SupplierName
		{
			get
			{
				if (Supplier_CU_ID == null)
					return string.Empty;

				Supplier_cu supplier =
					Supplier_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Supplier_CU_ID)));
				if (supplier != null)
					if (supplier.FullName != null)
						return supplier.FullName.ToString();

				return string.Empty;
			}
		}

		public string PriceTypeName
		{
			get
			{
				PriceType_p priceType =
					PriceType_p.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(PriceType_P_ID)));
				if (priceType != null)
					return priceType.Name_P;

				return string.Empty;
			}
		}

	}
}
