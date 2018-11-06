using System;
using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public class PersonType_ChartOfAccount_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IPersonType_ChartOfAccount_Viewer
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

			ID = ((IPersonType_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (ChartOfAccount_CU_ID != null)
				((PersonType_ChartOfAccount_cu)ActiveDBItem).ChartOfAccount_CU_ID = Convert.ToInt32(ChartOfAccount_CU_ID);

			if (PersonType_P_ID != null)
				((PersonType_ChartOfAccount_cu)ActiveDBItem).PersonType_P_ID = Convert.ToInt32(PersonType_P_ID);

			if (PersonChartOfAccountType_P_ID != null)
				((PersonType_ChartOfAccount_cu)ActiveDBItem).PersonChartOfAccountType_P_ID = Convert.ToInt32(PersonChartOfAccountType_P_ID);

			if (UserID != null)
				((PersonType_ChartOfAccount_cu)ActiveDBItem).InsertedBy = Convert.ToInt32(UserID);

			((PersonType_ChartOfAccount_cu)ActiveDBItem).IsOnDuty = true;
			switch (((IPersonType_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((PersonType_ChartOfAccount_cu)ActiveDBItem).IsOnDuty = false;
					break;
			}

			RelatedViewers = ((IPersonType_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).RelatedViewers;

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
			List<PersonType_ChartOfAccount_cu> list = PersonType_ChartOfAccount_cu.ItemsList;
			return list.ToArray();
		}

		public override bool ValidateBeforeSave(ref string message)
		{
			if (ChartOfAccount_CU_ID == null)
			{
				MessageToView = "يجــب إختيــار الحســاب المـالـــي";
				return false;
			}

			if (PersonType_P_ID == null)
			{
				MessageToView = "يجــب إختيــار نــوع الشخـــص";
				return false;
			}

			if (PersonChartOfAccountType_P_ID == null)
			{
				MessageToView = "يجــب إختيــار نــوع الحســاب الشخصـــي";
				return false;
			}

			return true;
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<PersonType_ChartOfAccount_cu>();

				((IPersonType_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((PersonType_ChartOfAccount_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((PersonType_ChartOfAccount_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			PersonChartOfAccountType_P_ID = ((PersonType_ChartOfAccount_cu)ActiveDBItem).PersonChartOfAccountType_P_ID;
			PersonType_P_ID = ((PersonType_ChartOfAccount_cu)ActiveDBItem).PersonType_P_ID;
			ChartOfAccount_CU_ID = ((PersonType_ChartOfAccount_cu)ActiveDBItem).ChartOfAccount_CU_ID;

			((IPersonType_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).ID = ((PersonType_ChartOfAccount_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((PersonType_ChartOfAccount_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((PersonType_ChartOfAccount_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of IPersonType_ChartOfAccount_Viewer

		public object PersonType_P_ID
		{
			get { return ((IPersonType_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).PersonType_P_ID; }
			set { ((IPersonType_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).PersonType_P_ID = value; }
		}

		public object ChartOfAccount_CU_ID
		{
			get { return ((IPersonType_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).ChartOfAccount_CU_ID; }
			set { ((IPersonType_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).ChartOfAccount_CU_ID = value; }
		}

		public object PersonChartOfAccountType_P_ID
		{
			get { return ((IPersonType_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).PersonChartOfAccountType_P_ID; }
			set { ((IPersonType_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).PersonChartOfAccountType_P_ID = value; }
		}

		#endregion
	}
}
