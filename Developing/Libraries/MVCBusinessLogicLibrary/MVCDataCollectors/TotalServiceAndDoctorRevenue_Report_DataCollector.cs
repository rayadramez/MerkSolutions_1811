using System;
using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class TotalServiceAndDoctorRevenue_Report_DataCollector<TEntity> : AbstractDataCollector<TEntity>,
		ITotalServiceAndDoctorRevenues_Report_Viewer
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

			ID = ((ITotalServiceAndDoctorRevenues_Report_Viewer)ActiveCollector.ActiveViewer).ID;

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
			List<GetTotalServiceAndDoctorRevenues_Result> list =
				GetTotalServiceAndDoctorRevenues_Result.GetItemsList((int?) InvoiceTypeID, (int?) ServiceID, (int?) ServiceTypeID,
					(int?) ServiceCategoryID, (bool?)IsOnDuty, (DateTime?) FromDate,
					(DateTime?) ToDate, (int?) DoctorID);
			if (list != null)
				return list.ToArray();
			return null;
		}

		#endregion

		#region Implementation of ITotalServiceAndDoctorRevenues_Report_Viewer

		public object InvoiceTypeID
		{
			get { return ((ITotalServiceAndDoctorRevenues_Report_Viewer)ActiveCollector.ActiveViewer).InvoiceTypeID; }
		}

		public object ServiceID
		{
			get { return ((ITotalServiceAndDoctorRevenues_Report_Viewer)ActiveCollector.ActiveViewer).ServiceID; }
		}

		public object ServiceTypeID
		{
			get { return ((ITotalServiceAndDoctorRevenues_Report_Viewer)ActiveCollector.ActiveViewer).ServiceTypeID; }
		}

		public object ServiceCategoryID
		{
			get { return ((ITotalServiceAndDoctorRevenues_Report_Viewer)ActiveCollector.ActiveViewer).ServiceCategoryID; }
		}

		public object DoctorID
		{
			get { return ((ITotalServiceAndDoctorRevenues_Report_Viewer)ActiveCollector.ActiveViewer).DoctorID; }
		}

		public object IsOnDuty
		{
			get { return ((ITotalServiceAndDoctorRevenues_Report_Viewer)ActiveCollector.ActiveViewer).IsOnDuty; }
		}

		public object FromDate
		{
			get { return ((ITotalServiceAndDoctorRevenues_Report_Viewer)ActiveCollector.ActiveViewer).FromDate; }
		}

		public object ToDate
		{
			get { return ((ITotalServiceAndDoctorRevenues_Report_Viewer)ActiveCollector.ActiveViewer).ToDate; }
		}

		#endregion
	}
}
