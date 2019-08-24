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
	public class InventoryItemPrice_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IInventoryItemPriceViewer
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

			ID = ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (InventoryItem_CU_ID != null)
				((InventoryItemPrice_cu)ActiveDBItem).InventoryItem_CU_ID = Convert.ToInt32(InventoryItem_CU_ID);

			if (UnitMeasurment_CU_ID != null)
			{
				InventoryItem_UnitMeasurment_cu bridge =
					InventoryItem_UnitMeasurment_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.UnitMeasurment_CU_ID).Equals(Convert.ToInt32(UnitMeasurment_CU_ID)) &&
							Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(InventoryItem_CU_ID)));

				if (bridge != null)
					((InventoryItemPrice_cu)ActiveDBItem).InventoryItem_UnitMeasurment_CU_ID = Convert.ToInt32(bridge.ID);
			}

			if (Date != null)
				((InventoryItemPrice_cu)ActiveDBItem).Date = Convert.ToDateTime(Date);

			if (Price != null)
				((InventoryItemPrice_cu)ActiveDBItem).Price = Convert.ToDouble(Price);

			if (PriceType_P_ID != null)
				((InventoryItemPrice_cu)ActiveDBItem).PriceType_P_ID = Convert.ToInt32(PriceType_P_ID);

			if (Customer_CU_ID != null)
				((InventoryItemPrice_cu)ActiveDBItem).Customer_CU_ID = Convert.ToInt32(Customer_CU_ID);

			if (Supplier_CU_ID != null)
				((InventoryItemPrice_cu)ActiveDBItem).Supplier_CU_ID = Convert.ToInt32(Supplier_CU_ID);

			if (UserID != null)
				((InventoryItemPrice_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((InventoryItemPrice_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((InventoryItemPrice_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItemPrice_Viewer; }
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
				ActiveDBItem = DBCommon.CreateNewDBEntity<InventoryItemPrice_cu>();

				((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override object[] CollectSearchCriteria()
		{
			List<InventoryItemPrice_cu> list = InventoryItemPrice_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			if (InventoryItem_CU_ID == null)
			{
				MessageToView = "يجـب إختيــار الصنـــــف";
				return false;
			}

			if (UnitMeasurment_CU_ID == null)
			{
				MessageToView = "يجـب إختيــار وحـــدة القيـــــاس";
				return false;
			}

			if (Date == null)
			{
				MessageToView = "يجـب إختيــار تـاريــــخ الإدخــــال";
				return false;
			}

			return true;
		}

		public override bool CheckIfActiveDBItemExists()
		{
			List<InventoryItemPrice_cu> list = InventoryItemPrice_cu.ItemsList;
			if (list.Count == 0)
				return false;

			InventoryItem_UnitMeasurment_cu bridge = null;
			if (UnitMeasurment_CU_ID != null)
			{
				bridge =
					InventoryItem_UnitMeasurment_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.UnitMeasurment_CU_ID).Equals(Convert.ToInt32(UnitMeasurment_CU_ID)) &&
							Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(InventoryItem_CU_ID)));
			}
			else
				return false;

			if (bridge == null)
				return false;

			List<InventoryItemPrice_cu> inventoryItemPricesList =
				list.FindAll(
					item =>
						Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(InventoryItem_CU_ID)) &&
						Convert.ToInt32(item.InventoryItem_UnitMeasurment_CU_ID).Equals(Convert.ToInt32(bridge.ID)));
			if (inventoryItemPricesList.Count == 0)
				return false;

			InventoryItemPrice_cu inventoryItemPrice = null;
			if (PriceType_P_ID != null && Convert.ToInt32(PriceType_P_ID).Equals((int) DB_PriceType.SellingPrice) &&
			    Customer_CU_ID != null)
				inventoryItemPrice =
					inventoryItemPricesList.Find(
						item =>
							Convert.ToInt32(item.Customer_CU_ID).Equals(Convert.ToInt32(Customer_CU_ID)) &&
							Convert.ToDateTime(item.Date)
								.Date.StartOfDayDateTime()
								.Equals(Convert.ToDateTime(Date).Date.StartOfDayDateTime()) &&
							Convert.ToInt32(item.PriceType_P_ID).Equals(PriceType_P_ID));
			else if (PriceType_P_ID != null && Convert.ToInt32(PriceType_P_ID).Equals((int) DB_PriceType.PurchasingPrice) &&
			         Supplier_CU_ID != null)
				inventoryItemPrice =
					inventoryItemPricesList.Find(
						item =>
							Convert.ToInt32(item.Supplier_CU_ID).Equals(Convert.ToInt32(Supplier_CU_ID)) &&
							Convert.ToDateTime(item.Date)
								.Date.StartOfDayDateTime()
								.Equals(Convert.ToDateTime(Date).Date.StartOfDayDateTime()) &&
							Convert.ToInt32(item.PriceType_P_ID).Equals(PriceType_P_ID));
			else if (PriceType_P_ID != null)
				inventoryItemPrice =
					inventoryItemPricesList.Find(
						item =>
							Convert.ToDateTime(item.Date)
								.Date.StartOfDayDateTime()
								.Equals(Convert.ToDateTime(Date).Date.StartOfDayDateTime()) &&
							Convert.ToInt32(item.PriceType_P_ID).Equals(PriceType_P_ID));
			if (inventoryItemPrice != null)
				return true;

			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((InventoryItemPrice_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((InventoryItemPrice_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			InventoryItem_CU_ID = ((InventoryItemPrice_cu)ActiveDBItem).InventoryItem_CU_ID;
			InventoryItem_UnitMeasurment_cu bridge =
				InventoryItem_UnitMeasurment_cu.ItemsList.Find(
					item =>
						Convert.ToInt32(item.ID)
							.Equals(Convert.ToInt32(((InventoryItemPrice_cu)ActiveDBItem).InventoryItem_UnitMeasurment_CU_ID)));
			if (bridge != null)
			{
				UnitMeasurment_cu unitMeasurment =
					UnitMeasurment_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bridge.UnitMeasurment_CU_ID)));
				if (unitMeasurment != null)
					UnitMeasurment_CU_ID = unitMeasurment.ID;
			}
			Date = ((InventoryItemPrice_cu)ActiveDBItem).Date;
			Price = ((InventoryItemPrice_cu)ActiveDBItem).Price;
			Customer_CU_ID = ((InventoryItemPrice_cu)ActiveDBItem).Customer_CU_ID;
			PriceType_P_ID = ((InventoryItemPrice_cu)ActiveDBItem).PriceType_P_ID;
			Supplier_CU_ID = ((InventoryItemPrice_cu)ActiveDBItem).Supplier_CU_ID;

			((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).ID = ((InventoryItemPrice_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((InventoryItemPrice_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InventoryHousing_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IInventoryItemPriceViewer

		public object InventoryItem_CU_ID
		{
			get { return ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).InventoryItem_CU_ID; }
			set { ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).InventoryItem_CU_ID = value; }
		}

		public object UnitMeasurment_CU_ID
		{
			get { return ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).UnitMeasurment_CU_ID; }
			set { ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).UnitMeasurment_CU_ID = value; }
		}

		public object Date
		{
			get { return ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).Date; }
			set { ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).Date = value; }
		}

		public object Price
		{
			get { return ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).Price; }
			set { ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).Price = value; }
		}

		public object Customer_CU_ID
		{
			get { return ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).Customer_CU_ID; }
			set { ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).Customer_CU_ID = value; }
		}

		public object PriceType_P_ID
		{
			get { return ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).PriceType_P_ID; }
			set { ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).PriceType_P_ID = value; }
		}

		public object Supplier_CU_ID
		{
			get { return ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).Supplier_CU_ID; }
			set { ((IInventoryItemPriceViewer)ActiveCollector.ActiveViewer).Supplier_CU_ID = value; }
		}

		#endregion
	}
}

