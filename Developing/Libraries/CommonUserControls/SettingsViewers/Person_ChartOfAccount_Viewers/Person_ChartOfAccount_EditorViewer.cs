using System;
using System.Collections.Generic;
using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.ChartOfAccountViewers;
using CommonUserControls.SettingsViewers.CustomersViewers;
using CommonUserControls.SettingsViewers.DoctorViewers;
using CommonUserControls.SettingsViewers.SupplierViewers;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.Person_ChartOfAccount_Viewers
{
	public partial class Person_ChartOfAccount_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<Person_ChartOfAccount_cu>,
		IPerson_ChartOfAccount_Viewer
	{
		private ChartOfAccount_EditorViewer _chartOfAccountEditorViewer;
		private Customer_EditorViewer _customerEditorViewer;
		private Doctor_EditorViewer _doctorEditorViewer;
		private Supplier_EditorViewer _supplierEditorViewer;
		
		private List<ChartOfAccount_cu> lastUsed_Debit = new List<ChartOfAccount_cu>();
		private List<ChartOfAccount_cu> lastUsed_Tax = new List<ChartOfAccount_cu>();
		private List<ChartOfAccount_cu> lastUsed_Current = new List<ChartOfAccount_cu>();
		private List<ChartOfAccount_cu> lastUsed_Credit = new List<ChartOfAccount_cu>();

		public Person_ChartOfAccount_EditorViewer()
		{
			InitializeComponent();

			switch (ApplicationStaticConfiguration.Organization)
			{
				case DB_Organization.AvvaAbraam:
					lyt_chkDoctor.Visibility = lyt_chkPatient.Visibility = LayoutVisibility.Never;
					break;
			}
		}

		private void Person_ChartOfAccount_EditorViewer_Load(object sender, EventArgs e)
		{

		}

		private void chkCustomer_CheckedChanged(object sender, EventArgs e)
		{
			lyt_lkeCustomer.Text = "العميـــل";
			CommonViewsActions.FillGridlookupEdit(lkeCustomer, Customer_cu.ItemsList, "FullName", "Person_CU_ID");
		}

		private void chkSupplier_CheckedChanged(object sender, EventArgs e)
		{
			lyt_lkeCustomer.Text = "المــــورد";
			CommonViewsActions.FillGridlookupEdit(lkeCustomer, Supplier_cu.ItemsList, "FullName", "Person_CU_ID");
		}

		private void chkDoctor_CheckedChanged(object sender, EventArgs e)
		{
			lyt_lkeCustomer.Text = "الطبيــــب";
			CommonViewsActions.FillGridlookupEdit(lkeCustomer, Doctor_cu.ItemsList, "Name_P", "Person_CU_ID");
		}

		private void chkPatient_CheckedChanged(object sender, EventArgs e)
		{
			lyt_lkeCustomer.Text = "المـريـــض";
			CommonViewsActions.FillGridlookupEdit(lkeCustomer, Patient_cu.ItemsList, "PatientFullName", "Person_CU_ID");
		}

		private void chkDebitAccount_CheckedChanged(object sender, System.EventArgs e)
		{
			lyt_lkeChartOfAccount_DebitAccount.Visibility = lyt_btnChartOfAccount_Debit.Visibility =
				chkDebitAccount.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;

			if (chkDebitAccount.Checked)
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_DebitAccount,
													  ChartOfAccount_cu.ItemsList.FindAll(
														  item =>
															  Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
																  .Equals(Convert.ToInt32(
																			  DB_ChartOfAccountCodeMargin
																				  .FirstMargin))));
		}

		private void chkTaxAccount_CheckedChanged(object sender, System.EventArgs e)
		{
			lyt_lkeChartOfAccount_TaxesAccount.Visibility = lyt_btnChartOfAccount_Tax.Visibility =
				chkDebitAccount.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;

			if (chkTaxAccount.Checked)
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_TaxesAccount,
													  ChartOfAccount_cu.ItemsList.FindAll(
														  item =>
															  Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
																  .Equals(Convert.ToInt32(
																			  DB_ChartOfAccountCodeMargin
																				  .FirstMargin))));
		}

		private void chkCurrentAccount_CheckedChanged(object sender, System.EventArgs e)
		{
			lyt_lkeChartOfAccount_CurrentAccount.Visibility = lyt_btnChartOfAccount_Current.Visibility =
				chkDebitAccount.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;

			if (chkCurrentAccount.Checked)
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_CurrentAccount,
													  ChartOfAccount_cu.ItemsList.FindAll(
														  item =>
															  Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
																  .Equals(Convert.ToInt32(
																			  DB_ChartOfAccountCodeMargin
																				  .FirstMargin))));
		}

		private void chkCreditAccount_CheckedChanged(object sender, System.EventArgs e)
		{
			lyt_lkeChartOfAccount_CreditAccount.Visibility = lyt_btnChartOfAccount_Credit.Visibility =
				chkDebitAccount.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;

			if (chkCreditAccount.Checked)
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_CreditAccount,
													  ChartOfAccount_cu.ItemsList.FindAll(
														  item =>
															  Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
																  .Equals(Convert.ToInt32(
																			  DB_ChartOfAccountCodeMargin
																				  .FirstMargin))));
		}

		private void lkeChartOfAccount_DebitAccount_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeChartOfAccount_DebitAccount.EditValue == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_DebitAccount,
													  ChartOfAccount_cu.ItemsList.FindAll(
														  item => Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(
																		  DB_ChartOfAccountCodeMargin.FirstMargin))));
				return;
			}

			ChartOfAccount_cu chartOfAccount =
				ChartOfAccount_cu.ItemsList.Find(item => Convert.ToInt32(item.ID)
													 .Equals(Convert.ToInt32(lkeChartOfAccount_DebitAccount.EditValue)));
			if (chartOfAccount == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_DebitAccount,
													  ChartOfAccount_cu.ItemsList.FindAll(
														  item => Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(
																		  DB_ChartOfAccountCodeMargin.FirstMargin))));
				return;
			}

			List<ChartOfAccount_cu> list = ChartOfAccount_cu.ItemsList.FindAll(
				item => Convert.ToInt32(item.ParentChartOfAccount_CU_ID).Equals(Convert.ToInt32(chartOfAccount.ID)));
			if (list.Count > 0)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_DebitAccount, list);
				lastUsed_Debit = list;
			}
			else
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_DebitAccount, lastUsed_Debit);
			}
		}

		private void lkeChartOfAccount_TaxesAccount_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeChartOfAccount_TaxesAccount.EditValue == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_TaxesAccount,
													  ChartOfAccount_cu.ItemsList.FindAll(
														  item => Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(
																		  DB_ChartOfAccountCodeMargin.FirstMargin))));
				return;
			}

			ChartOfAccount_cu chartOfAccount =
				ChartOfAccount_cu.ItemsList.Find(item => Convert.ToInt32(item.ID)
													 .Equals(Convert.ToInt32(lkeChartOfAccount_TaxesAccount.EditValue)));
			if (chartOfAccount == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_TaxesAccount,
													  ChartOfAccount_cu.ItemsList.FindAll(
														  item => Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(
																		  DB_ChartOfAccountCodeMargin.FirstMargin))));
				return;
			}

			List<ChartOfAccount_cu> list = ChartOfAccount_cu.ItemsList.FindAll(
				item => Convert.ToInt32(item.ParentChartOfAccount_CU_ID).Equals(Convert.ToInt32(chartOfAccount.ID)));
			if (list.Count > 0)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_TaxesAccount, list);
				lastUsed_Tax = list;
			}
			else
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_TaxesAccount, lastUsed_Tax);
			}
		}

		private void lkeChartOfAccount_CurrentAccount_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeChartOfAccount_CurrentAccount.EditValue == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_CurrentAccount,
													  ChartOfAccount_cu.ItemsList.FindAll(
														  item => Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(
																		  DB_ChartOfAccountCodeMargin.FirstMargin))));
				return;
			}

			ChartOfAccount_cu chartOfAccount =
				ChartOfAccount_cu.ItemsList.Find(item => Convert.ToInt32(item.ID)
													 .Equals(Convert.ToInt32(lkeChartOfAccount_CurrentAccount.EditValue)));
			if (chartOfAccount == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_CurrentAccount,
													  ChartOfAccount_cu.ItemsList.FindAll(
														  item => Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(
																		  DB_ChartOfAccountCodeMargin.FirstMargin))));
				return;
			}

			List<ChartOfAccount_cu> list = ChartOfAccount_cu.ItemsList.FindAll(
				item => Convert.ToInt32(item.ParentChartOfAccount_CU_ID).Equals(Convert.ToInt32(chartOfAccount.ID)));
			if (list.Count > 0)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_CurrentAccount, list);
				lastUsed_Current = list;
			}
			else
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_CurrentAccount, lastUsed_Current);
			}
		}

		private void lkeChartOfAccount_CreditAccount_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeChartOfAccount_CreditAccount.EditValue == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_CreditAccount,
													  ChartOfAccount_cu.ItemsList.FindAll(
														  item => Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(
																		  DB_ChartOfAccountCodeMargin.FirstMargin))));
				return;
			}

			ChartOfAccount_cu chartOfAccount =
				ChartOfAccount_cu.ItemsList.Find(item => Convert.ToInt32(item.ID)
													 .Equals(Convert.ToInt32(lkeChartOfAccount_CreditAccount.EditValue)));
			if (chartOfAccount == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_CreditAccount,
													  ChartOfAccount_cu.ItemsList.FindAll(
														  item => Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(
																		  DB_ChartOfAccountCodeMargin.FirstMargin))));
				return;
			}

			List<ChartOfAccount_cu> list = ChartOfAccount_cu.ItemsList.FindAll(
				item => Convert.ToInt32(item.ParentChartOfAccount_CU_ID).Equals(Convert.ToInt32(chartOfAccount.ID)));
			if (list.Count > 0)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_CreditAccount, list);
				lastUsed_Credit = list;
			}
			else
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_CreditAccount, lastUsed_Credit);
			}
		}

		private void btnChartOfAccount_Debit_Click(object sender, System.EventArgs e)
		{
			BaseController<Location_cu>.ShowEditorControl(ref _chartOfAccountEditorViewer, this, null, null,
														  EditorContainerType.Regular, ViewerName.ChartOfAccountViewer, DB_CommonTransactionType.CreateNew,
														  "الحسـابــــات", true);
			CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_DebitAccount,
												  ChartOfAccount_cu.ItemsList.FindAll(
													  item =>
														  Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(DB_ChartOfAccountCodeMargin.FirstMargin))));
		}

		private void btnChartOfAccount_Tax_Click(object sender, System.EventArgs e)
		{
			BaseController<Location_cu>.ShowEditorControl(ref _chartOfAccountEditorViewer, this, null, null,
														  EditorContainerType.Regular, ViewerName.ChartOfAccountViewer, DB_CommonTransactionType.CreateNew,
														  "الحسـابــــات", true);
			CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_TaxesAccount,
												  ChartOfAccount_cu.ItemsList.FindAll(
													  item =>
														  Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(DB_ChartOfAccountCodeMargin.FirstMargin))));
		}

		private void btnChartOfAccount_Current_Click(object sender, System.EventArgs e)
		{
			BaseController<Location_cu>.ShowEditorControl(ref _chartOfAccountEditorViewer, this, null, null,
														  EditorContainerType.Regular, ViewerName.ChartOfAccountViewer, DB_CommonTransactionType.CreateNew,
														  "الحسـابــــات", true);
			CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_CurrentAccount,
												  ChartOfAccount_cu.ItemsList.FindAll(
													  item =>
														  Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(DB_ChartOfAccountCodeMargin.FirstMargin))));
		}

		private void btnChartOfAccount_Credit_Click(object sender, System.EventArgs e)
		{
			BaseController<Location_cu>.ShowEditorControl(ref _chartOfAccountEditorViewer, this, null, null,
														  EditorContainerType.Regular, ViewerName.ChartOfAccountViewer, DB_CommonTransactionType.CreateNew,
														  "الحسـابــــات", true);
			CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount_CreditAccount,
												  ChartOfAccount_cu.ItemsList.FindAll(
													  item =>
														  Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(DB_ChartOfAccountCodeMargin.FirstMargin))));
		}

		private void btnCustomer_Click(object sender, EventArgs e)
		{
			DB_PersonType personType = (DB_PersonType) PersonType_P_ID;
			switch (personType)
			{
				case DB_PersonType.Customer:
					BaseController<Customer_cu>.ShowEditorControl(ref _customerEditorViewer, this, null, null,
					                                              EditorContainerType.Regular,
					                                              ViewerName.Customer_Viewer,
					                                              DB_CommonTransactionType.CreateNew,
					                                              "العمــــلاء", true);
					CommonViewsActions.FillGridlookupEdit(lkeCustomer, Customer_cu.ItemsList, "FullName", "Person_CU_ID");
					break;
				case DB_PersonType.Supplier:
					BaseController<Supplier_cu>.ShowEditorControl(ref _supplierEditorViewer, this, null, null,
					                                              EditorContainerType.Regular,
					                                              ViewerName.Supplier_Viewer,
					                                              DB_CommonTransactionType.CreateNew,
					                                              "المـورديــــن", true);
					CommonViewsActions.FillGridlookupEdit(lkeCustomer, Supplier_cu.ItemsList, "FullName", "Person_CU_ID");
					break;
				case DB_PersonType.Patient:

					break;
				case DB_PersonType.Doctor:

					break;
			}
		}

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

		public object Person_CU_ID
		{
			get { return lkeCustomer.EditValue; }
			set { lkeCustomer.EditValue = value; }
		}

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
			CommonViewsActions.FillGridlookupEdit(lkeCustomer, Customer_cu.ItemsList, "FullName", "Person_CU_ID");
		}

		public override void ClearControls()
		{
			chkCustomer.Checked= true;
			lkeCustomer.EditValue = null;
			chkDebitAccount.Checked = false;
			lkeChartOfAccount_DebitAccount.EditValue = null;
			chkTaxAccount.Checked= false;
			lkeChartOfAccount_TaxesAccount.EditValue = null;
			chkCurrentAccount.Checked= false;
			lkeChartOfAccount_CurrentAccount.EditValue = null;
			chkCreditAccount.Checked= false;
			lkeChartOfAccount_CreditAccount.EditValue = null;
		}

		#endregion
	}
}
