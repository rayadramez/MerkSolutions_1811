namespace MVCBusinessLogicLibrary.Viewers
{
	public interface ILocationViewer : IViewer
	{
		object Name_P { get; set; }
		object Name_S { get; set; }
		object Country_CU_ID { get; set; }
		object City_CU_ID { get; set; }
		object Region_CU_ID { get; set; }
		object Territory_CU_ID { get; set; }
		object Description { get; set; }
		object Address { get; set; }
		object InternalCode { get; set; }
	}
}
