namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IDoctorCategoryViewer : IViewer
	{
		object Name_P { get; set; }
		object Name_S { get; set; }
		object Description { get; set; }
	}
}
