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
	public class MedicationCategory_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IMedicationCategoryViewer
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

			ID = ((IMedicationCategoryViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((MedicationCategory_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((MedicationCategory_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (UserID != null)
				((MedicationCategory_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((MedicationCategory_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IMedicationCategoryViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((MedicationCategory_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IMedicationCategoryViewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<MedicationCategory_cu> list = MedicationCategory_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<MedicationCategory_cu>();

				((IMedicationCategoryViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
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

			if (((MedicationCategory_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((MedicationCategory_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((MedicationCategory_cu)ActiveDBItem).Name_P;
			Name_S = ((MedicationCategory_cu)ActiveDBItem).Name_S;

			((IMedicationCategoryViewer)ActiveCollector.ActiveViewer).ID = ((MedicationCategory_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((MedicationCategory_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((MedicationCategory_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IMedicationCategoryViewer

		public object Name_P
		{
			get { return ((IMedicationCategoryViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IMedicationCategoryViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IMedicationCategoryViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IMedicationCategoryViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		#endregion
	}
}
