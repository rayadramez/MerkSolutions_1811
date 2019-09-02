using System;
using System.Collections.Generic;
using System.Linq;

namespace MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary
{
	public class InventoryItemDetailsConstructor
	{
		public object PrintingID { get; set; }
		public object PrintingDate { get; set; }
		
		public object ColorName { get; set; }
		public object RawThickness { get; set; }
		public object RawUnitPriceInPounds { get; set; }
		public object RawTotalArea { get; set; }
		public object NetCostPrice { get; set; }
		public object SellingPrice { get; set; }
		public object Profit { get; set; }
		public object ProfitPercentage { get; set; }

		public object ItemID { get; set; }
		public object ItemName { get; set; }
		public object ItemInternalCode { get; set; }
		public object ItemWidth { get; set; }
		public object ItemHeight { get; set; }
		public object ItemDepth { get; set; }
		public object PartsCount { get; set; }
		public object TotalPartsArea { get; set; }
		public object PrintingUnitCost { get; set; }

		public object RawMaterialUnitCost { get; set; }
		public object TotalUnitCost { get; set; }
		public object AdditionalCost { get; set; }
		public object NetTotalUnitCost { get; set; }

		public ListType ListType { get; set; }

		public List<InventoryItem_AreaPartsDetailsConstructor> List_InventoryItem_AreaPartsDetailsConstructor
		{
			get;
			set;
		}

		public List<InventoryItem_PrintingDetailsConstructor> List_InventoryItem_PrintingDetailsConstructor
		{
			get;
			set;
		}
	}
}
