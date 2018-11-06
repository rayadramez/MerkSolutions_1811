using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class GetPatientPreviousInvoices_Result : DBCommon, IDBCommon
	{
		public string InvoiceCreationDateString
		{
			get { return InvoiceCreationDate.ConvertDateTimeToString(true, true); }
		}
	}
}
