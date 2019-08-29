namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IInventoryItem_Printing_Viewer : IViewer
	{
		object InventoryItemID { get; set; }
		object Date { get; set; }
		object PrintingMaxTimeInMinutes { get; set; }
		object AddedMinutes { get; set; }
		object PrintingAverageUnitCostPrice { get; set; }
		object UseRealCost { get; set; }
		object PrintingRealCostPrice { get; set; }
		object Description { get; set; }
	}
}
