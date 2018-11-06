namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IInventoryItemPriceViewer : IViewer
	{
		object InventoryItem_CU_ID { get; set; }
		object UnitMeasurment_CU_ID { get; set; }
		object Date { get; set; }
		object Price { get; set; }
		object Customer_CU_ID { get; set; }
		object PriceType_P_ID { get; set; }
		object Supplier_CU_ID { get; set; }
	}
}
