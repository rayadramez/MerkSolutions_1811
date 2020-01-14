namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IInventoryItem_Area_Viewer : IViewer
	{
		object InventoryItemID { get; set; }
		object Width { get; set; }
		object Height { get; set; }
		object Count { get; set; }
		object InternalCode { get; set; }
		object SizeUnitMeasure_P_ID { get; set; }
	}
}
