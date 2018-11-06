using System;
using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class Bank_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IBankViewer
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

			ID = ((IBankViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((Bank_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((Bank_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (Description != null)
				((Bank_cu)ActiveDBItem).Description = Description.ToString();

			if (ChartOfAccount_CU_ID != null)
				((Bank_cu)ActiveDBItem).ChartOfAccount_CU_ID = Convert.ToInt32(ChartOfAccount_CU_ID);

			if (UserID != null)
				((Bank_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((Bank_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IBankViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((Bank_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IBankViewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<Bank_cu> list = Bank_cu.ItemsList;
			return list.ToArray();
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			if (Name_P == null)
			{
				MessageToView = "يجــب كتابـــة إسـم الحســـاب";
				return false;
			}

			return true;
		}

		public override bool CheckIfActiveDBItemExists()
		{
			if (ActiveCollector.ActiveDBItem == null)
				return true;

			foreach (Bank_cu dbItem in Bank_cu.ItemsList)
				if (dbItem.Equals(Name_P))
				{
					MessageToView = "هـذا البنــك موجـود بالفعـــل" + "\r\n" + "لا يمكــن الإضـافــــة";
					return true;
				}

			return false;
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<Bank_cu>();

				((IBankViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((Bank_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((Bank_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((Bank_cu)ActiveDBItem).Name_P;
			Name_S = ((Bank_cu)ActiveDBItem).Name_S;
			ChartOfAccount_CU_ID = ((Bank_cu)ActiveDBItem).ChartOfAccount_CU_ID;
			Description = ((Bank_cu)ActiveDBItem).Description;

			((IBankViewer)ActiveCollector.ActiveViewer).ID = ((Bank_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((Bank_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((Bank_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IBankViewer

		public object Name_P
		{
			get { return ((IBankViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IBankViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IBankViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IBankViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object ChartOfAccount_CU_ID
		{
			get { return ((IBankViewer)ActiveCollector.ActiveViewer).ChartOfAccount_CU_ID; }
			set { ((IBankViewer)ActiveCollector.ActiveViewer).ChartOfAccount_CU_ID = value; }
		}

		public object Description
		{
			get { return ((IBankViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IBankViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
