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
	public class CashBox_DataCollector<TEntity> : AbstractDataCollector<TEntity>, ICashBoxViewer
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

			ID = ((ICashBoxViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((CashBox_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((CashBox_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (InternalCode != null)
				((CashBox_cu)ActiveDBItem).InternalCode = InternalCode.ToString();

			if (Description != null)
				((CashBox_cu)ActiveDBItem).Description = Description.ToString();

			if (Floor_CU_ID != null)
				((CashBox_cu)ActiveDBItem).Floor_CU_ID = Convert.ToInt32(Floor_CU_ID);

			if (ChartOfAccount_CU_ID != null)
				((CashBox_cu)ActiveDBItem).ChartOfAccount_CU_ID = Convert.ToInt32(ChartOfAccount_CU_ID);

			if (IsMain != null)
				((CashBox_cu)ActiveDBItem).IsMain = Convert.ToBoolean(IsMain);

			if (UserID != null)
				((CashBox_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((CashBox_cu)ActiveDBItem).IsOnDuty = true;
			switch (((ICashBoxViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((CashBox_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((ICashBoxViewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<CashBox_cu> list = CashBox_cu.ItemsList.FindAll(item => Convert.ToBoolean(item.IsMain).Equals(Convert.ToBoolean(IsMain)));
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<CashBox_cu>();

				((ICashBoxViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
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

			return true;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((CashBox_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((CashBox_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((CashBox_cu)ActiveDBItem).Name_P;
			Name_S = ((CashBox_cu)ActiveDBItem).Name_S;
			InternalCode = ((CashBox_cu)ActiveDBItem).InternalCode;
			Description = ((CashBox_cu)ActiveDBItem).Description;
			Floor_CU_ID = ((CashBox_cu)ActiveDBItem).Floor_CU_ID;

			IsMain = ((CashBox_cu)ActiveDBItem).IsMain;

			((ICashBoxViewer)ActiveCollector.ActiveViewer).ID = ((CashBox_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((CashBox_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((CashBox_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of ICashBoxViewer

		public object Name_P
		{
			get { return ((ICashBoxViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((ICashBoxViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((ICashBoxViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((ICashBoxViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object Floor_CU_ID
		{
			get { return ((ICashBoxViewer)ActiveCollector.ActiveViewer).Floor_CU_ID; }
			set { ((ICashBoxViewer)ActiveCollector.ActiveViewer).Floor_CU_ID = value; }
		}

		public object IsMain
		{
			get { return ((ICashBoxViewer)ActiveCollector.ActiveViewer).IsMain; }
			set { ((ICashBoxViewer)ActiveCollector.ActiveViewer).IsMain = value; }
		}

		public object InternalCode
		{
			get { return ((ICashBoxViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((ICashBoxViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object Description
		{
			get { return ((ICashBoxViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((ICashBoxViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		public object ChartOfAccount_CU_ID
		{
			get { return ((ICashBoxViewer)ActiveCollector.ActiveViewer).ChartOfAccount_CU_ID; }
			set { ((ICashBoxViewer)ActiveCollector.ActiveViewer).ChartOfAccount_CU_ID = value; }
		}

		#endregion
	}
}
