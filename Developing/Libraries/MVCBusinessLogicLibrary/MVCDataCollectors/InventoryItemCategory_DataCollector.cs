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
	public class InventoryItemCategory_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IInventoryItemCategoryViewer
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

			ID = ((IInventoryItemCategoryViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((InventoryItemCategory_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((InventoryItemCategory_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (InternalCode != null)
				((InventoryItemCategory_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (Description != null)
				((InventoryItemCategory_cu)ActiveDBItem).Description = Description.ToString();

			if (UserID != null)
				((InventoryItemCategory_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((InventoryItemCategory_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IInventoryItemCategoryViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((InventoryItemCategory_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IInventoryItemCategoryViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItemCategory_Viewer; }
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
			List<InventoryItemCategory_cu> list = InventoryItemCategory_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InventoryItemCategory_cu>();

				((IInventoryItemCategoryViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((InventoryItemCategory_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((InventoryItemCategory_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((InventoryItemCategory_cu)ActiveDBItem).Name_P;
			Name_S = ((InventoryItemCategory_cu)ActiveDBItem).Name_S;
			InternalCode = ((InventoryItemCategory_cu)ActiveDBItem).InternalCode;
			Description = ((InventoryItemCategory_cu)ActiveDBItem).Description;

			((IInventoryItemCategoryViewer)ActiveCollector.ActiveViewer).ID = ((InventoryItemCategory_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((InventoryItemCategory_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InventoryItemCategory_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IInventoryItemCategoryViewer

		public object Name_P
		{
			get { return ((IInventoryItemCategoryViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IInventoryItemCategoryViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IInventoryItemCategoryViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IInventoryItemCategoryViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object InternalCode
		{
			get { return ((IInventoryItemCategoryViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IInventoryItemCategoryViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object Description
		{
			get { return ((IInventoryItemCategoryViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IInventoryItemCategoryViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
