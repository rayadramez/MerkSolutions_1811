namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IInPatientRoomViewer : IViewer
	{
		object NameP { get; set; }
		object NameS { get; set; }
		object Floor { get; set; }
		object InPatientRoomClassification { get; set; }
		object Description { get; set; }
		object ShortName { get; set; }
		object InternalCode { get; set; }
		bool HasMainPatientPricing { get; }
		object PricePerDay_MainPatient { get; set; }
		object MinimumAddmissionAmount_MainPatient { get; set; }
		bool HasCompanionPricing { get; }
		object PricePerDay_CompanionPatient { get; set; }
		object MinimumAddmissionAmount_CompanionPatient { get; set; }
	}
}
