namespace MVCBusinessLogicLibrary.Viewers
{
	public interface ICustomerViewer : IViewer
	{
		object FirstName { get; set; }
		object SecondName { get; set; }
		object ThirdName { get; set; }
		object FourthName { get; set; }
		object MaritalStatus { get; set; }
		object Gender { get; set; }
		object BirthDate { get; set; }
		object InternalCode { get; set; }
		object Mobile1 { get; set; }
		object Mobile2 { get; set; }
		object Phone1 { get; set; }
		object Phone2 { get; set; }
		object Address { get; set; }
		object Email { get; set; }
		object IdentificationCardType { get; set; }
		object IdentificationCardNumber { get; set; }
		object IdentificationCardIssueDate { get; set; }
		object IdentificationCardExpirationDate { get; set; }
		object IsDebitChartOfAccount { get; set; }
		object Debit_ChartOfAccount { get; set; }
		object IsTaxChartOfAccount { get; set; }
		object Tax_ChartOfAccount { get; set; }
		object IsCreditChartOfAccount { get; set; }
		object Credit_ChartOfAccount { get; set; }
		object IsCurrentChartOfAccount { get; set; }
		object Current_ChartOfAccount { get; set; }
	}
}
