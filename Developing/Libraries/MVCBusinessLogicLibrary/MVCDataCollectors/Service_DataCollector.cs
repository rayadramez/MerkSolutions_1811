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
	public class Service_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IServiceViewer
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

			ID = ((IServiceViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((Service_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((Service_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (ServiceCategory_CU_ID != null)
				((Service_cu)ActiveDBItem).ServiceCategory_CU_ID = Convert.ToInt32(ServiceCategory_CU_ID);

			if (ServiceType_P_ID != null)
				((Service_cu)ActiveDBItem).ServiceType_P_ID = Convert.ToInt32(ServiceType_P_ID);

			if (ParentService_CU_ID != null)
				((Service_cu)ActiveDBItem).ParentService_CU_ID = Convert.ToInt32(ParentService_CU_ID);

			if (InternalCode != null)
				((Service_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (DefaultPriceFrom != null)
				((Service_cu)ActiveDBItem).DefaultPrice = Convert.ToDouble(DefaultPriceFrom);

			((Service_cu)ActiveDBItem).EnforceCategorization = Convert.ToBoolean(EnforceCategorization);
			((Service_cu)ActiveDBItem).IsDailyCharged = Convert.ToBoolean(IsDailyCharged);
			((Service_cu)ActiveDBItem).AllowAddmission = Convert.ToBoolean(AllowAddmission);

			if (Description != null)
				((Service_cu)ActiveDBItem).Description = Description.ToString();

			if (UserID != null)
				((Service_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((Service_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IServiceViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((Service_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IServiceViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.User_Viewer; }
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
			List<Service_cu> list = Service_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<Service_cu>();
				((IServiceViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((Service_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((Service_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((Service_cu)ActiveDBItem).Name_P;
			Name_S = ((Service_cu)ActiveDBItem).Name_S;
			ServiceCategory_CU_ID = ((Service_cu)ActiveDBItem).ServiceCategory_CU_ID;
			ServiceType_P_ID = ((Service_cu)ActiveDBItem).ServiceType_P_ID;
			ParentService_CU_ID = ((Service_cu)ActiveDBItem).ParentService_CU_ID;
			InternalCode = ((Service_cu)ActiveDBItem).InternalCode;
			EnforceCategorization = ((Service_cu)ActiveDBItem).EnforceCategorization;
			IsDailyCharged = ((Service_cu)ActiveDBItem).IsDailyCharged;
			DefaultPriceFrom = ((Service_cu)ActiveDBItem).DefaultPrice;
			AllowAddmission = ((Service_cu)ActiveDBItem).AllowAddmission;
			Description = ((Service_cu)ActiveDBItem).Description;

			((IServiceViewer)ActiveCollector.ActiveViewer).ID = ((Service_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((Service_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((Service_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IServiceViewer

		public object Name_P
		{
			get { return ((IServiceViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IServiceViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IServiceViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IServiceViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object ServiceCategory_CU_ID
		{
			get { return ((IServiceViewer)ActiveCollector.ActiveViewer).ServiceCategory_CU_ID; }
			set { ((IServiceViewer)ActiveCollector.ActiveViewer).ServiceCategory_CU_ID = value; }
		}

		public object ServiceType_P_ID
		{
			get { return ((IServiceViewer)ActiveCollector.ActiveViewer).ServiceType_P_ID; }
			set { ((IServiceViewer)ActiveCollector.ActiveViewer).ServiceType_P_ID = value; }
		}

		public object ParentService_CU_ID
		{
			get { return ((IServiceViewer)ActiveCollector.ActiveViewer).ParentService_CU_ID; }
			set { ((IServiceViewer)ActiveCollector.ActiveViewer).ParentService_CU_ID = value; }
		}

		public object InternalCode
		{
			get { return ((IServiceViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IServiceViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object EnforceCategorization
		{
			get { return ((IServiceViewer)ActiveCollector.ActiveViewer).EnforceCategorization; }
			set { ((IServiceViewer)ActiveCollector.ActiveViewer).EnforceCategorization = value; }
		}

		public object IsDailyCharged
		{
			get { return ((IServiceViewer)ActiveCollector.ActiveViewer).IsDailyCharged; }
			set { ((IServiceViewer)ActiveCollector.ActiveViewer).IsDailyCharged = value; }
		}

		public object DefaultPriceFrom
		{
			get { return ((IServiceViewer)ActiveCollector.ActiveViewer).DefaultPriceFrom; }
			set { ((IServiceViewer)ActiveCollector.ActiveViewer).DefaultPriceFrom = value; }
		}

		public object DefaultPriceTo
		{
			get { return ((IServiceViewer)ActiveCollector.ActiveViewer).DefaultPriceTo; }
			set { ((IServiceViewer)ActiveCollector.ActiveViewer).DefaultPriceTo = value; }
		}

		public object AllowAddmission
		{
			get { return ((IServiceViewer)ActiveCollector.ActiveViewer).AllowAddmission; }
			set { ((IServiceViewer)ActiveCollector.ActiveViewer).AllowAddmission = value; }
		}

		public object Description
		{
			get { return ((IServiceViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IServiceViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
