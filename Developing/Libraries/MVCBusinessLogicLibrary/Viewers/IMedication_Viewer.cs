namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IMedication_Viewer : IViewer
	{
		object MedicationCategory_CU_ID { get; set; }
		object Name_P { get; set; }
		object Name_S { get; set; }
		object Description { get; set; }
	}
}
