using System;
using System.Collections.Generic;
using ApplicationConfiguration;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class Service_StationPointData_Collector<TEntity> : AbstractDataCollector<TEntity>, IService_StationPointViewer
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

			ID = ((IService_StationPointViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Service_CU_ID != null)
				((Service_StationPoint_cu)ActiveDBItem).Service_CU_ID = Convert.ToInt32(Service_CU_ID);

			if (StationPoint_CU_ID != null)
				((Service_StationPoint_cu)ActiveDBItem).StationPoint_CU_ID = Convert.ToInt32(StationPoint_CU_ID);

			if (UserID != null)
				((Service_StationPoint_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((Service_StationPoint_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IService_StationPointViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((Service_StationPoint_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IService_StationPointViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.Service_StationPoint_Viewer; }
		}

		public override object UserID
		{
			get
			{
				if (ApplicationStaticConfiguration.ActiveLoginUser != null)
					return ApplicationStaticConfiguration.ActiveLoginUser.Person_CU_ID;
				return null;
			}
		}

		public override object EditingDate
		{
			get { return DateTime.Now; }
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
		public override void ClearControls()
		{
			throw new System.NotImplementedException();
		}

		public override void FillControls()
		{
			throw new System.NotImplementedException();
		}

		public override object[] CollectSearchCriteria()
		{
			List<Service_StationPoint_cu> list = Service_StationPoint_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<Service_StationPoint_cu>();

				((IService_StationPointViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((Service_StationPoint_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((Service_StationPoint_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			StationPoint_CU_ID = ((Service_StationPoint_cu)ActiveDBItem).StationPoint_CU_ID;
			Service_CU_ID = ((Service_StationPoint_cu)ActiveDBItem).Service_CU_ID;

			((IService_StationPointViewer)ActiveCollector.ActiveViewer).ID = ((Service_StationPoint_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((Service_StationPoint_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((Service_StationPoint_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IService_StationPointViewer

		public object Service_CU_ID
		{
			get { return ((IService_StationPointViewer)ActiveCollector.ActiveViewer).Service_CU_ID; }
			set { ((IService_StationPointViewer)ActiveCollector.ActiveViewer).Service_CU_ID = value; }
		}

		public object StationPoint_CU_ID
		{
			get { return ((IService_StationPointViewer)ActiveCollector.ActiveViewer).StationPoint_CU_ID; }
			set { ((IService_StationPointViewer)ActiveCollector.ActiveViewer).StationPoint_CU_ID = value; }
		}

		#endregion
	}
}
