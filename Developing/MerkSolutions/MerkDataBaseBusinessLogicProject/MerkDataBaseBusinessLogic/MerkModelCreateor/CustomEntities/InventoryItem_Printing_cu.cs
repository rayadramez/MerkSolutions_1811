using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InventoryItem_Printing_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<InventoryItem_Printing_cu> _items;
		public static List<InventoryItem_Printing_cu> ItemsList = new List<InventoryItem_Printing_cu>();

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
			get { return (int)DB_TableIdentity.InventoryItem_Printing_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "InventoryItem_Printing_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.InventoryItem_Printing_cu.FirstOrDefault(item => item.ID.Equals(id));
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
			ItemsList = DBContext_External.InventoryItem_Printing_cu.Where(item => item.IsOnDuty).OrderBy(item => item.ID).ToList();
			return true;
		}

		public string InventoryItemName
		{
			get
			{
				InventoryItem_cu inventoryItem = InventoryItem_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InventoryItem_CU_ID)));
				if (inventoryItem == null)
					return String.Empty;
				return inventoryItem.Name_P;
			}
		}

		public string RawMaterialName
		{
			get
			{
				RawMaterials_cu rawMaterials = RawMaterials_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(RawMaterial_CU_ID)));
				if (rawMaterials == null)
					return String.Empty;
				return rawMaterials.Name_P;
			}
		}

		public double CalculatedCost
		{
			get
			{
				double totalMinutes = Convert.ToDouble(TotalMinutes);
				double lightMinutes = Convert.ToDouble(LightMinutes);
				double unitCost = Convert.ToDouble(MinuteUnitCost);
				double cost = 0;

				if (Convert.ToBoolean(UseAverageCostPrice))
					cost = ((totalMinutes + lightMinutes) / 2) * unitCost;
				else
					cost = totalMinutes * unitCost;

				return cost;
			}
		}

		public double TotalCalculatedCost
		{
			get
			{
				double totalMinutes = Convert.ToDouble(TotalMinutes);
				double lightMinutes = Convert.ToDouble(LightMinutes);
				double unitCost = Convert.ToDouble(MinuteUnitCost);
				double cost = 0;

				if (Convert.ToBoolean(UseAverageCostPrice))
					cost = (((totalMinutes + lightMinutes) / 2) + Convert.ToDouble(AddedMinutes)) * unitCost;
				else
					cost = (totalMinutes + Convert.ToDouble(AddedMinutes)) * unitCost;

				return cost;
			}
		}
	}
}
