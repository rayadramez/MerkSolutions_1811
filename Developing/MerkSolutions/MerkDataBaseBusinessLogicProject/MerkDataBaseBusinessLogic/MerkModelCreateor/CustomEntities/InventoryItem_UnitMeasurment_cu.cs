using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InventoryItem_UnitMeasurment_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<InventoryItem_UnitMeasurment_cu> _items;
		public static List<InventoryItem_UnitMeasurment_cu> ItemsList = new List<InventoryItem_UnitMeasurment_cu>();

		#region ColumnNames

		public static String InventoryItem_CU_ID_ColumnaName
		{
			get { return "InventoryItem_CU_ID"; }
		}

		public static String UnitMeasurment_CU_ID_ColumnaName
		{
			get { return "UnitMeasurment_CU_ID"; }
		}

		#endregion

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.InventoryItem_UnitMeasurment_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "InventoryItem_UnitMeasurment_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.InventoryItem_UnitMeasurment_cu.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.InventoryItem_UnitMeasurment_cu.Where(item => item.IsOnDuty).ToList();
			return true;
		}

		public InventoryItem_cu InventoryItem
		{
			get
			{
				InventoryItem_cu inventoryItem =
					InventoryItem_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InventoryItem_CU_ID)));
				return inventoryItem;
			}
		}

		public UnitMeasurment_cu UnitMeasurment
		{
			get
			{
				UnitMeasurment_cu unitMeasurment =
					UnitMeasurment_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(UnitMeasurment_CU_ID)));
				return unitMeasurment;
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
	}
}
