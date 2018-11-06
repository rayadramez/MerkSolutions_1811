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
	public class UnitMeasurmentTreeLink_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IUnitMeasurmentTreeLinkViewer
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

			ID = ((IUnitMeasurmentTreeLinkViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (ChildUnitMeasurment_CU_ID != null)
				((UnitMeasurmentTreeLink_cu)ActiveDBItem).ChildUnitMeasurment_CU_ID = Convert.ToInt32(ChildUnitMeasurment_CU_ID);

			if (ParentUnitMeasurment_CU_ID != null)
				((UnitMeasurmentTreeLink_cu)ActiveDBItem).ParentUnitMeasurment_CU_ID = Convert.ToInt32(ParentUnitMeasurment_CU_ID);

			if (EncapsulatedChildQantity != null)
				((UnitMeasurmentTreeLink_cu)ActiveDBItem).EncapsulatedChildQantity = Convert.ToDouble(EncapsulatedChildQantity);

			if (UserID != null)
				((UnitMeasurmentTreeLink_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((UnitMeasurmentTreeLink_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IUnitMeasurmentTreeLinkViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((UnitMeasurmentTreeLink_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IUnitMeasurmentTreeLinkViewer)ActiveCollector.ActiveViewer).RelatedViewers;

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

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<UnitMeasurmentTreeLink_cu>();

				((IUnitMeasurmentTreeLinkViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((UnitMeasurmentTreeLink_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((UnitMeasurmentTreeLink_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			ParentUnitMeasurment_CU_ID = ((UnitMeasurmentTreeLink_cu)ActiveDBItem).ParentUnitMeasurment_CU_ID;
			ChildUnitMeasurment_CU_ID = ((UnitMeasurmentTreeLink_cu)ActiveDBItem).ChildUnitMeasurment_CU_ID;
			EncapsulatedChildQantity = ((UnitMeasurmentTreeLink_cu)ActiveDBItem).EncapsulatedChildQantity;

			((IUnitMeasurmentTreeLinkViewer)ActiveCollector.ActiveViewer).ID = ((UnitMeasurmentTreeLink_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((UnitMeasurmentTreeLink_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((UnitMeasurmentTreeLink_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IUnitMeasurmentTreeLinkViewer

		public object ParentUnitMeasurment_CU_ID
		{
			get { return ((IUnitMeasurmentTreeLinkViewer)ActiveCollector.ActiveViewer).ParentUnitMeasurment_CU_ID; }
			set { ((IUnitMeasurmentTreeLinkViewer)ActiveCollector.ActiveViewer).ParentUnitMeasurment_CU_ID = value; }
		}

		public object ChildUnitMeasurment_CU_ID
		{
			get { return ((IUnitMeasurmentTreeLinkViewer)ActiveCollector.ActiveViewer).ChildUnitMeasurment_CU_ID; }
			set { ((IUnitMeasurmentTreeLinkViewer)ActiveCollector.ActiveViewer).ChildUnitMeasurment_CU_ID = value; }
		}

		public object EncapsulatedChildQantity
		{
			get { return ((IUnitMeasurmentTreeLinkViewer)ActiveCollector.ActiveViewer).EncapsulatedChildQantity; }
			set { ((IUnitMeasurmentTreeLinkViewer)ActiveCollector.ActiveViewer).EncapsulatedChildQantity = value; }
		}

		#endregion
	}
}
