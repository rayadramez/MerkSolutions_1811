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
	public class StationPoint_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IStationPointViewer
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

			ID = ((IStationPointViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((StationPoint_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((StationPoint_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (InternalCode != null)
				((StationPoint_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (Description != null)
				((StationPoint_cu)ActiveDBItem).Description = Description.ToString();

			if (Station_P_ID != null)
				((StationPoint_cu)ActiveDBItem).Station_P_ID = Convert.ToInt32(Station_P_ID);

			if (UserID != null)
				((StationPoint_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((StationPoint_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IStationPointViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((StationPoint_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IStationPointViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.StationPoint_Viewer; }
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
			List<StationPoint_cu> list = StationPoint_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<StationPoint_cu>();

				((IStationPointViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((StationPoint_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((StationPoint_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((StationPoint_cu)ActiveDBItem).Name_P;
			Name_S = ((StationPoint_cu)ActiveDBItem).Name_S;
			InternalCode = ((StationPoint_cu)ActiveDBItem).InternalCode;
			Station_P_ID = ((StationPoint_cu)ActiveDBItem).Station_P_ID;
			Description = ((StationPoint_cu)ActiveDBItem).Description;

			((IStationPointViewer)ActiveCollector.ActiveViewer).ID = ((StationPoint_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((StationPoint_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((StationPoint_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IStationPointViewer

		public object Name_P
		{
			get { return ((IStationPointViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IStationPointViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IStationPointViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IStationPointViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object Station_P_ID
		{
			get { return ((IStationPointViewer)ActiveCollector.ActiveViewer).Station_P_ID; }
			set { ((IStationPointViewer)ActiveCollector.ActiveViewer).Station_P_ID = value; }
		}

		public object InternalCode
		{
			get { return ((IStationPointViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IStationPointViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object Description
		{
			get { return ((IStationPointViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IStationPointViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
