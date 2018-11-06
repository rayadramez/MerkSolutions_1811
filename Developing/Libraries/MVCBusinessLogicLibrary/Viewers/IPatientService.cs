namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IPatientService
	{
		object ServiceDate { get; set; }
		object ServiceCategoryID { get; set; }
		object ServiceID { get; set; }
		object InPatientRoomClassificationID { get; set; }
		object InPatientRoomID { get; set; }
		object InPatientRoomBedID { get; set; }
		object AdmissionDate { get; set; }
		object CompanionsNumbers { get; set; }
		object DoctorID { get; set; }
		object ServicePrice { get; set; }
		object ServiceDiscount { get; set; }
		object ServiceDescription { get; set; }
		object IsSurchargeAppliedToService { get; set; }
		object IsInsuranceAppliedToService { get; set; }
		object ServiceType { get; }
		object LabServices { get; set; }
	}
}
