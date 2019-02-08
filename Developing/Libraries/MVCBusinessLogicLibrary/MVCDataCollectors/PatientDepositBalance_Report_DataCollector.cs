using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class PatientDepositBalance_Report_DataCollector<TEntity> : AbstractDataCollector<TEntity>,
		IPatientDepositBalance_Report_Viewer
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

			ID = ((IPatientDepositBalance_Report_Viewer)ActiveCollector.ActiveViewer).ID;

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
			List<GetPatientDepositBalance_Result> list =
				GetPatientDepositBalance_Result.GetItemsList((int?)PatientID, (bool?)IsOnDuty, (int?)ServiceCategoryID,
					(int?)ServiceTypeID, (int?)ServiceID, (int?)UserID);
			if (list != null)
				return list.ToArray();
			return null;
		}

		#endregion

		#region Implementation of IPatientDepositBalance_Report_Viewer

		public object PatientID
		{
			get { return ((IPatientDepositBalance_Report_Viewer)ActiveCollector.ActiveViewer).PatientID; }
		}

		public object ServiceID
		{
			get { return ((IPatientDepositBalance_Report_Viewer)ActiveCollector.ActiveViewer).ServiceID; }
		}

		public object ServiceTypeID
		{
			get { return ((IPatientDepositBalance_Report_Viewer)ActiveCollector.ActiveViewer).ServiceTypeID; }
		}

		public object ServiceCategoryID
		{
			get { return ((IPatientDepositBalance_Report_Viewer)ActiveCollector.ActiveViewer).ServiceCategoryID; }
		}

		public object IsOnDuty
		{
			get { return ((IPatientDepositBalance_Report_Viewer)ActiveCollector.ActiveViewer).IsOnDuty; }
		}

		#endregion
	}
}
