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
	public class ServiceCategory_StationPoint_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IServiceCategory_StationPointViewer
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

			ID = ((IServiceCategory_StationPointViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (ServiceCategory_CU_ID != null)
				((ServiceCategory_StationPoint_cu)ActiveDBItem).ServiceCategory_CU_ID = Convert.ToInt32(ServiceCategory_CU_ID);

			if (StationPoint_CU_ID != null)
				((ServiceCategory_StationPoint_cu)ActiveDBItem).StationPoint_CU_ID = Convert.ToInt32(StationPoint_CU_ID);

			if (UserID != null)
				((ServiceCategory_StationPoint_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((ServiceCategory_StationPoint_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IServiceCategory_StationPointViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((ServiceCategory_StationPoint_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IServiceCategory_StationPointViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.ServiceCategory_StationPoint_Viewer; }
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
			List<ServiceCategory_StationPoint_cu> list = ServiceCategory_StationPoint_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<ServiceCategory_StationPoint_cu>();

				((IServiceCategory_StationPointViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((ServiceCategory_StationPoint_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((ServiceCategory_StationPoint_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			StationPoint_CU_ID = ((ServiceCategory_StationPoint_cu)ActiveDBItem).StationPoint_CU_ID;
			ServiceCategory_CU_ID = ((ServiceCategory_StationPoint_cu)ActiveDBItem).ServiceCategory_CU_ID;

			((IServiceCategory_StationPointViewer)ActiveCollector.ActiveViewer).ID = ((ServiceCategory_StationPoint_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((ServiceCategory_StationPoint_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((ServiceCategory_StationPoint_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IServiceCategory_StationPointViewer

		public object ServiceCategory_CU_ID
		{
			get { return ((IServiceCategory_StationPointViewer)ActiveCollector.ActiveViewer).ServiceCategory_CU_ID; }
			set { ((IServiceCategory_StationPointViewer)ActiveCollector.ActiveViewer).ServiceCategory_CU_ID = value; }
		}

		public object StationPoint_CU_ID
		{
			get { return ((IServiceCategory_StationPointViewer)ActiveCollector.ActiveViewer).StationPoint_CU_ID; }
			set { ((IServiceCategory_StationPointViewer)ActiveCollector.ActiveViewer).StationPoint_CU_ID = value; }
		}

		#endregion
	}
}
