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
	public class InventoryItem_Printing_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IInventoryItem_Printing_Viewer
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

			ID = ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (InventoryItemID != null)
				((InventoryItem_Printing_cu)ActiveDBItem).InventoryItem_CU_ID = Convert.ToInt32(InventoryItemID);

			if (Date != null)
				((InventoryItem_Printing_cu)ActiveDBItem).Date = Convert.ToDateTime(Date);

			if (PrintingMaxTimeInMinutes != null)
				((InventoryItem_Printing_cu)ActiveDBItem).PrintingMaxTimeInMinutes = Convert.ToDouble(PrintingMaxTimeInMinutes);

			if (AddedMinutes != null)
				((InventoryItem_Printing_cu)ActiveDBItem).AddedMinutes = Convert.ToDouble(AddedMinutes);

			if (PrintingAverageUnitCostPrice != null)
				((InventoryItem_Printing_cu)ActiveDBItem).PrintingAverageUnitCostPrice = Convert.ToDouble(PrintingAverageUnitCostPrice);

			if (UseRealCost != null)
				((InventoryItem_Printing_cu)ActiveDBItem).UseRealCost = Convert.ToBoolean(UseRealCost);

			if (PrintingRealCostPrice != null)
				((InventoryItem_Printing_cu)ActiveDBItem).PrintingRealCostPrice = Convert.ToDouble(PrintingRealCostPrice);

			if (Description != null)
				((InventoryItem_Printing_cu) ActiveDBItem).Description = Description.ToString();

			if (UserID != null)
				((InventoryItem_Printing_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((InventoryItem_Printing_cu)ActiveDBItem).IsOnDuty = true;

			switch (((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((InventoryItem_Printing_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }
		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItem_Area_Viewer; }
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
			List<InventoryItem_Printing_cu> list = InventoryItem_Printing_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InventoryItem_Printing_cu>();

				((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			return true;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((InventoryItem_Printing_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((InventoryItem_Printing_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			InventoryItemID = ((InventoryItem_Printing_cu)ActiveDBItem).InventoryItem_CU_ID;
			Date = ((InventoryItem_Printing_cu)ActiveDBItem).Date;
			PrintingMaxTimeInMinutes = ((InventoryItem_Printing_cu)ActiveDBItem).PrintingMaxTimeInMinutes;
			AddedMinutes = ((InventoryItem_Printing_cu)ActiveDBItem).AddedMinutes;
			PrintingAverageUnitCostPrice = ((InventoryItem_Printing_cu)ActiveDBItem).PrintingAverageUnitCostPrice;
			UseRealCost = ((InventoryItem_Printing_cu)ActiveDBItem).UseRealCost;
			PrintingRealCostPrice = ((InventoryItem_Printing_cu)ActiveDBItem).PrintingRealCostPrice;
			Description = ((InventoryItem_Printing_cu)ActiveDBItem).Description;

			((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).ID = ((InventoryItem_Printing_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((InventoryItem_Printing_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InventoryItem_Printing_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IInventoryItem_Printing_Viewer

		public object InventoryItemID
		{
			get { return ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).InventoryItemID; }
			set { ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).InventoryItemID = value; }
		}

		public object Date
		{
			get { return ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).Date; }
			set { ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).Date = value; }
		}

		public object PrintingMaxTimeInMinutes
		{
			get { return ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).PrintingMaxTimeInMinutes; }
			set { ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).PrintingMaxTimeInMinutes = value; }
		}

		public object AddedMinutes
		{
			get { return ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).AddedMinutes; }
			set { ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).AddedMinutes = value; }
		}

		public object PrintingAverageUnitCostPrice
		{
			get { return ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).PrintingAverageUnitCostPrice; }
			set { ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).PrintingAverageUnitCostPrice = value; }
		}

		public object UseRealCost
		{
			get { return ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).UseRealCost; }
			set { ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).UseRealCost = value; }
		}

		public object PrintingRealCostPrice
		{
			get { return ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).PrintingRealCostPrice; }
			set { ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).PrintingRealCostPrice = value; }
		}

		public object Description
		{
			get { return ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IInventoryItem_Printing_Viewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
