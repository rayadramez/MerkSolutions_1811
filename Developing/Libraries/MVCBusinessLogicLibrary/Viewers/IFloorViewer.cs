namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IFloorViewer : IViewer
	{
		object Name_P { get; set; }
		object Name_S { get; set; }
		object Location_CU_ID { get; set; }
		object ShortName { get; set; }
		object Description { get; set; }
		object InternalCode { get; set; }
	}
}
