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
	public class ServiceCategory_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IServiceCategoryViewer
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

			ID = ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (NameP != null)
				((ServiceCategory_cu)ActiveDBItem).Name_P = NameP.ToString();

			if (NameS != null)
				((ServiceCategory_cu)ActiveDBItem).Name_S = NameS.ToString();

			if (InternalCode != null)
				((ServiceCategory_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (ServiceType != null)
				((ServiceCategory_cu) ActiveDBItem).ServiceType_P_ID = Convert.ToInt32(ServiceType);

			((ServiceCategory_cu)ActiveDBItem).IsMedical = Convert.ToBoolean(IsMedical);

			((ServiceCategory_cu)ActiveDBItem).AllowAdmission = Convert.ToBoolean(AlloAdmission);

			if (Description != null)
				((ServiceCategory_cu)ActiveDBItem).Description = Description.ToString();

			if (UserID != null)
				((ServiceCategory_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((ServiceCategory_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IServiceCategoryViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((ServiceCategory_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<ServiceCategory_cu> list = ServiceCategory_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<ServiceCategory_cu>();

				((IServiceCategoryViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((ServiceCategory_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((ServiceCategory_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			NameP = ((ServiceCategory_cu)ActiveDBItem).Name_P;
			NameS = ((ServiceCategory_cu)ActiveDBItem).Name_S;
			InternalCode = ((ServiceCategory_cu)ActiveDBItem).InternalCode;
			ServiceType = ((ServiceCategory_cu)ActiveDBItem).ServiceType_P_ID;
			AlloAdmission = ((ServiceCategory_cu)ActiveDBItem).AllowAdmission;
			Description = ((ServiceCategory_cu)ActiveDBItem).Description;
			IsMedical = ((ServiceCategory_cu)ActiveDBItem).IsMedical;

			((IServiceCategoryViewer)ActiveCollector.ActiveViewer).ID = ((ServiceCategory_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((ServiceCategory_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((ServiceCategory_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IServiceCategoryViewer

		public object NameP
		{
			get { return ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).NameP; }
			set { ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).NameP = value; }
		}

		public object NameS
		{
			get { return ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).NameS; }
			set { ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).NameS = value; }
		}

		public object InternalCode
		{
			get { return ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object ServiceType
		{
			get { return ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).ServiceType; }
			set { ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).ServiceType = value; }
		}

		public object IsMedical
		{
			get { return ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).IsMedical; }
			set { ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).IsMedical = value; }
		}

		public object AlloAdmission
		{
			get { return ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).AlloAdmission; }
			set { ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).AlloAdmission = value; }
		}

		public object Description
		{
			get { return ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IServiceCategoryViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
