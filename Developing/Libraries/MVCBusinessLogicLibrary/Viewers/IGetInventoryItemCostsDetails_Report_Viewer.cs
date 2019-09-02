namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IGetInventoryItemCostsDetails_Report_Viewer : IViewer
	{
		object RawMaterialID { get; }
		object ColorID { get; }
		object ItemID { get; }
		object AdditionalCostToBeAdded { get; }
	}
}
