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
	public class Person_ChartOfAccount_DataCollector<TEntity> : AbstractDataCollector<TEntity>, IPerson_ChartOfAccount_Viewer
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

			ID = ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.Person_ChartOfAccount_Viewer; }
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
			List<Customer_cu> list = Customer_cu.ItemsList;
			return list.ToArray();
		}

		public override bool CreateNew()
		{
			if (ActiveDBItem == null)
			{
				ActiveDBItem = DBCommon.CreateNewDBEntity<Person_ChartOfAccount_cu>();

				((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			List<Person_ChartOfAccount_cu> list = null;
			if (Convert.ToBoolean(IsDebitChartOfAccount) || Convert.ToBoolean(IsTaxChartOfAccount)
														 || Convert.ToBoolean(IsCurrentChartOfAccount)
														 || Convert.ToBoolean(IsCreditChartOfAccount))
				list = new List<Person_ChartOfAccount_cu>();
			if (Convert.ToBoolean(IsDebitChartOfAccount) && Debit_ChartOfAccount != null)
			{
				Person_ChartOfAccount_cu personChart = DBCommon.CreateNewDBEntity<Person_ChartOfAccount_cu>();
				personChart.ChartOfAccount_CU_ID = Convert.ToInt32(Debit_ChartOfAccount);
				personChart.PersonChartOfAccountType_P_ID = (int)DB_PersonChartOtAccountType.DebitAccountingCode;
				personChart.Person_CU_ID = Convert.ToInt32(Person_CU_ID);
				personChart.IsOnDuty = true;
				if (UserID != null)
					personChart.InsertedBy = Convert.ToInt32(UserID);
				switch (((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
				{
					case DB_CommonTransactionType.DeleteExisting:
						personChart.IsOnDuty = false;
						break;
				}

				if (list != null)
					list.Add(personChart);
			}

			if (Convert.ToBoolean(IsCreditChartOfAccount) && Credit_ChartOfAccount != null)
			{
				Person_ChartOfAccount_cu personChart = DBCommon.CreateNewDBEntity<Person_ChartOfAccount_cu>();
				personChart.ChartOfAccount_CU_ID = Convert.ToInt32(Credit_ChartOfAccount);
				personChart.PersonChartOfAccountType_P_ID = (int)DB_PersonChartOtAccountType.CreditAccountingCode;
				personChart.Person_CU_ID = Convert.ToInt32(Person_CU_ID);
				personChart.IsOnDuty = true;
				if (UserID != null)
					personChart.InsertedBy = Convert.ToInt32(UserID);
				switch (((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
				{
					case DB_CommonTransactionType.DeleteExisting:
						personChart.IsOnDuty = false;
						break;
				}

				if (list != null)
					list.Add(personChart);
			}

			if (Convert.ToBoolean(IsCurrentChartOfAccount) && Current_ChartOfAccount != null)
			{
				Person_ChartOfAccount_cu personChart = DBCommon.CreateNewDBEntity<Person_ChartOfAccount_cu>();
				personChart.ChartOfAccount_CU_ID = Convert.ToInt32(Current_ChartOfAccount);
				personChart.PersonChartOfAccountType_P_ID = (int)DB_PersonChartOtAccountType.CurrentAccountingCode;
				personChart.Person_CU_ID = Convert.ToInt32(Person_CU_ID);
				personChart.IsOnDuty = true;
				if (UserID != null)
					personChart.InsertedBy = Convert.ToInt32(UserID);
				switch (((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
				{
					case DB_CommonTransactionType.DeleteExisting:
						personChart.IsOnDuty = false;
						break;
				}

				if (list != null)
					list.Add(personChart);
			}

			if (Convert.ToBoolean(IsTaxChartOfAccount) && Tax_ChartOfAccount != null)
			{
				Person_ChartOfAccount_cu personChart = DBCommon.CreateNewDBEntity<Person_ChartOfAccount_cu>();
				personChart.ChartOfAccount_CU_ID = Convert.ToInt32(Tax_ChartOfAccount);
				personChart.PersonChartOfAccountType_P_ID = (int)DB_PersonChartOtAccountType.TaxAccountingCode;
				personChart.Person_CU_ID = Convert.ToInt32(Person_CU_ID);
				personChart.IsOnDuty = true;
				if (UserID != null)
					personChart.InsertedBy = Convert.ToInt32(UserID);
				switch (((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).CommonTransactionType)
				{
					case DB_CommonTransactionType.DeleteExisting:
						personChart.IsOnDuty = false;
						break;
				}

				if (list != null)
					list.Add(personChart);
			}

			if (list != null && list.Count > 0)
			{
				foreach (Person_ChartOfAccount_cu personChartOfAccountCu in list)
				{
					personChartOfAccountCu.SaveChanges();
					personChartOfAccountCu.LoadItemsList();
				}
			}

			return true;
		}

		#endregion

		#region Implementation of IPerson_ChartOfAccount_Viewer

		public object PersonType_P_ID
		{
			get { return ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).PersonType_P_ID; }
			set { ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).PersonType_P_ID = value; }
		}

		public object Person_CU_ID
		{
			get { return ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).Person_CU_ID; }
			set { ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).Person_CU_ID = value; }
		}

		public object IsDebitChartOfAccount
		{
			get { return ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).IsDebitChartOfAccount; }
			set { ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).IsDebitChartOfAccount = value; }
		}

		public object Debit_ChartOfAccount
		{
			get { return ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).Debit_ChartOfAccount; }
			set { ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).Debit_ChartOfAccount = value; }
		}

		public object IsTaxChartOfAccount
		{
			get { return ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).IsTaxChartOfAccount; }
			set { ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).IsTaxChartOfAccount = value; }
		}

		public object Tax_ChartOfAccount
		{
			get { return ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).Tax_ChartOfAccount; }
			set { ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).Tax_ChartOfAccount = value; }
		}

		public object IsCreditChartOfAccount
		{
			get { return ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).IsCreditChartOfAccount; }
			set { ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).IsCreditChartOfAccount = value; }
		}

		public object Credit_ChartOfAccount
		{
			get { return ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).Credit_ChartOfAccount; }
			set { ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).Credit_ChartOfAccount = value; }
		}

		public object IsCurrentChartOfAccount
		{
			get { return ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).IsCurrentChartOfAccount; }
			set { ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).IsCurrentChartOfAccount = value; }
		}

		public object Current_ChartOfAccount
		{
			get { return ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).Current_ChartOfAccount; }
			set { ((IPerson_ChartOfAccount_Viewer)ActiveCollector.ActiveViewer).Current_ChartOfAccount = value; }
		}


		#endregion
	}
}
