using System;
using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class ChartOfAccount_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IChartOfAccountViewer
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

			ID = ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (Name_P != null)
				((ChartOfAccount_cu)ActiveDBItem).Name_P = Name_P.ToString();

			if (Name_S != null)
				((ChartOfAccount_cu)ActiveDBItem).Name_S = Name_S.ToString();

			if (Description != null)
				((ChartOfAccount_cu)ActiveDBItem).Description = Description.ToString();

			DB_ChartOfAccountCodeMargin chartOfAccountCodeMargin = DB_ChartOfAccountCodeMargin.None;
			if (ChartOfAccountCodeMargin_P_ID != null)
			{
				((ChartOfAccount_cu)ActiveDBItem).ChartOfAccountCodeMargin_P_ID = Convert.ToInt32(ChartOfAccountCodeMargin_P_ID);
				chartOfAccountCodeMargin = (DB_ChartOfAccountCodeMargin)Convert.ToInt32(ChartOfAccountCodeMargin_P_ID);
			}

			if (ParentChartOfAccount_CU_ID != null)
				((ChartOfAccount_cu)ActiveDBItem).ParentChartOfAccount_CU_ID = Convert.ToInt32(ParentChartOfAccount_CU_ID);

			if (Serial != null)
				switch (chartOfAccountCodeMargin)
				{
					case DB_ChartOfAccountCodeMargin.FirstMargin:
						((ChartOfAccount_cu)ActiveDBItem).Serial = Convert.ToInt64(Serial);
						break;
					case DB_ChartOfAccountCodeMargin.SecondMargin:
					case DB_ChartOfAccountCodeMargin.ThirdMargin:
					case DB_ChartOfAccountCodeMargin.FourthMargin:
					case DB_ChartOfAccountCodeMargin.FifthMargin:
						((ChartOfAccount_cu) ActiveDBItem).Serial =
							AccountingBusinessLogicEngine.GetChartOfAccountSerial(ParentChartOfAccount_CU_ID, Serial);
						break;
				}

			if (IsDebit != null)
				((ChartOfAccount_cu)ActiveDBItem).IsDebit = Convert.ToBoolean(IsDebit);

			if (UserID != null)
				((ChartOfAccount_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((ChartOfAccount_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IChartOfAccountViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((ChartOfAccount_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<ChartOfAccount_cu> list = ChartOfAccount_cu.ItemsList;
			return list.ToArray();
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			if (Name_P == null)
			{
				MessageToView = "يجــب كتابـــة إسـم الحســـاب";
				return false;
			}

			if (ChartOfAccountCodeMargin_P_ID == null)
			{
				MessageToView = "يجــب إختيـــار مستـــوى الحســـاب";
				return false;
			}

			if (Serial == null)
			{
				MessageToView = "يجــب كتـابــــة مسلســـل الحســــاب";
				return false;
			}

			if (Convert.ToInt32(ChartOfAccountCodeMargin_P_ID) != Convert.ToInt32(DB_ChartOfAccountCodeMargin.FirstMargin))
				if (ParentChartOfAccount_CU_ID == null)
				{
					MessageToView = "يجــب إختيـــار الحســـاب الأكبـــــر";
					return false;
				}

			int allowedNumberOfDigits =
				AccountingBusinessLogicEngine.GetChartOfAccountCodeMarginNumberOfDigits(ChartOfAccountCodeMargin_P_ID);
			if (AccountingBusinessLogicEngine.GetChartOfAccountSerial(ParentChartOfAccount_CU_ID, Serial).ToString().Length !=
			    allowedNumberOfDigits)
			{
				MessageToView = "يجــب كتـابــــة مسلســـل الحســــاب = " + allowedNumberOfDigits;
				return false;
			}

			return true;
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<ChartOfAccount_cu>();

				((IChartOfAccountViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((ChartOfAccount_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((ChartOfAccount_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			Name_P = ((ChartOfAccount_cu)ActiveDBItem).Name_P;
			Name_S = ((ChartOfAccount_cu)ActiveDBItem).Name_S;
			Description = ((ChartOfAccount_cu)ActiveDBItem).Description;
			IsDebit = ((ChartOfAccount_cu)ActiveDBItem).IsDebit;
			ChartOfAccountCodeMargin_P_ID = ((ChartOfAccount_cu)ActiveDBItem).ChartOfAccountCodeMargin_P_ID;
			Serial = ((ChartOfAccount_cu)ActiveDBItem).Serial;
			ParentChartOfAccount_CU_ID = ((ChartOfAccount_cu)ActiveDBItem).ParentChartOfAccount_CU_ID;
			Description = ((ChartOfAccount_cu)ActiveDBItem).Description;

			((IChartOfAccountViewer)ActiveCollector.ActiveViewer).ID = ((ChartOfAccount_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((ChartOfAccount_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((ChartOfAccount_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IChartOfAccountViewer

		public object Name_P
		{
			get { return ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).Name_P; }
			set { ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).Name_P = value; }
		}

		public object Name_S
		{
			get { return ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).Name_S; }
			set { ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).Name_S = value; }
		}

		public object ParentChartOfAccount_CU_ID
		{
			get { return ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).ParentChartOfAccount_CU_ID; }
			set { ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).ParentChartOfAccount_CU_ID = value; }
		}

		public object Serial
		{
			get { return ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).Serial; }
			set { ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).Serial = value; }
		}

		public object ChartOfAccountCodeMargin_P_ID
		{
			get { return ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).ChartOfAccountCodeMargin_P_ID; }
			set { ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).ChartOfAccountCodeMargin_P_ID = value; }
		}

		public object IsDebit
		{
			get { return ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).IsDebit; }
			set { ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).IsDebit = value; }
		}

		public object Description
		{
			get { return ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).Description; }
			set { ((IChartOfAccountViewer)ActiveCollector.ActiveViewer).Description = value; }
		}

		#endregion
	}
}
