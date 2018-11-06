namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IInvoicePaymentBrief_Report_Viewer : IViewer
	{
		object InvoiceTypeID { get; }
		object PaymentTypeID { get; }
		object FromDate { get; }
		object ToDate { get; }
		object UserID { get; }
	}
}
