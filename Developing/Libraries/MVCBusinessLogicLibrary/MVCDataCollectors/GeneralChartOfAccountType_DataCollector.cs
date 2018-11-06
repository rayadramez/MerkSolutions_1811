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
	public class GeneralChartOfAccountType_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IGeneralChartOfAccountTypeViewer
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

			ID = ((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((GeneralChartOfAccountType_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((GeneralChartOfAccountType_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (GeneralChartOfAccountType_P_ID != null)
				((GeneralChartOfAccountType_cu) ActiveDBItem).GeneralChartOfAccountType_P_ID =
					Convert.ToInt32(GeneralChartOfAccountType_P_ID);

			if (Description != null)
				((GeneralChartOfAccountType_cu)ActiveDBItem).Description = Description.ToString();

			if (UserID != null)
				((GeneralChartOfAccountType_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((GeneralChartOfAccountType_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((GeneralChartOfAccountType_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.GeneralChartOfAccountType_Viewer; }
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
			List<GeneralChartOfAccountType_cu> list = GeneralChartOfAccountType_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<GeneralChartOfAccountType_cu>();

				((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			if (Name_P == null)
			{
				MessageToView = "يجـــب كتـابـــة الإســـم العـربـــي";
				return false;
			}

			if (GeneralChartOfAccountType_P_ID == null)
			{
				MessageToView = "يجـــب إختيــــار نــوع المعـامــلات المـاليـــــة";
				return false;
			}

			return true;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((GeneralChartOfAccountType_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((GeneralChartOfAccountType_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override bool AfterSave()
		{
			GeneralChartOfAccountType_cu generalChartOfAccountType = (GeneralChartOfAccountType_cu) ActiveDBItem;
			if (generalChartOfAccountType == null)
				return false;

			ChartOfAccount_GeneralChartOfAccountType_cu chartOfAccountGeneralChartOfAccount =
				DBCommon.CreateNewDBEntity<ChartOfAccount_GeneralChartOfAccountType_cu>();
			if (chartOfAccountGeneralChartOfAccount == null)
				return false;

			if (ChartOfAccount_CU_ID != null)
				chartOfAccountGeneralChartOfAccount.ChartOfAccount_CU_ID = Convert.ToInt32(ChartOfAccount_CU_ID);
			chartOfAccountGeneralChartOfAccount.GeneralChartOfAccountType_CU_ID = generalChartOfAccountType.ID;
			if (UserID != null)
				chartOfAccountGeneralChartOfAccount.InsertedBy = Convert.ToInt32(UserID);

			chartOfAccountGeneralChartOfAccount.IsOnDuty = true;
			switch (((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					chartOfAccountGeneralChartOfAccount.IsOnDuty = false;
					break;
			}

			chartOfAccountGeneralChartOfAccount.SaveChanges();

			DB_GeneralChartOfAccountType privateGeneralChartOfAccountType =
				(DB_GeneralChartOfAccountType) generalChartOfAccountType.GeneralChartOfAccountType_P_ID;
			List<CashBoxTransactionType_GeneralChartOfAccountType_p> privateList =
				CashBoxTransactionType_GeneralChartOfAccountType_p.ItemsList.FindAll(
					item =>
						Convert.ToInt32(item.GeneralChartOfAccountType_P_ID).Equals(Convert.ToInt32(privateGeneralChartOfAccountType)));
			if (privateList.Count == 0)
				return false;

			CashBoxTransactionType_GeneralChartOfAccountType_cu cashBoxTransactionTypeGeneralChartOfAccountType = null;
			foreach (CashBoxTransactionType_GeneralChartOfAccountType_p cashBoxTransactionTypeGeneralChartOfAccountTypeP in privateList)
			{
				cashBoxTransactionTypeGeneralChartOfAccountType =
					DBCommon.CreateNewDBEntity<CashBoxTransactionType_GeneralChartOfAccountType_cu>();
				if (cashBoxTransactionTypeGeneralChartOfAccountType == null)
					return false;

				cashBoxTransactionTypeGeneralChartOfAccountType.CashBoxTransactionType_P_ID =
					cashBoxTransactionTypeGeneralChartOfAccountTypeP.CashBoxTransactionType_P_ID;
				cashBoxTransactionTypeGeneralChartOfAccountType.GeneralChartOfAccountType_CU_ID = generalChartOfAccountType.ID;

				if (UserID != null)
					cashBoxTransactionTypeGeneralChartOfAccountType.InsertedBy = Convert.ToInt32(UserID);

				cashBoxTransactionTypeGeneralChartOfAccountType.IsOnDuty = true;
				switch (((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
				{
					case DB_CommonTransactionType.DeleteExisting:
						cashBoxTransactionTypeGeneralChartOfAccountType.IsOnDuty = false;
						break;
				}

				cashBoxTransactionTypeGeneralChartOfAccountType.SaveChanges();
			}

			if (cashBoxTransactionTypeGeneralChartOfAccountType != null)
				cashBoxTransactionTypeGeneralChartOfAccountType.LoadItemsList();

			return true;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((GeneralChartOfAccountType_cu)ActiveDBItem).Name_P;
			Name_S = ((GeneralChartOfAccountType_cu)ActiveDBItem).Name_S;
			GeneralChartOfAccountType_P_ID = ((GeneralChartOfAccountType_cu)ActiveDBItem).GeneralChartOfAccountType_P_ID;
			Description = ((GeneralChartOfAccountType_cu)ActiveDBItem).Description;

			((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).ID = ((GeneralChartOfAccountType_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((GeneralChartOfAccountType_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((GeneralChartOfAccountType_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IGeneralChartOfAccountTypeViewer

		public object Name_P
		{
			get { return ((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object GeneralChartOfAccountType_P_ID
		{
			get { return ((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).GeneralChartOfAccountType_P_ID; }
			set { ((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).GeneralChartOfAccountType_P_ID = value; }
		}

		public object Description
		{
			get { return ((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		public object ChartOfAccount_CU_ID
		{
			get { return ((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).ChartOfAccount_CU_ID; }
			set { ((IGeneralChartOfAccountTypeViewer)ActiveCollector.ActiveViewer).ChartOfAccount_CU_ID = value; }
		}

		#endregion
	}
}
