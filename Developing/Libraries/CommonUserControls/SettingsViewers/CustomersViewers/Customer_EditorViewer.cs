using System;
using System.Collections.Generic;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.ChartOfAccountViewers;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.CustomersViewers
{
	public partial class Customer_EditorViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<Customer_cu>,
		ICustomerViewer
	{
		private ChartOfAccount_EditorViewer _chartOfAccountEditorViewer;
		private List<ChartOfAccount_cu> lastUsed_Debit = new List<ChartOfAccount_cu>();
		private List<ChartOfAccount_cu> lastUsed_Tax = new List<ChartOfAccount_cu>();
		private List<ChartOfAccount_cu> lastUsed_Current = new List<ChartOfAccount_cu>();
		private List<ChartOfAccount_cu> lastUsed_Credit = new List<ChartOfAccount_cu>();

		public Customer_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_Customer_EditorViewer);
			CommonViewsActions.SetupSyle(this);

			txtFirstNameP.Focus();
		}

		#region Overrides of CommonAbstractViewer<Customer_cu>

		public override object ViewerID
		{
			get { return (int) ViewerName.Customer_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "العمــــلاء"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeMaritalStatus, MaritalStatus_p.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeFirstIdentificationCardType, IdentificationCardType_p.ItemsList);
		}

		public override void ClearControls()
		{
			txtFirstNameP.EditValue = null;
			txtSecondNameP.EditValue = null;
			txtThirdNameP.EditValue = null;
			txtFourthNameP.EditValue = null;
			rdGender.EditValue = true;

			dtDateOfBirth.EditValue = null;
			dtFirstIdentificationCardIssueDate.EditValue = null;
			dtFirstIdentificationCardExpirationDate.EditValue = null;
		}

		#endregion

		#region Implementation of ICustomerViewer

		public object FirstName
		{
			get { return txtFirstNameP.EditValue; }
			set { txtFirstNameP.EditValue = value; }
		}

		public object SecondName
		{
			get { return txtSecondNameP.EditValue; }
			set { txtSecondNameP.EditValue = value; }
		}

		public object ThirdName
		{
			get { return txtThirdNameP.EditValue; }
			set { txtThirdNameP.EditValue = value; }
		}

		public object FourthName
		{
			get { return txtFourthNameP.EditValue; }
			set { txtFourthNameP.EditValue = value; }
		}

		public object MaritalStatus
		{
			get { return lkeMaritalStatus.EditValue; }
			set { lkeMaritalStatus.EditValue = value; }
		}

		public object Gender
		{
			get { return rdGender.EditValue; }
			set { rdGender.EditValue = value; }
		}

		public object BirthDate
		{
			get { return dtDateOfBirth.EditValue; }
			set { dtDateOfBirth.EditValue = value; }
		}

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		public object Mobile1
		{
			get { return txtMobile1.EditValue; }
			set { txtMobile1.EditValue = value; }
		}

		public object Mobile2
		{
			get { return txtMobile2.EditValue; }
			set { txtMobile2.EditValue = value; }
		}

		public object Phone1
		{
			get { return txtPhone1.EditValue; }
			set { txtPhone1.EditValue = value; }
		}

		public object Phone2
		{
			get { return txtPhone2.EditValue; }
			set { txtPhone2.EditValue = value; }
		}

		public object Address
		{
			get { return txtAddress.EditValue; }
			set { txtAddress.EditValue = value; }
		}

		public object Email
		{
			get { return txtEmail.EditValue; }
			set { txtEmail.EditValue = value; }
		}

		public object IdentificationCardType
		{
			get { return lkeFirstIdentificationCardType.EditValue; }
			set { lkeFirstIdentificationCardType.EditValue = value; }
		}

		public object IdentificationCardNumber
		{
			get { return txtFirstIdentifiactionCardNumber.EditValue; }
			set { txtFirstIdentifiactionCardNumber.EditValue = value; }
		}

		public object IdentificationCardIssueDate
		{
			get { return dtFirstIdentificationCardIssueDate.EditValue; }
			set { dtFirstIdentificationCardIssueDate.EditValue = value; }
		}

		public object IdentificationCardExpirationDate
		{
			get { return dtFirstIdentificationCardExpirationDate.EditValue; }
			set { dtFirstIdentificationCardExpirationDate.EditValue = value; }
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

		#endregion

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
	}
}
