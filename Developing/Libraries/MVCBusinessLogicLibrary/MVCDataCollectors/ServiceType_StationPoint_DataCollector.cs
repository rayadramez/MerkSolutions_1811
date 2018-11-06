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
	public class ServiceType_StationPoint_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IServiceType_StationPointViewer
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

			ID = ((IServiceType_StationPointViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (ServiceType_P_ID != null)
				((ServiceType_StationPoint_cu) ActiveDBItem).ServiceType_P_ID = Convert.ToInt32(ServiceType_P_ID);

			if (StationPoint_CU_ID != null)
				((ServiceType_StationPoint_cu)ActiveDBItem).StationPoint_CU_ID = Convert.ToInt32(StationPoint_CU_ID);

			if (UserID != null)
				((ServiceType_StationPoint_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((ServiceType_StationPoint_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IServiceType_StationPointViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((ServiceType_StationPoint_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IServiceType_StationPointViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.ServiceType_StationPoint_Viewer; }
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
			List<ServiceType_StationPoint_cu> list = ServiceType_StationPoint_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<ServiceType_StationPoint_cu>();

				((IServiceType_StationPointViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((ServiceType_StationPoint_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((ServiceType_StationPoint_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			StationPoint_CU_ID = ((ServiceType_StationPoint_cu)ActiveDBItem).StationPoint_CU_ID;
			ServiceType_P_ID = ((ServiceType_StationPoint_cu)ActiveDBItem).ServiceType_P_ID;

			((IServiceType_StationPointViewer)ActiveCollector.ActiveViewer).ID = ((ServiceType_StationPoint_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((ServiceType_StationPoint_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((ServiceType_StationPoint_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IServiceType_StationPointViewer

		public object ServiceType_P_ID
		{
			get { return ((IServiceType_StationPointViewer)ActiveCollector.ActiveViewer).ServiceType_P_ID; }
			set { ((IServiceType_StationPointViewer)ActiveCollector.ActiveViewer).ServiceType_P_ID = value; }
		}

		public object StationPoint_CU_ID
		{
			get { return ((IServiceType_StationPointViewer)ActiveCollector.ActiveViewer).StationPoint_CU_ID; }
			set { ((IServiceType_StationPointViewer)ActiveCollector.ActiveViewer).StationPoint_CU_ID = value; }
		}

		#endregion
	}
}
