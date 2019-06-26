using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;

namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IPatientInvoiceCreation : IPatientService, IViewer
	{
		object InvoiceCreationDate { get; set; }
		object PatientID { get; }
		object InvoiceTypeID { get; set; }
		object InvoiceDiscount { get; set; }
		object IsInsuranceAppliedToInvoice { get; set; }
		object IsSurchargeAppliedToInvoice { get; set; }
		object InsuranceCarrierID { get; set; }
		object InsuranceLevelID { get; set; }
		object InsurancePercentage { get; set; }
		object InsurancePatientMaxAmount { get; set; }
		object InsuranceMaxAmount { get; set; }
		object AccummulatedServicesPrice { get; set; }
		object AccummulativeServicesPatientShare { get; set; }
		object AccummulativeServicesInsuranceShare { get; }
		object AmountPaid { get; set; }
		object IsPaymentAttached { get; set; }
		object IsRemainingReturned { get; set; }
		object IsPaymentEnough { get; set; }
		object IsStampApplied { get; set; }
		object StampAmount { get; }
		object SurchargeAmount { get; }
		object StationPointID { get; set; }
		object StationPointStageID { get; set; }
		List<InvoiceDetail> Grid_InvoiceDetails { get; set; }
	}
}
