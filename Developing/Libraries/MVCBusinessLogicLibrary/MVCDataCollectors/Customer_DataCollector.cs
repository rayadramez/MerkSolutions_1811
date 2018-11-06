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
	public class Customer_DataCollector<TEntity> : AbstractDataCollector<TEntity>, ICustomerViewer
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

			ID = ((ICustomerViewer)ActiveCollector.ActiveViewer).ID;

			if (ActiveDBItem == null)
				return false;

			if (FirstName != null)
				((Person_cu)ActiveDBItem).FirstName_P = FirstName.ToString();

			if (SecondName != null)
				((Person_cu)ActiveDBItem).SecondName_P = SecondName.ToString();

			if (ThirdName != null)
				((Person_cu)ActiveDBItem).ThirdName_P = ThirdName.ToString();

			if (FourthName != null)
				((Person_cu)ActiveDBItem).FourthName_P = FourthName.ToString();

			((Person_cu)ActiveDBItem).PersonType_P_ID = Convert.ToInt32(DB_PersonType.Customer);

			if (MaritalStatus != null)
				((Person_cu)ActiveDBItem).MaritalStatus_P_ID = Convert.ToInt32(MaritalStatus);

			if (Gender != null)
				((Person_cu)ActiveDBItem).Gender = Convert.ToBoolean(Gender);

			if (BirthDate != null)
				((Person_cu)ActiveDBItem).BirthDate = Convert.ToDateTime(BirthDate);

			if (Mobile1 != null)
				((Person_cu)ActiveDBItem).Mobile1 = Mobile1.ToString();

			if (Mobile2 != null)
				((Person_cu)ActiveDBItem).Mobile2 = Mobile2.ToString();

			if (Phone1 != null)
				((Person_cu)ActiveDBItem).Phone1 = Phone1.ToString();

			if (Phone2 != null)
				((Person_cu)ActiveDBItem).Phone2 = Phone2.ToString();

			if (Address != null)
				((Person_cu)ActiveDBItem).Address = Address.ToString();

			if (Email != null)
				((Person_cu)ActiveDBItem).EMail = Email.ToString();

			if (IdentificationCardType != null)
				((Person_cu)ActiveDBItem).IdentificationCardType_P_ID = Convert.ToInt32(IdentificationCardType);

			if (IdentificationCardNumber != null)
				((Person_cu)ActiveDBItem).IdentificationCardNumber = IdentificationCardNumber.ToString();

			if (IdentificationCardIssueDate != null)
				((Person_cu)ActiveDBItem).IdentificationCardIssuingDate = Convert.ToDateTime(IdentificationCardIssueDate);

			if (IdentificationCardExpirationDate != null)
				((Person_cu)ActiveDBItem).IdentificationCardExpirationDate = Convert.ToDateTime(IdentificationCardExpirationDate);

			if (((Person_cu) ActiveDBItem).Customer_cu == null)
				((Person_cu)ActiveDBItem).Customer_cu = new Customer_cu();

			if (InternalCode != null)
				((Person_cu)ActiveDBItem).Customer_cu.InternalCode = InternalCode.ToString();

			if (((Person_cu)ActiveDBItem).Customer_cu != null)
				((Person_cu)ActiveDBItem).Customer_cu.IsOnDuty = true;

			((Person_cu)ActiveDBItem).IsOnDuty = true;
			switch (((ICustomerViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
			{
				case DB_CommonTransactionType.DeleteExisting:
					((Person_cu)ActiveDBItem).IsOnDuty = false;
					if (((Person_cu)ActiveDBItem).Customer_cu != null)
						((Person_cu)ActiveDBItem).Customer_cu.IsOnDuty = false;
					break;
			}

			RelatedViewers = ((ICustomerViewer)ActiveCollector.ActiveViewer).RelatedViewers;

			return true;
		}

		public override object ID { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.Customer_Viewer; }
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
				ActiveDBItem = DBCommon.CreateNewDBEntity<Person_cu>();
				if (ActiveDBItem != null)
					((Person_cu)ActiveDBItem).Customer_cu = DBCommon.CreateNewDBEntity<Customer_cu>();
				((ICustomerViewer)ActiveCollector.ActiveViewer).CommonTransactionType = DB_CommonTransactionType.SaveNew;
				return true;
			}
			return false;
		}

		public override bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector.ActiveDBItem == null)
				return false;

			if (((Person_cu)ActiveCollector.ActiveDBItem).SaveChanges())
			{
				((Person_cu)ActiveCollector.ActiveDBItem).LoadItemsList();
				return true;
			}

			return false;
		}

		public override bool AfterSave()
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
				personChart.PersonChartOfAccountType_P_ID = (int) DB_PersonChartOtAccountType.DebitAccountingCode;
				personChart.Person_CU_ID = ((Person_cu) ActiveCollector.ActiveDBItem).ID;
				personChart.IsOnDuty = true;
				if (UserID != null)
					personChart.InsertedBy = Convert.ToInt32(UserID);
				switch (((ICustomerViewer) ActiveCollector.ActiveViewer).CommonTransactionType)
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
				personChart.Person_CU_ID = ((Person_cu)ActiveCollector.ActiveDBItem).ID;
				personChart.IsOnDuty = true;
				if (UserID != null)
					personChart.InsertedBy = Convert.ToInt32(UserID);
				switch (((ICustomerViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
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
				personChart.Person_CU_ID = ((Person_cu)ActiveCollector.ActiveDBItem).ID;
				personChart.IsOnDuty = true;
				if (UserID != null)
					personChart.InsertedBy = Convert.ToInt32(UserID);
				switch (((ICustomerViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
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
				personChart.Person_CU_ID = ((Person_cu)ActiveCollector.ActiveDBItem).ID;
				personChart.IsOnDuty = true;
				if (UserID != null)
					personChart.InsertedBy = Convert.ToInt32(UserID);
				switch (((ICustomerViewer)ActiveCollector.ActiveViewer).CommonTransactionType)
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

			return false;
		}

		public override void Edit(IDBCommon entity)
		{
			FirstName = ((Person_cu)ActiveDBItem).FirstName_P;
			SecondName = ((Person_cu)ActiveDBItem).SecondName_P;
			ThirdName = ((Person_cu)ActiveDBItem).ThirdName_P;
			FourthName = ((Person_cu)ActiveDBItem).FourthName_P;
			MaritalStatus = ((Person_cu)ActiveDBItem).MaritalStatus_P_ID;
			Gender = ((Person_cu)ActiveDBItem).Gender;
			BirthDate = ((Person_cu)ActiveDBItem).BirthDate;
			Mobile1 = ((Person_cu)ActiveDBItem).Mobile1;
			Mobile2 = ((Person_cu)ActiveDBItem).Mobile2;
			Phone1 = ((Person_cu)ActiveDBItem).Phone1;
			Phone2 = ((Person_cu)ActiveDBItem).Phone2;
			Address = ((Person_cu)ActiveDBItem).Address;
			Email = ((Person_cu)ActiveDBItem).EMail;
			IdentificationCardType = ((Person_cu)ActiveDBItem).IdentificationCardType_P_ID;
			IdentificationCardNumber = ((Person_cu)ActiveDBItem).IdentificationCardNumber;
			IdentificationCardIssueDate = ((Person_cu)ActiveDBItem).IdentificationCardIssuingDate;
			IdentificationCardExpirationDate = ((Person_cu)ActiveDBItem).IdentificationCardExpirationDate;

			if (((Person_cu)ActiveDBItem).Customer_cu != null)
				InternalCode = ((Person_cu)ActiveDBItem).Customer_cu.InternalCode;

			((ICustomerViewer)ActiveCollector.ActiveViewer).ID = ((Person_cu)ActiveDBItem).ID;
			ActiveCollector.ActiveDBItem.ID = ((Person_cu)ActiveDBItem).ID;

			base.Edit(ActiveCollector.ActiveDBItem);
		}

		public override bool Delete(IDBCommon entity)
		{
			return ((Person_cu)entity).RemoveItem();
		}

		#endregion

		#region Implementation of ICustomerViewer

		public object FirstName
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).FirstName; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).FirstName = value; }
		}

		public object SecondName
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).SecondName; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).SecondName = value; }
		}

		public object ThirdName
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).ThirdName; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).ThirdName = value; }
		}

		public object FourthName
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).FourthName; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).FourthName = value; }
		}

		public object MaritalStatus
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).MaritalStatus; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).MaritalStatus = value; }
		}

		public object Gender
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).Gender; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).Gender = value; }
		}

		public object BirthDate
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).BirthDate; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).BirthDate = value; }
		}

		public object InternalCode
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).InternalCode; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).InternalCode = value; }
		}

		public object Mobile1
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).Mobile1; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).Mobile1 = value; }
		}

		public object Mobile2
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).Mobile2; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).Mobile2 = value; }
		}

		public object Phone1
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).Phone1; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).Phone1 = value; }
		}

		public object Phone2
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).Phone2; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).Phone2 = value; }
		}

		public object Address
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).Address; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).Address = value; }
		}

		public object Email
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).Email; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).Email = value; }
		}

		public object IdentificationCardType
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).IdentificationCardType; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).IdentificationCardType = value; }
		}

		public object IdentificationCardNumber
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).IdentificationCardNumber; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).IdentificationCardNumber = value; }
		}

		public object IdentificationCardIssueDate
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).IdentificationCardIssueDate; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).IdentificationCardIssueDate = value; }
		}

		public object IdentificationCardExpirationDate
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).IdentificationCardExpirationDate; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).IdentificationCardExpirationDate = value; }
		}

		public object IsDebitChartOfAccount
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).IsDebitChartOfAccount; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).IsDebitChartOfAccount = value; }
		}

		public object Debit_ChartOfAccount
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).Debit_ChartOfAccount; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).Debit_ChartOfAccount = value; }
		}

		public object IsTaxChartOfAccount
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).IsTaxChartOfAccount; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).IsTaxChartOfAccount = value; }
		}

		public object Tax_ChartOfAccount
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).Tax_ChartOfAccount; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).Tax_ChartOfAccount = value; }
		}

		public object IsCreditChartOfAccount
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).IsCreditChartOfAccount; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).IsCreditChartOfAccount = value; }
		}

		public object Credit_ChartOfAccount
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).Credit_ChartOfAccount; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).Credit_ChartOfAccount = value; }
		}

		public object IsCurrentChartOfAccount
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).IsCurrentChartOfAccount; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).IsCurrentChartOfAccount = value; }
		}

		public object Current_ChartOfAccount
		{
			get { return ((ICustomerViewer)ActiveCollector.ActiveViewer).Current_ChartOfAccount; }
			set { ((ICustomerViewer)ActiveCollector.ActiveViewer).Current_ChartOfAccount = value; }
		}

		#endregion
	}
}
