using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary
{
	public class MedicalInvoiceItemConstructor
	{
		public Invoice ActiveInvoice { get; set; }
		public InvoiceDetail ActiveInvoiceDetail { get; set; }
		public Patient_cu ActivePatient { get; set; }

		public bool IsInsuranceAppliedToInvoice { get; set; }
		public double InsurancePercentageApplied { get; set; }
		public object InsuranceCarrierID { get; set; }
		public object InsuranceLevelID { get; set; }

		public List<MedicalInvoiceItemConstructor> List_MMedicalInvoiceItemConstructors { get; set; }
		public List<IInvoiceItem> List_InvoiceItems { get; set; }
		public InvoiceItemType InvoiceItemType { get; set; }
		public double ItemPrice { get; set; }

		public MedicalInvoiceItemConstructor()
		{
			List_InvoiceItems = new List<IInvoiceItem>();
		}
	}
}
