namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IPatientViewer : IViewer
	{
		object PersonTitle { get; set; }
		object PersonGender { get; set; }
		object FirstNameP { get; set; }
		object SecondNameP { get; set; }
		object ThirdNameP { get; set; }
		object FourthNameP { get; set; }
		object FirstNameS { get; set; }
		object SecondNameS { get; set; }
		object ThirdNameS { get; set; }
		object FourthNameS { get; set; }
		object Nationality { get; set; }
		object MaritalStatus { get; set; }
		object DateOfBirth { get; set; }
		object InsuranceCarrier { get; set; }
		object InsuranceLevel { get; set; }
		object CountryOfResidence { get; set; }
		object City { get; set; }
		object Region { get; set; }
		object Address { get; set; }
		object Mobile1 { get; set; }
		object Mobile2 { get; set; }
		object Phone1{ get; set; }
		object Phone2 { get; set; }
		object Email { get; set; }
		object RelativeName { get; set; }
		object RelativeType { get; set; }
		object RelativePhone { get; set; }
		object RelativeAddress { get; set; }
		object IdentificationCardType { get; set; }
		object IdentificationCardNumber { get;set; }
		object IdentificationCardIssuingDate { get; set; }
		object IdentificationCardEpirationDate { get; set; }

	}
}
