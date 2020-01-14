namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IInventoryItem_Printing_Viewer : IViewer
	{
		object InventoryItemID { get; set; }
		object RawMaterialsID { get; set; }
		object Date { get; set; }
		object TotalMinutes { get; set; }
		object LightMinutes { get; set; }
		object AddedMinutes { get; set; }
		object UseAverageCostPrice { get; set; }
		object MinuteUnitCost { get; set; }
		object UseRealCost { get; set; }
		object PrintingRealCostPrice { get; set; }
		object RealMinutes { get; set; }
		object Description { get; set; }
	}
}
