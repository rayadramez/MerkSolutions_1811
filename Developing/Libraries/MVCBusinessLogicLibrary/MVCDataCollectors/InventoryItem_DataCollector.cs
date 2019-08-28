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
	public class InventoryItem_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IInventoryItemViewer
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

			ID = ((IInventoryItemViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((InventoryItem_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((InventoryItem_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (InventoryHousing_CU_ID != null)
				((InventoryItem_cu) ActiveDBItem).InventoryHousing_CU_ID = Convert.ToInt32(InventoryHousing_CU_ID);

			if (InventoryItemCategory_CU_ID != null)
				((InventoryItem_cu)ActiveDBItem).InventoryItemCategory_CU_ID = Convert.ToInt32(InventoryItemCategory_CU_ID);

			if (InventoryItemBrand_CU_ID != null)
				((InventoryItem_cu)ActiveDBItem).InventoryItemBrand_CU_ID = Convert.ToInt32(InventoryItemBrand_CU_ID);

			if (DefaultUnitMeasurment_CU_ID != null)
				((InventoryItem_cu)ActiveDBItem).InventoryTrackingUnitMeasurment_CU_ID = Convert.ToInt32(DefaultUnitMeasurment_CU_ID);

			if (InventoryItemType_P_ID != null)
				((InventoryItem_cu)ActiveDBItem).InventoryItemType_P_ID = Convert.ToInt32(InventoryItemType_P_ID);

			if (InternalCode != null)
				((InventoryItem_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (DefaultBarcode != null)
				((InventoryItem_cu)ActiveDBItem).DefaultBarcode = DefaultBarcode.ToString();

			if (DefaultSellingPrice != null)
				((InventoryItem_cu)ActiveDBItem).DefaultSellingPrice = Convert.ToDouble(DefaultSellingPrice);

			if (DefaultCost != null)
				((InventoryItem_cu)ActiveDBItem).DefaultCost = Convert.ToDouble(DefaultCost);

			if (RorderedPoint != null)
				((InventoryItem_cu)ActiveDBItem).RorderedPoint = Convert.ToDouble(RorderedPoint);

			if (StockMinLevel != null)
				((InventoryItem_cu)ActiveDBItem).StockMinLevel = Convert.ToDouble(StockMinLevel);

			if (AcceptOverrideMinAmount != null)
				((InventoryItem_cu)ActiveDBItem).AcceptOverrideMinAmount = Convert.ToBoolean(AcceptOverrideMinAmount);

			if (CanBeSold != null)
				((InventoryItem_cu)ActiveDBItem).CanBeSold = Convert.ToBoolean(CanBeSold);

			if (IsAvailable != null)
				((InventoryItem_cu)ActiveDBItem).IsAvailable = Convert.ToBoolean(IsAvailable);

			if (AcceptPartingSelling != null)
				((InventoryItem_cu)ActiveDBItem).AcceptPartingSelling = Convert.ToBoolean(AcceptPartingSelling);

			if (IsCountable != null)
				((InventoryItem_cu)ActiveDBItem).IsCountable = Convert.ToBoolean(IsCountable);

			if (SellingStartDate != null)
				((InventoryItem_cu)ActiveDBItem).SellingStartDate = Convert.ToDateTime(SellingStartDate);

			if (SellingEndDate != null)
				((InventoryItem_cu)ActiveDBItem).SellingEndDate = Convert.ToDateTime(SellingEndDate);

			if (ExpirationDate != null)
				((InventoryItem_cu)ActiveDBItem).ExpirationDate = Convert.ToDateTime(ExpirationDate);

			if (Description != null)
				((InventoryItem_cu)ActiveDBItem).Description = Description.ToString();

			if (Width != null)
				((InventoryItem_cu)ActiveDBItem).Width = Convert.ToDouble(Width);

			if (Height != null)
				((InventoryItem_cu)ActiveDBItem).Height = Convert.ToDouble(Height);

			if (Depth != null)
				((InventoryItem_cu)ActiveDBItem).Depth = Convert.ToDouble(Depth);

			((InventoryItem_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IInventoryItemViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((InventoryItem_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IInventoryItemViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItem_Viewer; }
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
			List<InventoryItem_cu> list = InventoryItem_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InventoryItem_cu>();

				((IInventoryItemViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((InventoryItem_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((InventoryItem_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((InventoryItem_cu)ActiveDBItem).Name_P;
			Name_S = ((InventoryItem_cu)ActiveDBItem).Name_S;
			InventoryHousing_CU_ID = ((InventoryItem_cu)ActiveDBItem).InventoryHousing_CU_ID;
			InventoryItemCategory_CU_ID = ((InventoryItem_cu)ActiveDBItem).InventoryItemCategory_CU_ID;
			InventoryItemBrand_CU_ID = ((InventoryItem_cu)ActiveDBItem).InventoryItemBrand_CU_ID;
			DefaultUnitMeasurment_CU_ID = ((InventoryItem_cu)ActiveDBItem).InventoryTrackingUnitMeasurment_CU_ID;
			InventoryItemType_P_ID = ((InventoryItem_cu)ActiveDBItem).InventoryItemType_P_ID;
			InternalCode = ((InventoryItem_cu)ActiveDBItem).InternalCode;
			DefaultBarcode = ((InventoryItem_cu)ActiveDBItem).DefaultBarcode;
			DefaultSellingPrice = ((InventoryItem_cu)ActiveDBItem).DefaultSellingPrice;
			DefaultCost = ((InventoryItem_cu)ActiveDBItem).DefaultCost;
			RorderedPoint = ((InventoryItem_cu)ActiveDBItem).RorderedPoint;
			StockMinLevel = ((InventoryItem_cu)ActiveDBItem).StockMinLevel;
			StockMaxLevel = ((InventoryItem_cu)ActiveDBItem).StockMaxLevel;
			AcceptOverrideMinAmount = ((InventoryItem_cu)ActiveDBItem).AcceptOverrideMinAmount;
			CanBeSold = ((InventoryItem_cu)ActiveDBItem).CanBeSold;
			IsAvailable = ((InventoryItem_cu)ActiveDBItem).IsAvailable;
			AcceptPartingSelling = ((InventoryItem_cu)ActiveDBItem).AcceptPartingSelling;
			IsCountable = ((InventoryItem_cu)ActiveDBItem).IsCountable;
			SellingStartDate = ((InventoryItem_cu)ActiveDBItem).SellingStartDate;
			SellingEndDate = ((InventoryItem_cu)ActiveDBItem).SellingEndDate;
			ExpirationDate = ((InventoryItem_cu)ActiveDBItem).ExpirationDate;
			Description = ((InventoryItem_cu)ActiveDBItem).Description;

			((IInventoryItemViewer)ActiveCollector.ActiveViewer).ID = ((InventoryItem_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((InventoryItem_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InventoryItem_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IInventoryItemViewer

		public object Name_P
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object InventoryHousing_CU_ID
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).InventoryHousing_CU_ID; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).InventoryHousing_CU_ID = value; }
		}

		public object InventoryItemCategory_CU_ID
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).InventoryItemCategory_CU_ID; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).InventoryItemCategory_CU_ID = value; }
		}

		public object InventoryItemBrand_CU_ID
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).InventoryItemBrand_CU_ID; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).InventoryItemBrand_CU_ID = value; }
		}

		public object DefaultUnitMeasurment_CU_ID
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).DefaultUnitMeasurment_CU_ID; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).DefaultUnitMeasurment_CU_ID = value; }
		}

		public object InventoryItemType_P_ID
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).InventoryItemType_P_ID; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).InventoryItemType_P_ID = value; }
		}

		public object InternalCode
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object DefaultBarcode
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).DefaultBarcode; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).DefaultBarcode = value; }
		}

		public object DefaultSellingPrice
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).DefaultSellingPrice; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).DefaultSellingPrice = value; }
		}

		public object DefaultCost
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).DefaultCost; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).DefaultCost = value; }
		}

		public object RorderedPoint
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).RorderedPoint; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).RorderedPoint = value; }
		}

		public object StockMinLevel
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).StockMinLevel; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).StockMinLevel = value; }
		}

		public object StockMaxLevel
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).StockMaxLevel; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).StockMaxLevel = value; }
		}

		public object AcceptOverrideMinAmount
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).AcceptOverrideMinAmount; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).AcceptOverrideMinAmount = value; }
		}

		public object CanBeSold
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).CanBeSold; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).CanBeSold = value; }
		}

		public object IsAvailable
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).IsAvailable; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).IsAvailable = value; }
		}

		public object AcceptPartingSelling
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).AcceptPartingSelling; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).AcceptPartingSelling = value; }
		}

		public object IsCountable
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).IsCountable; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).IsCountable = value; }
		}

		public object SellingStartDate
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).SellingStartDate; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).SellingStartDate = value; }
		}

		public object SellingEndDate
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).SellingEndDate; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).SellingEndDate = value; }
		}

		public object ExpirationDate
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).ExpirationDate; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).ExpirationDate = value; }
		}

		public object Description
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		public object Width
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).Width; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).Width = value; }
		}

		public object Height
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).Height; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).Height = value; }
		}

		public object Depth
		{
			get { return ((IInventoryItemViewer)ActiveCollector.ActiveViewer).Depth; }
			set { ((IInventoryItemViewer)ActiveCollector.ActiveViewer).Depth = value; }
		}

		#endregion
	}
}

