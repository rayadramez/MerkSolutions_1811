using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InventoryItemGroup_InventoryItem_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<InventoryItemGroup_InventoryItem_cu> _items;
		public static List<InventoryItemGroup_InventoryItem_cu> ItemsList = new List<InventoryItemGroup_InventoryItem_cu>();

		#region ColumnNames

		public static String Floor_CU_ID_ColumnaName
		{
			get { return "Floor_CU_ID"; }
		}

		public static String IsMain_ColumnaName
		{
			get { return "IsMain"; }
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
			get { return (int)DB_TableIdentity.InventoryItemGroup_InventoryItem_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "InventoryItemGroup_InventoryItem_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.InventoryItemGroup_InventoryItem_cu.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.InventoryItemGroup_InventoryItem_cu.Where(item => item.IsOnDuty).ToList();
			return true;
		}

		public InventoryItemGroup_cu InventoryItemGroup
		{
			get
			{
				InventoryItemGroup_cu itemsGroup = InventoryItemGroup_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InvetoryItemGroup_CU_ID)));
				return itemsGroup;
			}
		}

		public string InventoryItemGroupName
		{
			get
			{
				if (InventoryItemGroup == null)
					return string.Empty;

				return InventoryItemGroup.Name_P;
			}
		}

		public InventoryItem_cu InventoryItem
		{
			get
			{
				InventoryItem_cu inventoryItem = InventoryItem_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InventoryItem_CU_ID)));
				return inventoryItem;
			}
		}

		public string InventoryItemName
		{
			get
			{
				if (InventoryItem == null)
					return string.Empty;

				return InventoryItem.Name_P;
			}
		}

	}
}
