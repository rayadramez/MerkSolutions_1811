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
	public class InventoryItem_UnitMeasurment_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IInventoryItem_UnitMeasurment_Viewer
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

			ID = ((IInventoryItem_UnitMeasurment_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (List_InventoryItem_UnitMeasurment == null || List_InventoryItem_UnitMeasurment.Count == 0)
				return false;

			RelatedViewers = ((IInventoryItem_UnitMeasurment_Viewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.UserGroup_Viewer; }
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
			List<InventoryItem_UnitMeasurment_cu> list = InventoryItem_UnitMeasurment_cu.ItemsList.FindAll(item => item.IsOnDuty);
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<InventoryItem_UnitMeasurment_cu>();
				((IInventoryItem_UnitMeasurment_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			foreach (InventoryItem_UnitMeasurment_cu inventoryItem_UnitMeasurment in List_InventoryItem_UnitMeasurment)
			{
				((InventoryItem_UnitMeasurment_cu)ActiveDBItem).InventoryItem_CU_ID =
					inventoryItem_UnitMeasurment.InventoryItem_CU_ID;
				((InventoryItem_UnitMeasurment_cu)ActiveDBItem).UnitMeasurment_CU_ID =
					inventoryItem_UnitMeasurment.UnitMeasurment_CU_ID;

				if (UserID != null)
					((InventoryItem_UnitMeasurment_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

				((InventoryItem_UnitMeasurment_cu)ActiveDBItem).IsOnDuty = true;
				switch (((IInventoryItem_UnitMeasurment_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
				{
					case DB_CommonTransactionType.DeleteExisting:
						((InventoryItem_UnitMeasurment_cu)ActiveDBItem).IsOnDuty = false;
						break;
				}

				((InventoryItem_UnitMeasurment_cu) ActiveCollector.ActiveDBItem).SaveChanges();
			}

			((InventoryItem_UnitMeasurment_cu)ActiveCollector.ActiveDBItem).LoadItemsList();

			return true;
		}

		public override void Edit(IDBCommon entity)
		{
			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((InventoryItem_UnitMeasurment_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IInventoryItem_UnitMeasurment_Viewer

		public List<InventoryItem_UnitMeasurment_cu> List_InventoryItem_UnitMeasurment
		{
			get
			{
				return ((IInventoryItem_UnitMeasurment_Viewer) ActiveCollector.ActiveViewer).List_InventoryItem_UnitMeasurment;
			}
			set
			{
				((IInventoryItem_UnitMeasurment_Viewer) ActiveCollector.ActiveViewer).List_InventoryItem_UnitMeasurment = value;
			}
		}

		#endregion
	}
}
