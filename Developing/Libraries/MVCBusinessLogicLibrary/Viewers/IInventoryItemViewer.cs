namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IInventoryItemViewer : IViewer
	{
		object Name_P { get; set; }
		object Name_S { get; set; }
		object InventoryHousing_CU_ID { get; set; }
		object InventoryItemCategory_CU_ID { get; set; }
		object InventoryItemBrand_CU_ID { get; set; }
		object DefaultUnitMeasurment_CU_ID { get; set; }
		object InventoryItemType_P_ID { get; set; }
		object InternalCode { get; set; }
		object DefaultBarcode { get; set; }
		object DefaultSellingPrice { get; set; }
		object DefaultCost { get; set; }
		object RorderedPoint { get; set; }
		object StockMinLevel { get; set; }
		object StockMaxLevel { get; set; }
		object AcceptOverrideMinAmount { get; set; }
		object CanBeSold { get; set; }
		object IsAvailable { get; set; }
		object AcceptPartingSelling { get; set; }
		object IsCountable { get; set; }
		object SellingStartDate { get; set; }
		object SellingEndDate { get; set; }
		object ExpirationDate { get; set; }
		object Description { get; set; }
		object Width { get; set; }
		object Height { get; set; }
		object Depth { get; set; }
	}
}
