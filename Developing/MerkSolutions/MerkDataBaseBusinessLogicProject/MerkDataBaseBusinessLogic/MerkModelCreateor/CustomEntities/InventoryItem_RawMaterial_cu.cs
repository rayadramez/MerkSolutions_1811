using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InventoryItem_RawMaterial_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<InventoryItem_RawMaterial_cu> _items;
		public static List<InventoryItem_RawMaterial_cu> ItemsList = new List<InventoryItem_RawMaterial_cu>();

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
			get { return (int)DB_TableIdentity.InventoryItem_RawMaterial_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "InventoryItem_RawMaterial_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.InventoryItem_RawMaterial_cu.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.InventoryItem_RawMaterial_cu.Where(item => item.IsOnDuty).OrderBy(item => item.ID).ToList();
			return true;
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
					return String.Empty;
				return InventoryItem.Name_P;
			}
		}

		public RawMaterials_cu RawMaterials
		{
			get
			{
				RawMaterials_cu rawMaterial = RawMaterials_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(RawMaterial_CU_ID)));
				return rawMaterial;
			}
		}

		public string RawMaterialsName
		{
			get
			{
				if (RawMaterials == null)
					return String.Empty;
				return RawMaterials.Name_P;
			}
		}
	}
}
