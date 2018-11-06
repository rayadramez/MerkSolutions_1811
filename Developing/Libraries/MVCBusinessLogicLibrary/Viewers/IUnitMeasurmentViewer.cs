namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IUnitMeasurmentViewer : IViewer
	{
		object Name_P { get; set; }
		object Name_S { get; set; }
		object UnitMeasurment_P_ID { get; set; }
		object Description { get; set; }
		object InternalCode { get; set; }
	}
}
