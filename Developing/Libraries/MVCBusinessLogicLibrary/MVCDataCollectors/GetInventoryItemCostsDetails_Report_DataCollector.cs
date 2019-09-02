using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class GetInventoryItemCostsDetails_Report_DataCollector<TEntity> : AbstractDataCollector<TEntity>,
		IGetInventoryItemCostsDetails_Report_Viewer
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

			ID = ((IGetInventoryItemCostsDetails_Report_Viewer)ActiveCollector.ActiveViewer).ID;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.GetInventoryItemCostsDetails_Viewer; }
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
			List<GetInventoryItemCostsDetails_Result> list = GetInventoryItemCostsDetails_Result.GetItemsList(
				(int?) RawMaterialID, (int?) ColorID, (int?) ItemID,
				(int?) AdditionalCostToBeAdded);
			if (list != null)
				return list.ToArray();
			return null;
		}

		#endregion

		#region Implementation of IGetInventoryItemCostsDetails_Report_Viewer

		public object RawMaterialID
		{
			get { return ((IGetInventoryItemCostsDetails_Report_Viewer)ActiveCollector.ActiveViewer).RawMaterialID; }
		}

		public object ColorID
		{
			get { return ((IGetInventoryItemCostsDetails_Report_Viewer)ActiveCollector.ActiveViewer).ColorID; }
		}

		public object ItemID
		{
			get { return ((IGetInventoryItemCostsDetails_Report_Viewer)ActiveCollector.ActiveViewer).ItemID; }
		}

		public object AdditionalCostToBeAdded
		{
			get { return ((IGetInventoryItemCostsDetails_Report_Viewer)ActiveCollector.ActiveViewer).AdditionalCostToBeAdded; }
		}

		#endregion
	}
}
