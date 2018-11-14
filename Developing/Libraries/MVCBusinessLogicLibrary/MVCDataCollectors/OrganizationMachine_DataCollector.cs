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
	public class OrganizationMachine_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IOrganizationMachine_Viewer
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

			ID = ((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((OrganizationMachine_cu)ActiveDBItem).Name_P = Name_P.ToString();

			//if (StationPoint_CU_ID != null)
			//	((OrganizationMachine_cu)ActiveDBItem).StationPoint_CU_ID = Convert.ToInt32(StationPoint_CU_ID);

			//if (StationPointStage_CU_ID != null)
			//	((OrganizationMachine_cu)ActiveDBItem).StationPointStage_CU_ID = Convert.ToInt32(StationPointStage_CU_ID);

			((OrganizationMachine_cu)ActiveDBItem).OrganizationID = Convert.ToInt32(ApplicationStaticConfiguration.Organization);

			if (SkinName != null)
				((OrganizationMachine_cu)ActiveDBItem).SkinName = SkinName.ToString();

			if (Color != null)
				((OrganizationMachine_cu)ActiveDBItem).Color = Color.ToString();

			if (UserID != null)
				((OrganizationMachine_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((OrganizationMachine_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((OrganizationMachine_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.ChartOfAccountViewer; }
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
			List<OrganizationMachine_cu> list = OrganizationMachine_cu.ItemsList;
			return list.ToArray();
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			if (Name_P == null)
			{
				MessageToView = "يجــب كتابـــة إسـم الجهـــــاز";
				return false;
			}

			if (StationPoint_CU_ID == null)
			{
				MessageToView = "يجــب إختيـــار العيــادة";
				return false;
			}

			return true;
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<OrganizationMachine_cu>();

				((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((OrganizationMachine_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((OrganizationMachine_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((OrganizationMachine_cu)ActiveDBItem).Name_P;
			//StationPoint_CU_ID = ((OrganizationMachine_cu)ActiveDBItem).StationPoint_CU_ID;
			//StationPointStage_CU_ID = ((OrganizationMachine_cu)ActiveDBItem).StationPointStage_CU_ID;
			SkinName = ((OrganizationMachine_cu)ActiveDBItem).SkinName;
			Color = ((OrganizationMachine_cu)ActiveDBItem).Color;

			((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).ID = ((OrganizationMachine_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((OrganizationMachine_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((OrganizationMachine_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IOrganizationMachine_Viewer

		public object Name_P
		{
			get { return ((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object StationPoint_CU_ID
		{
			get { return ((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).StationPoint_CU_ID; }
			set { ((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).StationPoint_CU_ID = value; }
		}

		public object StationPointStage_CU_ID
		{
			get { return ((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).StationPointStage_CU_ID; }
			set { ((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).StationPointStage_CU_ID = value; }
		}

		public object OrganizationID
		{
			get { return ((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).OrganizationID; }
			set { ((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).OrganizationID = value; }
		}

		public object SkinName
		{
			get { return ((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).SkinName; }
			set { ((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).SkinName = value; }
		}

		public object Color
		{
			get { return ((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).Color; }
			set { ((IOrganizationMachine_Viewer)ActiveCollector.ActiveViewer).Color = value; }
		}

		#endregion
	}
}
