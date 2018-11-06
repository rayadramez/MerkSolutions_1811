namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IInsurancePolicyViewer : IViewer
	{
		object InsuranceCarrierID { get; set; }
		object InsuranceLevelID { get; set; }
		object InsurancePercetnage { get; set; }
		object PatientMaxAmount { get; set; }
	}
}
