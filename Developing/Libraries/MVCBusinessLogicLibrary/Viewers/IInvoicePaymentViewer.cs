using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IInvoicePaymentViewer : IViewer
	{
		PaymentViewerType PaymentViewerType { get; set; }
		object Invoice { get; set; }
		object Patient { get; }
		object ServiceID { get; set; }
		object IsRefund { get; set; }
		object IsPaymentEnough { get; set; }
		object PaymentDate { get; set; }
		object PaymentSerial { get; set; }
		object InvoicePaymentDescription { get; set; }
		object TotalRequired { get; set; }
		object TotalPayments { get; set; }
		object Remainder { get; set; }
		object PaymentAmount { get; set; }
		object PaymentTypeID { get; set; }
		object BankID_CheckPayment { get; set; }
		object BankAccountID_CheckPayment { get; set; }
		object CheckIssueDate_CheckPayment { get; set; }
		object CheckExhangeDate_CheckPayment { get; set; }
		object CheckDescription { get; set; }
		object CheckNumber_CheckPayment { get; set; }
		object BankID_VisaPayment { get; set; }
		object BankAccountID_VisaPayment { get; set; }
		object VisaCardNumber_VisaPayment { get; set; }
		object VisaDescription { get; set; }
	}
}
