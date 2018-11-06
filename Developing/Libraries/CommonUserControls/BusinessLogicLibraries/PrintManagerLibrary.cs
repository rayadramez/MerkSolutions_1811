using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.BusinessLogicLibraries
{
	public class PrintManagerLibrary
	{
		public enum ReceiptType
		{
			OutPatientPaymentReceipt = 1,
		}

		public static bool PrintPaymentReceipt(InvoicePayment invoicePayment, ReceiptType receiptType, short numberOfCoppies)
		{
			if (!SetInvoicePaymentSerial(invoicePayment))
				return false;



			return true;
		}

		public static bool IsInvoicePaymentHasSerial(InvoicePayment invoicePayment)
		{
			return invoicePayment != null && invoicePayment.PaymentSerial != null;
		}

		public static bool SetInvoicePaymentSerial(InvoicePayment invoicePayment)
		{
			if (invoicePayment == null)
				return false;

			if (IsInvoicePaymentHasSerial(invoicePayment))
				return true;

			string nextSerial = FinancialBusinessLogicLibrary.GetNextMedicalInvoiceSerial();
			if (string.IsNullOrEmpty(nextSerial) || string.IsNullOrWhiteSpace(nextSerial))
				return false;

			invoicePayment.PaymentSerial = nextSerial;

			return true;
		}
	}
}
