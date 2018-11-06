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
	public class DoctorCategory_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IDoctorCategoryViewer
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

			ID = ((IDoctorCategoryViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((DoctorCategory_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((DoctorCategory_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (Description != null)
				((DoctorCategory_cu)ActiveDBItem).Description = Description.ToString();

			if (UserID != null)
				((DoctorCategory_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((DoctorCategory_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IDoctorCategoryViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((DoctorCategory_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IDoctorCategoryViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.CashBoxViewer; }
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
			List<DoctorCategory_cu> list = DoctorCategory_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<DoctorCategory_cu>();

				((IDoctorCategoryViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
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

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((DoctorCategory_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((DoctorCategory_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((DoctorCategory_cu)ActiveDBItem).Name_P;
			Name_S = ((DoctorCategory_cu)ActiveDBItem).Name_S;
			Description = ((DoctorCategory_cu)ActiveDBItem).Description;

			((IDoctorCategoryViewer)ActiveCollector.ActiveViewer).ID = ((DoctorCategory_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((DoctorCategory_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((DoctorCategory_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IDoctorCategoryViewer

		public object Name_P
		{
			get { return ((IDoctorCategoryViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IDoctorCategoryViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IDoctorCategoryViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IDoctorCategoryViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object Description
		{
			get { return ((IDoctorCategoryViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IDoctorCategoryViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
