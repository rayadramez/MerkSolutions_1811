using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationConfiguration;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class InPatientRoomBedDataCollector<TEntity> : AbstractDataCollector<TEntity>, IInPatientRoomBedViewer
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

			ID = ((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			((InPatientRoomBed_cu)ActiveDBItem).DBCommonTransactionType =
				((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).CommonTransactionType;
			((InPatientRoomBed_cu)ActiveDBItem).Name_P = ((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).NameP.ToString();
			((InPatientRoomBed_cu)ActiveDBItem).Name_S = ((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).NameS.ToString();
			((InPatientRoomBed_cu)ActiveDBItem).InPatientRoom_CU_ID =
				Convert.ToInt32(((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).InPatientRoom);
			((InPatientRoomBed_cu) ActiveDBItem).InPatientRoomBedStatus_P_ID =
				Convert.ToInt32(((IInPatientRoomBedViewer) ActiveCollector.ActiveViewer).InPatientRoomBedStatus);

			if (((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).Description != null)
				((InPatientRoomBed_cu)ActiveDBItem).Description =
					((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).Description.ToString();
			else
				((InPatientRoomBed_cu)ActiveDBItem).Description = null;

			if (((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).InternalCode != null)
				((InPatientRoomBed_cu)ActiveDBItem).InternalCode =
					((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).InternalCode.ToString();
			else
				((InPatientRoomBed_cu)ActiveDBItem).InternalCode = null;

			if (((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).ShortName != null)
				((InPatientRoomBed_cu)ActiveDBItem).ShortName =
					((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).ShortName.ToString();
			else
				((InPatientRoomBed_cu)ActiveDBItem).ShortName = null;

			if (UserID != null)
				((InPatientRoomBed_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((InPatientRoomBed_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IStationPointStageViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((InPatientRoomBed_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { throw new System.NotImplementedException(); }
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
			get { return ActiveViewer.GridXML; }
		}

		public override List<IViewer> RelatedViewers { get; set; }
		public override void ClearControls()
		{
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).NameP = null;
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).NameS = null;
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).InPatientRoom = null;
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).InPatientRoomBedStatus = null;
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).Description = null;
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).InternalCode = null;
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).ShortName = null;
		}

		public override void FillControls()
		{
			throw new System.NotImplementedException();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InPatientRoomBed_cu>();
				((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveDBItem == null)
				return false;

			if (((InPatientRoomBed_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((InPatientRoomBed_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).NameP = ((InPatientRoomBed_cu)entity).Name_P;
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).NameS = ((InPatientRoomBed_cu)entity).Name_S;
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).ShortName = ((InPatientRoomBed_cu)entity).ShortName;
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).Description = ((InPatientRoomBed_cu)entity).Description;
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).InternalCode = ((InPatientRoomBed_cu)entity).InternalCode;
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).InPatientRoom = ((InPatientRoomBed_cu)entity).InPatientRoom_CU_ID;
			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).InPatientRoomBedStatus =
				((InPatientRoomBed_cu)entity).InPatientRoomBedStatus_P_ID;

			((IInPatientRoomBedViewer)ActiveCollector.ActiveViewer).ID = ((InPatientRoomBed_cu)entity).ID;
			ActiveCollector.ActiveDBItem.ID = ((InPatientRoomBed_cu)entity).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InPatientRoomBed_cu)entity).RemoveItem();
		}

		public override IEnumerable<TEntity> GetItemsList()
		{
			List<InPatientRoomBed_cu> list = DBCommon.GetItemsList<InPatientRoomBed_cu>().ToList().ToList();

			return (IEnumerable<TEntity>)list;
		}

		#endregion

		#region Implementation of IInPatientRoomBedViewer

		public object NameP { get; set; }
		public object NameS { get; set; }
		public object InPatientRoom { get; set; }
		public object Description { get; set; }
		public object ShortName { get; set; }
		public object InternalCode { get; set; }
		public object InPatientRoomBedStatus { get; set; }

		#endregion
	}
}
