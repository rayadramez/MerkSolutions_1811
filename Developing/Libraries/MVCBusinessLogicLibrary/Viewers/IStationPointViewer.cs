namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IStationPointViewer : IViewer
	{
		object Name_P { get; set; }
		object Name_S { get; set; }
		object Station_P_ID { get; set; }
		object InternalCode { get; set; }
		object Description { get; set; }
	}
}
