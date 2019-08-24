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
	public class InventoryItemGroup_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IInventoryItemGroupViewer
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

			ID = ((IInventoryItemGroupViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((InventoryItemGroup_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((InventoryItemGroup_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (InternalCode != null)
				((InventoryItemGroup_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (Description != null)
				((InventoryItemGroup_cu)ActiveDBItem).Description = Description.ToString();

			if (UserID != null)
				((InventoryItemGroup_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((InventoryItemGroup_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IInventoryItemGroupViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((InventoryItemGroup_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IInventoryItemGroupViewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<InventoryItemGroup_cu> list = InventoryItemGroup_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			if (Name_P == null)
			{
				MessageToView = "يجـب كتابــــة إســـم المجموعـــــة";
				return false;
			}

			return true;
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InventoryItemGroup_cu>();

				((IInventoryItemGroupViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((InventoryItemGroup_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((InventoryItemGroup_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((InventoryItemGroup_cu)ActiveDBItem).Name_P;
			Name_S = ((InventoryItemGroup_cu)ActiveDBItem).Name_S;
			InternalCode = ((InventoryItemGroup_cu)ActiveDBItem).InternalCode;
			Description = ((InventoryItemGroup_cu)ActiveDBItem).Description;

			((IInventoryItemGroupViewer)ActiveCollector.ActiveViewer).ID = ((InventoryItemGroup_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((InventoryItemGroup_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InventoryItemGroup_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IInventoryItemGroupViewer

		public object Name_P
		{
			get { return ((IInventoryItemGroupViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IInventoryItemGroupViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IInventoryItemGroupViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IInventoryItemGroupViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object InternalCode
		{
			get { return ((IInventoryItemGroupViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IInventoryItemGroupViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object Description
		{
			get { return ((IInventoryItemGroupViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IInventoryItemGroupViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
