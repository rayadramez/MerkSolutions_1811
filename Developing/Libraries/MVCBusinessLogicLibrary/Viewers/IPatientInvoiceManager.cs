namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IPatientInvoiceManager : IViewer
	{
		object InsuranceCarrierID { get; set; }
		object InsuranceLevelID { get; set; }
		object InvoiceTypeID { get; set; }
		object InsurancePercentage { get; set; }
		object PatientPercentage { get; set; }
		object TotalServicesBefore { get; set; }
		object InsuranceShareBefore { get; set; }
		object PatientShareBefore { get; set; }
	}
}
