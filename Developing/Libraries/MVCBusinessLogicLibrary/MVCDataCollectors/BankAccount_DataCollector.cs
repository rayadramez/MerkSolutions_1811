using System;
using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class BankAccount_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IBankAccountViewer
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

			ID = ((IBankAccountViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((BankAccount_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((BankAccount_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (Description != null)
				((BankAccount_cu)ActiveDBItem).Description = Description.ToString();

			if (Bank_CU_ID != null)
				((BankAccount_cu)ActiveDBItem).Bank_CU_ID = Convert.ToInt32(Bank_CU_ID);

			if (ChartOfAccount_CU_ID != null)
				((BankAccount_cu)ActiveDBItem).ChartOfAccount_CU_ID = Convert.ToInt32(ChartOfAccount_CU_ID);

			if (UserID != null)
				((BankAccount_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((BankAccount_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IBankAccountViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((BankAccount_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IBankAccountViewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<BankAccount_cu> list = BankAccount_cu.ItemsList;
			return list.ToArray();
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			if (Name_P == null)
			{
				MessageToView = "يجــب كتابـــة إسـم الحســـاب";
				return false;
			}

			if (Bank_CU_ID == null)
			{
				MessageToView = "يجــب إختيـــار البنــــك";
				return false;
			}

			return true;
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<BankAccount_cu>();

				((IBankAccountViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((BankAccount_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((BankAccount_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((BankAccount_cu)ActiveDBItem).Name_P;
			Name_S = ((BankAccount_cu)ActiveDBItem).Name_S;
			ChartOfAccount_CU_ID = ((BankAccount_cu)ActiveDBItem).ChartOfAccount_CU_ID;
			Bank_CU_ID = ((BankAccount_cu)ActiveDBItem).Bank_CU_ID;
			Description = ((BankAccount_cu)ActiveDBItem).Description;

			((IBankAccountViewer)ActiveCollector.ActiveViewer).ID = ((BankAccount_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((BankAccount_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((BankAccount_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IBankAccountViewer

		public object Name_P
		{
			get { return ((IBankAccountViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IBankAccountViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IBankAccountViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IBankAccountViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object ChartOfAccount_CU_ID
		{
			get { return ((IBankAccountViewer)ActiveCollector.ActiveViewer).ChartOfAccount_CU_ID; }
			set { ((IBankAccountViewer)ActiveCollector.ActiveViewer).ChartOfAccount_CU_ID = value; }
		}

		public object Bank_CU_ID
		{
			get { return ((IBankAccountViewer)ActiveCollector.ActiveViewer).Bank_CU_ID; }
			set { ((IBankAccountViewer)ActiveCollector.ActiveViewer).Bank_CU_ID = value; }
		}

		public object Description
		{
			get { return ((IBankAccountViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IBankAccountViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
