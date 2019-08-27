namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IGetRawMaterialCostPrices_Report_Viewer : IViewer
	{
		object RawMaterialID { get; }
		object FromDate { get; }
		object ToDate { get; }
		object UserID { get; }
	}
}
