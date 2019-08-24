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
	public class InventoryItem_Area_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IInventoryItem_Area_Viewer
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

			ID = ((IInventoryItem_Area_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Width != null)
				((InventoryItem_Area)ActiveDBItem).Width = Convert.ToDouble(Width);

			if (Height != null)
				((InventoryItem_Area)ActiveDBItem).Height = Convert.ToDouble(Height);

			if (Count != null)
				((InventoryItem_Area)ActiveDBItem).Count = Convert.ToInt32(Count);

			if (InternalCode != null)
				((InventoryItem_Area)ActiveDBItem).InternalCode = InternalCode.ToString();

			((InventoryItem_Area)ActiveDBItem).IsOnDuty = true;
			switch (((IInventoryItem_Area_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((InventoryItem_Area)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IInventoryItem_Area_Viewer)ActiveCollector.ActiveViewer).RelatedViewers;

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

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InventoryItem_Area>();

				((IInventoryItem_Area_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((InventoryItem_Area)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((InventoryItem_Area)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Width = ((InventoryItem_Area)ActiveDBItem).Width;
			Height = ((InventoryItem_Area)ActiveDBItem).Height;
			Count = ((InventoryItem_Area)ActiveDBItem).Count;
			InternalCode = ((InventoryItem_Area)ActiveDBItem).InternalCode;

			((IInventoryItem_Area_Viewer)ActiveCollector.ActiveViewer).ID = ((InventoryItem_Area)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((InventoryItem_Area)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InventoryItem_Area)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IInventoryItem_Area_Viewer

		public object InventoryItemID
		{
			get { return ((IInventoryItem_Area_Viewer)ActiveCollector.ActiveViewer).InventoryItemID; }
			set { ((IInventoryItem_Area_Viewer)ActiveCollector.ActiveViewer).InventoryItemID = value; }
		}

		public object Width
		{
			get { return ((IInventoryItem_Area_Viewer)ActiveCollector.ActiveViewer).Width; }
			set { ((IInventoryItem_Area_Viewer)ActiveCollector.ActiveViewer).Width = value; }
		}

		public object Height
		{
			get { return ((IInventoryItem_Area_Viewer)ActiveCollector.ActiveViewer).Height; }
			set { ((IInventoryItem_Area_Viewer)ActiveCollector.ActiveViewer).Height = value; }
		}

		public object Count
		{
			get { return ((IInventoryItem_Area_Viewer)ActiveCollector.ActiveViewer).Count; }
			set { ((IInventoryItem_Area_Viewer)ActiveCollector.ActiveViewer).Count = value; }
		}

		public object InternalCode
		{
			get { return ((IInventoryItem_Area_Viewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IInventoryItem_Area_Viewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		#endregion
	}
}
