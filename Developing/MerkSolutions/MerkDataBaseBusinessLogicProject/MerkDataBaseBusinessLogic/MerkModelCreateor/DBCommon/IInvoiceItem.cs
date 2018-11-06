using System.Collections.Generic;

namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon
{
	public enum InvoiceItemType
	{
		None = 0,
		Invoice = 1,
		InPatient_Parent_AccommodationService = 2,
		InPatient_Parent_SurgeryService = 3,
		InvoiceDetail_Accommodation = 4,
		InvoiceDetail_DoctorFees = 5,
		InvoiceDetail_Inventory = 6,
		OutPatient_InvoiceDetail_ExaminationService = 7,
		OutPatient_InvoiceDetail_InvestigationService = 8,
		OutPatient_InvoiceDetail_ParentLabService = 9,
		OutPatient_InvoiceDetail_ChildLabService = 10,
	}

	public interface IInvoiceItem
	{
		double PatientShare_BeforeAddsOn_InvoiceItem { get; set; }
		double InsuranceShare_BeforeAddsOn_InvoiceItem { get; set; }
		double TotalServicePrice_BeforeAddsOn_InvoiceItem { get; set; }

		double PatientShareDiscount_InvoiceItem { get; }

		double SurchargeAmount_PatientShare_InvoiceItem { get; }
		double SurchargeAmount_InsuranceShare_InvoiceItem { get; }
		double TotalSurchargeAmount_InvoiceItem { get; set;  }

		double StampAmount_PatientShare_InvoiceItem { get; }
		double StampAmount_InsuranceShare_InvoiceItem { get; }
		double TotalStampAmount_InvoiceItem { get; }

		double PatientShare_AfterAddsOn_InvoiceItem { get; }
		double InsuranceShare_AfterAddsOn_InvoiceItem { get;  }
		double TotalServicePrice_AfterAddsOn_InvoiceItem { get; }

		bool IsInsuranceApplied_InvoiceItem { get; set; }
		bool IsSurchargeApplied_InvoiceItem { get; set; }
		bool IsStampApplied_InvoiceItem { get; set; }

		List<IInvoiceItem> List_InvoiceItems { get; }
		InvoiceItemType InvoiceItemType { get; }
	}
}
