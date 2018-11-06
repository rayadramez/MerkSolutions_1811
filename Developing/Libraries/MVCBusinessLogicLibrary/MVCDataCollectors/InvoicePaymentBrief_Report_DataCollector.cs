using System;
using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class InvoicePaymentBrief_Report_DataCollector<TEntity> : AbstractDataCollector<TEntity>,
		IInvoicePaymentBrief_Report_Viewer
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

			ID = ((IInvoicePaymentBrief_Report_Viewer)ActiveCollector.ActiveViewer).ID;

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
			List<GetInvoicesPaymentBriefReport_Result> list =
				GetInvoicesPaymentBriefReport_Result.GetItemsList(null, (int?) InvoiceTypeID, (int?) PaymentTypeID, (DateTime?) FromDate,
					(DateTime?) ToDate, (int?) UserID);
			if (list != null)
				return list.ToArray();
			return null;
		}

		#endregion

		#region Implementation of IInvoicePaymentBrief_Report_Viewer

		public object InvoiceTypeID
		{
			get { return ((IInvoicePaymentBrief_Report_Viewer)ActiveCollector.ActiveViewer).InvoiceTypeID; }
		}

		public object PaymentTypeID
		{
			get { return ((IInvoicePaymentBrief_Report_Viewer)ActiveCollector.ActiveViewer).PaymentTypeID; }
		}

		public object FromDate
		{
			get { return ((IInvoicePaymentBrief_Report_Viewer)ActiveCollector.ActiveViewer).FromDate; }
		}

		public object ToDate
		{
			get { return ((IInvoicePaymentBrief_Report_Viewer)ActiveCollector.ActiveViewer).ToDate; }
		}

		public override object UserID
		{
			get { return ((IInvoicePaymentBrief_Report_Viewer)ActiveCollector.ActiveViewer).UserID; }
		}

		#endregion
	}
}
