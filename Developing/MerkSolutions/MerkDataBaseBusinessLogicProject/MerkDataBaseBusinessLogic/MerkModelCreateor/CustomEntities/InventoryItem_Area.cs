using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InventoryItem_Area : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<InventoryItem_Area> _items;
		public static List<InventoryItem_Area> ItemsList = new List<InventoryItem_Area>();

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
			get { return (int)DB_TableIdentity.InventoryItem_Area; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "InventoryItem_Area"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.InventoryItem_Area.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.InventoryItem_Area.Where(item => item.IsOnDuty).OrderBy(item => item.ID).ToList();
			return true;
		}

		public string InventoryItemName
		{
			get
			{
				InventoryItem_cu inventoryItem = InventoryItem_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InventoryItemID)));
				if (inventoryItem == null) 
					return String.Empty;
				return inventoryItem.Name_P;
			}
		}

		public double Area
		{
			get { return Width * Height; }
		}

		public double TotalArea
		{
			get { return Width * Height * Count; }
		}
	}
}
