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
	public class CashBoxTransactionType_GeneralChartOfAccountType_DataCollector<TEntity> : AbstractDataCollector<TEntity>,
		ICashBoxTransactionType_GeneralChartOfAccountType_Viewer
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

			ID = ((ICashBoxTransactionType_GeneralChartOfAccountType_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (List_CashBoxTransactionType_GeneralChartOfAccountType == null ||
			    List_CashBoxTransactionType_GeneralChartOfAccountType.Count == 0)
				return false;

			RelatedViewers =
				((ICashBoxTransactionType_GeneralChartOfAccountType_Viewer) ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.CashBoxTransactionType_GeneralChartOfAccountType_Viewer; }
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
				ActiveDBItem = DBCommon.CreateNewDBEntity<CashBoxTransactionType_GeneralChartOfAccountType_cu>();
				((ICashBoxTransactionType_GeneralChartOfAccountType_Viewer) ActiveCollector.ActiveViewer).CommonTransactionType =
					DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			foreach (CashBoxTransactionType_GeneralChartOfAccountType_cu itemToBeAdded in
					List_CashBoxTransactionType_GeneralChartOfAccountType)
			{
				((CashBoxTransactionType_GeneralChartOfAccountType_cu) ActiveDBItem).CashBoxTransactionType_P_ID =
					itemToBeAdded.CashBoxTransactionType_P_ID;
				((CashBoxTransactionType_GeneralChartOfAccountType_cu) ActiveDBItem).GeneralChartOfAccountType_CU_ID =
					itemToBeAdded.GeneralChartOfAccountType_CU_ID;

				if (UserID != null)
					((CashBoxTransactionType_GeneralChartOfAccountType_cu) ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

				((CashBoxTransactionType_GeneralChartOfAccountType_cu) ActiveDBItem).IsOnDuty = true;
				switch (
					((ICashBoxTransactionType_GeneralChartOfAccountType_Viewer) ActiveCollector.ActiveViewer).CommonTransactionType)
				{
					case DB_CommonTransactionType.DeleteExisting:
						((CashBoxTransactionType_GeneralChartOfAccountType_cu) ActiveDBItem).IsOnDuty = false;
						break;
				}

				((CashBoxTransactionType_GeneralChartOfAccountType_cu) ActiveCollector.ActiveDBItem).SaveChanges();
			}

			((CashBoxTransactionType_GeneralChartOfAccountType_cu)ActiveCollector.ActiveDBItem).LoadItemsList();

			return true;
		}

		public override void Edit(IDBCommon entity)
		{
			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((CashBoxTransactionType_GeneralChartOfAccountType_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of ICashBoxTransactionType_GeneralChartOfAccountType_Viewer

		public List<CashBoxTransactionType_GeneralChartOfAccountType_cu> List_CashBoxTransactionType_GeneralChartOfAccountType
		{
			get
			{
				return
					((ICashBoxTransactionType_GeneralChartOfAccountType_Viewer) ActiveCollector.ActiveViewer)
						.List_CashBoxTransactionType_GeneralChartOfAccountType;
			}
			set
			{
				((ICashBoxTransactionType_GeneralChartOfAccountType_Viewer) ActiveCollector.ActiveViewer)
					.List_CashBoxTransactionType_GeneralChartOfAccountType = value;
			}
		}

		#endregion
	}
}
