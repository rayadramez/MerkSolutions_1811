namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IServiceCategoryViewer : IViewer
	{
		object NameP { get; set; }
		object NameS { get; set; }
		object InternalCode { get; set; }
		object ServiceType { get; set; }
		object IsMedical { get; set; }
		object AlloAdmission { get; set; }
		object Description { get; set; }
	}
}
