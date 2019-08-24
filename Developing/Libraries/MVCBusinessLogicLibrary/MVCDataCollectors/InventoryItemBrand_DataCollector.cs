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
	public class InventoryItemBrand_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IInventoryItemBrandViewer
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

			ID = ((IInventoryItemBrandViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((InventoryItemBrand_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((InventoryItemBrand_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (InternalCode != null)
				((InventoryItemBrand_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (Description != null)
				((InventoryItemBrand_cu)ActiveDBItem).Description = Description.ToString();

			if (UserID != null)
				((InventoryItemBrand_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((InventoryItemBrand_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IInventoryItemBrandViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((InventoryItemBrand_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IInventoryItemBrandViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItemBrand_Viewer; }
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
			List<InventoryItemBrand_cu> list = InventoryItemBrand_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InventoryItemBrand_cu>();

				((IInventoryItemBrandViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((InventoryItemBrand_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((InventoryItemBrand_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((InventoryItemBrand_cu)ActiveDBItem).Name_P;
			Name_S = ((InventoryItemBrand_cu)ActiveDBItem).Name_S;
			InternalCode = ((InventoryItemBrand_cu)ActiveDBItem).InternalCode;
			Description = ((InventoryItemBrand_cu)ActiveDBItem).Description;

			((IInventoryItemBrandViewer)ActiveCollector.ActiveViewer).ID = ((InventoryItemBrand_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((InventoryItemBrand_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InventoryItemBrand_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IInventoryItemBrandViewer

		public object Name_P
		{
			get { return ((IInventoryItemBrandViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IInventoryItemBrandViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IInventoryItemBrandViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IInventoryItemBrandViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object InternalCode
		{
			get { return ((IInventoryItemBrandViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IInventoryItemBrandViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object Description
		{
			get { return ((IInventoryItemBrandViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IInventoryItemBrandViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
