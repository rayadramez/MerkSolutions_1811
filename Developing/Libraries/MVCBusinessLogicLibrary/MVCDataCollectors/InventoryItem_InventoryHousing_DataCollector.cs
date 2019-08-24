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
	public class InventoryItem_InventoryHousing_DataCollector<TEntity> : AbstractDataCollector<TEntity>,
		IInventoryItem_InventoryHousing_Viewer
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

			ID = ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (InventoryHousing_CU_ID != null)
				((InventoryItemTransaction) ActiveDBItem).InventoryHousing_CU_ID = Convert.ToInt32(InventoryHousing_CU_ID);

			if (InventoryItem_CU_ID != null)
				((InventoryItemTransaction) ActiveDBItem).InventoryItem_CU_ID = Convert.ToInt32(InventoryItem_CU_ID);

			if (UnitMeasurment_CU_ID != null)
				((InventoryItemTransaction) ActiveDBItem).UnitMeasurment_CU_ID = Convert.ToInt32(UnitMeasurment_CU_ID);

			if (Quantity != null)
				((InventoryItemTransaction) ActiveDBItem).Quantity = Convert.ToDouble(Quantity);

			if (InventoryItemTransactionType != null)
			{
				((InventoryItemTransaction)ActiveDBItem).InventoryItemTransactionType_P_ID =
									Convert.ToInt32(InventoryItemTransactionType);
				DB_InventoryItemTransactionType transactionType =
					(DB_InventoryItemTransactionType) Convert.ToInt32(InventoryItemTransactionType);
				switch (transactionType)
				{
					case DB_InventoryItemTransactionType.StartingInventoryBalance:
						((InventoryItemTransaction) ActiveDBItem).TransactionFactor = 1;
						break;
				}
			}

			if (ExpirationDate != null)
				((InventoryItemTransaction) ActiveDBItem).ExpirationDate = Convert.ToDateTime(ExpirationDate);

			if (Date != null)
				((InventoryItemTransaction) ActiveDBItem).Date = Convert.ToDateTime(Date);

			((InventoryItemTransaction) ActiveDBItem).IsOnDuty = true;
			switch (((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((InventoryItemTransaction) ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int) ViewerName.InventoryItem_InventoryHousing_Viewer; }
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
			List<InventoryItemTransaction> list = InventoryItemTransaction.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InventoryItemTransaction>();

				((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).CommonTransactionType =
					DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override string MessageToView { get; set; }

		public override bool ValidateBeforeSave(ref string message)
		{
			if (InventoryHousing_CU_ID == null)
			{
				MessageToView = "يجـب إختيــار المخـــزن";
				return false;
			}

			if (InventoryItem_CU_ID == null)
			{
				MessageToView = "يجـب إختيــار المنتـــج";
				return false;
			}

			if (UnitMeasurment_CU_ID == null)
			{
				MessageToView = "يجـب إختيــار وحــدة القيـــاس";
				return false;
			}

			if (InventoryItemTransactionType == null)
			{
				MessageToView = "يجـب نــوع العمليـــة";
				return false;
			}

			if (Date == null)
			{
				MessageToView = "يجـب إختيــار تـاريـــخ الإدخـــال";
				return false;
			}

			return true;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((InventoryItemTransaction) ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((InventoryItemTransaction) ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			InventoryHousing_CU_ID = ((InventoryItemTransaction) ActiveDBItem).InventoryHousing_CU_ID;
			InventoryItem_CU_ID = ((InventoryItemTransaction) ActiveDBItem).InventoryItem_CU_ID;
			Quantity = ((InventoryItemTransaction) ActiveDBItem).Quantity;
			UnitMeasurment_CU_ID = ((InventoryItemTransaction) ActiveDBItem).UnitMeasurment_CU_ID;
			ExpirationDate = ((InventoryItemTransaction) ActiveDBItem).ExpirationDate;
			Date = ((InventoryItemTransaction) ActiveDBItem).Date;

			((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).ID =
				((InventoryItemTransaction) ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((InventoryItemTransaction) ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InventoryItemTransaction) entity).RemoveItem();
		}

		#endregion

		#region Implementation of IInventoryItem_InventoryHousing_Viewer

		public object InventoryItem_CU_ID
		{
			get { return ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).InventoryItem_CU_ID; }
			set { ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).InventoryItem_CU_ID = value; }
		}

		public object InventoryHousing_CU_ID
		{
			get { return ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).InventoryHousing_CU_ID; }
			set { ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).InventoryHousing_CU_ID = value; }
		}

		public object Quantity
		{
			get { return ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).Quantity; }
			set { ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).Quantity = value; }
		}

		public object UnitMeasurment_CU_ID
		{
			get { return ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).UnitMeasurment_CU_ID; }
			set { ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).UnitMeasurment_CU_ID = value; }
		}

		public object InventoryItemTransactionType
		{
			get { return ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).InventoryItemTransactionType; }
			set { ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).InventoryItemTransactionType = value; }
		}

		public object ExpirationDate
		{
			get { return ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).ExpirationDate; }
			set { ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).ExpirationDate = value; }
		}

		public object Date
		{
			get { return ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).Date; }
			set { ((IInventoryItem_InventoryHousing_Viewer) ActiveCollector.ActiveViewer).Date = value; }
		}

		#endregion
	}
}
