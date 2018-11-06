namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IDiagnosis_Viewer : IViewer
	{
		object Name_P { get; set; }
		object Name_S { get; set; }
		object Abbreviation { get; set; }
		object Description { get; set; }
		object IsDoctorRelated { get; set; }
		object DoctorID { get; set; }
	}
}
