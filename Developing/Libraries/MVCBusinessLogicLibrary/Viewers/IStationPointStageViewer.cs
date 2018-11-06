namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IStationPointStageViewer : IViewer
	{
		object Name_P { get; set; }
		object Name_S { get; set; }
		object StationPoint_CU_ID { get; set; }
		object Floor_CU_ID { get; set; }
		object OrderIndex { get; set; }
		object InternalCode { get; set; }
		object Description { get; set; }
	}
}
