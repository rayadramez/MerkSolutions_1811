namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IDoctorViewer : IViewer
	{
		object FirstName_P { get; set; }
		object SecondName_P { get; set; }
		object ThirdName_P { get; set; }
		object FourthName_P { get; set; }
		object Gender { get; set; }
		object MaritalStatusID { get; set; }
		object BirthDate { get; set; }
		object InternalCode { get; set; }
		object Mobile1 { get; set; }
		object Mobile2 { get; set; }
		object Phone1 { get; set; }
		object Phone2 { get; set; }
		object Address { get; set; }
		object EMail { get; set; }
		object IdentificationCardTypeID { get; set; }
		object IdentificationCardNumber { get; set; }
		object IdentificationCardIssuingDate { get; set; }
		object IdentificationCardExpirationDate { get; set; }
		object LoginName { get; set; }
		object Password { get; set; }
		object ConfirmationPassword { get; set; }
		object DoctorRankID { get; set; }
		object DoctorSpecializationID { get; set; }
		object DoctorCategoryID { get; set; }
		object DoctorTaxTypeID { get; set; }
		object DoctorProfessionalFees { get; set; }
		object IsInternal { get; set; }
		object PrivateMobile { get; set; }
		object Description { get; set; }

	}
}
