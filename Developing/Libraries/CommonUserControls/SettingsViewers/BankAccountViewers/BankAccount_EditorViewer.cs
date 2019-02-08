using System;
using System.Collections.Generic;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.BankViewers;
using CommonUserControls.SettingsViewers.ChartOfAccountViewers;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.BankAccountViewers
{
	public partial class BankAccount_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<BankAccount_cu>,
		IBankAccountViewer
	{
		private ChartOfAccount_EditorViewer _chartOfAccountEditorViewer;
		private Bank_EditorViewer _bankEditorViewer;
		private List<ChartOfAccount_cu> lastUsed = new List<ChartOfAccount_cu>();

		public BankAccount_EditorViewer()
		{
			InitializeComponent();
		}

		private void BankAccount_EditorViewer_Load(object sender, System.EventArgs e)
		{
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_BankAccount_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<BankAccount_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.BankAccount_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "حسـابــــات البنــــوك"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount,
			                                      ChartOfAccount_cu.ItemsList.FindAll(
				                                      item => Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
					                                      .Equals(Convert.ToInt32(
						                                              DB_ChartOfAccountCodeMargin.FirstMargin))));
			CommonViewsActions.FillGridlookupEdit(lkeBank, Bank_cu.ItemsList);
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			lkeChartOfAccount.EditValue = null;
			txtDescription.EditValue = null;
			lkeBank.EditValue = null;
		}

		#endregion

		#region Implementation of IBankAccountViewer

		public object Name_P
		{
			get { return txtNameP.EditValue; }
			set { txtNameP.EditValue = value; }
		}

		public object Name_S
		{
			get { return txtNameS.EditValue; }
			set { txtNameS.EditValue = value; }
		}

		public object Bank_CU_ID
		{
			get { return lkeBank.EditValue; }
			set { lkeBank.EditValue = value; }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		public object ChartOfAccount_CU_ID
		{
			get { return lkeChartOfAccount.EditValue; }
			set { lkeChartOfAccount.EditValue = value; }
		}

		#endregion

		private void btnBank_Click(object sender, EventArgs e)
		{
			BaseController<Bank_cu>.ShowEditorControl(ref _bankEditorViewer, this, null, null,
			                                          EditorContainerType.Regular, ViewerName.Bank_Viewer,
			                                          DB_CommonTransactionType.CreateNew,
			                                          "البنـــــوك", true);
			CommonViewsActions.FillGridlookupEdit(lkeBank, Bank_cu.ItemsList);
		}

		private void btnChartOfAccount_Click(object sender, EventArgs e)
		{
			BaseController<ChartOfAccount_cu>.ShowEditorControl(ref _chartOfAccountEditorViewer, this, null, null,
													  EditorContainerType.Regular, ViewerName.ChartOfAccountViewer,
													  DB_CommonTransactionType.CreateNew,
													  "الحسـابـــات المـاليــــة", true);
			CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount,
			                                      ChartOfAccount_cu.ItemsList.FindAll(
				                                      item => Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
					                                      .Equals(Convert.ToInt32(
						                                              DB_ChartOfAccountCodeMargin.FirstMargin))));
		}

		private void lkeChartOfAccount_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeChartOfAccount.EditValue == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount,
													  ChartOfAccount_cu.ItemsList.FindAll(
														  item => Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(
																		  DB_ChartOfAccountCodeMargin.FirstMargin))));
				return;
			}

			ChartOfAccount_cu chartOfAccount =
				ChartOfAccount_cu.ItemsList.Find(item => Convert.ToInt32(item.ID)
													 .Equals(Convert.ToInt32(lkeChartOfAccount.EditValue)));
			if (chartOfAccount == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount,
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
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount, list);
				lastUsed = list;
			}
			else
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount, lastUsed);
			}
		}
	}
}
