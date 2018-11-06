using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class CustomerFinanceInvoice_Report_DataCollector<TEntity> : AbstractDataCollector<TEntity>,
		ICustomerFinanceInvoices_Report_Viewer
		where TEntity : DBCommon, IDBCommon, new()
	{
		#region Overrides of AbstractDataCollector<TEntity>

		public override AbstractDataCollector<TEntity> ActiveCollector { get; set; }
		public override AbstractDataCollector<TEntity> ParentActiveCollector { get; set; }

		public override bool Collect(AbstractDataCollector<TEntity> collector)
		{
			if (collector == null)
				return false;

			ActiveCollector = collector;

			ID = ((ICustomerFinanceInvoices_Report_Viewer)ActiveCollector.ActiveViewer).ID;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { throw new System.NotImplementedException(); }
		}

		public override object EditingDate
		{
			get { throw new System.NotImplementedException(); }
		}

		public override object IsOnDUty { get; set; }
		public override DB_CommonTransactionType CommonTransactionType { get; set; }

		public override string HeaderTitle
		{
			get { throw new System.NotImplementedException(); }
		}

		public override string GridXML
		{
			get { throw new System.NotImplementedException(); }
		}

		public override List<IViewer> RelatedViewers { get; set; }

		public override object[] CollectSearchCriteria()
		{
			List<GetCustomerInvoices_Result> list = GetCustomerInvoices_Result.GetItemsList((int?) CustomerID, (bool?) IsOnDuty,
				(int?) InvoiceTypeID,
				(bool?) IsPaymentEnough, (bool?) IsFinanciallyReviewed, (bool?) IsFinanciallyCompleted);
			if (list != null)
				return list.ToArray();
			return null;
		}

		#endregion

		#region Implementation of ICustomerFinanceInvoices_Report_Viewer

		public object CustomerID
		{
			get { return ((ICustomerFinanceInvoices_Report_Viewer) ActiveCollector.ActiveViewer).CustomerID; }
		}

		public object IsOnDuty
		{
			get { return ((ICustomerFinanceInvoices_Report_Viewer)ActiveCollector.ActiveViewer).IsOnDuty; }
		}

		public object InvoiceTypeID
		{
			get { return ((ICustomerFinanceInvoices_Report_Viewer)ActiveCollector.ActiveViewer).InvoiceTypeID; }
		}

		public object IsPaymentEnough
		{
			get { return ((ICustomerFinanceInvoices_Report_Viewer)ActiveCollector.ActiveViewer).IsPaymentEnough; }
		}

		public object IsFinanciallyReviewed
		{
			get { return ((ICustomerFinanceInvoices_Report_Viewer)ActiveCollector.ActiveViewer).IsFinanciallyReviewed; }
		}

		public object IsFinanciallyCompleted
		{
			get { return ((ICustomerFinanceInvoices_Report_Viewer)ActiveCollector.ActiveViewer).IsFinanciallyCompleted; }
		}

		#endregion
	}
}
