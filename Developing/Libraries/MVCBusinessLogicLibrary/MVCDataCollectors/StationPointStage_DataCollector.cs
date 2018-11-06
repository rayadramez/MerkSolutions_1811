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
	public class StationPointStage_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IStationPointStageViewer
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

			ID = ((IStationPointStageViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((StationPointStage_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((StationPointStage_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (InternalCode != null)
				((StationPointStage_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (Description != null)
				((StationPointStage_cu)ActiveDBItem).Description = Description.ToString();

			if (StationPoint_CU_ID != null)
				((StationPointStage_cu)ActiveDBItem).StationPoint_CU_ID = Convert.ToInt32(StationPoint_CU_ID);

			if (OrderIndex != null)
				((StationPointStage_cu)ActiveDBItem).OrderIndex = Convert.ToInt32(OrderIndex);

			if (Floor_CU_ID != null)
				((StationPointStage_cu)ActiveDBItem).Floor_CU_ID = Convert.ToInt32(Floor_CU_ID);

			if (UserID != null)
				((StationPointStage_cu) ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((StationPointStage_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IStationPointStageViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((StationPointStage_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IStationPointStageViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.StationPointStage_Viewer; }
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
			List<StationPointStage_cu> list = StationPointStage_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<StationPointStage_cu>();

				((IStationPointStageViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((StationPointStage_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((StationPointStage_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((StationPointStage_cu)ActiveDBItem).Name_P;
			Name_S = ((StationPointStage_cu)ActiveDBItem).Name_S;
			InternalCode = ((StationPointStage_cu)ActiveDBItem).InternalCode;
			StationPoint_CU_ID = ((StationPointStage_cu)ActiveDBItem).StationPoint_CU_ID;
			OrderIndex = ((StationPointStage_cu)ActiveDBItem).OrderIndex;
			Floor_CU_ID = ((StationPointStage_cu)ActiveDBItem).Floor_CU_ID;
			Description = ((StationPointStage_cu)ActiveDBItem).Description;

			((IStationPointStageViewer)ActiveCollector.ActiveViewer).ID = ((StationPointStage_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((StationPointStage_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((StationPointStage_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IStationPointStageViewer

		public object Name_P
		{
			get { return ((IStationPointStageViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IStationPointStageViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IStationPointStageViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IStationPointStageViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object StationPoint_CU_ID
		{
			get { return ((IStationPointStageViewer)ActiveCollector.ActiveViewer).StationPoint_CU_ID; }
			set { ((IStationPointStageViewer)ActiveCollector.ActiveViewer).StationPoint_CU_ID = value; }
		}

		public object Floor_CU_ID
		{
			get { return ((IStationPointStageViewer)ActiveCollector.ActiveViewer).Floor_CU_ID; }
			set { ((IStationPointStageViewer)ActiveCollector.ActiveViewer).Floor_CU_ID = value; }
		}

		public object OrderIndex
		{
			get { return ((IStationPointStageViewer)ActiveCollector.ActiveViewer).OrderIndex; }
			set { ((IStationPointStageViewer)ActiveCollector.ActiveViewer).OrderIndex = value; }
		}

		public object InternalCode
		{
			get { return ((IStationPointStageViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IStationPointStageViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object Description
		{
			get { return ((IStationPointStageViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IStationPointStageViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
