using System;
using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class GetInventoryItemAreaParts_DataCollector<TEntity> : AbstractDataCollector<TEntity>,
		IGetInventoryItemAreaParts_Viewer
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

			ID = ((IGetInventoryItemAreaParts_Viewer)ActiveCollector.ActiveViewer).ID;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.GetInventoryItemAreaParts_Viewer; }
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
			List<GetInventoryItemAreaParts_Result> list =
				GetInventoryItemAreaParts_Result.GetItemsList((int?) InventoryItemID);
			if (list != null)
				return list.ToArray();
			return null;
		}

		#endregion

		#region Implementation of IGetInventoryItemAreaParts_Viewer

		public object InventoryItemID
		{
			get { return ((IGetInventoryItemAreaParts_Viewer) ActiveCollector.ActiveViewer).InventoryItemID; }
		}

		#endregion
	}
}
