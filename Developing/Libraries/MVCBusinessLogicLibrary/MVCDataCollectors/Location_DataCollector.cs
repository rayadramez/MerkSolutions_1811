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
	public class Location_DataCollector<TEntity> : AbstractDataCollector<TEntity>, ILocationViewer
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

			ID = ((ILocationViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((Location_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((Location_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (InternalCode != null)
				((Location_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (Description != null)
				((Location_cu)ActiveDBItem).Description = Description.ToString();

			if (Address != null)
				((Location_cu)ActiveDBItem).Address = Address.ToString();

			if (Country_CU_ID != null)
				((Location_cu)ActiveDBItem).Country_CU_ID = Convert.ToInt32(Country_CU_ID);

			if (City_CU_ID != null)
				((Location_cu)ActiveDBItem).City_CU_ID = Convert.ToInt32(City_CU_ID);

			if (Region_CU_ID != null)
				((Location_cu)ActiveDBItem).Region_CU_ID = Convert.ToInt32(Region_CU_ID);

			if (Territory_CU_ID != null)
				((Location_cu)ActiveDBItem).Territory_CU_ID = Convert.ToInt32(Territory_CU_ID);

			((Location_cu) ActiveDBItem).Organization_P_ID = Convert.ToInt32(ApplicationStaticConfiguration.Organization);

			if (UserID != null)
				((Location_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((Location_cu)ActiveDBItem).IsOnDuty = true;
			switch (((ILocationViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((Location_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((ILocationViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.StationPointStage_Viewer; }
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
			List<Location_cu> list = Location_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CheckIfActiveDBItemExists()
		{
			if (ActiveCollector.ActiveDBItem == null)
				return true;

			foreach (Location_cu dbItem in Location_cu.ItemsList)
				if (dbItem.Equals(Name_P))
				{
					MessageToView = "هـذا المـوقـــع موجـود بالفعـــل" + "\r\n" + "لا يمكــن الإضـافــــة";
					return true;
				}

			return false;
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			if (Name_P == null)
			{
				MessageToView = "يجـــب كتـابـــة الإســـم الأول";
				return false;
			}

			return true;
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<Location_cu>();

				((ILocationViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((Location_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((Location_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((Location_cu)ActiveDBItem).Name_P;
			Name_S = ((Location_cu)ActiveDBItem).Name_S;
			Country_CU_ID = ((Location_cu)ActiveDBItem).Country_CU_ID;
			City_CU_ID = ((Location_cu)ActiveDBItem).City_CU_ID;
			Region_CU_ID = ((Location_cu)ActiveDBItem).Region_CU_ID;
			Territory_CU_ID = ((Location_cu)ActiveDBItem).Territory_CU_ID;
			Address = ((Location_cu)ActiveDBItem).Address;
			InternalCode = ((Location_cu)ActiveDBItem).InternalCode;
			Description = ((Location_cu)ActiveDBItem).Description;

			((ILocationViewer)ActiveCollector.ActiveViewer).ID = ((Location_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((Location_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((Location_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of ILocationViewer

		public object Name_P
		{
			get { return ((ILocationViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((ILocationViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((ILocationViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((ILocationViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object Country_CU_ID
		{
			get { return ((ILocationViewer)ActiveCollector.ActiveViewer).Country_CU_ID; }
			set { ((ILocationViewer)ActiveCollector.ActiveViewer).Country_CU_ID = value; }
		}

		public object City_CU_ID
		{
			get { return ((ILocationViewer)ActiveCollector.ActiveViewer).City_CU_ID; }
			set { ((ILocationViewer)ActiveCollector.ActiveViewer).City_CU_ID = value; }
		}

		public object Region_CU_ID
		{
			get { return ((ILocationViewer)ActiveCollector.ActiveViewer).Region_CU_ID; }
			set { ((ILocationViewer)ActiveCollector.ActiveViewer).Region_CU_ID = value; }
		}

		public object Territory_CU_ID
		{
			get { return ((ILocationViewer)ActiveCollector.ActiveViewer).Territory_CU_ID; }
			set { ((ILocationViewer)ActiveCollector.ActiveViewer).Territory_CU_ID = value; }
		}

		public object Description
		{
			get { return ((ILocationViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((ILocationViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		public object Address
		{
			get { return ((ILocationViewer)ActiveCollector.ActiveViewer).Address; }
			set { ((ILocationViewer)ActiveCollector.ActiveViewer).Address = value; }
		}

		public object InternalCode
		{
			get { return ((ILocationViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((ILocationViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		#endregion
	}
}
