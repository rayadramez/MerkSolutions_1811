namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IInventoryItem_InventoryHousing_Viewer : IViewer
	{
		object InventoryItem_CU_ID { get; set; }
		object InventoryHousing_CU_ID { get; set; }
		object Quantity { get; set; }
		object UnitMeasurment_CU_ID { get; set; }
		object InventoryItemTransactionType { get; set; }
		object ExpirationDate { get; set; }
		object Date { get; set; }
	}
}
