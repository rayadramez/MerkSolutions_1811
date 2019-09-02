using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationConfiguration;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class InventoryItem_RawMaterial_DataCollector<TEntity> : AbstractDataCollector<TEntity>,
		IInventoryItem_RawMaterial_Viewer
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

			ID = ((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (InventoryItemID != null)
				((InventoryItem_RawMaterial_cu)ActiveDBItem).InventoryItem_CU_ID = Convert.ToInt32(InventoryItemID);

			if (RawMaterialID != null)
				((InventoryItem_RawMaterial_cu)ActiveDBItem).RawMaterial_CU_ID = Convert.ToInt32(RawMaterialID);

			if (Width != null)
				((InventoryItem_RawMaterial_cu)ActiveDBItem).Width = Convert.ToDouble(Width);

			if (Height != null)
				((InventoryItem_RawMaterial_cu)ActiveDBItem).Height = Convert.ToDouble(Height);

			if (Count != null)
				((InventoryItem_RawMaterial_cu)ActiveDBItem).Count = Convert.ToInt32(Count);

			if (HasDimensions != null)
				((InventoryItem_RawMaterial_cu)ActiveDBItem).HasDimensions = Convert.ToBoolean(HasDimensions);

			if (UserID != null)
				((InventoryItem_RawMaterial_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((InventoryItem_RawMaterial_cu)ActiveDBItem).IsOnDuty = true;

			switch (((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((InventoryItem_RawMaterial_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }
		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItem_RawMaterial_Viewer; }
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
			List<InventoryItem_RawMaterial_cu> list =
				InventoryItem_RawMaterial_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InventoryItem_RawMaterial_cu>();

				((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
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

			if (((InventoryItem_RawMaterial_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((InventoryItem_RawMaterial_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			InventoryItemID = ((InventoryItem_RawMaterial_cu)ActiveDBItem).InventoryItem_CU_ID;
			RawMaterialID = ((InventoryItem_RawMaterial_cu)ActiveDBItem).RawMaterial_CU_ID;
			Width = ((InventoryItem_RawMaterial_cu)ActiveDBItem).Width;
			Height = ((InventoryItem_RawMaterial_cu)ActiveDBItem).Height;
			Count = ((InventoryItem_RawMaterial_cu)ActiveDBItem).Count;
			HasDimensions = ((InventoryItem_RawMaterial_cu)ActiveDBItem).HasDimensions;

			((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).ID = ((InventoryItem_RawMaterial_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((InventoryItem_RawMaterial_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InventoryItem_RawMaterial_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IInventoryItem_RawMaterial_Viewer

		public object InventoryItemID
		{
			get { return ((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).InventoryItemID; }
			set { ((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).InventoryItemID = value; }
		}

		public object RawMaterialID
		{
			get { return ((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).RawMaterialID; }
			set { ((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).RawMaterialID = value; }
		}

		public object Width
		{
			get { return ((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).Width; }
			set { ((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).Width = value; }
		}

		public object Height
		{
			get { return ((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).Height; }
			set { ((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).Height = value; }
		}

		public object Count
		{
			get { return ((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).Count; }
			set { ((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).Count = value; }
		}

		public object HasDimensions
		{
			get { return ((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).HasDimensions; }
			set { ((IInventoryItem_RawMaterial_Viewer)ActiveCollector.ActiveViewer).HasDimensions = value; }
		}

		#endregion
	}
}
