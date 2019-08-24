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
	public class InventoryHousing_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IInventoryHousingViewer
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

			ID = ((IInventoryHousingViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((InventoryHousing_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((InventoryHousing_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (InternalCode != null)
				((InventoryHousing_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (Description != null)
				((InventoryHousing_cu)ActiveDBItem).Description = Description.ToString();

			if (Floor_CU_ID != null)
				((InventoryHousing_cu)ActiveDBItem).Floor_CU_ID = Convert.ToInt32(Floor_CU_ID);

			if (IsMain != null)
				((InventoryHousing_cu)ActiveDBItem).IsMain = Convert.ToBoolean(IsMain);

			if (UserID != null)
				((InventoryHousing_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((InventoryHousing_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IInventoryHousingViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((InventoryHousing_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IInventoryHousingViewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<InventoryHousing_cu> list = InventoryHousing_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InventoryHousing_cu>();

				((IInventoryHousingViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((InventoryHousing_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((InventoryHousing_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((InventoryHousing_cu)ActiveDBItem).Name_P;
			Name_S = ((InventoryHousing_cu)ActiveDBItem).Name_S;
			InternalCode = ((InventoryHousing_cu)ActiveDBItem).InternalCode;
			Description = ((InventoryHousing_cu)ActiveDBItem).Description;
			Floor_CU_ID = ((InventoryHousing_cu)ActiveDBItem).Floor_CU_ID;

			IsMain = ((InventoryHousing_cu)ActiveDBItem).IsMain;

			((IInventoryHousingViewer)ActiveCollector.ActiveViewer).ID = ((InventoryHousing_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((InventoryHousing_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InventoryHousing_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IInventoryHousingViewer

		public object Name_P
		{
			get { return ((IInventoryHousingViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IInventoryHousingViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IInventoryHousingViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IInventoryHousingViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object Floor_CU_ID
		{
			get { return ((IInventoryHousingViewer)ActiveCollector.ActiveViewer).Floor_CU_ID; }
			set { ((IInventoryHousingViewer)ActiveCollector.ActiveViewer).Floor_CU_ID = value; }
		}

		public object IsMain
		{
			get { return ((IInventoryHousingViewer)ActiveCollector.ActiveViewer).IsMain; }
			set { ((IInventoryHousingViewer)ActiveCollector.ActiveViewer).IsMain = value; }
		}

		public object InternalCode
		{
			get { return ((IInventoryHousingViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IInventoryHousingViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object Description
		{
			get { return ((IInventoryHousingViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IInventoryHousingViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
