namespace MVCBusinessLogicLibrary.Viewers
{
	public interface ICustomerFinanceInvoices_Report_Viewer : IViewer
	{
		object CustomerID { get; }
		object IsOnDuty { get; }
		object InvoiceTypeID { get; }
		object IsPaymentEnough { get; }
		object IsFinanciallyReviewed { get; }
		object IsFinanciallyCompleted { get; }
	}
}
