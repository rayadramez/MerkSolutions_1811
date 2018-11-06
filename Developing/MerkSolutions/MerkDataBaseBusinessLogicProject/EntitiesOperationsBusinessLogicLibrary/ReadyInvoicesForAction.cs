namespace MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary
{
	public class ReadyInvoicesForAction
	{
		public object PatientID { get; set; }
		public object PatientFullName { get; set; }
		public object ServiceName { get; set; }
		public object InvoiceID { get; set; }
		public object InvoiceDetailID { get; set; }
		public DB_InvoiceType InvoiceType { get; set; }
		public object InvoiceCreationDate { get; set; }
		public object InvoiceSerial { get; set; }
		public object DoctorID { get; set; }
		public object DoctorName { get; set; }
		public object IsPaymentEnough { get; set; }
		public double TotalRequired { get; set; }
		public double TotalPayments { get; set; }
		public Invoice ActiveInvoice { get; set; }
		public Patient_cu ActivePatient { get; set; }
	}
}
