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
	public class UnitMeasurment_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IUnitMeasurmentViewer
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

			ID = ((IUnitMeasurmentViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((UnitMeasurment_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((UnitMeasurment_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (InternalCode != null)
				((UnitMeasurment_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (Description != null)
				((UnitMeasurment_cu)ActiveDBItem).Description = Description.ToString();

			if (UnitMeasurment_P_ID != null)
				((UnitMeasurment_cu)ActiveDBItem).UnitMeasurment_P_ID = Convert.ToInt32(UnitMeasurment_P_ID);

			if (UserID != null)
				((UnitMeasurment_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((UnitMeasurment_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IUnitMeasurmentViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((UnitMeasurment_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IUnitMeasurmentViewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
				ActiveDBItem = DBCommon.CreateNewDBEntity<UnitMeasurment_cu>();

				((IUnitMeasurmentViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((UnitMeasurment_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((UnitMeasurment_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((UnitMeasurment_cu)ActiveDBItem).Name_P;
			Name_S = ((UnitMeasurment_cu)ActiveDBItem).Name_S;
			InternalCode = ((UnitMeasurment_cu)ActiveDBItem).InternalCode;
			Description = ((UnitMeasurment_cu)ActiveDBItem).Description;
			UnitMeasurment_P_ID = ((UnitMeasurment_cu)ActiveDBItem).UnitMeasurment_P_ID;

			((IUnitMeasurmentViewer)ActiveCollector.ActiveViewer).ID = ((UnitMeasurment_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((UnitMeasurment_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((UnitMeasurment_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IUnitMeasurmentViewer

		public object Name_P
		{
			get { return ((IUnitMeasurmentViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IUnitMeasurmentViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IUnitMeasurmentViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IUnitMeasurmentViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object UnitMeasurment_P_ID
		{
			get { return ((IUnitMeasurmentViewer)ActiveCollector.ActiveViewer).UnitMeasurment_P_ID; }
			set { ((IUnitMeasurmentViewer)ActiveCollector.ActiveViewer).UnitMeasurment_P_ID = value; }
		}

		public object InternalCode
		{
			get { return ((IUnitMeasurmentViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IUnitMeasurmentViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object Description
		{
			get { return ((IUnitMeasurmentViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IUnitMeasurmentViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
