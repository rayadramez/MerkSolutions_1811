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
	public class Floor_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IFloorViewer 
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

			ID = ((IFloorViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((Floor_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((Floor_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (InternalCode != null)
				((Floor_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (Description != null)
				((Floor_cu)ActiveDBItem).Description = Description.ToString();

			if (Location_CU_ID != null)
				((Floor_cu)ActiveDBItem).Location_CU_ID = Convert.ToInt32(Location_CU_ID);

				if (UserID != null)
					((Floor_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((Floor_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IFloorViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((Floor_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IFloorViewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<Floor_cu> list = Floor_cu.ItemsList;
			return list.ToArray();
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			if (Name_P == null)
			{
				MessageToView = "يجـــب كتـابـــة الإســـم الأول";
				return false;
			}

			return true;
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<Floor_cu>();

				((IFloorViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((Floor_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((Floor_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((Floor_cu)ActiveDBItem).Name_P;
			Name_S = ((Floor_cu)ActiveDBItem).Name_S;
			InternalCode = ((Floor_cu)ActiveDBItem).InternalCode;
			Description = ((Floor_cu)ActiveDBItem).Description;
			ShortName = ((Floor_cu)ActiveDBItem).ShortName;
			Location_CU_ID = ((Floor_cu)ActiveDBItem).Location_CU_ID;

			((IFloorViewer)ActiveCollector.ActiveViewer).ID = ((Floor_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((Floor_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((Floor_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IFloorViewer

		public object Name_P
		{
			get { return ((IFloorViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IFloorViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IFloorViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IFloorViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object Location_CU_ID
		{
			get { return ((IFloorViewer)ActiveCollector.ActiveViewer).Location_CU_ID; }
			set { ((IFloorViewer)ActiveCollector.ActiveViewer).Location_CU_ID = value; }
		}

		public object ShortName
		{
			get { return ((IFloorViewer)ActiveCollector.ActiveViewer).ShortName; }
			set { ((IFloorViewer)ActiveCollector.ActiveViewer).ShortName = value; }
		}

		public object Description
		{
			get { return ((IFloorViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IFloorViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		public object InternalCode
		{
			get { return ((IFloorViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IFloorViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}


		#endregion
	}
}
