namespace MVCBusinessLogicLibrary.Viewers
{
	public interface ITotalServiceAndDoctorRevenues_Report_Viewer : IViewer
	{
		object InvoiceTypeID { get; }
		object ServiceID { get; }
		object ServiceTypeID { get; }
		object ServiceCategoryID { get; }
		object DoctorID { get; }
		object IsOnDuty { get; }
		object FromDate { get; }
		object ToDate { get; }
	}
}
