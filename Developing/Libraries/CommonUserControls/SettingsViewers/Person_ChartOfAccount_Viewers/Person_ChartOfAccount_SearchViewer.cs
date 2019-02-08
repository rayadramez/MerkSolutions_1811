using System;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.Person_ChartOfAccount_Viewers
{
	public partial class Person_ChartOfAccount_SearchViewer :
		//UserControl
		CommonAbstractSearchViewer<Person_ChartOfAccount_cu>,
		IPerson_ChartOfAccount_Viewer
	{
		public Person_ChartOfAccount_SearchViewer()
		{
			InitializeComponent();
		}

		#region Overrides of CommonAbstractViewer<Person_ChartOfAccount_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.Person_ChartOfAccount_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربـط الحسـابـــات المـاليـــة بالأشخـــاص"; }
		}

		public override void FillControls()
		{
		}

		public override void ClearControls()
		{
			chkCustomer.Checked = true;
			lkeCustomer.EditValue = null;
			chkDebitAccount.Checked = false;
			lkeChartOfAccount_DebitAccount.EditValue = null;
			chkTaxAccount.Checked = false;
			lkeChartOfAccount_TaxesAccount.EditValue = null;
			chkCurrentAccount.Checked = false;
			lkeChartOfAccount_CurrentAccount.EditValue = null;
			chkCreditAccount.Checked = false;
			lkeChartOfAccount_CreditAccount.EditValue = null;
		}

		public override string GridXML
		{
			get { return ""; }
		}

		#endregion

		#region Implementation of IPerson_ChartOfAccount_Viewer

		public object PersonType_P_ID
		{
			get
			{
				if (chkCustomer.Checked)
					return (int)DB_PersonType.Customer;
				if (chkSupplier.Checked)
					return (int)DB_PersonType.Supplier;
				if (chkDoctor.Checked)
					return (int)DB_PersonType.Doctor;
				if (chkPatient.Checked)
					return (int)DB_PersonType.Patient;

				return (int)DB_PersonType.None;
			}
			set
			{
				DB_PersonType persontype = (DB_PersonType)value;
				switch (persontype)
				{
					case DB_PersonType.Customer:
						chkCustomer.Checked = true;
						break;
					case DB_PersonType.Supplier:
						chkSupplier.Checked = true;
						break;
					case DB_PersonType.Doctor:
						chkDoctor.Checked = true;
						break;
					case DB_PersonType.Patient:
						chkPatient.Checked = true;
						break;
				}
			}
		}

		public object Person_CU_ID { get; set; }

		public object IsDebitChartOfAccount
		{
			get { return chkDebitAccount.Checked; }
			set { chkDebitAccount.Checked = Convert.ToBoolean(value); }
		}

		public object Debit_ChartOfAccount
		{
			get { return lkeChartOfAccount_DebitAccount.EditValue; }
			set { lkeChartOfAccount_DebitAccount.EditValue = value; }
		}

		public object IsTaxChartOfAccount
		{
			get { return chkTaxAccount.Checked; }
			set { chkTaxAccount.Checked = Convert.ToBoolean(value); }
		}

		public object Tax_ChartOfAccount
		{
			get { return lkeChartOfAccount_TaxesAccount.EditValue; }
			set { lkeChartOfAccount_TaxesAccount.EditValue = value; }
		}

		public object IsCreditChartOfAccount
		{
			get { return chkCreditAccount.Checked; }
			set { chkCreditAccount.Checked = Convert.ToBoolean(value); }
		}

		public object Credit_ChartOfAccount
		{
			get { return lkeChartOfAccount_CreditAccount.EditValue; }
			set { lkeChartOfAccount_CreditAccount.EditValue = value; }
		}

		public object IsCurrentChartOfAccount
		{
			get { return chkCurrentAccount.Checked; }
			set { chkCurrentAccount.Checked = Convert.ToBoolean(value); }
		}

		public object Current_ChartOfAccount
		{
			get { return lkeChartOfAccount_CurrentAccount.EditValue; }
			set { lkeChartOfAccount_CurrentAccount.EditValue = value; }
		}

		#endregion
	}
}
