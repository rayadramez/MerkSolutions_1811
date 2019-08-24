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
	public class InventoryItemGroup_InventoryItem_DataCollector<TEntity> : AbstractDataCollector<TEntity>,
		IInventoryItemGroup_InventoryItem_Viewer
		where TEntity : DBCommon, IDBCommon, new()
	{
		private IInventoryItemGroup_InventoryItem_Viewer _inventoryItemGroupImplementation;

		#region Overrides of AbstractDataCollector<TEntity>

		public override AbstractDataCollector<TEntity> ActiveCollector { get; set; }
		public override AbstractDataCollector<TEntity> ParentActiveCollector { get; set; }

		public override bool Collect(AbstractDataCollector<TEntity> collector)
		{
			if (collector == null)
				return false;

			ActiveCollector = collector;

			ID = ((IInventoryItemGroup_InventoryItem_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (List_InventoryItemGroup_InventoryItem == null || List_InventoryItemGroup_InventoryItem.Count == 0)
				return false;

			foreach (InventoryItemGroup_InventoryItem_cu bridge in List_InventoryItemGroup_InventoryItem)
			{
				((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).InvetoryItemGroup_CU_ID = bridge.InvetoryItemGroup_CU_ID;
				((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).InventoryItem_CU_ID = bridge.InventoryItem_CU_ID;
			}

			if (UserID != null)
				((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IInventoryItemGroup_InventoryItem_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IInventoryItemGroup_InventoryItem_Viewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItemGroup_InventoryItem_Viewer; }
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
			List<InventoryItemGroup_InventoryItem_cu> list =
				InventoryItemGroup_InventoryItem_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InventoryItemGroup_InventoryItem_cu>();
				((IInventoryItemGroup_InventoryItem_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			foreach (InventoryItemGroup_InventoryItem_cu bridge in List_InventoryItemGroup_InventoryItem)
			{
				((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).InvetoryItemGroup_CU_ID = bridge.InvetoryItemGroup_CU_ID;
				((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).InventoryItem_CU_ID = bridge.InventoryItem_CU_ID;

				if (UserID != null)
					((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

				((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).IsOnDuty = true;
				switch (((IInventoryItemGroup_InventoryItem_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
				{
					case DB_CommonTransactionType.DeleteExisting:
						((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).IsOnDuty = false;
						break;
				}

				((InventoryItemGroup_InventoryItem_cu)ActiveCollector.ActiveDBItem).SaveChanges();
			}

			((InventoryItemGroup_InventoryItem_cu)ActiveCollector.ActiveDBItem).LoadItemsList();

			return true;
		}

		public override void Edit(IDBCommon entity)
		{
			//NameP = ((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).Name_P;
			//NameS = ((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).Name_S;
			//InternalCode = ((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).InternalCode;
			//Description = ((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).Description;

			//((IInventoryItemGroup_InventoryItem_Viewer)ActiveCollector.ActiveViewer).ID = ((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).ID;
			//ActiveCollector.ActiveDBItem.ID = ((InventoryItemGroup_InventoryItem_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InventoryItemGroup_InventoryItem_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IInventoryItemGroup_InventoryItem_Viewer

		public List<InventoryItemGroup_InventoryItem_cu> List_InventoryItemGroup_InventoryItem
		{
			get
			{
				return ((IInventoryItemGroup_InventoryItem_Viewer) ActiveCollector.ActiveViewer)
					.List_InventoryItemGroup_InventoryItem;
			}
			set
			{
				((IInventoryItemGroup_InventoryItem_Viewer) ActiveCollector.ActiveViewer)
					.List_InventoryItemGroup_InventoryItem = value;
			}
		}

		#endregion
	}
}
