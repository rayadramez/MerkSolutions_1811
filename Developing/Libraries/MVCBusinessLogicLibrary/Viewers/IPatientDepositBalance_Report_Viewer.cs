namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IPatientDepositBalance_Report_Viewer : IViewer
	{
		object PatientID { get; }
		object ServiceID { get; }
		object ServiceCategoryID { get; }
		object ServiceTypeID { get; }
		object UserID { get; }
		object IsOnDuty { get; }
	}
}
