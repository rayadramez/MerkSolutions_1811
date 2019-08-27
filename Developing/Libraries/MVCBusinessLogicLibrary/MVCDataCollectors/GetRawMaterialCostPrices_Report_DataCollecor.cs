using System;
using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class GetRawMaterialCostPrices_Report_DataCollecor<TEntity> : AbstractDataCollector<TEntity>,
		IGetRawMaterialCostPrices_Report_Viewer
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

			ID = ((IGetRawMaterialCostPrices_Report_Viewer)ActiveCollector.ActiveViewer).ID;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.GetRawMaterialCostPrices_Viewer; }
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
			List<GetRawMaterialCostPrices_Result> list =
				GetRawMaterialCostPrices_Result.GetItemsList((int?)RawMaterialID, (DateTime?)FromDate, (DateTime?)ToDate, (int?)UserID);
			if (list != null)
				return list.ToArray();
			return null;
		}

		#endregion

		#region Implementation of IGetRawMaterialCostPrices_Report_Viewer

		public object RawMaterialID
		{
			get { return ((IGetRawMaterialCostPrices_Report_Viewer)ActiveCollector.ActiveViewer).RawMaterialID; }
		}

		public object FromDate
		{
			get { return ((IGetRawMaterialCostPrices_Report_Viewer)ActiveCollector.ActiveViewer).FromDate; }
		}

		public object ToDate
		{
			get { return ((IGetRawMaterialCostPrices_Report_Viewer)ActiveCollector.ActiveViewer).ToDate; }
		}

		public override object UserID
		{
			get { return ((IGetRawMaterialCostPrices_Report_Viewer)ActiveCollector.ActiveViewer).UserID; }
		}

		#endregion
	}
}
