using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class GetInventoryItemAreaParts_Result : DBCommon, IDBCommon
	{
		public static List<GetInventoryItemAreaParts_Result> GetItemsList(object inventoryItemID)
		{
			return
				DBContext_External.GetInventoryItemAreaParts((int?)inventoryItemID).ToList();
		}

		public InventoryItem_cu InventoryItem
		{
			get
			{
				InventoryItem_cu inventoryItem =
					InventoryItem_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(ItemID)));
				return inventoryItem;
			}
		}

		public string InventoryItemName
		{
			get { return InventoryItem.Name_P; }
		}

		public string InternalCode
		{
			get { return InventoryItem.InternalCode; }
		}

		public double PartArea
		{
			get
			{
				//InventoryItem_Area area =
				//	InventoryItem_Area.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(AreaID)));
				//if (area == null)
				//	return 0;
				return Convert.ToDouble(Width) * Convert.ToDouble(Height);
			}
		}

		public double TotalArea
		{
			get
			{
				return Convert.ToDouble(Width) * Convert.ToDouble(Height) * Convert.ToDouble(Count);
			}
		}

		public string AreaInternalCode
		{
			get
			{
				InventoryItem_Area area =
					InventoryItem_Area.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(AreaID)));
				return area.InternalCode;
			}
		}
	}
}
